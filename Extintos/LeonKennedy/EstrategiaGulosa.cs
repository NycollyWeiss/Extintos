using System;
using System.Collections.Generic;
using System.Linq;
using Extintos.Enumeration;
using Extintos.LeonKennedy;

namespace Extintos.Model
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
                    if (!EstrategiaValidator.JogadaValidator(
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

        private int ComidinhaDoGuloso(
            InformacoesTurno info,
            Dinossauro dino,
            Cercados cercado)
        {
            var antes = PontuacaoTotal(info);
            var depois = PontuacaoSimulada(info, cercado, dino);

            return depois - antes;
        }

        private int PontuacaoTotal(
            InformacoesTurno info)
        {
            var pontos = 0;

            foreach (var cercado in info.CercadosJogador)
            {
                var dinos =
                    cercado.Dinossauros ??
                    new List<Dinossauro>();

                var qtd = dinos.Count;

                switch (cercado.Cercados)
                {
                    case Cercados.FI:
                        pontos += ScoreFlorestaIgualdade(qtd);
                        break;

                    case Cercados.CD:
                        pontos += ScoreCampinaDiferenca(
                            dinos.Distinct().Count());
                        break;

                    case Cercados.MT:
                        if (qtd == 3)
                            pontos += 7;
                        break;

                    case Cercados.PA:
                        pontos += qtd / 2 * 5;
                        break;

                    case Cercados.RI:
                        pontos += qtd;
                        break;

                    case Cercados.RS:
                        if (qtd == 1)
                            pontos += 7;
                        break;

                    case Cercados.IS:
                        if (qtd == 1)
                        {
                            var unico = dinos[0];

                            var apareceEmOutro =
                                info.CercadosJogador
                                    .Where(c => c.Cercados != Cercados.IS)
                                    .SelectMany(c =>
                                        c.Dinossauros ??
                                        new List<Dinossauro>())
                                    .Any(d => d == unico);

                            if (!apareceEmOutro)
                                pontos += 7;
                        }

                        break;
                }
            }

            return pontos;
        }

        private int PontuacaoSimulada(
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

                var qtd = dinos.Count;

                switch (cercado.Cercados)
                {
                    case Cercados.FI:
                        pontos += ScoreFlorestaIgualdade(qtd);
                        break;

                    case Cercados.CD:
                        pontos += ScoreCampinaDiferenca(
                            dinos.Distinct().Count());
                        break;

                    case Cercados.MT:
                        if (qtd == 3)
                            pontos += 7;
                        break;

                    case Cercados.PA:
                        pontos += qtd / 2 * 5;
                        break;

                    case Cercados.RI:
                        pontos += qtd;
                        break;

                    case Cercados.RS:
                        if (qtd == 1)
                            pontos += 7;
                        break;

                    case Cercados.IS:
                        if (qtd == 1)
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

        private int ScoreFlorestaIgualdade(
            int qtd)
        {
            return qtd switch
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

        private int ScoreCampinaDiferenca(
            int qtd)
        {
            return qtd switch
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

        private (Dinossauro, Cercados)?
            ObterPrimeiraJogadaValida(
                InformacoesTurno info)
        {
            foreach (var item in info.MaoJogador)
            {
                if (item.QuantidadeDinossauros <= 0)
                    continue;

                foreach (Cercados cercado in Enum.GetValues(typeof(Cercados)))
                    if (EstrategiaValidator.JogadaValidator(
                            info,
                            cercado,
                            item.Dinossauro))
                        return (
                            item.Dinossauro,
                            cercado);
            }

            return null;
        }
    }
}