using Extintos.Enumeration;
namespace Extintos.Auxiliares
{
    public class AuxDinossauro
    {
        
        public Dinossauro Dino { get; }
        public int QuantidadeDinossauros { get; set; }
        
        public AuxDinossauro(Dinossauro dinossauro, int quantidadeDinossauros)
        {
            Dino = dinossauro;
            QuantidadeDinossauros = quantidadeDinossauros;
        }
        
    }
}