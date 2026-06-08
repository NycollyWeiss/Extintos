using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Extintos.Auxiliares;
using Extintos.Enumeration;
using Extintos.Services;

namespace Extintos.Model
{
    public class Tabuleiro
    {
        public class JogadaOponente
        {
            public int IdJogador { get; set; }
            public Dinossauro Dinossauro { get; set; }
            public Cercados Cercado { get; set; }
            public int Turno { get; set; }
            // Sugestão: Se o histórico guardar quantidades, adicione: public int Quantidade { get; set; }

            public JogadaOponente(int idJogador, Dinossauro dinossauro, Cercados cercado, int turno)
            {
                IdJogador = idJogador;
                Dinossauro = dinossauro;
                Cercado = cercado;
                Turno = turno;
            }
        }
        
        public int QuantidadeJogadores { get; private set; }
        public int QuantidadeEspecies { get; private set; }
        public InformacoesTurno Turno { get; private set; }
        public AuxCercado CercadoAtual { get; private set; }       
        public Jogador LeonKennedy { get; private set; }
        
        // CORREÇÃO 1: Inicialização para evitar NullReferenceException
        public List<Oponente> QuengasDoLeon { get; private set; } = new List<Oponente>();
        
        public int IdDaPartida { get; private set; }

        // Construtor privado obriga o uso do Builder
        private Tabuleiro() { }

        public static int ContaDinosValidosNoTabuleiro(Jogador jogador, Dinossauro especie)
        {
            if (jogador?.MeusCercados == null) return 0;
            
            return jogador.MeusCercados
                .Where(c => c.Cercados != Cercados.RI)
                .SelectMany(c => c.Dinossauros)
                .Where(d => d.Dino == especie)
                .Sum(d => d.QuantidadeDinossauros);
        }

        public static Dictionary<int, List<AuxDinossauro>> EspeciesEQuantidadesPorRound(List<JogadaOponente> historicoOponentes)
        {
            return historicoOponentes
                .GroupBy(jogada => jogada.Turno)
                .ToDictionary(
                    grupo => grupo.Key,
                    grupo => grupo
                        .GroupBy(j => j.Dinossauro)
                        .Select(dinoGrupo => new AuxDinossauro(
                            dinoGrupo.Key,
                            // CORREÇÃO 2: Se sua classe JogadaOponente tiver "Quantidade", use .Sum(x => x.Quantidade).
                            // Se cada registro no histórico for 1 dinossauro, .Count() está correto.
                            dinoGrupo.Count() 
                        ))
                        .ToList()
                );
        }

        public static List<AuxDinossauro> QuantidadeAtualDeCadaEspecie(List<Oponente> quengasDoLeon)
        {
            if (quengasDoLeon == null || !quengasDoLeon.Any()) return new List<AuxDinossauro>();

            return quengasDoLeon
                .SelectMany(oponente => oponente.MeusCercados ?? new List<AuxCercado>())
                .SelectMany(cercado => cercado.Dinossauros ?? new List<AuxDinossauro>())
                .GroupBy(dino => dino.Dino)
                .Select(grupo => new AuxDinossauro(grupo.Key, grupo.Sum(d => d.QuantidadeDinossauros)))
                .ToList();
        }

        // CORREÇÃO 3: Retornar uma nova lista em vez de modificar a original (evita bugs de referência)
        public static List<AuxDinossauro> ConsolidarDino(List<AuxDinossauro> dinosPraConsolidar)
        {
            if (dinosPraConsolidar == null || !dinosPraConsolidar.Any()) return new List<AuxDinossauro>();

            return dinosPraConsolidar
                .GroupBy(x => x.Dino) 
                .Select(g => new AuxDinossauro(g.Key, g.Sum(x => x.QuantidadeDinossauros)))
                .ToList();
        }

        public static int QuantidadeConhecidaPorEspecie(List<AuxDinossauro> universoConhecido, Dinossauro especie)
        {
            return universoConhecido?.FirstOrDefault(x => x.Dino == especie)?.QuantidadeDinossauros ?? 0;
        }
      
        public class Builder
        {
            private readonly Tabuleiro _tabuleiro = new Tabuleiro();

            // Mantém o async, mas agora sabemos que DEVEMOS usar 'await' ao chamar
            public async Task<Builder> QuantidadeJogadoresAsync(int idDaPartida, CancellationToken ct = default)
            {
                var jogadores = await DraftService.PegaJogadoresAsync(idDaPartida, ct);
                _tabuleiro.QuantidadeJogadores = jogadores.Count();
                return this;
            }

            public Builder IdDaPartida(int idDaPartida)
            {
                _tabuleiro.IdDaPartida = idDaPartida;
                return this;
            }

            // Você pode adicionar mais métodos de build aqui se precisar
            public Builder ComJogadorPrincipal(Jogador jogador)
            {
                _tabuleiro.LeonKennedy = jogador;
                return this;
            }

            public Tabuleiro Build() => _tabuleiro;
        }
    }
}