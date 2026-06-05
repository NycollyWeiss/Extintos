using Extintos.LeonKennedy;
using System;
using System.Windows.Forms;

namespace Extintos
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            //Draftosaurus
            Application.Run(Forms.TelaInicial);
            
            //Teste Leon
            //Application.Run(new FormResultadoTeste());
            
            /* Para mudar a quantidade de jogadores:
               ResultadoTeste.cs -->  Procurar por "PartidaSimulada.ExecutarPartidaJogadores(Log);"
           --> Alterar o valor presente no ExecutarPartidaJogadores;
             */
        }
    }
}