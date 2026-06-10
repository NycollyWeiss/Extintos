using Extintos.Enumeration;
using System.Collections.Generic;
using System.Linq;
using Extintos.Auxiliares;

namespace Extintos.LeonKennedy
{
    

    public class EstrategiaAnalizador
    {
        public static int BonusJogada(InformacoesTurno info, Cercados cercado, Dinossauro dino)
        {
            var bonus = 0;
            var alvo = info.CercadosJogador.FirstOrDefault(x => x.Cercados == cercado);
            if (alvo == null) return 0;

            var dinosNoCercado = alvo.Dinossauros ?? new List<AuxDinossauro>();
            var qtdAtual = dinosNoCercado.Sum(d => d.QuantidadeDinossauros);

            int qtdDessaEspecieNoZoo = info.CercadosJogador
                .Where(c => c.Cercados != Cercados.RI)
                .SelectMany(c => c.Dinossauros ?? new List<AuxDinossauro>())
                .Where(d => d.Dino == dino)
                .Sum(d => d.QuantidadeDinossauros);

            int qtdDessaEspecieNaMao = info.MaoJogador
                .Where(d => d.Dino == dino)
                .Sum(d => d.QuantidadeDinossauros);

            int totalEspecieDisponivelParaMi = qtdDessaEspecieNoZoo + qtdDessaEspecieNaMao + 1;

            switch (cercado)
            {
                case Cercados.MT:

                    if (qtdAtual == 2)
                        bonus += 50;

                    break;

                case Cercados.PA:

                    if ((qtdAtual + 1) % 2 == 0)
                        bonus += 30;

                    break;

                case Cercados.RS: 
                    if (qtdAtual == 0)
                    {
                        if (totalEspecieDisponivelParaMi >= 4) 
                            bonus += 300; 
                        else if (totalEspecieDisponivelParaMi == 3) 
                            bonus += 100;
                        else 
                            bonus -= 150;
                    }
                    break;

                case Cercados.IS: 
                    if (qtdAtual == 0)
                    {
                        var cercadoCD = info.CercadosJogador
                            .FirstOrDefault(x => x.Cercados == Cercados.CD);
                        int qtdEspeciesNoCD = cercadoCD?.Dinossauros?
                            .Count(d => d.QuantidadeDinossauros > 0) ?? 0;
                        
                        if (qtdEspeciesNoCD == 6)
                        {
                            bonus -= 400; 
                            break;
                        }

                        if (info.NumeroTurno < 9)
                        {
                            bonus -= 200; 
                            break;
                        }

                        bool especieEhUnicaNoMeuZoo = (qtdDessaEspecieNoZoo == 0);
                        
                        if (especieEhUnicaNoMeuZoo)
                        {
                            bonus += 200; 
                        }
                        else
                        {
                            bonus -= 300; 
                        }
                    }
                    break;

                case Cercados.FI:

                    bonus += qtdAtual * 5;

                    break;

                case Cercados.CD:
                        bonus += 8;

                    break;
            }

            return bonus;
        }

        public static int PotencialFuturo(InformacoesTurno info, Cercados cercado, Dinossauro dino)
        {
            var alvo = info.CercadosJogador.First(x => x.Cercados == cercado);

            var qtd = alvo.Dinossauros.Count;

            switch (cercado)
            {
                case Cercados.MT:

                    return (3 - qtd) * 7;

                case Cercados.FI:

                    return qtd * 3;

                case Cercados.CD:

                    return alvo.Dinossauros
                        .Distinct()
                        .Count() * 4;

                default:

                    return 0;
            }
        }
    }
}
