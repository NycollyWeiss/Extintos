using System;
using Draft;
using Extintos.Auxiliares;
using Extintos.Enumeration;

namespace Extintos.Model
{
    public class PartidasInfo
    {
        public readonly struct PartidaInfo
        {
            public bool EmAndamento => StatusPartida == 'J';
            public bool Encerrada => StatusPartida == 'E';
            public int TurnoAtual { get; }
            public bool TurnoEmAndamento => StatusTurno == 'A';
            public bool TurnoFinalizado => StatusTurno == 'F';
            public int IdJogadorDaVez { get; }
            public Dado FaceDadoAtual { get; }


            public char StatusPartida { get; }
            public char StatusTurno { get; }


            public PartidaInfo(string retornoBruto)
            {
                var dados = retornoBruto.Split(',');
                if (dados.Length != 5)
                    throw new FormatException($"Formato inválido: {retornoBruto}");

                StatusTurno = Convert.ToChar(dados[0]);
                TurnoAtual = Convert.ToInt32(dados[1]);
                StatusPartida = Convert.ToChar(dados[2]);
                IdJogadorDaVez = Convert.ToInt32(dados[3]);
                FaceDadoAtual = (Dado)Enum.Parse(typeof(Dado), dados[4].Trim().ToUpper());
            }

            public static PartidaInfo Obter(int idPartida)
            {
                var retorno = Jogo.VerificarPartida(idPartida);
                return new PartidaInfo(retorno);
            }


            public string ExibicaoDadosPartida()
            {
                return $"Status: {(EmAndamento ? "Jogando" : "Encerrada")} | " +
                       $"Turno: {TurnoAtual}/12 | " +
                       $"Vez: Jogador #{IdJogadorDaVez} | " +
                       $"Dado: {FaceDadoAtual.PegaNome()}";
            }
        }

        //como usar
        //var info = PartidaInfo.Obter(_dadosJogador.idPartida);
    }
}