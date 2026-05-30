using System.Linq;
using Extintos.Enumeration;

namespace Extintos.LeonKennedy
{
    public class EstrategiaAnalizador
    {
        public static int BonusJogada(
            InformacoesTurno info,
            Cercados cercado,
            Dinossauro dino)
        {
            var bonus = 0;

            var alvo = info.CercadosJogador
                .First(x => x.Cercados == cercado);

            var qtdAtual = alvo.Dinossauros.Count;

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
                        bonus += 40;

                    break;

                case Cercados.IS:

                    if (qtdAtual == 0)
                        bonus += 35;

                    break;

                case Cercados.FI:

                    bonus += qtdAtual * 5;

                    break;

                case Cercados.CD:

                    if (!alvo.Dinossauros.Contains(dino))
                        bonus += 8;

                    break;
            }

            return bonus;
        }

        public static int PotencialFuturo(
            InformacoesTurno info,
            Cercados cercado,
            Dinossauro dino)
        {
            var alvo = info.CercadosJogador
                .First(x => x.Cercados == cercado);

            var qtd = alvo.Dinossauros.Count;

            switch (cercado)
            {
                case Cercados.MT:

                    return (3 - qtd) * 10;

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