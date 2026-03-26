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

            string retorno = Jogo.ListarPartidas(status); //fazer validações pra garantir que só vai entrar um caracter só
            retorno = retorno.Replace("\r", "");
            retorno = retorno.Substring(0, retorno.Length - 1);
            string[] retornoPartidas = retorno.Split('\n');

            List<Partida> listaPartidas = new List<Partida>();

            for (int i = 0; i < retornoPartidas.Length; i++)
            {
                string partida = retornoPartidas[i];

                string[] dados = partida.Split(','); // [0] = id da partida, [1] = nome da partida, [2] =
                
                Partida p = new Partida();
                p.Id = Convert.ToInt32(dados[0]);
                p.Nome = dados[1];
                p.Data = Convert.ToDateTime(dados[2]);
                p.Status = Convert.ToChar(dados[3]);

                listaPartidas.Add(p);

            }

            return listaPartidas;

        }


        public static 
    

    

    }
}
