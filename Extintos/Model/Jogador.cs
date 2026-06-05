using System;
using System.Collections.Generic;
using Draft;
using Extintos.Enumeration;
using Extintos.Auxiliares;

namespace Extintos.Model
{
    public class Jogador
    {
        public List<AuxCercado> _meusCercados;

        public int IdJogador { get; set; }

        public string NomeJogador { get; set; }

        public string Senha { get; set; }

        public int Pontuacao { get; set; }


        public int idPartida { get; set; }

        //carregador lazy, carrega uma lista inicial de cercados por causa do erro do caralho q eu n entendi ate agr
        public List<AuxCercado> meusCercados
        {
            get => _meusCercados ??= CercadosExtension.CercadoAuxLista();
            set => _meusCercados = value;
        }

        public static Jogador EntrarNaPartida(int idPartida, string nomeJogador, string senhaJogador)
        {
            var retornoEntrar = Jogo.Entrar(idPartida, nomeJogador, senhaJogador);

         
            if (retornoEntrar.StartsWith("ERRO"))
            {
                throw new Exception($"Servidor recusou a entrada: {retornoEntrar}");
            }

            // 2. Verifica se a resposta está vazia ou nula
            if (string.IsNullOrWhiteSpace(retornoEntrar))
            {
                throw new Exception("Servidor retornou uma resposta vazia ao tentar entrar na partida.");
            }

            var dadosJogador = retornoEntrar.Split(',');

            // 3. Verifica se o formato tem o esperado (ID e Senha)
            if (dadosJogador.Length < 2)
            {
                throw new Exception($"Formato de resposta inesperado do servidor: '{retornoEntrar}'");
            }

            var jogador = new Jogador();

            // 4. Conversão segura para evitar o FormatException
            if (int.TryParse(dadosJogador[0], out int idConvertido))
            {
                jogador.IdJogador = idConvertido;
            }
            else
            {
                throw new Exception($"O valor retornado pelo servidor para o ID não é um número válido: '{dadosJogador[0]}'");
            }

            jogador.Senha = dadosJogador[1];
            jogador.NomeJogador = nomeJogador;
            jogador.Pontuacao = 0;
            jogador.idPartida = idPartida;
            jogador.meusCercados = CercadosExtension.CercadoAuxLista();

            return jogador;
        }
        public static string BuscaPeloId(int idJogador, int idPartida)
        {
            var jogadores = Partida.ListarJogadores(idPartida);
            var jogadorEncontrado = jogadores.Find(j => j.IdJogador == idJogador);

            if (jogadorEncontrado == null)
                return null;

            return jogadorEncontrado.NomeJogador;
        }

        public static string BuscaPeloId(int idJogador)
        {
            var todasPartidas = Partida.ListarPartidas('T');

            foreach (var partida in todasPartidas)
            {
                var jogadores = Partida.ListarJogadores(partida.IdPartida);
                var jogadorEncontrado = jogadores.Find(j => j.IdJogador == idJogador);

                if (jogadorEncontrado != null)
                    return jogadorEncontrado.NomeJogador;
            }

            return null;
        }

        public void ColocarDinossauro(Dinossauro dino, Cercados cerca)
        {
            var cercado = meusCercados.Find(c => c.Cercados.Equals(cerca));
            cercado.Dinossauros.Add(dino);
        }
    }
}