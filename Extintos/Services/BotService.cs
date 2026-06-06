using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Draft;
using Extintos.Auxiliares;
using Extintos.Enumeration;
using Extintos.Interfaces;
using Extintos.Model;

namespace Extintos.Services
{
    // responsável por toda a lógica de decisão e execução de turno do bot
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
            try
            {
                token.ThrowIfCancellationRequested();

                if (!ValidarDadosJogador()) return;

                var decisoes = new InformacoesTurno(_jogador.IdJogador, _jogador.idPartida, _jogador.Senha, _jogador);

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

                if (!ValidarMaoECercados(decisoes)) return;

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

                var proximoTurno = await DraftService.JogarAsync(_jogador.IdJogador, _jogador.Senha, codigoDino, 
                    codigoCercado, token);

                Console.WriteLine($"Servidor retornou: {proximoTurno}");

                if (proximoTurno == decisoes.NumeroTurno)
                {
                    Console.WriteLine("Servidor recusou ou não processou a jogada.");
                    return;
                }

                Console.WriteLine("Jogada confirmada.");

                _jogador.ColocarDinossauro(escolha.dino, escolha.cercado);
                _ultimoTurnoJogado = decisoes.NumeroTurno;
                
                if (JogadaConfirmada != null)
                    await JogadaConfirmada.Invoke(codigoDino, codigoCercado);

                JaJogueiNesseTurno(decisoes.NumeroTurno);
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
            finally
            {
                await VerificarHistoricoAsync();
            }
        }

        public bool JaJogueiNesseTurno(int turnoAtual)
        {
            var historico = DraftService.ObterHistoricoBruto(_jogador.idPartida);
            if (string.IsNullOrWhiteSpace(historico)) return false;

            var achouTurno = historico.IndexOf($"Turno {turnoAtual}", StringComparison.OrdinalIgnoreCase) >= 0;
            var achouJogador = historico.IndexOf(_jogador.IdJogador.ToString(), 
                StringComparison.OrdinalIgnoreCase) >= 0;

            if (achouTurno && achouJogador)
            {
                Console.WriteLine($"Já joguei neste turno (histórico): Turno {turnoAtual}");
                return true;
            }

            return false;
        }

        public async Task<bool> AguardarAtualizacaoHistoricoAsync(
            int turno, int idJogador, int tentativas = 5, int delayMs = 1500)
        {
            for (var i = 0; i < tentativas; i++)
            {
                await Task.Delay(delayMs);
                var historicoBruto = DraftService.ObterHistoricoBruto(_jogador.idPartida);
                Console.WriteLine($"🔍 Verificando histórico ({i + 1}/{tentativas})...");

                if (historicoBruto.Contains($"Turno {turno}") &&
                    historicoBruto.Contains(idJogador.ToString()))
                {
                    Console.WriteLine("Histórico atualizado com sucesso!");
                    return true;
                }
            }

            Console.WriteLine("⚠ Histórico não refletiu após todas as tentativas.");
            return false;
        }

        private bool ValidarDadosJogador()
        {
            if (_jogador == null)
            {
                Console.WriteLine("Jogador nulo.");
                return false;
            }

            if (_jogador.idPartida <= 0 || string.IsNullOrWhiteSpace(_jogador.Senha))
            {
                Console.WriteLine("Partida ou senha inválida.");
                return false;
            }

            return true;
        }

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
                    var turnos = DraftService.ObterTurnos(_jogador.idPartida, 2);
                    var dinosOponente = DadosOponete.ParserDinosOponente(turnos, _jogador.IdJogador);
                    DinossaurosNoUniverso.AddRange(dinosOponente);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Falha ao obter dinos do oponente: {ex.Message}");
                }
            }

            var consolidado = DinossaurosNoUniverso
                .GroupBy(x => x.Dinossauro)
                .Select(g => new AuxDinossauro(g.Key, g.Sum(x => x.QuantidadeDinossauros)))
                .ToList();

            DinossaurosNoUniverso.Clear();
            DinossaurosNoUniverso.AddRange(consolidado);

            _ultimoTurnoProcessado = TurnoAtual;
        }

        private bool ValidarMaoECercados(InformacoesTurno decisoes)
        {
            if (decisoes.MaoJogador == null || decisoes.MaoJogador.Count == 0)
            {
                Console.WriteLine("Mão vazia.");
                return false;
            }

            if (decisoes.CercadosJogador == null || decisoes.CercadosJogador.Count == 0)
            {
                Console.WriteLine("Nenhum cercado carregado.");
                return false;
            }

            return true;
        }

        private void LogarEstadoTurno(InformacoesTurno decisoes)
        {
            Console.WriteLine($"Jogador: {_jogador.IdJogador}");
            Console.WriteLine($"Turno: {decisoes.NumeroTurno}");
            Console.WriteLine($"Dado: {decisoes.DadoAtual}");

            Console.WriteLine("MÃO:");
            foreach (var d in decisoes.MaoJogador)
                Console.WriteLine($"{d.Dinossauro} x{d.QuantidadeDinossauros}");

            Console.WriteLine("CERCADOS:");
            foreach (var c in decisoes.CercadosJogador)
                Console.WriteLine($"{c.Cercados} -> {c.Dinossauros.Count}");
        }

        private async Task VerificarHistoricoAsync()
        {
            try
            {
                await Task.Delay(2000);
                var historico = DraftService.ObterHistoricoBruto(_jogador.idPartida);
                Console.WriteLine(
                    "Histórico atualizado? " +
                    historico.Contains(_jogador.IdJogador.ToString()));
            }
            catch
            {
                // silencia falha de verificação de histórico
            }
        }
    }
}