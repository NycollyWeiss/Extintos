using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Extintos.Auxiliares;
using Extintos.Enumeration;
using Extintos.Interfaces;
using Extintos.Model;

namespace Extintos.Services
{
    public class BotService
    {
        private readonly Jogador _jogador;
        private readonly IEstategia _estrategia;

        private int _ultimoTurnoProcessado = -1;
        private int _ultimoTurnoJogado = -1;

        public int TurnoAtual { get; private set; } = 1;
        public List<AuxDinossauro> DinossaurosNoUniverso { get; } = new();

        public event Func<string, string, Task> JogadaConfirmada;

        public BotService(Jogador jogador, IEstategia estrategia)
        {
            _jogador = jogador;
            _estrategia = estrategia;
        }

        public async Task ExecutarTurnoAsync(CancellationToken token)
        {
            Console.WriteLine("[Bot] === ExecutarTurnoAsync INICIADO ===");
            
            try
            {
                token.ThrowIfCancellationRequested();
                Console.WriteLine("[Bot] Token OK");

                if (_jogador == null || _jogador.IdPartida <= 0 || string.IsNullOrWhiteSpace(_jogador.Senha))
                {
                    Console.WriteLine("[Bot] Jogador nulo ou partida/senha inválida.");
                    return;
                }
                Console.WriteLine($"[Bot] Jogador OK: ID={_jogador.IdJogador}, Partida={_jogador.IdPartida}");

                Console.WriteLine("[Bot] Criando InformacoesTurno...");
               
                 var decisoes = await InformacoesTurno.CriarAsync(_jogador.IdJogador,
                    _jogador.IdPartida,
                    _jogador.Senha,
                    _jogador);
                try
                {
                    Console.WriteLine($"[Bot] InformacoesTurno criado. Turno={decisoes.NumeroTurno}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Bot ERRO ao criar InformacoesTurno] {ex.Message}");
                    Console.WriteLine($"[Bot STACK] {ex.StackTrace}");
                    return;
                }

                TurnoAtual = decisoes.NumeroTurno;

                Console.WriteLine();
                Console.WriteLine("======================================");
                Console.WriteLine($"Turno atual: {TurnoAtual}");
                Console.WriteLine("======================================");

                if (_ultimoTurnoJogado >= 0 && _ultimoTurnoJogado == TurnoAtual)
                {
                    Console.WriteLine($"[Bot] Já joguei no turno {TurnoAtual}");
                    return;
                }

                AtualizarUniversoDinos(decisoes);
                LogarEstadoTurno(decisoes);

                if (decisoes.MaoJogador == null || decisoes.MaoJogador.Count == 0)
                {
                    Console.WriteLine("[Bot] Mão vazia.");
                    return;
                }

                if (decisoes.CercadosJogador == null || decisoes.CercadosJogador.Count == 0)
                {
                    Console.WriteLine("[Bot] Nenhum cercado carregado.");
                    return;
                }

                Console.WriteLine("[Bot] Avaliando jogada com estratégia...");
                var jogada = _estrategia.Avaliar(decisoes);
                if (!jogada.HasValue)
                {
                    Console.WriteLine("[Bot] Estratégia não encontrou jogada válida.");
                    return;
                }

                var escolha = jogada.Value;
                Console.WriteLine($"[Bot] Escolha: {escolha.dino.PegaNome()} -> {escolha.cercado.PegaNome()}");

                var delayMs = Helper.Next(2000, 3000);
                Console.WriteLine($"[Bot] Aguardando {delayMs}ms antes de jogar...");
                await Task.Delay(delayMs, token);

                var codigoDino = escolha.dino.PegaCodigo();
                var codigoCercado = escolha.cercado.PegaCodigo();

                Console.WriteLine($"[Bot] Enviando: {codigoDino} -> {codigoCercado}");

                var proximoTurno = await DraftService.JogarAsync(
                    _jogador.IdJogador, 
                    _jogador.Senha, 
                    codigoDino, 
                    codigoCercado, 
                    token);
                    
                Console.WriteLine($"[Bot] Servidor retornou: {proximoTurno}");

                if (proximoTurno == decisoes.NumeroTurno)
                {
                    Console.WriteLine("[Bot] Servidor recusou ou não processou a jogada.");
                    return;
                }

                Console.WriteLine("[Bot] Jogada confirmada.");

                _jogador.ColocarDinossauro(escolha.dino, escolha.cercado);
                _ultimoTurnoJogado = decisoes.NumeroTurno;

                if (JogadaConfirmada != null)
                {
                    Console.WriteLine("[Bot] Disparando evento JogadaConfirmada...");
                    await JogadaConfirmada.Invoke(codigoDino, codigoCercado);
                }

                await VerificarHistoricoAsync();
                
                Console.WriteLine("[Bot] === ExecutarTurnoAsync FINALIZADO ===");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("[Bot] Operação cancelada.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Bot ERRO COMPLETO] {ex}");
                Console.WriteLine($"[Bot STACK] {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[Bot INNER ERROR] {ex.InnerException.Message}");
                    Console.WriteLine($"[Bot INNER STACK] {ex.InnerException.StackTrace}");
                }
            }
        }

