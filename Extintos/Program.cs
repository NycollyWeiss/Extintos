using System;
using System.Windows.Forms;
using Extintos.LeonKennedy;
using Extintos.Services;

namespace Extintos
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            try
            {
                MusicaService.Iniciar();
                Application.Run(Forms.TelaInicial);
            }
            finally
            {
                MusicaService.Parar();
            }

            //Application.Run(new FormResultadoTeste());
        }
    }
}