using System;
using System.Reflection;

namespace Extintos.Enumeration
{
   
    [AttributeUsage(AttributeTargets.Field)]
    internal class DinossauroInfoAttribute : Attribute
    {
        public string Nome { get; }
        public string Codigo { get; }
        public string Cor { get; }

        //public int Quantidade { get; set; }

        public DinossauroInfoAttribute(string nome, string cor)
        {
            Nome = nome;
            Cor = cor;
            //Quantidade = quantidade;
        }
    }

    
    public enum Dinossauro
    {
        [DinossauroInfo("Braquiossauro", "Roxo")]
        BR,
        [DinossauroInfo("Espinossauro", "Laranja")]
        EP,
        [DinossauroInfo("Estegossauro","Azul")]
        ET,
        [DinossauroInfo("Parasaurolófo", "Verde")]
        PA,
        [DinossauroInfo("Tiranossauro","Vermelho")]
        TI,
        [DinossauroInfo("Tricerátops", "Amarelo")]
        TR
    }

   
    
    internal static class DinoExtension
    {
        public static DinossauroInfoAttribute GetInfo(this Dinossauro dino)
        {
            var field = dino.GetType().GetField(dino.ToString());
            return field?.GetCustomAttribute<DinossauroInfoAttribute>();
        }

        public static string PegaNome(this Dinossauro dino)
        {
            return dino.GetInfo()?.Nome ?? dino.ToString();
        }

        public static string PegaCor(this Dinossauro dino)
        {
            return dino.GetInfo()?.Cor;
        }

        //public static string PegaQuantidade(this Dinossauro dino)
        //{
        //    return dino.GetInfo()?.Codigo ?? "??";
        //}
    }
}
