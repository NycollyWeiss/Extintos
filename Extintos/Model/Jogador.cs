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

        public static Jogador EntrarNaPartida(int idPartida, string nomeJogador, string senhaPartida)

        {
            var retornoEntrar = Jogo.Entrar(idPartida, nomeJogador, senhaPartida);
            var dadosJogador = retornoEntrar.Split(',');

            var jogador = new Jogador();

            jogador.IdJogador = Convert.ToInt32(dadosJogador[0]);
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