using System.Collections.Generic;
using Extintos.Enumeration;

namespace Extintos.Auxiliares
{
    public class AuxCercado
    {
        public Cercados Cercados { get; set; }
        public List<AuxDinossauro> Dinossauros { get; set; } //acho q faz mais sentido ser uma lista de 
        //auxdino pois assim ele ja pega o obj dino e a quantidade, assim nao precisa contar toda vez
        //so chamar o quantidade 
        
        public AuxCercado(Cercados cercados)
        {
            Cercados = cercados;
            Dinossauros = new List<AuxDinossauro>();

        }
        
    }
}