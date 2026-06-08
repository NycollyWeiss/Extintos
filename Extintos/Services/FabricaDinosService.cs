using System.Collections.Generic;
using System.Drawing;
using Extintos.Auxiliares;
using Extintos.Model;

namespace Extintos.Services
{
    public class FabricaDinosService
    {
        public List<DinossauroVisual> Criar(
            List<AuxDinossauro> maoJogador,
            int centroX,
            int centroY)
        {
            var dinos = new List<DinossauroVisual>();

            const int tamanhoDino = 100;
            const int espacamentoX = 95;
            const int espacamentoY = 95;

            var index = 0;

            foreach (var item in maoJogador)
            {
                for (var i = 0; i < item.QuantidadeDinossauros; i++)
                {
                    if (index >= 6)
                        break;

                    var coluna = index % 3;
                    var linha = index / 3;

                    var posX = centroX + coluna * espacamentoX;
                    var posY = centroY + linha * espacamentoY;

                    var dino = new DinossauroVisual(
                        ProvedorDeImagens.PegarImagemDinossauro(
                            item.Dino),
                        posX,
                        posY)
                    {
                        Tipo = item.Dino.ToString(),
                        largura = tamanhoDino,
                        altura = tamanhoDino,
                        area = new Rectangle(
                            posX,
                            posY,
                            tamanhoDino,
                            tamanhoDino)
                    };

                    dinos.Add(dino);

                    index++;
                }
            }

            return dinos;
        }
    }
}