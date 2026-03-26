using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Draft;

namespace Extintos
{
    internal class Partida
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public char Status { get; set; }
        public List<Jogador> jogadores { get; set; }

        public static List<Partida> ListarPartidas(string status)
        {
            string retorno = Jogo.ListarPartidas(status);
            retorno = retorno.Replace("\r", "");
            retorno = retorno.Substring(0, retorno.Length - 1);
            string[] retornoPartidas = retorno.Split('\n');
            List<Partida> listaPartidas = new List<Partida>();

            for (int i = 0; i < retornoPartidas.Length; i++)
            {
                string partida = retornoPartidas[i];
                string[] dados = partida.Split(',');

                Partida p = new Partida();
                p.Id = Convert.ToInt32(dados[0]);
                p.Nome = dados[1];
                p.Data = Convert.ToDateTime(dados[2]);
                p.Status = Convert.ToChar(dados[3]);
                listaPartidas.Add(p);
            }

            return listaPartidas;
        }

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

       /* public static string IniciarPartida(int idJogador, string senhaJogador)
        {
      
            string retornoEntrar = Jogo.Iniciar(idJogador, senhaJogador);

           //deixar as mensagens numa enum ou na utils ?

             int primeiroJogar =Convert.ToInt32(retornoEntrar.Substring(0, retornoEntrar.IndexOf(',')));

            string mensagemInicio = "O Jogador: " + Jogador.BuscaPeloId(idJogador) + "Iniciou a partida!\n O primeiro a jogar é o : " + Jogador.BuscaPeloId(primeiroJogar) + ;

            lblFaceDado.Text = retornoEntrar.Substring(retornoEntrar.IndexOf(',') + 1);
        }*/

      
    }
}