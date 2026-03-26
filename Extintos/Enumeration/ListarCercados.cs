using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Extintos.Enumeration.DadoFace;
using static Extintos.Enumeration.ListarCercados;

namespace Extintos.Enumeration
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class ListarCercados : Attribute
    {
        public string Nome { get; }

        public string Restricao { get; }

        public string Pontuacao { get; }
    

     public ListarCercados(string nome, string restricao, string pontuacao)
        {
            Nome = nome;
            Restricao = restricao;
            Pontuacao = pontuacao;
        }

        public enum Cercados
        {
            [ListarCercados("Campina da Diferença", "Este cercado só pode conter dinossauros de espécies diferentes.", "Pontuação baseada na quantidade. 1 / 3 / 6 / 10/ 15 e 21 respectivamente.")]
            CD,
            [ListarCercados("Floresta da Igualdade", "Só pode conter dinossauros da mesma espécie.", "Pontuação baseada na quantidade. 2 / 4 / 8 / 12 / 18 e 24 respectivamente.")]
            FI,
            [ListarCercados("Ilha Solitária", "Pode ter apenas um dinossauro.", "7 pontos caso o dinossauro deste cercado seja o único desta espécie em seu zoo.")]
            IS,
            [ListarCercados("Mata Tripla", "Pode conter até 3 dinossauros de qualquer espécie, iguais ou diferentes.", "7 pontos caso tenha exatamente 3 dinossauros.")]
            MT,
            [ListarCercados("Pradaria do Amor", "Pode conter todas as espécies de dinossauros.", "5 pontos para cada casal de dinossauros da mesma espécie. É permitido diversos casais da mesma espécie.")]
            PA,
            [ListarCercados("Rio", "Pode conter qualquer quantidade de dinossauros.", "1 ponto para cada um.")]
            RI,
            [ListarCercados("Rei da Selva", "Pode ter apenas um dinossauro.", "7 pontos caso seu zoo tenha uma quantidade igual ou maior desta espécie que qualquer outro zoo.")]
            RS,

        }

    }

    internal static class CercadosExtension
    {
        public static ListarCercados? PegaInfo(this Cercados cercados)
        {
            var field = cercados.GetType().GetField(cercados.ToString());
            return field?.GetCustomAttribute<ListarCercados>();
        }

        public static string PegaNome(this Cercados cercados)
        {
            return cercados.PegaInfo()?.Nome ?? cercados.ToString();
        }

        public static string PegaRestricao(this Cercados cercados)
        {
            return cercados.PegaInfo()?.Restricao ?? string.Empty;
        }

        public static string PegaPontuacao(this Cercados cercados)
        {
            return cercados.PegaInfo()?.Pontuacao ?? cercados.ToString();
        }


    }
}

