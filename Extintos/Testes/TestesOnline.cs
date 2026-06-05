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
using Extintos.Services;

namespace Extintos.LeonKennedy
{
    public static class TestRunnerOnline
    {
        public static async Task ExecutarPartidaAsync(int idPartida, string senhaPartida, int totalBots, Action<string> log)
        {
            log(" INICIANDO TESTE");

       
            Jogador leon = Jogador.EntrarNaPartida(idPartida, "LeonKennedy", senhaPartida);
            log($"Leon conectado. ID: {leon.IdJogador}");

            for (int i = 1; i <= totalBots; i++)
            {
                int idBot = i;
                Task.Run(() => RodarBot(idPartida, senhaPartida, $"Bot_{idBot}", log));
            }

          
            try { Partida.IniciarPartida(leon.IdJogador, leon.Senha, idPartida); }
            catch { log("O jogo pode já estar iniciado ou aguardando players."); }

      
            var estrategia = new EstrategiaGulosa();
            int ultimoTurnoJogado = 0;

            while (true)
            {
                try
                {
                    var status = Partida.VerificaPartida(idPartida);
                    if (status.statusPartida == 'E') { log("Partida encerrada!"); break; }

                    if (status.statusPartida == 'J' && status.idJogador == leon.IdJogador && status.statusTurno == 'A' && ultimoTurnoJogado != status.numeroTurno)
                    {
                        log($"\nTurno {status.numeroTurno} - Avaliando...");
                        
                        var info = new InformacoesTurno(leon.IdJogador, idPartida, leon.Senha, leon);
                        
                        LogMao(info, log);
                        var opcoes = AvaliarOpcoes(info, status.numeroTurno);
                        var jogada = estrategia.Avaliar(info);

                        if (jogada.HasValue)
                        {
                            LogOpcoes(opcoes, jogada.Value, log);
                            await DraftService.JogarAsync(leon.IdJogador, leon.Senha, jogada.Value.dino.PegaCodigo(), jogada.Value.cercado.PegaCodigo(), CancellationToken.None);
                            leon.ColocarDinossauro(jogada.Value.dino, jogada.Value.cercado);
                            log($" Jogada realizada: {jogada.Value.dino.PegaNome()} no {jogada.Value.cercado}");
                        }
                        ultimoTurnoJogado = status.numeroTurno;
                    }
                }
                catch (Exception ex) { log($"Erro: {ex.Message}"); }
                await Task.Delay(1000);
            }
        }

        private static async Task RodarBot(int idPartida, string senha, string nome, Action<string> log)
        {
            var bot = Jogador.EntrarNaPartida(idPartida, nome, senha);
            var estrategia = new EstrategiaGulosa();
            int ultimoTurno = 0;

            while (true)
            {
                var status = Partida.VerificaPartida(idPartida);
                if (status.statusPartida == 'E') break;

                if (status.statusPartida == 'J' && status.idJogador == bot.IdJogador && status.statusTurno == 'A' && ultimoTurno != status.numeroTurno)
                {
                    var info = new InformacoesTurno(bot.IdJogador, idPartida, bot.Senha, bot);
                    var jogada = estrategia.Avaliar(info);
                    if (jogada.HasValue)
                    {
                        await DraftService.JogarAsync(bot.IdJogador, bot.Senha, jogada.Value.dino.PegaCodigo(), jogada.Value.cercado.PegaCodigo(), CancellationToken.None);
                        bot.ColocarDinossauro(jogada.Value.dino, jogada.Value.cercado);
                    }
                    ultimoTurno = status.numeroTurno;
                }
                await Task.Delay(3000);
            }
        }

        
        private static void LogMao(InformacoesTurno info, Action<string> log)
        {
            var itens = info.MaoJogador.Where(x => x.QuantidadeDinossauros > 0);
            log("  Mão: " + (itens.Any() ? string.Join(" ", itens.Select(x => $"{x.Dinossauro}x{x.QuantidadeDinossauros}")) : "[vazia]"));
        }

        private static List<OpcaoAvaliada> AvaliarOpcoes(InformacoesTurno info, int turno)
        {
            var opcoes = new List<OpcaoAvaliada>();
            foreach (var m in info.MaoJogador.Where(x => x.QuantidadeDinossauros > 0))
                foreach (var c in CercadosExtension.CercadosLista())
                {
                    var dinos = info.CercadosJogador.FirstOrDefault(x => x.Cercados == c)?.Dinossauros ?? new List<Dinossauro>();
                    if (c.SePodeColocarNoCercado(dinos, m.Dinossauro))
                        opcoes.Add(new OpcaoAvaliada { Dino = m.Dinossauro, Cercado = c, Ganho = PontuacaoHelper.GanhoSimulado(info.CercadosJogador, m.Dinossauro, c, turno) });
                }
            return opcoes.OrderByDescending(x => x.Ganho).ToList();
        }

        private static void LogOpcoes(List<OpcaoAvaliada> opcs, (Dinossauro dino, Cercados cercado) escolha, Action<string> log)
        {
            foreach (var o in opcs.Take(5))
                log($"     {o.Dino,-12} → {o.Cercado,-5} | Ganho: {o.Ganho} {(escolha.dino == o.Dino && escolha.cercado == o.Cercado ? "★" : "")}");
        }
    }
}