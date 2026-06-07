using Draft;
using Extintos.Auxiliares;
using Extintos.Services;
using System;
using System.Collections.Generic;

namespace Extintos.Model
{
    public class Oponente
    {

        //consulta o universo conhecido do oponente ate n turno
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
            Console.WriteLine(
         $"Entrou em ObterJogadasOponentesAteTurno. Turno selecionado = {turnoSelecionado}");

            var resultado = new List<AuxJogadaOponente>();

            for (int turno = 1; turno <= turnoSelecionado; turno++)
            {
                var retorno = Jogo.VerificarTurno(idPartida, turno);


                Console.WriteLine("================================");
                Console.WriteLine($"Turno consultado: {turno}");
                Console.WriteLine(retorno);

                resultado.AddRange(DadosOponete.ParserJogadaOponente(retorno, meuId, turno));
            }

            Console.WriteLine(
        $"Total de jogadas encontradas: {resultado.Count}");

            return resultado;
        }
    }
}
