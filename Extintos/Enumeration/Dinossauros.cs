using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Extintos.Enumeration
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class DinossauroInfo : Attribute
    {
        public DinossauroInfo(string nome, string cor, string codigo)

        {
            Cor = cor;
            Nome = nome;
            Codigo = codigo;
        }

        public string Nome { get; set; }
        public string Cor { get; set; }

        public string Codigo { get; set; }
    }

    public enum Dinossauro
    {
        

        [DinossauroInfo("(BR) Braquiossauro", "Roxo", "BR")]
    
        BR,

        [DinossauroInfo("(EP) Espinossauro", "Laranja", "EP")]
       
        EP,

        [DinossauroInfo("(ET) Estegossauro", "Azul", "ET")]
        
        ET,

        [DinossauroInfo("(PA) Parasaurolófo", "Verde", "PA")]
    
        PA,

        [DinossauroInfo("(TI) Tiranossauro", "Vermelho", "TI")]
      
        TI,

        [DinossauroInfo("(TR) Tricerátops", "Amarelo", "TR")]
      
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
        public static string PegaCodigo(this Dinossauro dino)
        {
            return dino.GetInfo()?.Codigo;
        }

        public static List<Dinossauro> EspeciesExistentes ()
        {
            return Enum.GetValues(typeof(Dinossauro))
                .Cast<Dinossauro>()
                .ToList();
        }
    }
}
