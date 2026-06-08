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
        public ConfigEstrategia ConfigEstrategia;

        public EstrategiaGulosa(ConfigEstrategia configEstrategia)
        {
            this.ConfigEstrategia = configEstrategia;
        }

        public string Nome => "Guloso Inteligente v3 (Blindado)";

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
                    
                    // Lógica de desempate: se o score for igual, prefere o RIO (segurança)
                    if (score > melhorScore)
                    {
                        melhorScore = score;
                        melhorDino = item.Dino;
                        melhorCercado = cercado;
                    }
                    else if (score == melhorScore && cercado == Cercados.RI)
                    {
                        melhorDino = item.Dino;
                        melhorCercado = cercado;
                    }
                }
            }

            return melhorScore == int.MinValue
                ? ObterJogadaDeEmergencia(info)
                : (melhorDino, melhorCercado);
        }

        private int AvaliarJogada(InformacoesTurno info, Dinossauro dino, Cercados cercado)
        {
            if (cercado == Cercados.FI && !PodeColocarFlorestaIgualdade(info, dino))
                return int.MinValue;

            if (cercado == Cercados.RS && !ValidarSoberaniaReiDaSelva(info, dino, isSimulacao: true))
                return int.MinValue;

            return ComidinhaDoGuloso(info, dino, cercado) +
                   EstrategiaAnalizador.BonusJogada(info, cercado, dino) +
                   EstrategiaAnalizador.PotencialFuturo(info, cercado, dino);
        }

        private int ComidinhaDoGuloso(InformacoesTurno info, Dinossauro dino, Cercados cercado) =>
            PontuacaoSimulada(info, cercado, dino) - PontuacaoTotal(info);

        private int PontuacaoTotal(InformacoesTurno info)
        {
            var pontos = 0;
            foreach (var cercado in info.CercadosJogador)
            {
                var dinos = cercado.Dinossauros ?? new List<AuxDinossauro>();
                var qtdDinos = dinos.Sum(d => d.QuantidadeDinossauros);
                pontos += CalcularPontuacaoCercado(cercado.Cercados, qtdDinos, dinos, info);
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
                pontos += CalcularPontuacaoCercado(cercado.Cercados, qtdDinos, dinos, info);
            }
            return pontos;
        }

        private int CalcularPontuacaoCercado(Cercados tipoCercado, int qtdDinos, List<AuxDinossauro> dinos, InformacoesTurno info)
        {
            switch (tipoCercado)
            {
                case Cercados.FI:
                    return qtdDinos switch { 1 => 2, 2 => 4, 3 => 8, 4 => 12, 5 => 18, 6 => 24, _ => 0 };
                case Cercados.CD:
                    var especiesDistintas = dinos.Where(d => d.QuantidadeDinossauros > 0).Select(d => d.Dino).Distinct().Count();
                    return especiesDistintas switch { 1 => 1, 2 => 3, 3 => 6, 4 => 10, 5 => 15, 6 => 21, _ => 0 };
                case Cercados.MT:
                    return qtdDinos == 3 ? 7 : 0;
                case Cercados.PA:
                    return (qtdDinos / 2) * 5;
                case Cercados.RI:
                    return qtdDinos;
                case Cercados.RS:
                    if (qtdDinos == 1 && ValidarSoberaniaReiDaSelva(info, dinos[0].Dino, isSimulacao: false))
                        return 7;
                    return 0;
                case Cercados.IS:
                    if (qtdDinos == 1)
                    {
                        var dinoIlha = dinos.First(d => d.QuantidadeDinossauros > 0).Dino;
                        var especieEhUnica = info.CercadosJogador
                            .SelectMany(c => c.Dinossauros ?? new List<AuxDinossauro>())
                            .Where(d => d.QuantidadeDinossauros > 0)
                            .Count(d => d.Dino == dinoIlha) == 1;
                            
                        if (especieEhUnica && info.NumeroTurno >= 9)
                            return 7;
                    }
                    return 0;
                default:
                    return 0;
            }
        }

        private bool ValidarSoberaniaReiDaSelva(InformacoesTurno info, Dinossauro especie, bool isSimulacao = false)
        {
            var meusDinosValidos = info.CercadosJogador
                .Where(c => c.Cercados != Cercados.RI)
                .SelectMany(c => c.Dinossauros ?? new List<AuxDinossauro>())
                .Where(d => d.Dino == especie)
                .Sum(d => d.QuantidadeDinossauros);

            if (isSimulacao)
                meusDinosValidos++;

            if (meusDinosValidos >= 4) return true;
            if (meusDinosValidos == 3) return true;

            return false; 
        }

        /// <summary>
        /// Método de emergência reescrito para NUNCA escolher Mata Tripla vazia se o Rio for uma opção válida e segura.
        /// </summary>
        private (Dinossauro, Cercados)? ObterJogadaDeEmergencia(InformacoesTurno info)
        {
            // 1. Primeiro, tenta encontrar QUALQUER jogada no RIO que seja válida (o porto seguro)
            foreach (var item in info.MaoJogador.Where(x => x.QuantidadeDinossauros > 0))
            {
                if (Validator.JogadaValidator(info, Cercados.RI, item.Dino))
                {
                    return (item.Dino, Cercados.RI);
                }
            }

            // 2. Se o Rio não for válido (ex: dado VZ e Rio já tem dinos), procura a melhor jogada restante
            int melhorScore = int.MinValue;
            (Dinossauro, Cercados)? melhorJogada = null;

            foreach (var item in info.MaoJogador.Where(x => x.QuantidadeDinossauros > 0))
            {
                foreach (Cercados cercado in Enum.GetValues(typeof(Cercados)))
                {
                    if (cercado == Cercados.RI) continue; // Já verificamos acima

                    if (Validator.JogadaValidator(info, cercado, item.Dino))
                    {
                        int score = AvaliarJogada(info, item.Dino, cercado);
                        if (score > melhorScore)
                        {
                            melhorScore = score;
                            melhorJogada = (item.Dino, cercado);
                        }
                    }
                }
            }

            return melhorJogada;
        }

        private bool PodeColocarFlorestaIgualdade(InformacoesTurno info, Dinossauro especie)
        {
            var florestaIgualdade = info.CercadosJogador.FirstOrDefault(x => x.Cercados == Cercados.FI);
            
            if (florestaIgualdade?.Dinossauros?.Any(d => d.QuantidadeDinossauros > 0) == true)
                return true;

            var qtdNaMao = info.MaoJogador.Where(d => d.Dino == especie).Sum(d => d.QuantidadeDinossauros);
            var qtdNoZoo = info.CercadosJogador
                .Where(c => c.Cercados != Cercados.RI)
                .SelectMany(c => c.Dinossauros ?? new List<AuxDinossauro>())
                .Where(d => d.Dino == especie)
                .Sum(d => d.QuantidadeDinossauros);

            return (qtdNaMao + qtdNoZoo + 1) >= 3;
        }
    }
}