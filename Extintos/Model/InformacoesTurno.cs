using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Draft;
using Extintos.Model;
using Extintos.Services;
using Extintos.Auxiliares;

namespace Extintos.Enumeration
{
    public class InformacoesTurno
    {
        private InformacoesTurno()
        {
        }

        public static async Task<InformacoesTurno> CriarAsync(
            int idJogador, 
            int idPartida, 
            string senhaJogador, 
            Jogador jogador)
        {
            Console.WriteLine("[InformacoesTurno] Iniciando criação...");
            
            var instancia = new InformacoesTurno();
            
            try
            {
                Console.WriteLine("[InformacoesTurno] Obtendo estado da partida...");
                var estado = PartidasInfo.PartidaInfo.Obter(idPartida);
                
                instancia.StatusPartida = estado.StatusPartida;
                instancia.StatusTurno = estado.StatusTurno;
                instancia.DadoAtual = estado.FaceDadoAtual;
                instancia.JogueioDado = estado.IdJogadorDaVez == idJogador;
                instancia.NumeroTurno = estado.TurnoAtual;
                
                Console.WriteLine($"[InformacoesTurno] Estado obtido: Turno={estado.TurnoAtual}, Dado={estado.FaceDadoAtual}");
                
                Console.WriteLine("[InformacoesTurno] Obtendo mão do jogador...");
                instancia.MaoJogador = DraftService.ObterMao(idJogador, senhaJogador);
                Console.WriteLine($"[InformacoesTurno] Mão obtida: {instancia.MaoJogador?.Count ?? 0} dinossauros");
                
                instancia.CercadosJogador = jogador.MeusCercados;
                instancia.IdJogadorQueRolouDado = idJogador;
                
                Console.WriteLine("[InformacoesTurno] Construindo tabuleiro...");
                
                // CORREÇÃO CRÍTICA: Usar await corretamente com o builder
                var builder = new Tabuleiro.Builder();
                await builder.QuantidadeJogadoresAsync(idPartida);
                builder.IdDaPartida(idPartida);
                instancia.Tabuleiro = builder.Build();
                
                instancia.IdPartida = idPartida;
                instancia.MeuId = idJogador;
                instancia.QtdJogadores = instancia.Tabuleiro.QuantidadeJogadores;
                
                Console.WriteLine($"[InformacoesTurno] Tabuleiro construído: {instancia.QtdJogadores} jogadores");
                Console.WriteLine("[InformacoesTurno] Criação concluída com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[InformacoesTurno ERRO] {ex.Message}");
                Console.WriteLine($"[InformacoesTurno STACK] {ex.StackTrace}");
                throw;
            }
            
            return instancia;
        }

        public List<AuxDinossauro> MaoJogador { get; set; } = new();
        public List<AuxCercado> CercadosJogador { get; set; } = new();
        public Dado DadoAtual { get; set; }
        public int NumeroTurno { get; set; }
        public bool JogueioDado { get; set; }
        public char StatusPartida { get; set; }
        public char StatusTurno { get; set; }
        public int IdJogadorQueRolouDado { get; set; }
        public Tabuleiro Tabuleiro { get; set; }
        public int QtdJogadores { get; set; }
        public int IdPartida { get; set; }
        public int MeuId { get; set; }
    }
}