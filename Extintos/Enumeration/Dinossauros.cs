using System;
using System.Reflection;

namespace Extintos.Enumeration
{
    // 1. O Atributo que guarda as informações "meta" dos dinossauros
    [AttributeUsage(AttributeTargets.Field)]
    internal class DinossauroInfoAttribute : Attribute
    {
        public string Nome { get; }
        public string Codigo { get; }
        public string Cor { get; }

        public DinossauroInfoAttribute(string nome, string codigo, string cor)
        {
            Nome = nome;
            Codigo = codigo;
            Cor = cor;
        }
    }

    // 2. O Enum com os dados populados via Atributo
    public enum EnumDinos
    {
        [DinossauroInfo("Braquiossauro", "BR", "Roxo")]
        BR,
        [DinossauroInfo("Espinossauro", "EP", "Laranja")]
        EP,
        [DinossauroInfo("Estegossauro", "ET", "Azul")]
        ET,
        [DinossauroInfo("Parasaurolófo", "PA", "Verde")]
        PA,
        [DinossauroInfo("Tiranossauro", "TI", "Vermelho")]
        TI,
        [DinossauroInfo("Tricerátops", "TR", "Amarelo")]
        TR
    }

    // 3. Métodos de Extensão para "pegar" os dados do Enum facilmente
    internal static class DinoExtension
    {
        public static DinossauroInfoAttribute GetInfo(this EnumDinos dino)
        {
            var field = dino.GetType().GetField(dino.ToString());
            return field?.GetCustomAttribute<DinossauroInfoAttribute>();
        }

        public static string PegaNome(this EnumDinos dino)
        {
            return dino.GetInfo()?.Nome ?? dino.ToString();
        }

        public static string PegaCor(this EnumDinos dino)
        {
            return dino.GetInfo()?.Cor ?? "Cor não definida";
        }

        public static string PegaCodigo(this EnumDinos dino)
        {
            return dino.GetInfo()?.Codigo ?? "??";
        }
    }
}
