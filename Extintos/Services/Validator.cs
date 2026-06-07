using Extintos.Auxiliares;
using Extintos.Enumeration;
using System;
using System.Linq;

namespace Extintos.Model
{
    internal static class Validator
    {
        public static bool JogadaValidator(
            InformacoesTurno info,
            Cercados cercado,
            Dinossauro dino)
        {
            Console.WriteLine($"Testando {dino} em {cercado}");

            if (info == null)
                return false;

            var possuiDino = info.MaoJogador.Any(x => x.Dinossauro == dino &&
                                                      x.QuantidadeDinossauros > 0);

            if (!possuiDino)
                return false;

            var cercadoAtual = info.CercadosJogador
                .FirstOrDefault(x => x.Cercados == cercado);

            if (cercadoAtual == null)
            {
                Console.WriteLine("Falhou: cercadoAtual null");
                return false;
            }

            if (!ValidarDado(info, cercado, cercadoAtual))
            {
                Console.WriteLine("Falhou: ValidarDado");

                return false;
            }

            return cercado.SePodeColocarNoCercado(
                cercadoAtual.Dinossauros,
                dino);
        }

      
        private static bool ValidarDado(
            InformacoesTurno info,
            Cercados cercado,
            AuxCercado cercadoAtual)
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

                    return info.DadoAtual
                        .ValidaCercados()
                        .Contains(cercado);
            }
        }
    }
}