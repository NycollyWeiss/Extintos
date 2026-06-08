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
    // responsável por toda a lógica de decisão e execução de turno do bot.
    public class BotService
    {
        // dependências
        private readonly Jogador _jogador;
        private readonly IEstategia _estrategia;

        // estado interno
        private int _ultimoTurnoProcessado = -1;
        private int _ultimoTurnoJogado = -1;

        public int TurnoAtual { get; private set; } = 1;
        public List<AuxDinossauro> DinossaurosNoUniverso { get; } = new();

        // Evento para a UI
        // Disparado após uma jogada ser confirmada pelo servidor.
        // A UI assina isso para executar a animação visual (ExecutarMovimentoVisual).
        public event Func<string, string, Task> JogadaConfirmada;

        //  construtor
        public BotService(Jogador jogador, IEstategia estrategia)
        {
            _jogador = jogador;
            _estrategia = estrategia;
        }

        // API pública
        public async Task ExecutarTurnoAsync(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                if (_jogador == null || _jogador.IdPartida <= 0 || string.IsNullOrWhiteSpace(_jogador.Senha))
                {
                    Console.WriteLine("Jogador nulo ou partida/senha inválida.");
                    return;
                }

                var decisoes = new InformacoesTurno(_jogador.IdJogador, _jogador.IdPartida, _jogador.Senha, _jogador);
                TurnoAtual = decisoes.NumeroTurno;

                Console.WriteLine();
                Console.WriteLine("======================================");
                Console.WriteLine($"Turno atual: {TurnoAtual}");
                Console.WriteLine("======================================");

                if (_ultimoTurnoJogado == TurnoAtual)
                {
                    Console.WriteLine($"Já joguei no turno {TurnoAtual}");
                    return;
                }

                AtualizarUniversoDinos(decisoes);
                LogarEstadoTurno(decisoes);

                if (decisoes.MaoJogador == null || decisoes.MaoJogador.Count == 0 || 
                    decisoes.CercadosJogador == null || decisoes.CercadosJogador.Count == 0)
                {
                    Console.WriteLine("Mão vazia ou nenhum cercado carregado.");
                    return;
                }

                var jogada = _estrategia.Avaliar(decisoes);
                if (!jogada.HasValue)
                {
                    Console.WriteLine("Estratégia não encontrou jogada válida.");
                    return;
                }

                var escolha = jogada.Value;
                Console.WriteLine($"Escolha: {escolha.dino.PegaNome()} -> {escolha.cercado.PegaNome()}");

                await Task.Delay(Helper.Next(2000, 3000), token);

                var codigoDino = escolha.dino.PegaCodigo();
                var codigoCercado = escolha.cercado.PegaCodigo();

                Console.WriteLine($"Enviando: {codigoDino} -> {codigoCercado}");

                var proximoTurno = await DraftService.JogarAsync(_jogador.IdJogador, _jogador.Senha, codigoDino, codigoCercado, token);
                Console.WriteLine($"Servidor retornou: {proximoTurno}");

                if (proximoTurno == decisoes.NumeroTurno)
                {
                    Console.WriteLine("Servidor recusou ou não processou a jogada.");
                    return;
                }

                Console.WriteLine("Jogada confirmada.");

                // atualiza o Model localmente
                _jogador.ColocarDinossauro(escolha.dino, escolha.cercado);
                _ultimoTurnoJogado = decisoes.NumeroTurno;
                if (JogadaConfirmada != null)
                    await JogadaConfirmada.Invoke(codigoDino, codigoCercado);
                JaJogueiNesseTurno(decisoes.NumeroTurno);
                await VerificarHistoricoAsync(); // só roda após jogada confirmada
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operação cancelada.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERRO COMPLETO: {ex}");
                Console.WriteLine($"STACK: {ex.StackTrace}");
            }
        }

        public bool JaJogueiNesseTurno(int turnoAtual)
        {
            var historico = DraftService.ObterHistoricoBruto(_jogador.IdPartida);
            if (string.IsNullOrWhiteSpace(historico)) return false;

            var achouTurno = historico.IndexOf($"Turno {turnoAtual}", StringComparison.OrdinalIgnoreCase) >= 0;
            var achouJogador = historico.IndexOf(_jogador.IdJogador.ToString(), StringComparison.OrdinalIgnoreCase) >= 0;

            if (achouTurno && achouJogador)
            {
                Console.WriteLine($"Já joguei neste turno (histórico): Turno {turnoAtual}");
                return true;
            }
            return false;
        }

        // privados
        private void AtualizarUniversoDinos(InformacoesTurno decisoes)
        {
            if (_ultimoTurnoProcessado == TurnoAtual) return;

            if (TurnoAtual == 1)
            {
                DinossaurosNoUniverso.Clear();
                DinossaurosNoUniverso.AddRange(decisoes.MaoJogador);
            }
            else if (TurnoAtual == 2 || TurnoAtual == 7 || TurnoAtual == 8)
            {
                DinossaurosNoUniverso.AddRange(decisoes.MaoJogador);
            }
            else if (TurnoAtual == 3 || TurnoAtual == 9)
            {
                try
                {
                    var turnos = DraftService.ObterTurnos(_jogador.IdPartida, 2);
                    var dinosOponente = DadosOponete.ParserDinosOponente(turnos, _jogador.IdJogador);
                    DinossaurosNoUniverso.AddRange(dinosOponente);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Falha ao obter dinos do oponente: {ex.Message}");
                }
            }

            var consolidado = DinossaurosNoUniverso
                .GroupBy(x => x.Dino)
                .Select(g => new AuxDinossauro(g.Key, g.Sum(x => x.QuantidadeDinossauros)))
                .ToList();

            DinossaurosNoUniverso.Clear();
            DinossaurosNoUniverso.AddRange(consolidado);

            _ultimoTurnoProcessado = TurnoAtual;
        }

        private void LogarEstadoTurno(InformacoesTurno decisoes)
        {
            Console.WriteLine($"Jogador: {_jogador.IdJogador}");
            Console.WriteLine($"Turno: {decisoes.NumeroTurno}");
            Console.WriteLine($"Dado: {decisoes.DadoAtual}");

            Console.WriteLine("MÃO:");
            foreach (var d in decisoes.MaoJogador)
                Console.WriteLine($"{d.Dino} x{d.QuantidadeDinossauros}");

            Console.WriteLine("CERCADOS:");
            foreach (var c in decisoes.CercadosJogador)
                Console.WriteLine($"{c.Cercados} -> {c.Dinossauros.Count}");
        }

        private async Task VerificarHistoricoAsync()
        {
            try
            {
                await Task.Delay(2000);
                var historico = DraftService.ObterHistoricoBruto(_jogador.IdPartida);
                Console.WriteLine("Histórico atualizado? " + historico.Contains(_jogador.IdJogador.ToString()));
            }
            catch
            {
                // silencia falha de verificação de histórico
            }
        }
    }
}