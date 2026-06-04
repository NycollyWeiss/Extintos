namespace Extintos.LeonKennedy
{
    public class ConfigEstrategia
{
    public double PesoPotencialFuturo { get; set; } = 0.4;

    /// <summary>Peso do fator de completude (0.0 a 1.0)</summary>
    public double PesoCompletude { get; set; } = 0.3;

    /// <summary>Peso do espaço restante no cercado (0 a 5)</summary>
    public int PesoEspacoRestante { get; set; } = 1;

    /// <summary>Peso da sinergia com dinos na mão (0 a 3)</summary>
    public int PesoSinergiaMao { get; set; } = 2;

    /// <summary>Penalidade por concentrar uma espécie em muitos cercados (0 a 5)</summary>
    public int PesoPenalidadeConcentracao { get; set; } = 2;

    // === VALORES BASE POR CERCADO ===

    public int ValorBaseRS { get; set; } = 4; // Rei da Selva
    public int ValorBaseFI { get; set; } = 3; // Floresta da Igualdade
    public int ValorBaseCD { get; set; } = 2; // Campina da Diferença
    public int ValorBasePA { get; set; } = 2; // Pradaria do Amor
    public int ValorBaseMT { get; set; } = 3; // Mata Tripla
    public int ValorBaseIS { get; set; } = 3; // Ilha Solitária
    public int ValorBaseRI { get; set; } = 1; // Rio

    // === BÔNUS CONTEXTUAIS ===

    public int BonusDadoFavoravel { get; set; } = 2;
    public int BonusUltimaVaga { get; set; } = 3;
    public int BonusDadoVZ { get; set; } = 4;
    public int BonusUltimaUnidade { get; set; } = 2;

    // === COMPORTAMENTO ESTRATÉGICO ===

    /// <summary>Evitar cercados já ocupados por oponentes</summary>
    public bool EvitarConfrontoDireto { get; set; } = true;

    /// <summary>Peso da penalidade por confronto direto (0 a 5)</summary>
    public int PesoPenalidadeConfronto { get; set; } = 2;

    /// <summary>Preferir diversificar espécies por cercados</summary>
    public bool PreferirDiversificacao { get; set; } = true;

    /// <summary>Valorizar jogar a última unidade de uma espécie da mão</summary>
    public bool ValorizarUltimaUnidade { get; set; } = false;

    // === SELEÇÃO DE JOGADA ===

    /// <summary>Modo de seleção da jogada final</summary>
    public ModoSelecaoEstrategia ModoSelecao { get; set; } = ModoSelecaoEstrategia.Top3Aleatorio;

    /// <summary>Fator de variação aleatória (0.0 = determinístico, 1.0 = máximo caos)</summary>
    public double FatorVariacao { get; set; } = 0.15;

    // === EXECUÇÃO ===

    /// <summary>Executar animação visual das jogadas (para debug/demo)</summary>
    public bool ExecutarAnimacaoVisual { get; set; } = true;
}

/// <summary>
///     Modos de seleção da jogada final pela IA.
/// </summary>
public enum ModoSelecaoEstrategia
{
    /// <summary>Sempre escolhe a jogada com maior score (determinístico)</summary>
    SempreMelhor,

    /// <summary>Escolhe aleatoriamente entre as top N jogadas (balanceado)</summary>
    Top3Aleatorio,

    /// <summary>Seleção por roleta viciada: melhores têm mais chance, mas não garantido</summary>
    Ponderado,

    /// <summary>Prioriza jogadas de alto risco/alto retorno (agressivo)</summary>
    Agressivo
}
}
