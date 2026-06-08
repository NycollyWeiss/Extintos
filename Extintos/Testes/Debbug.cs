using System;
using System.Collections.Generic;
using System.Linq;
using Extintos.Auxiliares;
using Extintos.Enumeration;
using Extintos.Interfaces;
using Extintos.LeonKennedy;
using Extintos.Model;



    public static class TesteOnline
    {
    
        public static void TestesOnlines(
            InformacoesTurno infoReal, 
            (Dinossauro dino, Cercados cercado)? jogadaReal, 
            Action<string> log)
        {
            log("\n[VALIDAÇÃO EM TEMPO REAL] Analisando a mente do Guloso...");
            
            if (jogadaReal == null) 
            {
                log("Leon Kennedy retornou NULL na partida real! TEM QUE TÁ VENDO ISSO AI.");
                return;
            }

            var dinoJogado = jogadaReal.Value.dino;
            var cercadoJogado = jogadaReal.Value.cercado;

            log($" Leon Kennedy decidiu: {dinoJogado} no cercado {cercadoJogado}");

            
            if (!ValidarRegras(infoReal, dinoJogado, cercadoJogado, log))
                return;

           
            ValidarMelhorJogada(infoReal, dinoJogado, cercadoJogado, log);
        }

        private static bool ValidarRegras(InformacoesTurno info, Dinossauro dino, Cercados cercado, Action<string> log)
        {
            var itemMao = info.MaoJogador?.FirstOrDefault(x => x.Dino == dino);
            if (itemMao == null || itemMao.QuantidadeDinossauros <= 0)
            {
                log("BURRO BURRO: Tentou jogar um dinossauro que NÃO TEM na mão. TÁ ALUCINANDO.");
                return false;
            }

            var cercadoAtual = info.CercadosJogador?.FirstOrDefault(c => c.Cercados == cercado);
            var dinosNoCercado = cercadoAtual?.Dinossauros ?? new List<Dinossauro>();

            if (!cercado.SePodeColocarNoCercado(dinosNoCercado, dino))
            {
                log("BURRO BURRO: Regra do jogo violada. Não pode colocar esse dino nesse cercado leon keneddy.");
                return false;
            }

            return true;
        }

        private static void ValidarMelhorJogada(InformacoesTurno info, Dinossauro dinoEscolhido, Cercados cercadoEscolhido, Action<string> log)
        {
            var botSimulator = new EstrategiaGulosa(); 
            
            int scoreEscolhido = CalcularScoreCompleto(info, dinoEscolhido, cercadoEscolhido);
            int melhorScorePossivel = int.MinValue;
            (Dinossauro dino, Cercados cercado) melhorCaminho = (dinoEscolhido, cercadoEscolhido);

            
            foreach (var item in info.MaoJogador.Where(x => x.QuantidadeDinossauros > 0))
            {
                foreach (Cercados c in Enum.GetValues(typeof(Cercados)))
                {
                    var dinosNoCercado = info.CercadosJogador
                                             .FirstOrDefault(x => x.Cercados == c)?.Dinossauros 
                                             ?? new List<Dinossauro>();

                    if (!c.SePodeColocarNoCercado(dinosNoCercado, item.Dinossauro))
                        continue; 

                    var scoreSimulado = CalcularScoreCompleto(info, item.Dinossauro, c);

                    if (scoreSimulado > melhorScorePossivel)
                    {
                        melhorScorePossivel = scoreSimulado;
                        melhorCaminho = (item.Dinossauro, c);
                    }
                }
            }

            // Veredito final
            log($"Score da jogada escolhida: {scoreEscolhido}");
            log($" Melhor score mapeado: {melhorScorePossivel} ({melhorCaminho.dino} → {melhorCaminho.cercado})");

            if (scoreEscolhido < melhorScorePossivel)
            {
                log("TA PASSANDO FOME O fudido do Leon Kennedy deixou pontos em potencial no prato do inimigo!");
                log($"A jogada ideal seria {melhorCaminho.dino} no cercado {melhorCaminho.cercado}.");
            }
            else
            {
                log("TOME! Leon Kennedy fez a jogada matematicamente mais aceitavel para este turno.");
            }
        }

       
        private static int CalcularScoreCompleto(InformacoesTurno info, Dinossauro dino, Cercados cercado)
        {
            var estr = new EstrategiaGulosa();
            
            
            
            var antes = estr.PontuacaoTotal(info); 
            var depois = estr.PontuacaoSimulada(info, cercado, dino); 
            var ganhoPontuacao = depois - antes;

            var bonus = EstrategiaAnalizador.BonusJogada(info, cercado, dino, TODO);
            var potencial = EstrategiaAnalizador.PotencialFuturo(info, cercado, dino);

            return ganhoPontuacao + bonus + potencial;
        }
    }

  