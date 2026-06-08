using System;

namespace Extintos.Enumeration
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class CercadosInfo : Attribute
    {
        public CercadosInfo(string nome, string restricao, string pontuacao, string codigo)
        {
            Nome = nome;
            Restricao = restricao;
            Pontuacao = pontuacao;
            Codigo = codigo;
        }

        public string Nome { get; }
        public string Restricao { get; }
        public string Pontuacao { get; }
        public string Codigo { get; }
    }

    public enum Cercados
    {
        [CercadosInfo("Campina da Diferença", "Espécies diferentes", "1/3/6/10/15/21", "CD")]
        CD,
        [CercadosInfo("Floresta da Igualdade", "Mesma espécie", "2/4/8/12/18/24", "FI")]
        FI,
        [CercadosInfo("Ilha Solitária", "Apenas 1 dino", "7 pontos se único no zoo", "IS")]
        IS,
        [CercadosInfo("Mata Tripla", "Até 3 dinos", "7 pontos se tiver 3", "MT")]
        MT,
        [CercadosInfo("Pradaria do Amor", "Qualquer dino", "5 por casal", "PA")]
        PA,
        [CercadosInfo("Rio", "Qualquer quantidade", "1 ponto por dino", "RI")]
        RI,
        [CercadosInfo("Rei da Selva", "Apenas 1 dino", "7 pontos se dominar", "RS")]
        RS
    }
}