using Extintos.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extintos.Auxiliares
{
    internal class AuxUniverso
    {
        public static void ConsolidarDino(List<AuxDinossauro> dinosPraConsolidar)
        {
            var consolidado = dinosPraConsolidar
                .GroupBy(x => x.Dino)
                .Select(g =>
                    new AuxDinossauro(
                        g.Key,
                        g.Sum(x => x.QuantidadeDinossauros)))
                .ToList();


            dinosPraConsolidar.Clear();
            dinosPraConsolidar.AddRange(consolidado);
        }

        public static int QuantidadeConhecidaPorEspecie(List<AuxDinossauro> universoConhecido, Dinossauro especie)
        {
            return universoConhecido
                .FirstOrDefault(x => x.Dino == especie)?.QuantidadeDinossauros ?? 0;
        }


    }
}
