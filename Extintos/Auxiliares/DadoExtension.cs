using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Extintos.Enumeration;

namespace Extintos.Auxiliares;

     public static class DadoExtension
     
    {
        public static DadoFace? PegaInfo(this Dado dado)
        {
            var field = dado.GetType().GetField(dado.ToString());
            return field?.GetCustomAttribute<DadoFace>();
        }

        public static string PegaNome(this Dado dado)
        {
            return dado.PegaInfo()?.Nome ?? dado.ToString();
        }

        public static string PegaRestricao(this Dado dado)
        {
            return dado.PegaInfo()?.Restricao ?? string.Empty;
        }

        public static List<DadoFace> listaDadoFaces(this Dado dado)
        {
            return Enum.GetValues(typeof(Dado)).Cast<DadoFace>().ToList();
        }
        public static List<Cercados> ValidaCercados(this Dado dado)
        {
            switch (dado)
            {
                case Dado.AL:
                    var listaAlimentacao = new List<Cercados>();
                    listaAlimentacao.Add(Cercados.FI);
                    listaAlimentacao.Add(Cercados.MT);
                    listaAlimentacao.Add(Cercados.PA);
                    listaAlimentacao.Add(Cercados.RI);
                    return listaAlimentacao;

                case Dado.FL:
                    var listaFloresta = new List<Cercados>();
                    listaFloresta.Add(Cercados.FI);
                    listaFloresta.Add(Cercados.MT);
                    listaFloresta.Add(Cercados.RI);
                    listaFloresta.Add(Cercados.RS);
                    return listaFloresta;

                case Dado.PR:
                    var listaPradais = new List<Cercados>();
                    listaPradais.Add(Cercados.IS);
                    listaPradais.Add(Cercados.PA);
                    listaPradais.Add(Cercados.CD);
                    listaPradais.Add(Cercados.RI);
                    return listaPradais;

                case Dado.WC:
                    var listaBanheiro = new List<Cercados>();
                    listaBanheiro.Add(Cercados.RI);
                    listaBanheiro.Add(Cercados.RS);
                    listaBanheiro.Add(Cercados.IS);
                    listaBanheiro.Add(Cercados.CD);
                    return listaBanheiro;

                case Dado.TI:
                    var listaTi = new List<Cercados>();
                    listaTi.Add(Cercados.FI);
                    listaTi.Add(Cercados.MT);
                    listaTi.Add(Cercados.PA);
                    listaTi.Add(Cercados.RI);
                    listaTi.Add(Cercados.RS);
                    listaTi.Add(Cercados.IS);
                    return listaTi;
                case Dado.VZ:
                    var listaVz = new List<Cercados>();
                    listaVz.Add(Cercados.FI);
                    listaVz.Add(Cercados.MT);
                    listaVz.Add(Cercados.PA);
                    listaVz.Add(Cercados.RI);
                    listaVz.Add(Cercados.RS);
                    listaVz.Add(Cercados.IS);
                    return listaVz;
                default:
                    var listaTodos = new List<Cercados>();
                    listaTodos.Add(Cercados.FI);
                    listaTodos.Add(Cercados.MT);
                    listaTodos.Add(Cercados.PA);
                    listaTodos.Add(Cercados.RI);
                    listaTodos.Add(Cercados.RS);
                    listaTodos.Add(Cercados.IS);
                    return listaTodos;
            }
        }
    }
