using System.Linq;
using Extintos.Enumeration;

namespace Extintos.Model
{
    /*           -----Validador das jogadas da estratégia.-----
 
    Explicação do arquivo:
    Recebe o estado atual do turno, o cercado escolhido e o dinossauro escolhido,
    verifica se a jogada respeita as regras e restrições do jogo
    e informa para a estratégia se essa opção pode ser considerada.

    Este arquivo não escolhe a melhor jogada e não calcula score.
    Ele apenas aprova ou descarta uma possibilidade antes da avaliação da estratégia.

    */

    internal static class EstrategiaValidator
    {
        public static bool JogadaValidator(
            InformacoesTurno info,
            Cercados cercado,
            Dinossauro dino)
        {
            if (info == null)
                return false;

            var possuiDino = info.MaoJogador.Any(x => x.Dinossauro == dino && x.QuantidadeDinossauros > 0);

            if (!possuiDino)
                return false;

            var cercadoAtual = info.CercadosJogador.FirstOrDefault(x => x.Cercados == cercado);

            if (cercadoAtual == null)
                return false;

            if (!ValidarDado(info, cercado, cercadoAtual))
                return false;

            return cercado.SePodeColocarNoCercado(cercadoAtual.Dinossauros,dino);
        }

        private static bool ValidarDado(InformacoesTurno info,Cercados cercado,AuxCercado cercadoAtual)
        {
            // Quem rolou o dado ignora a restrição
            if (info.JogueioDado)
                return true;

            switch (info.DadoAtual)
            {
                case Dado.VZ:

                    return !cercadoAtual.Dinossauros.Any();

                case Dado.TI:

                    return !cercadoAtual.Dinossauros
                        .Contains(Dinossauro.TI);

                default:

                    return info.DadoAtual.ValidaCercados().Contains(cercado);
            }
        }
    }
}