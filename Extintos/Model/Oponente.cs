using Draft;
using Extintos.Auxiliares;
using Extintos.Services;
using System.Collections.Generic;

namespace Extintos.Model
{
    public class Oponente : Jogador
    {
        
//cercados preenchidos, id, pontuaçao atual, pontuaçao pontencial, dinos de preferencia(por cercado), quantidade de t-rex
        public Oponente(
            List<AuxCercado> meusCercados,
            int idJogador,
            string nomeJogador,
            int pontuacao,
            int idPartida,
            Jogador jogadorQueVaiPassarMao,
            Jogador jogadorQueVaiReceberSuaMao)
            : base(meusCercados, idJogador, nomeJogador, pontuacao, idPartida, jogadorQueVaiPassarMao, jogadorQueVaiReceberSuaMao)
        {
        }
//


        // public static int PontuacaoAtual();
        

        public static List<AuxDinossauro> ObterDinosOponentesAteTurno(int idPartida, int meuId, int turnoSelecionado)
        {
            var resultado = new List<AuxDinossauro>();

            for (int turno = 1; turno <= turnoSelecionado; turno++)
            {
                var retorno = Jogo.VerificarTurno(idPartida, turno);

                resultado.AddRange(DadosOponete.ParserDinosOponente(retorno, meuId));
            }

            AuxUniverso.ConsolidarDino(resultado);

            return resultado;
        }

        //consulta detalhadamente as jogadas realizadas pelos oponentes ate n turno
        public static List<AuxJogadaOponente> ObterJogadasOponentesAteTurno( int idPartida, int meuId, int turnoSelecionado)
        {
            var resultado = new List<AuxJogadaOponente>();

            for (int turno = 1; turno <= turnoSelecionado; turno++)
            {
                var retorno = Jogo.VerificarTurno(idPartida, turno);

                resultado.AddRange(DadosOponete.ParserJogadaOponente(retorno, meuId, turno));
            }

            return resultado;
        }
    }
}