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

        public static int QuantasVezesComeu { get; private set; } = 0;

        public EstrategiaGulosa(ConfigEstrategia config = null)
        {
            _config = config ?? new ConfigEstrategia();
        }

        public string Nome => "Guloso Inteligente";

        public (Dinossauro dino, Cercados cercado)? Avaliar(InformacoesTurno info)
        {
            if (info == null) return null;

            if (info.MaoJogador == null || info.MaoJogador.All(x => x.QuantidadeDinossauros <= 0))
                return null;

            var melhorScore = int.MinValue;
            Dinossauro melhorDino = 0;
            Cercados melhorCercado = 0;

            foreach (var item in info.MaoJogador)
            {
                if (item.QuantidadeDinossauros <= 0) continue;

                var dino = item.Dinossauro;

                foreach (Cercados cercado in Enum.GetValues(typeof(Cercados)))
                {
                    if (!Validator.JogadaValidator(info, cercado, dino)) continue;

                    var score = AvaliarJogada(info, dino, cercado);

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

            var jogadaEscolhida = (melhorDino, melhorCercado);

            ValidarJogadaBRFudido(info, jogadaEscolhida, msg => Console.WriteLine(msg));

            return jogadaEscolhida;
        }

        private int AvaliarJogada(InformacoesTurno info, Dinossauro dino, Cercados cercado)
        {
            if (cercado == Cercados.FI && PodeColocarFlorestaIgualdade(info, dino))
                return 100;

            if (cercado == Cercados.FI && !PodeColocarFlorestaIgualdade(info, dino))
                return int.MinValue;

            if (cercado == Cercados.RS)
            {
                var podeVirarRei = Tabuleiro.DinoViraReiDaSelva(info, dino, info.QuantidadeJogadores);
                if (!podeVirarRei) return -100;
            }

            var ganhoPontuacao = ComidinhaDoGuloso(info, dino, cercado);
            var bonus = EstrategiaAnalizador.BonusJogada(info, cercado, dino);
            var potencial = EstrategiaAnalizador.PotencialFuturo(info, cercado, dino);

            return ganhoPontuacao + bonus + potencial;
        }

        public int ComidinhaDoGuloso(InformacoesTurno info, Dinossauro dino, Cercados cercado)
        {
            var antes = PontuacaoTotal(info);
            var depois = PontuacaoSimulada(info, cercado, dino);
            return depois - antes;
        }

        #region Validação de Jogada (Produção)

        private static void ValidarJogadaBRFudido(
            InformacoesTurno info,
            (Dinossauro dino, Cercados cercado) jogada,
            Action<string> log)
        {
            var ok = true;

            var item = info.MaoJogador.FirstOrDefault(x => x.Dinossauro == jogada.dino);

            if (item == null || item.QuantidadeDinossauros <= 0)
            {
                log("Dino inválido (não está na mão) BURRO BURRO");
                ok = false;
            }

            var cercadoAtual = info.CercadosJogador.FirstOrDefault(c => c.Cercados == jogada.cercado);
            var lista = cercadoAtual != null ? cercadoAtual.Dinossauros : new List<Dinossauro>();

            if (!jogada.cercado.SePodeColocarNoCercado(lista, jogada.dino))
            {
                log("Jogada inválida no cercado. BURRO BURRO");
                ok = false;
            }

            var ganhoEscolhido = CalcularGanho(info, jogada.dino, jogada.cercado);
            var melhor = MelhorGanhoPossivel(info);

            log(string.Format("Ganho escolhido: {0}", ganhoEscolhido));
            log(string.Format("Melhor ganho possível: {0}", melhor));

            if (ganhoEscolhido < melhor)
            {
                log("NÃO FOI A MELHOR JOGADA!! TÁ SEM FOME FILHO ??!");
                ok = false;
            }

            if (ok)
            {
                log("Jogada válida e ótima! Tome");
                QuantasVezesComeu += 1;
                log(QuantasVezesComeu.ToString());
            }
        }

        private static int CalcularGanho(InformacoesTurno info, Dinossauro dino, Cercados cercado)
        {
            var guloso = new EstrategiaGulosa();
            var antes = guloso.PontuacaoTotal(info);
            var depois = guloso.PontuacaoSimulada(info, cercado, dino);
            return depois - antes;
        }

        private static int MelhorGanhoPossivel(InformacoesTurno info)
        {
            var melhor = int.MinValue;

            foreach (var item in info.MaoJogador)
            {
                if (item.QuantidadeDinossauros <= 0) continue;

                foreach (Cercados cercado in Enum.GetValues(typeof(Cercados)))
                {
                    if (!Validator.JogadaValidator(info, cercado, item.Dinossauro)) continue;

                    var ganho = CalcularGanho(info, item.Dinossauro, cercado);
                    if (ganho > melhor) melhor = ganho;
                }
            }

            return melhor == int.MinValue ? 0 : melhor;
        }

        #endregion

        #region Configuração de Regra da Ilha Solitária

        private const int TurnoMinimoPontuarIlhaSolitaria = 10;

        private static bool IlhaSolitariaPontua(int turnoAtual)
        {
            return turnoAtual >= TurnoMinimoPontuarIlhaSolitaria;
        }

        #endregion

        public int PontuacaoTotal(InformacoesTurno info)
        {
            var pontos = 0;

            foreach (var cercado in info.CercadosJogador)
            {
                var dinos = cercado.Dinossauros != null
                    ? cercado.Dinossauros
                    : new List<Dinossauro>();

                var qtdDinos = dinos.Count;

                switch (cercado.Cercados)
                {
                    case Cercados.FI:
                        pontos += ScoreFlorestaIgualdade(qtdDinos);
                        break;

                    case Cercados.CD:
                        pontos += ScoreCampinaDiferenca(dinos.Distinct().Count());
                        break;

                    case Cercados.MT:
                        if (qtdDinos == 3) pontos += 7;
                        break;

                    case Cercados.PA:
                        pontos += qtdDinos / 2 * 5;
                        break;

                    case Cercados.RI:
                        pontos += qtdDinos;
                        break;

                    case Cercados.RS:
                        if (qtdDinos == 1 && info.NumeroTurno >= 11) pontos += 7;
                        break;

                    case Cercados.IS:
                        if (qtdDinos == 1)
                        {
                            var dinoIlha = dinos[0];
                            var especieEhUnica = info.CercadosJogador
                                .SelectMany(c => c.Dinossauros != null ? c.Dinossauros : new List<Dinossauro>())
                                .Count(d => d == dinoIlha) == 1;

                            if (especieEhUnica && IlhaSolitariaPontua(info.NumeroTurno))
                                pontos += 7;
                        }
                        break;
                }
            }

            return pontos;
        }

        public int PontuacaoSimulada(InformacoesTurno info, Cercados alvo, Dinossauro novoDino)
        {
            var pontos = 0;

            foreach (var cercado in info.CercadosJogador)
            {
                var dinos = cercado.Dinossauros != null
                    ? new List<Dinossauro>(cercado.Dinossauros)
                    : new List<Dinossauro>();

                if (cercado.Cercados == alvo)
                    dinos.Add(novoDino);

                var qtdDinos = dinos.Count;

                switch (cercado.Cercados)
                {
                    case Cercados.FI:
                        pontos += ScoreFlorestaIgualdade(qtdDinos);
                        break;

                    case Cercados.CD:
                        pontos += ScoreCampinaDiferenca(dinos.Distinct().Count());
                        break;

                    case Cercados.MT:
                        if (qtdDinos == 3) pontos += 7;
                        break;

                    case Cercados.PA:
                        pontos += qtdDinos / 2 * 5;
                        break;

                    case Cercados.RI:
                        pontos += qtdDinos;
                        break;

                    case Cercados.RS:
                        if (qtdDinos == 1 && info.NumeroTurno >= 11) pontos += 7;
                        break;

                    case Cercados.IS:
                        if (qtdDinos == 1)
                        {
                            var apareceEmOutro = info.CercadosJogador
                                .Where(c => c.Cercados != Cercados.IS)
                                .SelectMany(c => c.Dinossauros != null ? c.Dinossauros : new List<Dinossauro>())
                                .Any(d => d == novoDino);

                            if (!apareceEmOutro) pontos += 7;
                        }
                        break;
                }
            }

            return pontos;
        }

        public int ScoreFlorestaIgualdade(int qtdDinos)
        {
            switch (qtdDinos)
            {
                case 1: return 2;
                case 2: return 4;
                case 3: return 8;
                case 4: return 12;
                case 5: return 18;
                case 6: return 24;
                default: return 0;
            }
        }

        public int ScoreCampinaDiferenca(int qtdDinos)
        {
            switch (qtdDinos)
            {
                case 1: return 1;
                case 2: return 3;
                case 3: return 6;
                case 4: return 10;
                case 5: return 15;
                case 6: return 21;
                default: return 0;
            }
        }

        public (Dinossauro, Cercados)? ObterPrimeiraJogadaValida(InformacoesTurno info)
        {
            foreach (var item in info.MaoJogador)
            {
                if (item.QuantidadeDinossauros <= 0) continue;

                foreach (Cercados cercado in Enum.GetValues(typeof(Cercados)))
                    if (Validator.JogadaValidator(info, cercado, item.Dinossauro))
                        return (item.Dinossauro, cercado);
            }

            return null;
        }

        public static bool PodeColocarFlorestaIgualdade(InformacoesTurno info, Dinossauro especie)
        {
            var florestaIgualdade = info.CercadosJogador
                .FirstOrDefault(x => x.Cercados == Cercados.FI);

            if (florestaIgualdade != null && florestaIgualdade.Dinossauros.Any())
                return true;

            var jogadas = Oponente.ObterJogadasOponentesAteTurno(info.IdPartida, info.MeuId, info.NumeroTurno - 1);

            return !jogadas.Any(d => d.Dinossauro == especie &&
                (d.Cercado == Cercados.FI || d.Cercado == Cercados.RS));
        }
    }
}