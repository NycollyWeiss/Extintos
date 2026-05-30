using Extintos.Enumeration;

namespace Extintos.Model
{
    internal interface IEstategia
    {
        string Nome { get; }
        (Dinossauro dino, Cercados cercado)? Avaliar(InformacoesTurno informacoesTurno);
    }
}