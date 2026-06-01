using System;
using System.Collections.Generic;
using Draft;
using Extintos.Model;
using Extintos.Services;
using Extintos.Auxiliares;

namespace Extintos.Enumeration
{
    public class InformacoesTurno
    {
        public InformacoesTurno(int idJogador, int idPartida, string senhaJogador, Jogador jogador)
        {
            try
            {
                var estado = Partida.VerificaPartida(idPartida); // atribuir resposabilidade pro draftserver
                StatusPartida = estado.statusPartida;
                StatusTurno = estado.statusTurno;
                DadoAtual = (Dado)Enum.Parse(typeof(Dado), estado.faceDado); //criar funçao de parse de tipos
                JogueioDado = estado.idJogador == idJogador;
                NumeroTurno = estado.numeroTurno;
                var raw = Jogo.ExibirMao(idJogador, senhaJogador); // atribuir resposabilidade pro draftserver
                MaoJogador = ParserMao.Parse(raw);
                CercadosJogador = jogador.meusCercados;
                IdJogadorQueRolouDado = idJogador;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao montar DecisoesTurno: {ex.Message}");
            }
        }

        public InformacoesTurno()
        {
        }

        public List<AuxDinossauro> MaoJogador { get; set; } = new();
        public List<AuxCercado> CercadosJogador { get; set; } = new();
        public Dado DadoAtual { get; set; }
        public int NumeroTurno { get; set; }
        public bool JogueioDado { get; set; }
        public char StatusPartida { get; set; }
        public char StatusTurno { get; set; }

        public int IdJogadorQueRolouDado { get; set; }

        public static InformacoesTurno CriarOffline(
            List<AuxDinossauro> mao,
            List<AuxCercado> cercados,
            Dado dado,
            int turno)
        {
            return new InformacoesTurno
            {
                MaoJogador = mao,
                CercadosJogador = cercados,
                DadoAtual = dado,
                NumeroTurno = turno,
                StatusPartida = 'E',
                StatusTurno = 'A',
                JogueioDado = true
            };
        }
    }
}