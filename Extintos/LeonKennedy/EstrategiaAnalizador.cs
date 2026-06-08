using Extintos.Auxiliares;
using Extintos.Enumeration;
using Extintos.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Extintos.LeonKennedy
{
    public class EstrategiaAnalizador
    {
        public static int BonusJogada(InformacoesTurno info, Cercados cercado, Dinossauro dino)
        {
            var bonus = 0;
            var alvo = info.CercadosJogador.FirstOrDefault(x => x.Cercados == cercado);
            if (alvo == null) return 0;

            var dinosNoCercado = alvo.Dinossauros ?? new List<AuxDinossauro>();
            var qtdAtual = dinosNoCercado.Sum(d => d.QuantidadeDinossauros);

            int qtdDessaEspecieNoZoo = info.CercadosJogador
                .Where(c => c.Cercados != Cercados.RI)
                .SelectMany(c => c.Dinossauros ?? new List<AuxDinossauro>())
                .Where(d => d.Dino == dino)
                .Sum(d => d.QuantidadeDinossauros);

            int qtdDessaEspecieNaMao = info.MaoJogador
                .Where(d => d.Dino == dino)
                .Sum(d => d.QuantidadeDinossauros);

            int totalEspecieDisponivelParaMi = qtdDessaEspecieNoZoo + qtdDessaEspecieNaMao + 1;

            switch (cercado)
            {
                case Cercados.MT: // MATA TRIPLA
                    if (qtdAtual == 2)
                    {
                        // PRIORIDADE ABSOLUTA! Faltando 1 para 7 pontos.
                        // Isso deve ser a jogada mais atraente do tabuleiro (exceto RS garantido).
                        bonus += 250;
                    }
                    else if (qtdAtual == 1)
                    {
                        bonus += 40;
                        // Se a PA já tem casal completo, MT se torna ainda mais atraente
                        var qtdTotalPA = info.CercadosJogador
                            .FirstOrDefault(x => x.Cercados == Cercados.PA)?
                            .Dinossauros?.Sum(d => d.QuantidadeDinossauros) ?? 0;
                        if (qtdTotalPA >= 2) bonus += 60;
                    }
                    else
                    {
                        bonus -= 20; // Não comece MT cedo demais
                    }
                    break;

                case Cercados.PA: // PRADARIA DO AMOR
                    int qtdDessaEspecieNaPA = dinosNoCercado
                        .Where(d => d.Dino == dino)
                        .Sum(d => d.QuantidadeDinossauros);
                    int qtdTotalNaPA = dinosNoCercado.Sum(d => d.QuantidadeDinossauros);

                    if (qtdDessaEspecieNaPA % 2 != 0)
                    {
                        // Vai formar um casal AGORA! (ex: de 1 vai para 2)
                        bonus += 150;
                        if (dino == Dinossauro.TI) bonus += 80; // Casal de T-Rex é espetacular!
                    }
                    else
                    {
                        // Vai se tornar ÍMPAR (ex: de 0 vai para 1, ou de 2 vai para 3).
                        
                        // REGRA DO CASAL ÚNICO: Se já temos >= 2 dinos na PA (casal pronto),
                        // tentar um segundo casal é arriscado e geralmente desperdício.
                        if (qtdTotalNaPA >= 2)
                        {
                            // Única exceção: T-Rex E tiver o par na mão
                            if (dino == Dinossauro.TI && qtdDessaEspecieNaMao >= 2)
                            {
                                bonus += 30;
                            }
                            else
                            {
                                // PENALIDADE SEVERA: Não tente um segundo casal na PA!
                                // Vá para MT, FI ou CD.
                                bonus -= 200;
                            }
                        }
                        else
                        {
                            // A PA está vazia ou tem apenas 1 dino solto.
                            // CORREÇÃO CRÍTICA: Preciso ter >= 2 na mão (um para jogar, um que sobra)
                            if (qtdDessaEspecieNaMao >= 2)
                            {
                                bonus += 50; // Tenho o par na mão, investimento seguro.
                                if (dino == Dinossauro.TI) bonus += 30;
                            }
                            else
                            {
                                // DESPERDÍCIO CRÍTICO: Iniciar casal sem ter o par na mão.
                                bonus -= 250;
                            }
                        }
                    }
                    break;

                case Cercados.FI: // FLORESTA DA IGUALDADE
                    if (qtdAtual == 0)
                    {
                        // Só comece FI se tiver pelo menos 3 dinos dessa espécie no total
                        if (totalEspecieDisponivelParaMi >= 4) 
                            bonus += 150; // Excelente, vai encher fácil!
                        else if (totalEspecieDisponivelParaMi >= 3) 
                            bonus += 60; // Arriscado, mas aceitável.
                        else 
                            bonus -= 300; // SUICÍDIO: Iniciar FI com 1 ou 2 dinos.
                    }
                    else
                    {
                        // Já tem dinos na FI. Incentivar a NÃO PARAR DE ENCHER!
                        if (qtdAtual == 2 && totalEspecieDisponivelParaMi >= 3) 
                            bonus += 120; // Não deixe morrer com 2! Busque o 3º (8 pts)
                        else if (qtdAtual >= 3) 
                            bonus += 80; // Continue enchendo, pontuação exponencial.
                        else 
                            bonus -= 50; // Está estagnado.
                    }
                    break;

                case Cercados.CD: // CAMPINA DA DIFERENÇA
                    bool temNoCD = dinosNoCercado.Any(d => d.Dino == dino && d.QuantidadeDinossauros > 0);
                    if (!temNoCD)
                    {
                        if (qtdDessaEspecieNaMao <= 1) 
                            bonus += 70; // Dino solitário na mão, perfeito para Campina!
                        else 
                            bonus += 25; // Bom, mas talvez fosse melhor pra FI ou PA.
                    }
                    break;

                case Cercados.RS: // REI DA SELVA
                    if (qtdAtual == 0)
                    {
                        if (totalEspecieDisponivelParaMi >= 4) 
                            bonus += 300; // Soberania garantida! T-REX DE 7 PONTOS!
                        else if (totalEspecieDisponivelParaMi == 3) 
                            bonus += 100; // Forte candidato.
                        else 
                            bonus -= 150; // Prematuro.
                    }
                    break;

                case Cercados.IS: // ILHA SOLITÁRIA
                    if (qtdAtual == 0)
                    {
                        var cercadoCD = info.CercadosJogador
                            .FirstOrDefault(x => x.Cercados == Cercados.CD);
                        int qtdEspeciesNoCD = cercadoCD?.Dinossauros?
                            .Count(d => d.QuantidadeDinossauros > 0) ?? 0;
                        
                        if (qtdEspeciesNoCD == 6)
                        {
                            bonus -= 400; // IMPOSSÍVEL PONTUAR. FUJA!
                            break;
                        }

                        if (info.NumeroTurno < 9)
                        {
                            bonus -= 200; // Muito cedo.
                            break;
                        }

                        bool especieEhUnicaNoMeuZoo = (qtdDessaEspecieNoZoo == 0);
                        
                        if (especieEhUnicaNoMeuZoo)
                        {
                            bonus += 200; // Late game + espécie inédita = 7 pontos!
                        }
                        else
                        {
                            bonus -= 300; // Já temos esse dino. IS dará ZERO.
                        }
                    }
                    break;
            }
            return bonus;
        }

        public static int PotencialFuturo(InformacoesTurno info, Cercados cercado, Dinossauro dino)
        {
            var alvo = info.CercadosJogador.FirstOrDefault(x => x.Cercados == cercado);
            if (alvo == null) return 0;
            var qtd = alvo.Dinossauros?.Sum(d => d.QuantidadeDinossauros) ?? 0;

            switch (cercado)
            {
                case Cercados.MT: 
                    // Se tem 2, o potencial é MÁXIMO (falta 1 para 7 pts)
                    return (3 - qtd) * 15;
                case Cercados.FI: 
                    return qtd * 20; // Potencial exponencial
                case Cercados.CD:
                    var especiesDistintas = alvo.Dinossauros?
                        .Count(d => d.QuantidadeDinossauros > 0) ?? 0;
                    return (6 - especiesDistintas) * 10;
                case Cercados.PA: 
                    return 25; // Potencial para casais
                default: 
                    return 0;
            }
        }
    }
}