        private void AtualizarUniversoDinos(InformacoesTurno decisoes)
        {
            Console.WriteLine($"[Bot] Atualizando universo de dinos. Turno={TurnoAtual}, Último processado={_ultimoTurnoProcessado}");
            
            if (_ultimoTurnoProcessado == TurnoAtual) 
            {
                Console.WriteLine("[Bot] Universo já atualizado para este turno.");
                return;
            }

            if (TurnoAtual == 1)
            {
                Console.WriteLine("[Bot] Turno 1: Limpando e adicionando mão do jogador");
                DinossaurosNoUniverso.Clear();
                DinossaurosNoUniverso.AddRange(decisoes.MaoJogador);
            }
            else if (TurnoAtual == 2 || TurnoAtual == 7 || TurnoAtual == 8)
            {
                Console.WriteLine($"[Bot] Turno {TurnoAtual}: Adicionando mão do jogador ao universo");
                DinossaurosNoUniverso.AddRange(decisoes.MaoJogador);
            }
            else if (TurnoAtual == 3 || TurnoAtual == 9)
            {
                try
                {
                    Console.WriteLine($"[Bot] Turno {TurnoAtual}: Obtendo dinos dos oponentes");
                    var turnos = DraftService.ObterTurnos(_jogador.IdPartida, 2);
                    var dinosOponente = DadosOponete.ParserDinosOponente(turnos, _jogador.IdJogador);
                    Console.WriteLine($"[Bot] Obtidos {dinosOponente.Count} dinos dos oponentes");
                    DinossaurosNoUniverso.AddRange(dinosOponente);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Bot] Falha ao obter dinos do oponente: {ex.Message}");
                    Console.WriteLine($"[Bot STACK] {ex.StackTrace}");
                }
            }
            else
            {
                Console.WriteLine($"[Bot] Turno {TurnoAtual}: Nenhuma ação especial de atualização");
            }

            Console.WriteLine($"[Bot] Consolidando universo. Antes: {DinossaurosNoUniverso.Count} registros");
            
            var consolidado = DinossaurosNoUniverso
                .GroupBy(x => x.Dino)
                .Select(g => new AuxDinossauro(g.Key, g.Sum(x => x.QuantidadeDinossauros)))
                .ToList();

            DinossaurosNoUniverso.Clear();
            DinossaurosNoUniverso.AddRange(consolidado);

            Console.WriteLine($"[Bot] Universo consolidado. Depois: {DinossaurosNoUniverso.Count} espécies únicas");

            _ultimoTurnoProcessado = TurnoAtual;
        }

        private void LogarEstadoTurno(InformacoesTurno decisoes)
        {
            Console.WriteLine($"[Bot] Jogador: {_jogador.IdJogador}");
            Console.WriteLine($"[Bot] Turno: {decisoes.NumeroTurno}");
            Console.WriteLine($"[Bot] Dado: {decisoes.DadoAtual}");
            Console.WriteLine($"[Bot] Joguei o dado: {decisoes.JogueioDado}");

            Console.WriteLine("[Bot] MÃO:");
            foreach (var d in decisoes.MaoJogador)
                Console.WriteLine($"  {d.Dino} x{d.QuantidadeDinossauros}");

            Console.WriteLine("[Bot] CERCADOS:");
            foreach (var c in decisoes.CercadosJogador)
                Console.WriteLine($"  {c.Cercados} -> {c.Dinossauros.Count} dinos");
        }

        private async Task VerificarHistoricoAsync()
        {
            try
            {
                await Task.Delay(2000);
                var historico = DraftService.ObterHistoricoBruto(_jogador.IdPartida);
                var contemJogador = historico.Contains(_jogador.IdJogador.ToString());
                Console.WriteLine($"[Bot] Histórico atualizado? {contemJogador}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Bot] Erro ao verificar histórico: {ex.Message}");
            }
        }
    }
}