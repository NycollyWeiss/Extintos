using Extintos.Auxiliares;
using Extintos.Enumeration;
using Extintos.LeonKennedy;
using Extintos.Model;
using Extintos.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Extintos.Interfaces
{
    internal class EstrategiaGulosa : IEstategia
    {
        private readonly ConfigEstrategia _config;

        public EstrategiaGulosa(ConfigEstrategia config = null)
        {
            _config = config ?? new ConfigEstrategia();
        }

        public string Nome => "Guloso Inteligente";

        public (Dinossauro dino, Cercados cercado)? Avaliar(
            InformacoesTurno info)
        {
            if (info == null)
                return null;

            if (info.MaoJogador == null ||
                info.MaoJogador.All(x => x.QuantidadeDinossauros <= 0))
                return null;

            var melhorScore = int.MinValue;
            Dinossauro melhorDino = 0;
            Cercados melhorCercado = 0;

            foreach (var item in info.MaoJogador)
            {
                if (item.QuantidadeDinossauros <= 0)
                    continue;

                var dino = item.Dinossauro;

                foreach (Cercados cercado in Enum.GetValues(typeof(Cercados)))
                {
                    if (!Validator.JogadaValidator(
                            info,
                            cercado,
                            dino))
                        continue;

                    var score = AvaliarJogada(
                        info,
                        dino,
                        cercado);

                    if (score > melhorScore)
                    {
                        melhorScore = score;
                        melhorDino = dino;
                        melhorCercado = cercado;
                    }
                }
            }

            if (melhorScore == int.MinValue)
                return ObterPrimeiraJogadaValida(info);

            return (melhorDino, melhorCercado);
        }

        private int AvaliarJogada(
            InformacoesTurno info,
            Dinossauro dino,
            Cercados cercado)
        {

            if (cercado == Cercados.FI && PodeColocarFlorestaIgualdade(info, dino))
            {
                return 100;
            }
            if (cercado == Cercados.FI && !PodeColocarFlorestaIgualdade(info, dino))
            {
                return int.MinValue;
            }


            if (cercado == Cercados.RS)
            {
                bool podeVirarRei =
                    Tabuleiro.DinoViraReiDaSelva(
                        info,
                        dino,
                        info.QuantidadeJogadores);

                if (!podeVirarRei)
                {
                    return -100;
                }
            }

            var ganhoPontuacao =
                ComidinhaDoGuloso(info, dino, cercado);

            var bonus =
                EstrategiaAnalizador.BonusJogada(
                    info,
                    cercado,
                    dino);

            var potencial =
                EstrategiaAnalizador.PotencialFuturo(
                    info,
                    cercado,
                    dino);

            return ganhoPontuacao +
                   bonus +
                   potencial;
        }

        public int ComidinhaDoGuloso(
            InformacoesTurno info,
            Dinossauro dino,
            Cercados cercado)
        {
            var antes = PontuacaoTotal(info);
            var depois = PontuacaoSimulada(info, cercado, dino);

            return depois - antes;
        }
//validar a quatidade nos cercados dos oponetes, precisa do tabuleiro universal
        #region Configuração de Regra do Ilha Solitária

        private const int TurnoMinimoPontuarIlhaSolitaria = 10;

        private static bool IlhaSolitariaPontua(int turnoAtual)
        {
            return turnoAtual >= TurnoMinimoPontuarIlhaSolitaria;
        }

        #endregion
        public int PontuacaoTotal(
            InformacoesTurno info)
        {
            var pontos = 0;
            
            foreach (var cercado in info.CercadosJogador)
            {
                var dinos =
                    cercado.Dinossauros ??
                    new List<Dinossauro>();
                
                var qtdDinos = dinos.Count;

                switch (cercado.Cercados)
                {
                    case Cercados.FI:
                        pontos += ScoreFlorestaIgualdade(qtdDinos);
                        break;

                    case Cercados.CD:
                        pontos += ScoreCampinaDiferenca(
                            dinos.Distinct().Count());
                        break;

                    case Cercados.MT:
                        if (qtdDinos == 3)
                            pontos += 7;
                        break;

                    case Cercados.PA:
                        pontos += qtdDinos / 2 * 5;
                        break;

                    case Cercados.RI:
                        pontos += qtdDinos;
                        break;

                    case Cercados.RS:
                        if (qtdDinos == 1 && info.NumeroTurno >= 11)
                            pontos += 7;
                      
                        break;

                    case Cercados.IS:
                        if (qtdDinos == 1)
                        {
                            var dinoIlha = dinos[0];

                            var especieEhUnica = info.CercadosJogador
                                .SelectMany(c =>
                                    c.Dinossauros ?? new List<Dinossauro>())
                                .Count(d => d == dinoIlha) == 1;

                            if (especieEhUnica &&
                                IlhaSolitariaPontua(info.NumeroTurno))
                            {
                                pontos += 7;
                            }
                        }

                        break;
                }
            }

            return pontos;
        }

        public int PontuacaoSimulada(
            InformacoesTurno info,
            Cercados alvo,
            Dinossauro novoDino)
        {
            var pontos = 0;

            foreach (var cercado in info.CercadosJogador)
            {
                var dinos =
                    cercado.Dinossauros?.ToList() ??
                    new List<Dinossauro>();

                if (cercado.Cercados == alvo)
                    dinos.Add(novoDino);

                var qtdDinos = dinos.Count;

                switch (cercado.Cercados)
                {
                    case Cercados.FI:
                        pontos += ScoreFlorestaIgualdade(qtdDinos);
                        break;

                    case Cercados.CD:
                        pontos += ScoreCampinaDiferenca(
                            dinos.Distinct().Count());
                        break;

                    case Cercados.MT:
                        if (qtdDinos == 3)
                            pontos += 7;
                        break;

                    case Cercados.PA:
                        pontos += qtdDinos / 2 * 5;
                        break;

                    case Cercados.RI:
                        pontos += qtdDinos;
                        break;

                    case Cercados.RS:
                        if (qtdDinos == 1 && info.NumeroTurno >= 11)
                            pontos += 7;
                        else
                        {
                            pontos += 0;
                        }
                        break;

                    case Cercados.IS:
                        if (qtdDinos == 1)
                        {
                            var apareceEmOutro =
                                info.CercadosJogador
                                    .Where(c => c.Cercados != Cercados.IS)
                                    .SelectMany(c =>
                                        c.Dinossauros ??
                                        new List<Dinossauro>())
                                    .Any(d => d == novoDino);

                            if (!apareceEmOutro)
                                pontos += 7;
                        }
                        

                        break;
                }
            }

            return pontos;
        }

        public int ScoreFlorestaIgualdade(
            int qtdDinos)
        {
            return qtdDinos switch
            {
                1 => 2,
                2 => 4,
                3 => 8,
                4 => 12,
                5 => 18,
                6 => 24,
                _ => 0
            };
        }

        public int ScoreCampinaDiferenca(
            int qtdDinos)
        {
            return qtdDinos switch
            {
                1 => 1,
                2 => 3,
                3 => 6,
                4 => 10,
                5 => 15,
                6 => 21,
                _ => 0
            };
        }

      public (Dinossauro, Cercados)?
            ObterPrimeiraJogadaValida(
                InformacoesTurno info)
        {
            foreach (var item in info.MaoJogador)
            {
                if (item.QuantidadeDinossauros <= 0)
                    continue;

                foreach (Cercados cercado in Enum.GetValues(typeof(Cercados)))
                    if (Validator.JogadaValidator(
                            info,
                            cercado,
                            item.Dinossauro))
                        return (
                            item.Dinossauro,
                            cercado);
            }

            return null;
        }

        public static bool PodeColocarFlorestaIgualdade(InformacoesTurno info, Dinossauro especie)
        {
            var florestaIgualdade = info.CercadosJogador
                .FirstOrDefault(x => x.Cercados == Cercados.FI);

            // Já tem dino la
            if (florestaIgualdade != null &&
                florestaIgualdade.Dinossauros.Any())
                return true;

            var jogadas = Oponente.ObterJogadasOponentesAteTurno(info.IdPartida, info.MeuId, info.NumeroTurno - 1);

            return !jogadas.Any(d =>d.Dinossauro == especie &&
                (d.Cercado == Cercados.FI ||
                 d.Cercado == Cercados.RS));
        }
    }
}