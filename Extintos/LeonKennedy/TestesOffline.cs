using System;
using System.Collections.Generic;
using System.Linq;
using Extintos.Enumeration;
using Extintos.Model;

namespace Extintos.LeonKennedy
{
    public static class TestRunnerOffline
    {
        private static readonly Random rng = new();
        private static int QuantasVezesComeu;

        public static void ExecutarTodos(Action<string> log)
        {
            log("🫦🫦🫦🫦🫦 Iniciando validação do Guloso...🫦🫦🫦🫦🫦🫦");
            log("========================================");

            for (var i = 0; i < 50; i++)
            {
                var info = CriarEstadoFake();
                var estrategia = new EstrategiaGulosa();

                var jogada = estrategia.Avaliar(info);

                log($"\n[TESTE {i + 1}]");
                LogEstado(info, log);

                if (jogada == null)
                {
                    log("Nenhuma jogada retornada! BURRO BURRO");
                    continue;
                }

                log($"Escolha: {jogada.Value.dino} → {jogada.Value.cercado}");

                ValidarJogada(info, jogada.Value, log);
            }

            log("\n========================================");
            log("Testes finalizados!");
        }


        private static InformacoesTurno CriarEstadoFake()
        {
            return new InformacoesTurno
            {
                MaoJogador = GerarMao(),
                CercadosJogador = GerarCercados(),
                NumeroTurno = rng.Next(1, 6),
                DadoAtual = Dado.AL,
                StatusPartida = 'E',
                StatusTurno = 'A',
                JogueioDado = true
            };
        }

        private static List<AuxDinossauro> GerarMao()
        {
            var lista = new List<AuxDinossauro>();
            var dinos = Enum.GetValues(typeof(Dinossauro)).Cast<Dinossauro>();

            foreach (var d in dinos) lista.Add(new AuxDinossauro(d, rng.Next(0, 3)));

            return lista;
        }

        /*  private static List<DadoFace> GerarDado()
          {
              var lista = new List<DadoFace>();
              var dadinho = Enum.GetValues(typeof(Dado)).Cast<Dado>();

              foreach (var d in dadinho) lista.Add(new DadoFace(d, rng.Next(0, 3)));

              return lista;
          } */

        private static List<AuxCercado> GerarCercados()
        {
            var lista = new List<AuxCercado>();
            var dinos = Enum.GetValues(typeof(Dinossauro)).Cast<Dinossauro>().ToList();

            foreach (var c in CercadosExtension.CercadosLista())
            {
                var aux = new AuxCercado(c)
                {
                    Dinossauros = new List<Dinossauro>()
                };

                var qtd = rng.Next(0, 3);

                for (var i = 0; i < qtd; i++)
                    aux.Dinossauros.Add(dinos[rng.Next(dinos.Count)]);

                lista.Add(aux);
            }

            return lista;
        }


        private static void LogEstado(InformacoesTurno info, Action<string> log)
        {
            log("Mão:");
            foreach (var m in info.MaoJogador.Where(x => x.QuantidadeDinossauros > 0))
                log($" - {m.Dinossauro} x{m.QuantidadeDinossauros}");

            log("Cercados:");
            foreach (var c in info.CercadosJogador)
                log($" - {c.Cercados}: {c.Dinossauros.Count}");
        }

        private static void ValidarJogada(
            InformacoesTurno info,
            (Dinossauro dino, Cercados cercado) jogada,
            Action<string> log)
        {
            var ok = true;

            var item = info.MaoJogador.FirstOrDefault(x => x.Dinossauro == jogada.dino);

            if (item == null || item.QuantidadeDinossauros <= 0)
            {
                log("Dino inválido (não está na mão) BURRO BURRO");
                ok = false;
            }

            var cercadoAtual = info.CercadosJogador.FirstOrDefault(c => c.Cercados == jogada.cercado);
            var lista = cercadoAtual?.Dinossauros ?? new List<Dinossauro>();

            if (!jogada.cercado.SePodeColocarNoCercado(lista, jogada.dino))
            {
                log(" Jogada inválida no cercado. BURRO BURRO");
                ok = false;
            }

            var ganhoEscolhido = CalcularGanho(info, jogada.dino, jogada.cercado);
            var melhor = MelhorGanhoPossivel(info);

            log($"Ganho escolhido: {ganhoEscolhido}");
            log($"Melhor ganho possível: {melhor}");

            if (ganhoEscolhido < melhor)
            {
                log("NÃO FOI A MELHOR JOGADA MELHORE A FOME FILHO");
                ok = false;
            }

            if (ok)
            {
                log("Jogada válida e ótima!Tome");
                QuantasVezesComeu += 1;
                log(QuantasVezesComeu.ToString());
            }
        }


        private static int CalcularGanho(InformacoesTurno info, Dinossauro dino, Cercados cercado)
        {
            var antes = Pontuacao(info);
            var depois = PontuacaoSimulada(info, dino, cercado);
            return depois - antes;
        }

        private static int MelhorGanhoPossivel(InformacoesTurno info)
        {
            var melhor = int.MinValue;

            foreach (var item in info.MaoJogador.Where(x => x.QuantidadeDinossauros > 0))
            foreach (var c in CercadosExtension.CercadosLista())
            {
                var atual = info.CercadosJogador.FirstOrDefault(x => x.Cercados == c);
                var lista = atual?.Dinossauros ?? new List<Dinossauro>();

                if (!c.SePodeColocarNoCercado(lista, item.Dinossauro))
                    continue;

                var ganho = CalcularGanho(info, item.Dinossauro, c);

                if (ganho > melhor)
                    melhor = ganho;
            }

            return melhor;
        }


        private static int Pontuacao(InformacoesTurno info)
        {
            var pts = 0;

            foreach (var c in info.CercadosJogador)
            {
                var dinos = c.Dinossauros ?? new List<Dinossauro>();
                var qtd = dinos.Count;

                switch (c.Cercados)
                {
                    case Cercados.FI:
                        pts += qtd switch { 1 => 2, 2 => 4, 3 => 8, 4 => 12, 5 => 18, 6 => 24, _ => 0 };
                        break;

                    case Cercados.CD:
                        pts += dinos.Distinct().Count() switch
                        {
                            1 => 1, 2 => 3, 3 => 6, 4 => 10, 5 => 15, 6 => 21, _ => 0
                        };
                        break;

                    case Cercados.MT:
                        if (qtd == 3) pts += 7;
                        break;

                    case Cercados.PA:
                        pts += qtd / 2 * 5;
                        break;

                    case Cercados.RI:
                        pts += qtd;
                        break;

                    case Cercados.RS:
                        if (qtd == 1) pts += 7;
                        break;

                    case Cercados.IS:
                        if (qtd == 1)
                        {
                            var unico = !info.CercadosJogador
                                .SelectMany(x => x.Dinossauros ?? new List<Dinossauro>())
                                .Any(d => d == dinos[0]);

                            if (unico) pts += 7;
                        }

                        break;
                }
            }

            return pts;
        }

        private static int PontuacaoSimulada(InformacoesTurno info, Dinossauro dino, Cercados alvo)
        {
            var clone = info.CercadosJogador
                .Select(c => new AuxCercado(c.Cercados)
                {
                    Dinossauros = c.Dinossauros?.ToList() ?? new List<Dinossauro>()
                }).ToList();

            var alvoC = clone.First(c => c.Cercados == alvo);
            alvoC.Dinossauros.Add(dino);

            return Pontuacao(new InformacoesTurno { CercadosJogador = clone });
        }
    }
}