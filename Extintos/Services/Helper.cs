using System;

namespace Extintos
{
    public static class Helper
    {
        private static readonly Random _random = new();
        private static readonly object _lock = new();

        public static int Next(int min, int max)
        {
            lock (_lock)
            {
                return _random.Next(min, max);
            }
        }
    }
}