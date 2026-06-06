using Extintos.Enumeration;

namespace Extintos.Interfaces
{
    public interface IEstategia
    {
        string Nome { get; }
        (Dinossauro dino, Cercados cercado)? Avaliar(InformacoesTurno informacoesTurno);
    }
}