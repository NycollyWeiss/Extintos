using System.Collections.Generic;
using Extintos.Enumeration;

namespace Extintos.Interfaces
{
    public class ResultadoDecisao
    {
        public string EstrategiaEscolhida { get; set; } = string.Empty;
        public Dinossauro Dinossauro { get; set; }
        public Cercados Cercado { get; set; }
        public int Pontuacao { get; set; }
        public List<ResultadoEstrategia> TodasAsOpcoes { get; set; } = new();

        public string LogResumo =>
            $"[{EstrategiaEscolhida}] {Dinossauro.PegaNome()}→{Cercado.PegaNome()} ({Pontuacao}pts)";
    }
}