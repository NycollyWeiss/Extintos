using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Extintos.Enumeration
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class DadoFace : Attribute

    {
        public string Nome { get; }
        public string Restricao { get; }

        public DadoFace(string nome, string restricao)
        {
            Nome = nome;
            Restricao = restricao;
        }


        public enum Dado
        {
            [DadoFace("Alimentação", "Os dinossauros devem ser posicionados...")]
            AL,
            [DadoFace("Floresta", "Os dinossauros devem ser posicionados na seção florestal de seu zoo")]
            FL,
            [DadoFace("Pradaria", "Os dinossauros devem ser posicionados na seção de pradarias de seu zoo")]
            PR,
            [DadoFace("Tiranossauro Rex", "Os dinossauros devem ser posicionados em um cercado que não tenha um T-Rex")]
            TI,
            [DadoFace("Cercado Vazio", "Os dinossauros devem ser posicionados em um cercado ainda vazio")]
            VZ,
            [DadoFace("Banheiros", "Os dinossauros devem ser posicionados no lado dos banheiros do seu zoo")]
            WC,

        }

        public static DadoFace ? PegaInfo(this Dado dado) 
        {
          var field = dado.GetType().GetField(dado.ToString());
            return field?.GetCustomAttribute<DadoFace>();
        }

        public static string PegaNome(this Dado dado) =>
            dado.PegaInfo()?.Nome ?? dado.ToString();

        public static string PegaRestricao(this Dado dado) =>
            dado.PegaInfo()?.Restricao ?? string.Empty;
    }
}
}
