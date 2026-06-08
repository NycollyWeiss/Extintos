using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Draft;
using Extintos.Enumeration;
using Extintos.Auxiliares;
using Extintos.Services;

namespace Extintos.Model
{
    public class Jogador
    {
        private List<AuxCercado> _meusCercados;

        public int IdJogador { get; set; }
        public string NomeJogador { get; set; }
        
        public string Senha { get; private set; }
        
        public int Pontuacao { get; set; } 
        public int IdPartida { get; set; }
        
        public Jogador JogadorQueVaiPassarMao { get; set; }
        public Jogador JogadorQueVaiReceberSuaMao { get; set; }

        public List<AuxCercado> MeusCercados
        { 
            get => _meusCercados ??= CercadosExtension.CercadoAuxLista();
            private set => _meusCercados = value; // Setter privado para resolver o aviso do IDE
        }

        public Jogador(List<AuxCercado> meusCercados, int idJogador, string nomeJogador,
            int pontuacao,
            int idPartida,
            Jogador jogadorQueVaiPassarMao,
            Jogador jogadorQueVaiReceberSuaMao)
        {
            _meusCercados = meusCercados;
            IdJogador = idJogador;
            NomeJogador = nomeJogador;
            Pontuacao = pontuacao;
            IdPartida = idPartida;
            JogadorQueVaiPassarMao = jogadorQueVaiPassarMao;
            JogadorQueVaiReceberSuaMao = jogadorQueVaiReceberSuaMao;
        }

       
        public Jogador()
        {
        }
        
        public static Jogador EntrarNaPartida(int idPartida, string nomeJogador, string senhaJogador)
        {
            var retornoEntrar = DraftService.EntrarPartida(idPartida, nomeJogador, senhaJogador);

            if (retornoEntrar.StartsWith("ERRO"))
            {
                throw new Exception($"Servidor recusou a entrada: {retornoEntrar}");
            }

            if (string.IsNullOrWhiteSpace(retornoEntrar))
            {
                throw new Exception("Servidor retornou uma resposta vazia ao tentar entrar na partida.");
            }

            var dadosJogador = retornoEntrar.Split(',');

            if (dadosJogador.Length < 2)
            {
                throw new Exception($"Formato de resposta inesperado do servidor: '{retornoEntrar}'");
            }

            if (!int.TryParse(dadosJogador[0], out int idConvertido))
            {
                throw new Exception($"O valor retornado pelo servidor para o ID não é um número válido: '{dadosJogador[0]}'");
            }

            return new Jogador
            {
                IdJogador = idConvertido,
                Senha = dadosJogador[1],
                NomeJogador = nomeJogador,
                Pontuacao = 0,
                IdPartida = idPartida,
                MeusCercados = CercadosExtension.CercadoAuxLista()
            };
        }

        public static string BuscaPeloId(int idJogador, int idPartida)
        {
            var jogadores = DraftService.PegaJogadoresAsync(idPartida, CancellationToken.None).GetAwaiter().GetResult();
            
            
            var jogadorEncontrado = jogadores?.FirstOrDefault(j => j.IdJogador == idJogador);

            return jogadorEncontrado?.NomeJogador;
        }
        

        public void ColocarDinossauro(Dinossauro dino, Cercados cerca)
        {
            var cercado = MeusCercados.FirstOrDefault(c => c.Cercados.Equals(cerca));
            
            if (cercado != null)
            {
                var dinoExistente = cercado.Dinossauros.FirstOrDefault(d => d.Dino == dino);

                if (dinoExistente != null)
                {
                    dinoExistente.QuantidadeDinossauros++;
                }
                else
                {
                    cercado.Dinossauros.Add(new AuxDinossauro(dino, 1));
                }
            }
        }
    }
}