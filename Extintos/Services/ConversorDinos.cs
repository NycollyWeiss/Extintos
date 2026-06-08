using Extintos.Enumeration;
using System;

namespace Extintos.Services
{
    //converte nomes ou tipos de dinossauros para os códigos esperados pelo servidor
    public static class ConversorDinos
    {
        public static string ConverterParaCodigo(string tipo)
        {
            var t = tipo.ToUpperInvariant();
            if (t.Contains("TIRANOSSAURO") || t == "TI") return "Ti";
            if (t.Contains("BRAQUIOSSAURO") || t == "BR") return "Br";
            if (t.Contains("ESTEGOSSAURO")  || t == "ET") return "Et";
            if (t.Contains("PARASAUROLOFO") || t == "PA") return "Pa";
            if (t.Contains("ESPINOSSAURO")  || t == "EP") return "Ep";
            if (t.Contains("TRICERATOPS")   || t == "TR") return "Tr";
            return tipo;
        }
        
        public static Dinossauro? StringParaEnum(string raw)
        {
            var t = raw.Trim().ToUpperInvariant();
            
            if (t == "TI" || t.Contains("TIRANOSSAURO")) return Dinossauro.TI;
            if (t == "BR" || t.Contains("BRAQUIOSSAURO")) return Dinossauro.BR;
            if (t == "ET" || t.Contains("ESTEGOSSAURO")) return Dinossauro.ET;
            if (t == "PA" || t.Contains("PARASAUROLOFO")) return Dinossauro.PA;
            if (t == "EP" || t.Contains("ESPINOSSAURO")) return Dinossauro.EP;
            if (t == "TR" || t.Contains("TRICERATOPS")) return Dinossauro.TR;

            if (Enum.TryParse<Dinossauro>(t, true, out var dino)) return dino;

            return null;
        }
    }
}