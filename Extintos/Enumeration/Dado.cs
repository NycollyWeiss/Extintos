using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Extintos.Enumeration
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DadoFace : Attribute

    {
        public DadoFace(string nome, string restricao)
        {
            Nome = nome;
            Restricao = restricao;
        }

        public string Nome { get; }
        public string Restricao { get; }
    }

    public enum Dado
    {
        [DadoFace("Alimentação", "Os dinossauros devem ser posicionados no lado da praça de alimentação do seu zoo")]
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
        WC
    }
    
}


