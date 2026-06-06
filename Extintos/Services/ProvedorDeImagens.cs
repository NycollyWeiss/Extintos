using System.Drawing;
using Extintos.Enumeration;
using Extintos.Properties;

namespace Extintos.Services
{
    public static class ProvedorDeImagens
    {
        public static Image PegarImagemDinossauro(Dinossauro dino)
        {
            switch (dino)
            {
                case Dinossauro.TI: return Resources.Tiranossauro;
                case Dinossauro.BR: return Resources.Braquiossauro;
                case Dinossauro.ET: return Resources.Estegossauro;
                case Dinossauro.PA: return Resources.Parasaurolofo;
                case Dinossauro.EP: return Resources.Espinossauro;
                case Dinossauro.TR: return Resources.Triceratops;
                default: return null;
            }
        }

        public static Image PegarImagemDado(string face)
        {
            switch (face)
            {
                case "AL": return Resources.Dado_Alimentacao;
                case "FL": return Resources.Dado_Floresta;
                case "PR": return Resources.Dado_Pradaria;
                case "TI": return Resources.Dado_ReiSelva;
                case "VZ": return Resources.Dado_CercadoVazio;
                case "WC": return Resources.Dado_Banheiro;
                default: return null;
            }
        }
    }
}