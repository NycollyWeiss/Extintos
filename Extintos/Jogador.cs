using Draft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extintos
{
    internal class Jogador

    {
        public int Id { get; set; }

        public string NomeJogador { get; set; } // limitar 20 caracteres 

        public string Senha { get; set; }

        public int Pontuacao { get; set; }


        public static List<Jogador> ListarJogadores(int idPartida)
        {

            string retorno = Jogo.ListarJogadores(idPartida); 
            retorno = retorno.Replace("\r", "");
            retorno = retorno.Substring(0, retorno.Length - 1);
            string[] retornoJogadores = retorno.Split('\n');

            List<Jogador> listaJogadores = new List<Jogador>();

            for (int i = 0; i < retornoJogadores.Length; i++)
            {
                string jogador = retornoJogadores[i];

                string[] dados = jogador.Split(',');

                Jogador j = new Jogador();
                j.Id = Convert.ToInt32(dados[0]);
                j.NomeJogador = dados[1];
                j.Pontuacao = Convert.ToInt32(dados[2]);


                listaJogadores.Add(j);

            }

            return listaJogadores;

        }

        public static Jogador EntrarNaPartida(int idPartida, string nomeJogador, string senhaPartida)
        {
            
            string retornoEntrar = Jogo.Entrar(idPartida, nomeJogador, senhaPartida);
            string[] dadosJogador = retornoEntrar.Split(',');
                
            Jogador jogador = new Jogador();
            jogador.Id = Convert.ToInt32(dadosJogador[0]);
            jogador.Senha = dadosJogador[1];
            jogador.NomeJogador = nomeJogador;
            jogador.Pontuacao = 0;

            return jogador;
        }

//        string jogadores = Jogo.ListarJogadores(idPartida);
//        string[] ativos = jogadores.Split(',');
//            for (int i = 0; i<ativos.Length; i++)
//            {
//                if (nomeJogador.Equals(ativos[i]))
//                {
//                    MessageBox.Show("Jogador já existente!! Digite outro nome\n\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    txtNomeJogador.Clear();
//                    nomeJogador = txtNomeJogador.Text;
//                    return;
//                }
//}

    }
}
