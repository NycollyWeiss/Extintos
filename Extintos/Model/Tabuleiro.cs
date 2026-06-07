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
    //Quantidade de jogadores
    //id de cada
    //ordem de passagem de mao de todos,
    //dinos no rei da selva,
    //dinos na floresta da igualdade,
    //quantidade atual de cada especie,
    //oponente mais forte,
    //oponente mais fraco(assim n tem que fazer track)
    //Especies e quantidades por round,
    //tabuleiro dos oponentes 

        public int QuantidadeJogadores { get; private set; }
        public int QuantidadeEspecies { get; private set; }
        public InformacoesTurno Turno { get; private set; }
        public AuxCercado Cercados { get; private set; }
        public Jogador LeonKennedy { get; private set; }
        public List<Oponente> QuengasDoLeon { get; private set; }
        public int IdDaPartida { get; private set; }

        private Tabuleiro() { }

        public static int ContaDinosNoTabuleiro(InformacoesTurno info, Dinossauro especie)
        {
            return info.CercadosJogador
                .SelectMany(cercado => cercado.Dinossauros)
                .Count(dino => dino.Dino == especie);
        }
        public static List<Oponente> OrdemDePassagemDeMao(List<Oponente> quengasLeon)
        {
            return quengasLeon
                .OrderBy(q => q.IdJogador)
                .ToList();
        }
        public static bool DinoViraReiDaSelva(InformacoesTurno info, Dinossauro especie, int quantidadeJogadores)
        {
            var count = ContaDinosNoTabuleiro(info, especie);
            return quantidadeJogadores == 2 ? count >= 2 : count >= 3;
        }

        public class Builder
        {
            private readonly Tabuleiro _tabuleiro = new Tabuleiro();

            public async Task<Builder> QuantidadeJogadoresAsync(int idDaPartida, CancellationToken ct = default)
            {
                var jogadores = await DraftService.PegaJogadoresAsync(idDaPartida, ct);

                var count = jogadores.Count();

                if (count >= 2 && count <= 4)
                {
                    _tabuleiro.QuantidadeJogadores = count;
                    return this;
                }

                throw new InvalidOperationException("Número de quengas está maior do que o Senhor Leon Kennedy aguenta.");
            }

            //trocar pra aux dino, ai já associa especie e quantidade diireto, mas precisa ver se tem
            //lista de auxdino no dinossauro
            public Builder QuantidadeEspecies(int quantidadeJogadores)
            {
                _tabuleiro.QuantidadeEspecies = quantidadeJogadores switch
                {
                    2 => 4,
                    3 => 6, 
                    4 => 8,
                    _ => throw new InvalidOperationException("Quantidade de quengas inválida.")
                };

                return this;
            }

            //public async Task<Builder> TurnoAsync(InformacoesTurno informacoesTurno){ return this; }
            // public async Task<Builder> ComCercadosAsync() { return this; }
           // public async Task<Builder> LeonKennedyAsync() { return this; }

           public async Task<Builder> QuengasDoLeonAsync(int idPartida, CancellationToken ct = default)
           {
               var jogadores = await DraftService.PegaJogadoresAsync(idPartida, ct);

               _tabuleiro.QuengasDoLeon = jogadores
                   .Select(j => new Oponente(
                       j.meusCercados,
                       j.IdJogador,
                       j.NomeJogador,
                       j.Pontuacao,
                       j.IdPartida = idPartida,
                       j.JogadorQueVaiPassarMao,
                       j.JogadorQueVaiReceberSuaMao))
                   .ToList();

               return this;
           }
            public Builder IdDaPartida(int idDaPartida)
            {
                _tabuleiro.IdDaPartida = idDaPartida;
                return this;
            }

            public Tabuleiro Build() => _tabuleiro;
        }
    }
}
