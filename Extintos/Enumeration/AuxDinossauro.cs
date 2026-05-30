namespace Extintos.Enumeration
{
    public class AuxDinossauro
    {
        public AuxDinossauro(Dinossauro dinossauro, int quantidadeDinossauros)
        {
            Dinossauro = dinossauro;
            QuantidadeDinossauros = quantidadeDinossauros;
        }

        public AuxDinossauro()
        {
        }

        public Dinossauro Dinossauro { get; set; }
        public int QuantidadeDinossauros { get; set; }
    }
}