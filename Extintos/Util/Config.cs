using System.Drawing;
using System.Windows.Forms;

namespace Extintos
{
    internal class Config
    {
        public static void Fullscreen(Form form) //método para configurar o formulário em tela cheia
        {
            if (form == null) return;

            form.FormBorderStyle =
                FormBorderStyle.Sizable; // Configura o estilo da borda do formulário para ser redimensionável
            form.Size = new Size(Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height); // Configura o tamanho do formulário para ocupar toda a tela
            form.WindowState =
                FormWindowState
                    .Maximized; // Configura o estado da janela para maximizado, garantindo que o formulário seja exibido em tela cheia
            form.Show();
        }


        //public List<String> jogadores ()
    }
}