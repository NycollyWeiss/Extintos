using Extintos.Enumeration;

namespace Extintos.Interfaces
{
    internal interface IEstategia
    {
        string Nome { get; }
        (Dinossauro dino, Cercados cercado)? Avaliar(InformacoesTurno informacoesTurno);
    }
}