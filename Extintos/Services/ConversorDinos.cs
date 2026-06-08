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
    }
}