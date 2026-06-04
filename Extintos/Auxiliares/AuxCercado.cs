using System.Collections.Generic;
using Extintos.Enumeration;

namespace Extintos.Auxiliares
{
    public class AuxCercado
    {
        public AuxCercado(Cercados cercados)
        {
            Cercados = cercados;
        }

        public Cercados Cercados { get; set; }
        public List<Dinossauro> Dinossauros { get; set; } = new();
    }
}