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
                var estado = PartidasInfo.PartidaInfo.Obter(idPartida); // atribuir resposabilidade pro draftserver
                StatusPartida = estado.StatusPartida;
                StatusTurno = estado.StatusTurno;
                DadoAtual = estado.FaceDadoAtual;
                JogueioDado = estado.IdJogadorDaVez == idJogador;
                NumeroTurno = estado.TurnoAtual;
                MaoJogador = DraftService.ObterMao(idJogador, senhaJogador);
                CercadosJogador = jogador.MeusCercados;
                IdJogadorQueRolouDado = idJogador;
                QuantidadeJogadores = Partida.QuantidadeJogadores(idPartida);
                IdPartida = idPartida;
                MeuId = idJogador;
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
        public int QuantidadeJogadores { get; set; }
        public int IdPartida { get; set; }
        public int MeuId { get; set; }

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