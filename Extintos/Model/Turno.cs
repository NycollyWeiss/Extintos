using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extintos.Model
{
    internal class Turno
    {
        public int Numero { get; set; }       
        public char Status { get; set; }          
        public int IdJogadorAtual { get; set; }  
        public string FaceDado { get; set; }      
    }
    
    
}
