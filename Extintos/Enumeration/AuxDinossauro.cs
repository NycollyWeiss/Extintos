namespace Extintos.Enumeration
{
    public class AuxDinossauro
    {
        public Dinossauro Dinossauro { get; set; }
        public int QuantidadeDinossauros { get; set; }

        public AuxDinossauro(Dinossauro dinossauro, int quantidadeDinossauros)
        {
            Dinossauro = dinossauro;
            QuantidadeDinossauros = quantidadeDinossauros;
        }
    }
}