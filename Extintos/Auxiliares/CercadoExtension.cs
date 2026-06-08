#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Extintos.Enumeration;


namespace Extintos.Auxiliares
{
    internal static class CercadosExtension
    {
        private static readonly ConcurrentDictionary<Cercados, CercadosInfo?> InfoCache = new();

        public static CercadosInfo? PegaInfo(this Cercados cercado)
        {
            return InfoCache.GetOrAdd(cercado, c =>
            {
                var field = c.GetType().GetField(c.ToString());
                return field?.GetCustomAttribute<CercadosInfo>();
            });
        }

        public static string PegaNome(this Cercados cercado) => cercado.PegaInfo()?.Nome ?? cercado.ToString();
        public static string PegaCodigo(this Cercados cercado) => cercado.PegaInfo()?.Codigo ?? cercado.ToString();
        public static List<Cercados> CercadosLista() => Enum.GetValues(typeof(Cercados)).Cast<Cercados>().ToList();
        public static List<AuxCercado> CercadoAuxLista() => CercadosLista().Select(c => new AuxCercado(c)).ToList();




        public static bool SePodeColocarNoCercado(this Cercados cercado, AuxCercado cercadoEscolhido,
            Dinossauro novoDino)
        {
            if (!Enum.IsDefined(typeof(Dinossauro), novoDino)) return false;

            int totalDinosNoCercado = cercadoEscolhido.Dinossauros.Sum(d => d.QuantidadeDinossauros);
            bool existeDessaEspecie = cercadoEscolhido.Dinossauros.Any(d => d.Dino == novoDino);
            bool estaVazio = totalDinosNoCercado == 0;

            return cercado switch
            {

                Cercados.FI => estaVazio || (existeDessaEspecie && totalDinosNoCercado < 6),


                Cercados.CD => estaVazio || (!existeDessaEspecie && totalDinosNoCercado < 6),


                Cercados.IS => estaVazio,
                
                Cercados.RS => estaVazio,


                Cercados.MT => totalDinosNoCercado < 3,


                Cercados.PA => totalDinosNoCercado < 6,


                Cercados.RI => true,
                _ => false
            };
        }


        public static bool SePodeColocarNoCercado(this Cercados cercado, List<Dinossauro> dinosNoCercado,
            Dinossauro novoDino)
        {
            var mockCercado = new AuxCercado(cercado);
            foreach (var d in dinosNoCercado)
                mockCercado.Dinossauros.Add(new AuxDinossauro(d, 1));

            return SePodeColocarNoCercado(cercado, mockCercado, novoDino);
        }

        

        public static int ObterPontuacaoMaxima(this Cercados cercado, int quantidade)
        {
            return cercado switch
            {
                Cercados.FI => quantidade >= 6 ? 24 :
                    quantidade >= 5 ? 18 :
                    quantidade >= 4 ? 12 :
                    quantidade >= 3 ? 8 :
                    quantidade >= 2 ? 4 :
                    quantidade >= 1 ? 2 : 0,
                Cercados.CD => quantidade >= 6 ? 21 :
                    quantidade >= 5 ? 15 :
                    quantidade >= 4 ? 10 :
                    quantidade >= 3 ? 6 :
                    quantidade >= 2 ? 3 :
                    quantidade >= 1 ? 1 : 0,
                Cercados.MT => quantidade == 3 ? 7 : 0,
                Cercados.PA => (quantidade / 2) * 5,
                Cercados.RI => quantidade,
                Cercados.RS => quantidade == 1 ? 7 : 0,
                Cercados.IS => quantidade == 1 ? 7 : 0,
                _ => 0
            };
        }

    
    }
}