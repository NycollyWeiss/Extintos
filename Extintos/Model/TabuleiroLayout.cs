using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Extintos.Model
{
    public class TabuleiroLayout
    {
        public int TabTamanho { get; } = 600;
        public int MaoLargura { get; } = 450;
        public int MaoAltura { get; } = 400;

        public int TabX { get; private set; }
        public int TabY { get; private set; }
        public int MaoX { get; private set; }
        public int MaoY { get; private set; }

        public Dictionary<string, List<Point>> PosicoesCercados { get; private set; } = new Dictionary<string, List<Point>>();

        public void Atualizar(int clientWidth, int clientHeight)
        {
            TabX = (clientWidth - TabTamanho) / 2;
            TabY = (clientHeight - TabTamanho) / 2;
            
            MaoX = (clientWidth - MaoLargura) / 16;
            MaoY = clientHeight - MaoAltura;

            // Chama o seu método original
            InicializarPosicoesCercados();
        }

        public Dictionary<string, Rectangle> ObterCercadosMapeados()
        {
            return new Dictionary<string, Rectangle>
            {
                { "FI", new Rectangle(TabX + 30, TabY + 30, 200, 135) },
                { "MT", new Rectangle(TabX + 30, TabY + 225, 145, 135) },
                { "PA", new Rectangle(TabX + 60, TabY + 410, 155, 160) },
                { "RS", new Rectangle(TabX + 400, TabY + 50, 60, 60) },
                { "CD", new Rectangle(TabX + 367, TabY + 230, 197, 150) },
                { "IS", new Rectangle(TabX + 470, TabY + 415, 60, 60) },
                { "RI", new Rectangle(TabX + 270, TabY + 445, 130, 150) }
            };
        }


        public Point ObterPosicaoLivre(string cercado, List<DinoNoTabuleiro> dinosFixosNoTabuleiro)
        {
            if (!PosicoesCercados.ContainsKey(cercado))
                return Point.Empty;


            var ocupados = dinosFixosNoTabuleiro.Count(d => d.Cercado == cercado);
            var lista = PosicoesCercados[cercado];

            if (ocupados >= lista.Count)
                ocupados = lista.Count - 1;

            return lista[ocupados];
        }
        
        public void InicializarPosicoesCercados()
        {
            PosicoesCercados.Clear();
            
            PosicoesCercados["FI"] = new List<Point>
            {
                new(TabX + 35, TabY + 80),
                new(TabX + 65, TabY + 80),
                new(TabX + 100, TabY + 80),
                new(TabX + 135, TabY + 80),
                new(TabX + 170, TabY + 80),
                new(TabX + 200, TabY + 80)
            };

            PosicoesCercados["MT"] = new List<Point>
            {
                new(TabX + 65, TabY + 270),
                new(TabX + 95, TabY + 270),
                new(TabX + 125, TabY + 270)
            };

            PosicoesCercados["PA"] = new List<Point>
            {
                new(TabX + 110, TabY + 430),
                new(TabX + 140, TabY + 430),
                new(TabX + 110, TabY + 460),
                new(TabX + 140, TabY + 460),
                new(TabX + 110, TabY + 490),
                new(TabX + 140, TabY + 490)
            };

            PosicoesCercados["RS"] = new List<Point>
            {
                new(TabX + 420, TabY + 70)
            };

            PosicoesCercados["CD"] = new List<Point>
            {
                new(TabX + 368, TabY + 310),
                new(TabX + 398, TabY + 310),
                new(TabX + 433, TabY + 310),
                new(TabX + 468, TabY + 310),
                new(TabX + 498, TabY + 310),
                new(TabX + 535, TabY + 310)
            };

            PosicoesCercados["RI"] = new List<Point>
            {
                new(TabX + 290, TabY + 465),
                new(TabX + 325, TabY + 465),
                new(TabX + 360, TabY + 465),
                new(TabX + 290, TabY + 495),
                new(TabX + 325, TabY + 495),
                new(TabX + 360, TabY + 495),
                new(TabX + 290, TabY + 525),
                new(TabX + 325, TabY + 525),
                new(TabX + 360, TabY + 525),
                new(TabX + 290, TabY + 555),
                new(TabX + 325, TabY + 555),
                new(TabX + 360, TabY + 555)
            };

            PosicoesCercados["IS"] = new List<Point>
            {
                new(TabX + 490, TabY + 435)
            };
        }
    }
}