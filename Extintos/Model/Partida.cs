using Draft;
using Extintos.Enumeration;
using Extintos.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Extintos.Enumeration.DadoFace;

namespace Extintos
{
    internal class Partida
    {
        public int IdPartida { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public char Status { get; set; }
        public int Turno { get; set; }
        public List<Jogador> Jogadores { get; set; }

        //  public List<Turno> Turnos { get; set; }

        public static List<Partida> ListarPartidas(char Status)
        {
            string status = Convert.ToString(Status);
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
                p.IdPartida = Convert.ToInt32(dados[0]);
                p.Nome = dados[1];
                p.Data = Convert.ToDateTime(dados[2]);
                p.Status = Convert.ToChar(dados[3]);
                p.Jogadores = Partida.ListarJogadores(p.IdPartida);
                listaPartidas.Add(p);
            }

            return listaPartidas;
        }

        public static List<Jogador> ListarJogadores(int Id)
        {
            string retorno = Jogo.ListarJogadores(Id);
            retorno = retorno.Replace("\r", "");
            retorno = retorno.Substring(0, retorno.Length - 1);
            string[] retornoJogadores = retorno.Split('\n');
            List<Jogador> listaJogadores = new List<Jogador>();
            for (int i = 0; i < retornoJogadores.Length; i++)
            {
                string jogador = retornoJogadores[i];
                string[] dados = jogador.Split(',');
                Jogador j = new Jogador();
                j.IdJogador = Convert.ToInt32(dados[0]);
                j.NomeJogador = dados[1];
                j.Pontuacao = Convert.ToInt32(dados[2]);
                listaJogadores.Add(j);
            }

            return listaJogadores;
        }


        public static Partida BuscaPeloId(char Status, int IdPartida)
        {
            List<Partida> partidas = Partida.ListarPartidas(Status);
            Partida partidaEncontrada = partidas.Find(p => p.IdPartida == IdPartida);
            return partidaEncontrada;
        }

        public static (char statusPartida, int numeroTurno, char statusTurno, int idJogador, string faceDado)
            VerificarPartida(int idPartida)
        {

            string retorno = Jogo.VerificarPartida(idPartida);
            string[] dados = retorno.Split(',');

            return (
                Convert.ToChar(dados[0]), // statusPartida se é J ou E
                Convert.ToInt32(dados[1]), // numeroTurn 1-12
                Convert.ToChar(dados[2]), // statusTurno se tá A ou F
                Convert.ToInt32(dados[3]), // idJogador
                dados[4] // faceDado AL, FL, PR e tal
            );
        }

        public static string IniciarPartida(int idJogador, string senhaJogador)

        {
            string retornoEntrar = Jogo.Iniciar(idJogador, senhaJogador);
            int idPartida = Convert.ToInt32(retornoEntrar.Substring(0, retornoEntrar.IndexOf(',')));

            var verificacao = Partida.VerificarPartida(idPartida);

            DadoFace.Dado dadoAtual = (DadoFace.Dado)Enum.Parse(typeof(DadoFace.Dado), verificacao.faceDado);
            //boto um trim ? talvez 

            string mensagemInicio = $"O Jogador: {Jogador.BuscaPeloId(idJogador)} iniciou a partida!\n" +
                                    $"O primeiro a jogar é: {Jogador.BuscaPeloId(verificacao.idJogador)}\n" +
                                    $"Turno: {verificacao.numeroTurno}\n" +
                                    $"Face do Dado: {dadoAtual.PegaNome()}\n";


            return mensagemInicio;
        }

    }
}

