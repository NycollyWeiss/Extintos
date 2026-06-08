#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Extintos.Enumeration;

namespace Extintos.Auxiliares
{
    public static class DinossauroExtension
    {
        private static readonly ConcurrentDictionary<Dinossauro, DinossauroInfo?> InfoCache = new();

        public static DinossauroInfo? GetInfo(this Dinossauro dino)
        {
            return InfoCache.GetOrAdd(dino, d =>
            {
                var field = d.GetType().GetField(d.ToString());
                return field?.GetCustomAttribute<DinossauroInfo>();
            });
        }

        public static string PegaNome(this Dinossauro dino) => dino.GetInfo()?.Nome ?? dino.ToString();
        
        public static string PegaCodigo(this Dinossauro dino) => dino.GetInfo()?.Codigo ?? "??";

        public static List<Dinossauro> EspeciesExistentes()
        {
            return Enum.GetValues(typeof(Dinossauro))
                .Cast<Dinossauro>()
                .ToList();
        }
    }
    //adicionar o pega foto 
}