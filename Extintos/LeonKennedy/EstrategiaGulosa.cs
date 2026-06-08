using Extintos.Auxiliares;
using Extintos.Enumeration;
using Extintos.Model;
using Extintos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Extintos.Interfaces;

namespace Extintos.LeonKennedy
{
    internal class EstrategiaGulosa : IEstategia
    {
        public string Nome => "Guloso Inteligente";

        public (Dinossauro dino, Cercados cercado)? Avaliar(InformacoesTurno info)
        {
            if (info?.MaoJogador == null || !info.MaoJogador.Any(x => x.QuantidadeDinossauros > 0))
                return null;

            var melhorScore = int.MinValue;
            Dinossauro melhorDino = default;
            Cercados melhorCercado = default;

            foreach (var item in info.MaoJogador.Where(x => x.QuantidadeDinossauros > 0))
            {
                foreach (Cercados cercado in Enum.GetValues(typeof(Cercados)))
                {
                    if (!Validator.JogadaValidator(info, cercado, item.Dino))
                        continue;

                    var score = AvaliarJogada(info, item.Dino, cercado);

                    if (score > melhorScore)
                    {
                        melhorScore = score;
                        melhorDino = item.Dino;
                        melhorCercado = cercado;
                    }
                }
            }

            return melhorScore == int.MinValue 
                ? ObterPrimeiraJogadaValida(info) 
                : (melhorDino, melhorCercado);
        }

        private int AvaliarJogada(InformacoesTurno info, Dinossauro dino, Cercados cercado)
        {
            if (cercado == Cercados.FI)
                return PodeColocarFlorestaIgualdade(info, dino) ? 100 : int.MinValue;

            if (cercado == Cercados.RS && !ValidarSoberaniaReiDaSelva(info, dino, isSimulacao: true))
                return -100;

            return ComidinhaDoGuloso(info, dino, cercado) +
                   EstrategiaAnalizador.BonusJogada(info, cercado, dino) +
                   EstrategiaAnalizador.PotencialFuturo(info, cercado, dino);
        }

        private int ComidinhaDoGuloso(InformacoesTurno info, Dinossauro dino, Cercados cercado) =>
            PontuacaoSimulada(info, cercado, dino) - PontuacaoTotal(info);

        #region Configuração de Regra do Ilha Solitária

        private const int TurnoMinimoPontuarIlhaSolitaria = 10;
        private static bool IlhaSolitariaPontua(int turnoAtual) => turnoAtual >= TurnoMinimoPontuarIlhaSolitaria;

        #endregion

        private int PontuacaoTotal(InformacoesTurno info)
        {
            var pontos = 0;

            foreach (var cercado in info.CercadosJogador)
            {
                var dinos = cercado.Dinossauros ?? new List<AuxDinossauro>();
                var qtdDinos = dinos.Sum(d => d.QuantidadeDinossauros);

                switch (cercado.Cercados)
                {
                    case Cercados.FI: 
                        pontos += ScoreFlorestaIgualdade(qtdDinos); 
                        break;
                    case Cercados.CD: 
                        var especiesDistintas = dinos.Where(d => d.QuantidadeDinossauros > 0).Select(d => d.Dino).Distinct().Count();
                        pontos += ScoreCampinaDiferenca(especiesDistintas); 
                        break;
                    case Cercados.MT: 
                        if (qtdDinos == 3) pontos += 7; 
                        break;
                    case Cercados.PA: 
                        pontos += (qtdDinos / 2) * 5; 
                        break;
                    case Cercados.RI: 
                        pontos += qtdDinos; 
                        break;
                    case Cercados.RS:
                        if (qtdDinos == 1 && ValidarSoberaniaReiDaSelva(info, dinos[0].Dino, isSimulacao: false))
                            pontos += 7;
                        break;
                    case Cercados.IS:
                        if (qtdDinos == 1)
                        {
                            var dinoIlha = dinos.First(d => d.QuantidadeDinossauros > 0).Dino;
                            var especieEhUnica = info.CercadosJogador
                                .SelectMany(c => c.Dinossauros ?? new List<AuxDinossauro>())
                                .Where(d => d.QuantidadeDinossauros > 0)
                                .Count(d => d.Dino == dinoIlha) == 1;

                            if (especieEhUnica && IlhaSolitariaPontua(info.NumeroTurno))
                                pontos += 7;
                        }
                        break;
                }
            }

            return pontos;
        }

