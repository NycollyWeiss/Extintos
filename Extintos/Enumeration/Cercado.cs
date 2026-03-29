using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Draft;

namespace Extintos.Enumeration
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class CercadosInfo : Attribute
    {
        public string Nome { get; }

        public string Restricao { get; }

        public string Pontuacao { get; }
        

        public CercadosInfo(string nome, string restricao, string pontuacao)
        {
            Nome = nome;
            Restricao = restricao;
            Pontuacao = pontuacao;
           
        }
    }

    public enum Cercados
        {
            [CercadosInfo("Campina da Diferença", "Este cercado só pode conter dinossauros de espécies diferentes.", "Pontuação baseada na quantidade. 1 / 3 / 6 / 10/ 15 e 21 respectivamente.")]
            CD,
            [CercadosInfo("Floresta da Igualdade", "Só pode conter dinossauros da mesma espécie.", "Pontuação baseada na quantidade. 2 / 4 / 8 / 12 / 18 e 24 respectivamente.")]
            FI,
            [CercadosInfo("Ilha Solitária", "Pode ter apenas um dinossauro.", "7 pontos caso o dinossauro deste cercado seja o único desta espécie em seu zoo.")]
            IS,
            [CercadosInfo("Mata Tripla", "Pode conter até 3 dinossauros de qualquer espécie, iguais ou diferentes.", "7 pontos caso tenha exatamente 3 dinossauros.")]
            MT,
            [CercadosInfo("Pradaria do Amor", "Pode conter todas as espécies de dinossauros.", "5 pontos para cada casal de dinossauros da mesma espécie. É permitido diversos casais da mesma espécie.")]
            PA,
            [CercadosInfo("Rio", "Pode conter qualquer quantidade de dinossauros.", "1 ponto para cada um.")]
            RI,
            [CercadosInfo("Rei da Selva", "Pode ter apenas um dinossauro.", "7 pontos caso seu zoo tenha uma quantidade igual ou maior desta espécie que qualquer outro zoo.")]
            RS,

        }



    internal static class CercadosExtension
    {
        public static CercadosInfo? PegaInfo(this Cercados cercado)
        {
            var field = cercado.GetType().GetField(cercado.ToString());
            return field?.GetCustomAttribute<CercadosInfo>();
        }

        public static string PegaNome(this Cercados cercado)
        {
            return cercado.PegaInfo()?.Nome ?? cercado.ToString();
        }

        public static string PegaRestricao(this Cercados cercado)
        {
            return cercado.PegaInfo()?.Restricao ?? string.Empty;
        }

        public static string PegaPontuacao(this Cercados cercado)
        {
            return cercado.PegaInfo()?.Pontuacao ?? cercado.ToString();
        }
        
        public static List<Cercados> cercadosLista()
        {
            string cercados = Jogo.ListarCercados();
            Cercados cercadosConvertidoAmem = (Cercados)Enum.Parse(typeof(Cercados), cercados);
            List<Cercados> todosCercados = new List<Cercados>();
            todosCercados.Add(cercadosConvertidoAmem);
            
            return todosCercados;
        }

        public static bool SePodeAdicionar(this Cercados cercado, List<string> dinosNoCercado, string novoDino)
        {
            int quantos = dinosNoCercado.Count;

            switch (cercado)
            {
                case Cercados.FI:
                    if (quantos == 0) return true;
                    if (quantos >= 6) return false;
                    return dinosNoCercado.All(dino => dino == novoDino);
                // ve se o que quer add é igual aos que ja estao la

                case Cercados.CD:
                    if (quantos >= 6) return false;
                    return !dinosNoCercado.Contains(novoDino);
                // pra ver se ja tem a especie la se tver n pode colocar

                case Cercados.IS:
                case Cercados.RS:
                    return quantos == 0; // só se estiver vazio

                case Cercados.MT: // só 3
                    if (quantos >= 6) return false;
                    return quantos < 3;

                case Cercados.PA: // 
                    if (quantos >= 6) return false;
                    return true;

                case Cercados.RI: 
                    if (quantos >= 6) return false;
                    return true;

                default:
                    return false;
                
            }
           
        }
        //como usa  
        // cada jogador tem uma lista pra cada cercado
        //List<Cercado> meusCercados = new List<Cercado>
        //  PodeAdicionar(meuCercadoAtual, novoDino)
        
    }
}