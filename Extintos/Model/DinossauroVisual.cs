using System.Drawing;

namespace Extintos
{
    public class DinossauroVisual
    {
        public int altura;
        public Rectangle area;
        public bool ativo;
        public Image imagem;
        public int largura;

        public Point posicao;

        public DinossauroVisual(Image img, int x, int y)
        {
            imagem = img;
            posicao = new Point(x, y);
            largura = img.Width;
            altura = img.Height;
            ativo = false;
        }

        public string Tipo { get; set; }
    }
}