using Extintos.Enumeration;

namespace Extintos.Model
{
    public class Tabuleiro
    {
        public static int ContaDinosNoTabuleiro(InformacoesTurno info, Dinossauro especie)
        {
            int quantidade = 0;

            foreach (var cercado in info.CercadosJogador)
            {
                foreach (var dino in cercado.Dinossauros)
                {
                    if (dino == especie)
                    {
                        quantidade++;
                    }
                }
            }

            return quantidade;
        }

        public static bool DinoViraReiDaSelva(InformacoesTurno info, Dinossauro especie, int quantidadeJogadores)
        {
            if (quantidadeJogadores == 2)
            {
                return ContaDinosNoTabuleiro(info, especie) >= 2;
            }

            return ContaDinosNoTabuleiro(info, especie) >= 3;
        }

        //se tal dino receber true, da p colocar o peso dele pro cercado RS no maximo
    }
}