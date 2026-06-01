using Extintos.Enumeration;
namespace Extintos.Interfaces
{
    public class ResultadoEstrategia
    {
        public string NomeEstrategia { get; set; } = string.Empty;
        public Dinossauro Dinossauro { get; set; }
        public Cercados Cercado { get; set; }
        public int Pontuacao { get; set; }
        public bool Sucesso { get; set; }
        public string? Erro { get; set; }
    }
}