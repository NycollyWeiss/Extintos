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
                    if (qtdAtual == 2) bonus += 100; // Última vaga! Prioridade máxima.
                    else if (qtdAtual == 1) bonus += 30; // Caminho em andamento.
                    else 
                    {
                        // Se for a ÚNICA jogada válida (ex: dado VZ e tudo cheio), não penalize tanto, 
                        // mas ainda prefira outras opções se existirem.
                        bonus -= 10; 
                    }
                    break;

                case Cercados.PA: // PRADARIA DO AMOR
                    int qtdDessaEspecieNaPA = dinosNoCercado.Where(d => d.Dino == dino).Sum(d => d.QuantidadeDinossauros);
                    if (qtdDessaEspecieNaPA % 2 != 0) 
                    {
                        // Vai se tornar PAR (ex: de 1 vai para 2). COMBO FECHADO!
                        bonus += 120; 
                        if (dino == Dinossauro.TI) bonus += 50; 
                    }
                    else 
                    {
                        // Vai se tornar ÍMPAR (ex: de 0 vai para 1).
                        // CORREÇÃO CRÍTICA: Preciso ter >= 2 na mão. 
                        // Se eu tenho 1 e jogo ele, fico com 0. Preciso de 2 para sobrar 1 para o próximo turno!
                        if (qtdDessaEspecieNaMao >= 2) 
                        {
                            bonus += 40; // Tenho o par na mão, é um investimento seguro.
                            if (dino == Dinossauro.TI) bonus += 20;
                        }
                        else 
                        {
                            bonus -= 200; // DESPERDÍCIO CRÍTICO AUMENTADO: Deixar dino solto na PA sem ter o par na mão.
                        }
                    }
                    break;

                case Cercados.FI: // FLORESTA DA IGUALDADE
                    if (qtdAtual == 0)
                    {
                        if (totalEspecieDisponivelParaMi >= 4) bonus += 120; 
                        else if (totalEspecieDisponivelParaMi >= 3) bonus += 50; 
                        else bonus -= 250; // SUICÍDIO: Iniciar FI com apenas 1 ou 2 dinos no total.
                    }
                    else
                    {
                        if (qtdAtual == 2 && totalEspecieDisponivelParaMi >= 3) bonus += 100; 
                        else if (qtdAtual >= 3) bonus += 60; 
                        else bonus -= 30; 
                    }
                    break;

                case Cercados.CD: // CAMPINA DA DIFERENÇA
                    bool temNoCD = dinosNoCercado.Any(d => d.Dino == dino && d.QuantidadeDinossauros > 0);
                    if (!temNoCD)
                    {
                        if (qtdDessaEspecieNaMao <= 1) bonus += 60; 
                        else bonus += 20; 
                    }
                    break;

                case Cercados.RS: // REI DA SELVA
                    if (qtdAtual == 0)
                    {
                        if (totalEspecieDisponivelParaMi >= 4) bonus += 250; 
                        else if (totalEspecieDisponivelParaMi == 3) bonus += 90; 
                        else bonus -= 120; 
                    }
                    break;

                case Cercados.IS: // ILHA SOLITÁRIA
                    if (qtdAtual == 0)
                    {
                        var cercadoCD = info.CercadosJogador.FirstOrDefault(x => x.Cercados == Cercados.CD);
                        int qtdEspeciesNoCD = cercadoCD?.Dinossauros?.Count(d => d.QuantidadeDinossauros > 0) ?? 0;
                        if (qtdEspeciesNoCD == 6)
                        {
                            bonus -= 300; 
                            break;
                        }

                        if (info.NumeroTurno < 9)
                        {
                            bonus -= 150; // Muito cedo.
                            break;
                        }

                        bool especieEhUnicaNoMeuZoo = (qtdDessaEspecieNoZoo == 0);
                        
                        if (especieEhUnicaNoMeuZoo)
                        {
                            bonus += 180; 
                        }
                        else
                        {
                            bonus -= 250; 
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
                case Cercados.MT: return (3 - qtd) * 10;
                case Cercados.FI: return qtd * 15;
                case Cercados.CD:
                    var especiesDistintas = alvo.Dinossauros?.Count(d => d.QuantidadeDinossauros > 0) ?? 0;
                    return (6 - especiesDistintas) * 8;
                case Cercados.PA: return 20;
                default: return 0;
            }
        }
    }
}