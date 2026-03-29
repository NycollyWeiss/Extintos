namespace Extintos.Enumeration
{
    public class AuxCercado
    {
        public Cercados Cercados  { get; set; }
        public  int QuantidadeDinos { get; set; }


        public AuxCercado(Cercados cercados, int quantidadedinos)
        {
            Cercados = cercados;
            QuantidadeDinos= quantidadedinos;
        }
        
    }
}