        private int PontuacaoSimulada(InformacoesTurno info, Cercados alvo, Dinossauro novoDino)
        {
            var pontos = 0;

            foreach (var cercado in info.CercadosJogador)
            {
                var dinos = cercado.Dinossauros?.Select(d => new AuxDinossauro(d.Dino, d.QuantidadeDinossauros)).ToList() 
                            ?? new List<AuxDinossauro>();

                if (cercado.Cercados == alvo)
                {
                    var existente = dinos.FirstOrDefault(d => d.Dino == novoDino);
                    if (existente != null)
                    {
                        dinos.Remove(existente);
                        dinos.Add(new AuxDinossauro(novoDino, existente.QuantidadeDinossauros + 1));
                    }
                    else
                    {
                        dinos.Add(new AuxDinossauro(novoDino, 1));
                    }
                }

                var qtdDinos = dinos.Sum(d => d.QuantidadeDinossauros);

                switch (cercado.Cercados)
                {
                    case Cercados.FI: 
                        pontos += ScoreFlorestaIgualdade(qtdDinos); 
                        break;
                    case Cercados.CD: 
                        var especiesDistintas = dinos.Where(d => d.QuantidadeDinossauros > 0).Select(d => d.Dino).Distinct().Count();
                        pontos += ScoreCampinaDiferenca(especiesDistintas); 
                        break;
                    case Cercados.MT: 
                        if (qtdDinos == 3) pontos += 7; 
                        break;
                    case Cercados.PA: 
                        pontos += (qtdDinos / 2) * 5; 
                        break;
                    case Cercados.RI: 
                        pontos += qtdDinos; 
                        break;
                    case Cercados.RS:
                        if (qtdDinos == 1 && ValidarSoberaniaReiDaSelva(info, dinos[0].Dino, isSimulacao: true))
                            pontos += 7;
                        break;
                    case Cercados.IS:
                        if (qtdDinos == 1)
                        {
                            var apareceEmOutro = info.CercadosJogador
                                .Where(c => c.Cercados != Cercados.IS)
                                .SelectMany(c => c.Dinossauros ?? new List<AuxDinossauro>())
                                .Where(d => d.QuantidadeDinossauros > 0)
                                .Any(d => d.Dino == novoDino);

                            if (!apareceEmOutro) pontos += 7;
                        }
                        break;
                }
            }

            return pontos;
        }

        private bool ValidarSoberaniaReiDaSelva(InformacoesTurno info, Dinossauro especie, bool isSimulacao)
        {
            var meusDinosValidos = info.CercadosJogador
                .Where(c => c.Cercados != Cercados.RI)
                .SelectMany(c => c.Dinossauros ?? new List<AuxDinossauro>())
                .Where(d => d.Dino == especie)
                .Sum(d => d.QuantidadeDinossauros);

            if (isSimulacao)
                meusDinosValidos++;

            var historicoOponentes = ObterJogadasOponentesAteTurnoFallback(info.IdPartida, info.MeuId, info.NumeroTurno);

            var maxOponentes = historicoOponentes
                .Where(j => j.Dinossauro == especie && j.Cercado != Cercados.RI)
                .GroupBy(j => j.IdJogador)
                .Select(g => g.Count())
                .DefaultIfEmpty(0)
                .Max();

            return meusDinosValidos > maxOponentes;
        }

        private int ScoreFlorestaIgualdade(int qtdDinos) =>
            qtdDinos switch { 1 => 2, 2 => 4, 3 => 8, 4 => 12, 5 => 18, 6 => 24, _ => 0 };

        private int ScoreCampinaDiferenca(int qtdDinos) =>
            qtdDinos switch { 1 => 1, 2 => 3, 3 => 6, 4 => 10, 5 => 15, 6 => 21, _ => 0 };

        private (Dinossauro, Cercados)? ObterPrimeiraJogadaValida(InformacoesTurno info)
        {
            foreach (var item in info.MaoJogador.Where(x => x.QuantidadeDinossauros > 0))
            {
                foreach (Cercados cercado in Enum.GetValues(typeof(Cercados)))
                {
                    if (Validator.JogadaValidator(info, cercado, item.Dino))
                        return (item.Dino, cercado);
                }
            }
            return null;
        }

        private bool PodeColocarFlorestaIgualdade(InformacoesTurno info, Dinossauro especie)
        {
            var florestaIgualdade = info.CercadosJogador.FirstOrDefault(x => x.Cercados == Cercados.FI);
            if (info.NumeroTurno == 1) return false;
            if (florestaIgualdade?.Dinossauros?.Any() == true)
                return true;

            var jogadas = ObterJogadasOponentesAteTurnoFallback(info.IdPartida, info.MeuId, info.NumeroTurno - 1);
            
            return !jogadas.Any(d => d.Dinossauro == especie && 
                                    (d.Cercado == Cercados.FI || d.Cercado == Cercados.RS));
        }
        
        private List<Tabuleiro.JogadaOponente> ObterJogadasOponentesAteTurnoFallback(int idPartida, int meuId, int turno)
        {
            return new List<Tabuleiro.JogadaOponente>();
        }
    }
}