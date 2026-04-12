using Draft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extintos.Enumeration;

namespace Extintos.Model
{
    internal class Jogador

    {
        public int IdJogador { get; set; }

        public string NomeJogador { get; set; } // limitar 20 caracteres 

        public string Senha { get; set; }

        public int Pontuacao { get; set; }

        public int idPartida { get; set; }

        public List<AuxCercado> meusCercados { get; set; } = CercadosExtension.CercadoAuxLista();


        public static Jogador EntrarNaPartida(int idPartida, string nomeJogador, string senhaPartida)
        {
            
            string retornoEntrar = Jogo.Entrar(idPartida, nomeJogador, senhaPartida);
            string[] dadosJogador = retornoEntrar.Split(',');
                
            Jogador jogador = new Jogador();

            jogador.IdJogador = Convert.ToInt32(dadosJogador[0]);
            jogador.Senha = dadosJogador[1];
            jogador.NomeJogador = nomeJogador;
            jogador.Pontuacao = 0;
            jogador.idPartida = idPartida;

            return jogador;
        }

        // busca com o id da partida (mais eficiente, uma chamada só)
        public static string BuscaPeloId(int idJogador, int idPartida)
        {
            List<Jogador> jogadores = Partida.ListarJogadores(idPartida);
            Jogador jogadorEncontrado = jogadores.Find(j => j.IdJogador == idJogador);

            if (jogadorEncontrado == null)
                return null;

            return jogadorEncontrado.NomeJogador;
        }

        // busca sem o id da partida (percorre todas as partidas)
        public static string BuscaPeloId(int idJogador)
        {
            List<Partida> todasPartidas = Partida.ListarPartidas('T');

            foreach (Partida partida in todasPartidas)
            {
                List<Jogador> jogadores = Partida.ListarJogadores(partida.IdPartida);
                Jogador jogadorEncontrado = jogadores.Find(j => j.IdJogador == idJogador);

                if (jogadorEncontrado != null)
                    return jogadorEncontrado.NomeJogador;
            }

            return null;
        }

        public void ColocarDinossauro(Dinossauro dino, Cercados cerca)
        {
            AuxCercado cercado = meusCercados.Find(c => c.Cercados.Equals(cerca));
            cercado.Dinossaurios.Add(dino);
        }
        
    }
}
