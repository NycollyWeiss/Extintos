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

        
      /*private static List<Dado> GerarDado()
        {
            var lista = new List<DadoFace>();
            var dado = Enum.GetValues(typeof(Dado)).Cast<Dado>();

            foreach (var da in dado) lista.Add(new DadoFace(dado.ToString);
                return lista;
            
        }
*/
         

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
                log("NÃO FOI A MELHOR JOGADA!! TÁ SEM FOME FILHO ??!");
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
                int bonusTrex = 2;
                Dinossauro dinoTrex = Dinossauro.TI;
                switch (c.Cercados)
                {
                    case Cercados.FI:
                        pts += qtd switch { 1 => 2, 2 => 4, 3 => 8, 4 => 12, 5 => 18, 6 => 24, _ => 0 };
                        if (dinos.Contains(dinoTrex))
                        {
                            pts += bonusTrex;
                            pts += qtd switch { 1 => 2, 2 => 4, 3 => 8, 4 => 12, 5 => 18, 6 => 24, _ => 0 };
                        }
                        break;

                    case Cercados.CD:
                        pts += dinos.Distinct().Count() switch
                        {
                            1 => 1, 2 => 3, 3 => 6, 4 => 10, 5 => 15, 6 => 21, _ => 0
                        };
                        if (dinos.Contains(dinoTrex))
                        {
                            pts += bonusTrex;
                        }

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
                        if (qtd == 1 && info.NumeroTurno >10) pts += 7;
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
 

    // ─────────────────────────────────────────────────────────────────────────
    //  Tipos de bot disponíveis na simulação
    // ─────────────────────────────────────────────────────────────────────────
    public enum TipoBot
    {
        Guloso,
        Aleatorio
    }

    // ─────────────────────────────────────────────────────────────────────────
    //  Representa um jogador dentro da partida simulada
    // ─────────────────────────────────────────────────────────────────────────
    public class JogadorSimulado
    {
        public string Nome { get; }
        public TipoBot Tipo { get; }
        public List<AuxDinossauro> Mao { get; set; } = new();
        public List<AuxCercado> Cercados { get; set; } = new();
        public int PontuacaoTotal { get; private set; }

        private static readonly Random rng = new();

        public JogadorSimulado(string nome, TipoBot tipo)
        {
            Nome = nome;
            Tipo = tipo;
            Cercados = InicializarCercados();
        }

        // Cada jogador começa com todos os cercados vazios
        private static List<AuxCercado> InicializarCercados()
        {
            return CercadosExtension.CercadosLista()
                .Select(c => new AuxCercado(c) { Dinossauros = new List<Dinossauro>() })
                .ToList();
        }

        // Calcula e armazena pontuação atual
        public int CalcularPontuacao()
        {
            PontuacaoTotal = PontuacaoHelper.Calcular(Cercados);
            return PontuacaoTotal;
        }

        // ── Decisão de jogada ──────────────────────────────────────────────

        /// <summary>
        /// Retorna a jogada escolhida junto com o log detalhado de todas as
        /// possibilidades avaliadas e os ganhos de cada uma.
        /// </summary>
        public (Dinossauro dino, Cercados cercado)? Jogar(
            int turno,
            Dado dado,
            Action<string> log,
            out List<OpcaoAvaliada> todasOpcoes)
        {
            todasOpcoes = new List<OpcaoAvaliada>();

            // Monta todas as jogadas válidas com seu ganho
            foreach (var item in Mao.Where(x => x.QuantidadeDinossauros > 0))
            {
                foreach (var cercado in CercadosExtension.CercadosLista())
                {
                    var atual = Cercados.FirstOrDefault(c => c.Cercados == cercado);
                    var lista = atual?.Dinossauros ?? new List<Dinossauro>();

                    if (!cercado.SePodeColocarNoCercado(lista, item.Dinossauro))
                        continue;

                    var ganho = PontuacaoHelper.GanhoSimulado(Cercados, item.Dinossauro, cercado);

                    todasOpcoes.Add(new OpcaoAvaliada
                    {
                        Dino = item.Dinossauro,
                        Cercado = cercado,
                        Ganho = ganho
                    });
                }
            }

            if (!todasOpcoes.Any())
                return null;

            // Ordena para log: melhor primeiro
            todasOpcoes = todasOpcoes.OrderByDescending(o => o.Ganho).ToList();

            (Dinossauro dino, Cercados cercado)? escolha = Tipo switch
            {
                TipoBot.Guloso => EscolherGuloso(todasOpcoes),
                TipoBot.Aleatorio => EscolherAleatorio(todasOpcoes),
                _ => EscolherAleatorio(todasOpcoes)
            };

            return escolha;
        }

        private static (Dinossauro, Cercados) EscolherGuloso(List<OpcaoAvaliada> opcoes)
        {
            // Pega o maior ganho; entre empates, escolhe aleatório
            var melhor = opcoes.First().Ganho;
            var candidatos = opcoes.Where(o => o.Ganho == melhor).ToList();
            var idx = new Random().Next(candidatos.Count);
            return (candidatos[idx].Dino, candidatos[idx].Cercado);
        }

        private static (Dinossauro, Cercados) EscolherAleatorio(List<OpcaoAvaliada> opcoes)
        {
            var idx = new Random().Next(opcoes.Count);
            return (opcoes[idx].Dino, opcoes[idx].Cercado);
        }

        // Remove um dino da mão após jogar
        public void RemoverDaMao(Dinossauro dino)
        {
            var item = Mao.FirstOrDefault(x => x.Dinossauro == dino);
            if (item != null && item.QuantidadeDinossauros > 0)
                item.QuantidadeDinossauros--;
        }

        // Coloca o dino no cercado escolhido
        public void AdicionarAoCercado(Dinossauro dino, Cercados cercado)
        {
            var c = Cercados.FirstOrDefault(x => x.Cercados == cercado);
            c?.Dinossauros.Add(dino);
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    //  DTO de uma opção avaliada durante o turno
    // ─────────────────────────────────────────────────────────────────────────
    public class OpcaoAvaliada
    {
        public Dinossauro Dino { get; set; }
        public Cercados Cercado { get; set; }
        public int Ganho { get; set; }
        public bool FoiEscolhida { get; set; }
    }

    // ─────────────────────────────────────────────────────────────────────────
    //  Helper de pontuação (extraído do TestRunnerOffline original)
    // ─────────────────────────────────────────────────────────────────────────
    public static class PontuacaoHelper
    {
        public static int Calcular(List<AuxCercado> cercados)
        {
            var pts = 0;

            foreach (var c in cercados)
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
                        { 1 => 1, 2 => 3, 3 => 6, 4 => 10, 5 => 15, 6 => 21, _ => 0 };
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
                            // Único no mundo: não aparece em nenhum outro cercado deste jogador
                            var unico = cercados
                                .SelectMany(x => x.Dinossauros ?? new List<Dinossauro>())
                                .Count(d => d == dinos[0]) > 1;

                            if (unico) pts += 7;
                        }
                        break;
                }
            }

            return pts;
        }

        public static int GanhoSimulado(List<AuxCercado> cercados, Dinossauro dino, Cercados alvo)
        {
            var antes = Calcular(cercados);

            var clone = cercados
                .Select(c => new AuxCercado(c.Cercados)
                {
                    Dinossauros = c.Dinossauros?.ToList() ?? new List<Dinossauro>()
                }).ToList();

            clone.First(c => c.Cercados == alvo).Dinossauros.Add(dino);

            var depois = Calcular(clone);
            return depois - antes;
        }
    }

    // ─────────────────────────────────────────────────────────────────────────
    //  Motor de partida simulada
    // ─────────────────────────────────────────────────────────────────────────
    public static class PartidaSimulada
    {
        private static readonly Random rng = new();

     
        public static void ExecutarPartida(int qtdJogadores, Action<string> log)
        {
            if (qtdJogadores < 2 || qtdJogadores > 4)
                throw new ArgumentOutOfRangeException(nameof(qtdJogadores), "Suportado: 2, 3 ou 4 jogadores.");

            log("");
            log($"╔══════════════════════════════════════════════════════╗");
            log($"║  PARTIDA SIMULADA — {qtdJogadores} JOGADORES         ║");
            log($"╚══════════════════════════════════════════════════════╝");
            log("");

           
            var jogadores = CriarJogadores(qtdJogadores);

            log("Jogadores nesta partida:");
            foreach (var j in jogadores)
                log($"   {j.Nome} [{j.Tipo}]");
            log("");

         
            var baralho = CriarBaralho();
            log($" Saco criado: {baralho.Count} dinossauros, sorteando...");
            log("");

       
            const int maoSize = 6;
            DistribuirMaos(jogadores, baralho, maoSize, log);

          
            const int maxRodadas = 30;
            var rodada = 0;

            while (rodada < maxRodadas)
            {
                rodada++;

                log($"┌─────────────────────────────────────────────────────");
                log($"│  RODADA {rodada}");
                log($"└─────────────────────────────────────────────────────");

                var algumJogou = false;

                for (var idx = 0; idx < jogadores.Count; idx++)
                {
                    var jogador = jogadores[idx];
                    var dado = SortearDado();

                    log($"\n  ▶ Turno de {jogador.Nome} ({jogador.Tipo})  |  Dado: {dado}");
                    LogMao(jogador, log);

                    var jogada = jogador.Jogar(rodada, dado, log, out var opcoes);

                    LogOpcoes(opcoes, jogada, log);

                    if (jogada == null)
                    {
                        log($"   {jogador.Nome} não tem jogada válida — passa a vez.");
                        continue;
                    }

                
                    jogador.RemoverDaMao(jogada.Value.dino);
                    jogador.AdicionarAoCercado(jogada.Value.dino, jogada.Value.cercado);

                    log($"  Jogada: [{jogada.Value.dino}] → cercado [{jogada.Value.cercado}]");
                    log($"  Pontuação de {jogador.Nome}: {jogador.CalcularPontuacao()} pts");

                    algumJogou = true;
                }

                PassarMaos(jogadores, log);

              
                ReabastecerMaos(jogadores, baralho, maoSize, log);

                log("");
                LogPlacar(jogadores, log);

       
                if (!algumJogou && baralho.Count == 0)
                {
                    log("\n🏁 Nenhum jogador tem mais jogadas possíveis. Fim de partida!");
                    break;
                }
            }

            LogResultadoFinal(jogadores, log);
        }

      

        public static void ExecutarPartida2Jogadores(Action<string> log) =>
            ExecutarPartida(2, log);

        public static void ExecutarPartida3Jogadores(Action<string> log) =>
            ExecutarPartida(3, log);

        public static void ExecutarPartida4Jogadores(Action<string> log) =>
            ExecutarPartida(4, log);

      
        private static List<JogadorSimulado> CriarJogadores(int qtd)
        {
         
            var tiposOponentes = new[] { TipoBot.Aleatorio, TipoBot.Guloso, TipoBot.Aleatorio };
            var jogadores = new List<JogadorSimulado>();

            for (var i = 0; i < qtd; i++)
            {
                var tipo = i == 0 ? TipoBot.Guloso : tiposOponentes[i - 1];
                var emoji = tipo == TipoBot.Guloso ? "🧠" : "🎲";
        
                
                string nomeJogador = i == 0 ? $"{emoji} Leon Kennedy" : $"{emoji} Jogador {i + 1}";

                jogadores.Add(new JogadorSimulado(nomeJogador, tipo));
            }

            return jogadores;
        }
        

        /// <summary>
        /// Baralho: todas as combinações de Dinossauro * quantidade típica.
        /// Ajuste os multiplicadores conforme as regras reais do jogo.
        /// </summary>
        private static List<Dinossauro> CriarBaralho()
        {
            var baralho = new List<Dinossauro>();
            var dinos = Enum.GetValues(typeof(Dinossauro)).Cast<Dinossauro>().ToList();

            // 4 cópias de cada dino (ajuste conforme quantidade real do jogo)
            foreach (var d in dinos)
                for (var i = 0; i < 4; i++)
                    baralho.Add(d);

            // Embaralha (Fisher-Yates)
            for (var i = baralho.Count - 1; i > 0; i--)
            {
                var j = rng.Next(i + 1);
                (baralho[i], baralho[j]) = (baralho[j], baralho[i]);
            }

            return baralho;
        }

        private static void DistribuirMaos(
            List<JogadorSimulado> jogadores,
            List<Dinossauro> baralho,
            int tamanho,
            Action<string> log)
        {
            log("Distribuindo mãos iniciais:");

            foreach (var j in jogadores)
            {
                var dinos = Enum.GetValues(typeof(Dinossauro)).Cast<Dinossauro>().ToList();
                j.Mao = dinos.Select(d => new AuxDinossauro(d, 0)).ToList();

                for (var i = 0; i < tamanho && baralho.Count > 0; i++)
                {
                    var carta = baralho[0];
                    baralho.RemoveAt(0);
                    var slot = j.Mao.First(x => x.Dinossauro == carta);
                    slot.QuantidadeDinossauros++;
                }

                var resumo = string.Join(", ",
                    j.Mao.Where(x => x.QuantidadeDinossauros > 0)
                         .Select(x => $"{x.Dinossauro}×{x.QuantidadeDinossauros}"));

                log($"   {j.Nome}: [{resumo}]");
            }

            log($"   (Dinossauros restante: {baralho.Count} dinos)");
            log("");
        }

        private static Dado SortearDado()
        {
            var valores = Enum.GetValues(typeof(Dado)).Cast<Dado>().ToArray();
            return valores[rng.Next(valores.Length)];
        }

     
        private static void PassarMaos(List<JogadorSimulado> jogadores, Action<string> log)
        {
            if (jogadores.Count < 2) return;

            log("\n  Passando mãos...");

            // Guarda mão do último para dar ao primeiro
            var maoTemp = jogadores[jogadores.Count - 1].Mao;

            for (var i = jogadores.Count - 1; i > 0; i--)
                jogadores[i].Mao = jogadores[i - 1].Mao;

            jogadores[0].Mao = maoTemp;

            foreach (var j in jogadores)
            {
                var resumo = string.Join(", ",
                    j.Mao.Where(x => x.QuantidadeDinossauros > 0)
                         .Select(x => $"{x.Dinossauro}×{x.QuantidadeDinossauros}"));
                log($"     {j.Nome} recebe: [{(string.IsNullOrEmpty(resumo) ? "vazia" : resumo)}]");
            }
        }

      
        private static void ReabastecerMaos(
            List<JogadorSimulado> jogadores,
            List<Dinossauro> baralho,
            int tamanho,
            Action<string> log)
        {
            if (baralho.Count == 0) return;

            foreach (var j in jogadores)
            {
                var totalNaMao = j.Mao.Sum(x => x.QuantidadeDinossauros);

                if (totalNaMao < tamanho / 2)
                {
                    var faltam = tamanho - totalNaMao;
                    var reabasteceu = 0;

                    for (var i = 0; i < faltam && baralho.Count > 0; i++)
                    {
                        var carta = baralho[0];
                        baralho.RemoveAt(0);
                        var slot = j.Mao.First(x => x.Dinossauro == carta);
                        slot.QuantidadeDinossauros++;
                        reabasteceu++;
                    }

                    if (reabasteceu > 0)
                        log($"  {j.Nome} reabasteceu {reabasteceu} dinos do saco.");
                }
            }
        }

     
        private static void LogMao(JogadorSimulado j, Action<string> log)
        {
            var itens = j.Mao.Where(x => x.QuantidadeDinossauros > 0).ToList();

            if (!itens.Any())
            {
                log("  Mão: [vazia]");
                return;
            }

            log("  Mão: " + string.Join("  ", itens.Select(x => $"{x.Dinossauro}×{x.QuantidadeDinossauros}")));
        }

        private static void LogOpcoes(
            List<OpcaoAvaliada> opcoes,
            (Dinossauro dino, Cercados cercado)? escolhida,
            Action<string> log)
        {
            if (!opcoes.Any())
            {
                log("  Nenhuma opção disponível.");
                return;
            }

            log("  Opções avaliadas (ordenadas por ganho):");

            for (var i = 0; i < opcoes.Count; i++)
            {
                var o = opcoes[i];
                var foiEscolhida = escolhida.HasValue &&
                                   escolhida.Value.dino == o.Dino &&
                                   escolhida.Value.cercado == o.Cercado;

                var marcador = foiEscolhida ? "  ★ ESCOLHIDA" : "            ";
                var ganhoStr = o.Ganho >= 0 ? $"+{o.Ganho}" : $"{o.Ganho}";

                log($"     [{i + 1:D2}] {o.Dino,-14} → {o.Cercado,-5}  ganho: {ganhoStr,4} pts {marcador}");
            }

          
            var melhorGanho = opcoes.First().Ganho;
            var ganhoEscolhido = escolhida.HasValue
                ? opcoes.First(o => o.Dino == escolhida.Value.dino && o.Cercado == escolhida.Value.cercado).Ganho
                : 0;

            if (ganhoEscolhido < melhorGanho)
                log($"    Escolha sub-ótima! Melhor disponível era +{melhorGanho}, escolheu +{ganhoEscolhido}  (bot Aleatório)");
        }

        private static void LogPlacar(List<JogadorSimulado> jogadores, Action<string> log)
        {
            log("   PLACAR ATUAL:");
            var ordenados = jogadores.OrderByDescending(j => j.PontuacaoTotal).ToList();

            for (var i = 0; i < ordenados.Count; i++)
            {
                var medal = i == 0 ? "🥇" : i == 1 ? "🥈" : i == 2 ? "🥉" : "  ";
                log($"     {medal} {ordenados[i].Nome,-20} {ordenados[i].PontuacaoTotal,4} pts");
            }
        }

        private static void LogResultadoFinal(List<JogadorSimulado> jogadores, Action<string> log)
        {
            log("");
            log("╔══════════════════════════════════════════════════════╗");
            log("║              🏆  RESULTADO FINAL                     ║");
            log("╠══════════════════════════════════════════════════════╣");

            var ordenados = jogadores.OrderByDescending(j => j.PontuacaoTotal).ToList();

            for (var i = 0; i < ordenados.Count; i++)
            {
                var j = ordenados[i];
                var medal = i == 0 ? "🥇" : i == 1 ? "🥈" : i == 2 ? "🥉" : "  ";
                log($"║  {medal} {j.Nome,-22} {j.PontuacaoTotal,4} pts  [{j.Tipo}]");
            }

            log("╠══════════════════════════════════════════════════════╣");

            // Detalhamento dos cercados de cada jogador
            foreach (var j in ordenados)
            {
                log($"║  Cercados de {j.Nome}:");
                foreach (var c in j.Cercados.Where(x => x.Dinossauros.Any()))
                {
                    var dinos = string.Join(", ", c.Dinossauros.Select(d => d.ToString()));
                    log($"║    {c.Cercados,-5}: [{dinos}]");
                }
            }

            log("╚══════════════════════════════════════════════════════╝");
            log("");

            var vencedor = ordenados.First();
            if (vencedor.Tipo == TipoBot.Guloso)
            {
                log($" O FILHA DA PUTA DO {vencedor.Nome} com {vencedor.PontuacaoTotal} pontos!\n O GULOSO COMEU O RABO DOS OPONENTES!!");
            }
            else
            {
                log($"Infelizmente o vencedor é: {vencedor.Nome} com {vencedor.PontuacaoTotal} pontos. O GULOSO TOMOU OZEMPIC SÓ PODE!");

            }
        }
    }

   
    public static class TestRunnerPartidas
    {
        public static void ExecutarTodos(Action<string> log)
        {
            log("🦕🦕🦕  SIMULAÇÃO COMPLETA DE PARTIDAS  🦕🦕🦕");
            log("=".PadRight(56, '='));

            log("\n\n");
            PartidaSimulada.ExecutarPartida2Jogadores(log);

            log("\n\n");
            PartidaSimulada.ExecutarPartida3Jogadores(log);

            log("\n\n");
            PartidaSimulada.ExecutarPartida4Jogadores(log);

            log("\n=".PadRight(56, '='));
            log("Todas as simulações finalizadas!");
        }
    }
}
