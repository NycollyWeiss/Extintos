using System;
using System.Collections.Generic;
using System.Threading;
using Draft;
using Extintos.Auxiliares;
using Extintos.Enumeration;
using Extintos.Model;
using Extintos.Services;

namespace Extintos
{
    internal class Partida
    {
        //DEFINIÇÕES DA CLASSE E PRIORIDADES 
        public int IdPartida { get; set; }
        public string Nome { get; set; }
        public DateTime Data { get; set; }

        public char Status { get; set; }

        // public int Turno { get; set; }
        public List<Jogador> Jogadores { get; set; }


        //  public List<Turno> Turnos { get; set; }

        //LÓGICA DO DATA ACCESS
        //O fluxo é: Camada de Interface (UI) -> Classe Partida (Model) -> Classe Jogo (Service)
        public static List<Partida> ListarPartidas(char Status)
        {
            var status = Convert.ToString(Status);
            var retorno = Jogo.ListarPartidas(status);
            retorno = retorno.Replace("\r", "");
            retorno = retorno.Substring(0, retorno.Length - 1);
            var retornoPartidas = retorno.Split('\n');
            var listaPartidas = new List<Partida>();
            for (var i = 0; i < retornoPartidas.Length; i++)
            {
                var partida = retornoPartidas[i];
                var dados = partida.Split(',');
                var p = new Partida();
                p.IdPartida = Convert.ToInt32(dados[0]);
                p.Nome = dados[1];
                p.Data = Convert.ToDateTime(dados[2]);
                p.Status = Convert.ToChar(dados[3]);
                //p.Jogadores = Partida.ListarJogadores(p.IdPartida);
                listaPartidas.Add(p);
            }

            return listaPartidas;
        }

        
        public static Partida BuscaPeloId(char Status, int IdPartida)
        {
            var partidas = DraftService.ObterEstadoAsync(IdPartida, new CancellationToken());
            var partidaEncontrada = partidas.Find(p => p.IdPartida == IdPartida);
            return partidaEncontrada;
        }

// metodo tipo tupla, tudo que ta nos ( ) é os tipos de retorno e o verificaPartida é o nome e o parametro
//putaria de c# em java é mais bonito, negocio feiao
//é uma versao melhor do metodo do claro
        public static (char statusPartida, int numeroTurno, char statusTurno, int idJogador, string faceDado)
            VerificaPartida(int idPartida)
        {
            var retorno = Jogo.VerificarPartida(idPartida);

            // Se o servidor retornar ERRO, lançamos uma exceção clara para o log
            if (retorno.StartsWith("ERRO"))
            {
                throw new Exception(retorno);
            }

            var dados = retorno.Split(',');
            
            // Garantia de que temos dados suficientes
            if (dados.Length < 5) throw new Exception("Formato de retorno inválido: " + retorno);

            return (
                Convert.ToChar(dados[0]), // statusPartida
                Convert.ToInt32(dados[1]), // numeroTurno
                Convert.ToChar(dados[2]), // statusTurno
                Convert.ToInt32(dados[3]), // idJogador
                dados[4]                  // faceDado
            );
        }


        public static string IniciarPartida(int idJogador, string senhaJogador, int idPartida)
          
        {
            var retornoEntrar = Jogo.Iniciar(idJogador, senhaJogador);
            var verificacao = VerificaPartida(idPartida);

            // usei var pq ela faz uma tupla e guarda tipos diferentes de dados


            var dadoAtual = (Dado)Enum.Parse(typeof(Dado), verificacao.faceDado);

            //Boto um trim ? talvez 

            var mensagemInicio = $"O Jogador: {Jogador.BuscaPeloId(idJogador, idPartida)} iniciou a partida!\n" +
                                 $"Jogador com o dado: {Jogador.BuscaPeloId(verificacao.idJogador, idPartida)}\n" +
                                 $"Turno: {verificacao.numeroTurno}\n" +
                                 $"Face do Dado: {dadoAtual.PegaNome()}\n"; //aqui ta a dor de cabeça


            return mensagemInicio;
        }
    }
}