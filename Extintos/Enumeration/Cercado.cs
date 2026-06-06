using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Extintos.Enumeration;
using Extintos.Auxiliares;

#region Attribute para Metadados dos Cercados

[AttributeUsage(AttributeTargets.Field)]
internal sealed class CercadosInfo : Attribute
{
    public CercadosInfo(string nome, string restricao, string pontuacao, string codigo)
    {
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Restricao = restricao ?? string.Empty;
        Pontuacao = pontuacao ?? string.Empty;
        Codigo = codigo ?? throw new ArgumentNullException(nameof(codigo));
    }

    public string Nome { get; }
    public string Restricao { get; }
    public string Pontuacao { get; }
    public string Codigo { get; }
}

#endregion

#region Enumeração dos Cercados

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

#endregion

#region Extension Methods

internal static class CercadosExtension
{
    private static readonly ConcurrentDictionary<Cercados, CercadosInfo?> _infoCache = new();

    public static CercadosInfo? PegaInfo(this Cercados cercado)
    {
        return _infoCache.GetOrAdd(cercado, c =>
        {
            try
            {
                var field = c.GetType().GetField(c.ToString(), BindingFlags.Public | BindingFlags.Static);
                return field?.GetCustomAttribute<CercadosInfo>();
            }
            catch (Exception)
            {
                return null;
            }
        });
    }


    public static string PegaNome(this Cercados cercado)
    {
        return cercado.PegaInfo()?.Nome ?? cercado.ToString();
    }


    public static string PegaRestricao(this Cercados cercado)
    {
        return cercado.PegaInfo()?.Restricao ?? string.Empty;
    }


    public static string PegaPontuacao(this Cercados cercado)
    {
        return cercado.PegaInfo()?.Pontuacao ?? string.Empty;
    }


    public static string PegaCodigo(this Cercados cercado)
    {
        return cercado.PegaInfo()?.Codigo ?? cercado.ToString();
    }


    public static List<Cercados> CercadosLista()
    {
        return Enum.GetValues(typeof(Cercados))
            .Cast<Cercados>()
            .ToList();
    }

    public static List<AuxCercado> CercadoAuxLista()
    {
        return CercadosLista().Select(c => new AuxCercado(c)).ToList();
    }

    public static bool SePodeColocarNoCercado(this Cercados cercado, List<Dinossauro> dinosNoCercado,
        Dinossauro novoDino)
    {
        if (novoDino == default) return false;
        var dinosCodigos = dinosNoCercado?.Select(d => d.PegaCodigo()).ToList() ?? new List<string>();
        var quantosTemNoCercado = dinosCodigos.Count;
        var novo = novoDino.PegaCodigo();
        return cercado switch
        {
            Cercados.FI => PodeFlorestaIgualdade(dinosCodigos, novo),
            Cercados.CD => PodeCampinaDaDiferenca(dinosCodigos, novo),
            Cercados.IS => quantosTemNoCercado == 0,
            Cercados.RS => quantosTemNoCercado == 0,
            Cercados.MT => quantosTemNoCercado < 3,
            Cercados.PA => quantosTemNoCercado <= 6,
            Cercados.RI => true,
            _ => false
        };
    }


    private static bool PodeFlorestaIgualdade(List<string> dinos, string novoDino)
    {
        if (dinos.Count == 0)
            return true;

        if (dinos.Count >= 6)
            return false;

        if (dinos.Contains(novoDino)) return true;
        return false;
    }

    private static bool PodeCampinaDaDiferenca(List<string> dinos, string novoDino)
    {
        if (dinos.Count == 0)
            return true;

        if (dinos.Count >= 6)
            return false;

        if (!dinos.Contains(novoDino)) return true;


        return false;
    }


    public static int ObterPontuacaoMaxima(this Cercados cercado, int quantidade)
    {
        return cercado switch
        {
            Cercados.FI => quantidade switch
            {
                6 => 24, 5 => 18, 4 => 12, 3 => 8, 2 => 4, 1 => 2, _ => 0
            },
            Cercados.CD => quantidade switch
            {
                6 => 21, 5 => 15, 4 => 10, 3 => 6, 2 => 3, 1 => 1, _ => 0
            },
            Cercados.MT => quantidade == 3 ? 7 : 0,
            Cercados.PA => quantidade / 2 * 5, // 5 pontos por par
            Cercados.RI => quantidade, // 1 ponto por dino
            Cercados.RS => quantidade == 1 ? 7 : 0,
            Cercados.IS => quantidade == 1 ? 7 : 0,
            _ => 0
        };
    }
}

#endregion