using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Extintos.Enumeration.DadoFace; // pq o rinder gerou isso ?
using Extintos;

namespace Extintos
{
   
    [AttributeUsage(AttributeTargets.Field)]
    internal class DinossauroInfo : Attribute
    {
        public string Nome { get; }
        public string Cor { get; }
        
        public string Codigo { get; }

        //public int Quantidade { get; set; }

        public DinossauroInfo(string nome, string cor, string codigo)
        
        {   
            Cor = cor;
            Nome = nome;
            Codigo = codigo;

        }
        
    }
    
    public enum Dinossauro
    {
        [DinossauroInfo("(BR) Braquiossauro", "Roxo", "BR")]
        BR,
        [DinossauroInfo("(EP) Espinossauro", "Laranja","EP")]
        EP,
        [DinossauroInfo("(ET) Estegossauro","Azul", "ET")]
        ET,
        [DinossauroInfo("(PA) Parasaurolófo", "Verde","PA")]
        PA,
        [DinossauroInfo("(TI) Tiranossauro","Vermelho","TI")]
        TI,
        [DinossauroInfo("(TR) Tricerátops", "Amarelo","TR")]
        TR
    }

   
    
    internal static class DinoExtension
    {
        public static DinossauroInfo GetInfo(this Dinossauro dino)
        {
            var field = dino.GetType().GetField(dino.ToString());
            return field?.GetCustomAttribute<DinossauroInfo>();
        }

        public static string PegaNome(this Dinossauro dino)
        {
            return dino.GetInfo()?.Nome ?? dino.ToString();
        }

        public static string PegaCor(this Dinossauro dino)
        {
            return dino.GetInfo()?.Cor;
        }
        
        public static string PegaCodigo(this Dinossauro dino)
        {
            return dino.GetInfo()?.Codigo;
        }
    }
}


    
  