using System;
using System.Windows.Forms;
using Extintos.LeonKennedy;

namespace Extintos
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.Run(Forms.TelaInicial);

            //Application.Run(new FormResultadoTeste());
        }
    }
}