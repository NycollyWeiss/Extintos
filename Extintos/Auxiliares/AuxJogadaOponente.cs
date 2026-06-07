using Extintos.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extintos.Auxiliares
{
    public class AuxJogadaOponente
    {
        public int IdJogador {  get; set; }
        public Dinossauro Dinossauro { get; set; }
        public Cercados Cercado { get; set; }
        public int Turno { get; set; }

        public AuxJogadaOponente(int idJogador,Dinossauro dinossauro, Cercados cercado, int turno)
        {
            IdJogador = idJogador;
            Dinossauro = dinossauro;
            Cercado = cercado;
            Turno = turno;
        }
    }
}
