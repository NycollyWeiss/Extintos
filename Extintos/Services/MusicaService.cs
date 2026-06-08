using System;
using System.Diagnostics;
using System.IO;
using System.Media;

namespace Extintos.Services
{
    internal static class MusicaService
    {
        private const string NomeArquivo = "Frank Ocean - American Wedding (Lyrics) [qrin3JE4HhU].wav";
        private static SoundPlayer _player;

        public static void Iniciar()
        {
            if (_player != null)
            {
                return;
            }

            string caminhoMusica = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", NomeArquivo);

            if (!File.Exists(caminhoMusica))
            {
                Debug.WriteLine($"Musica nao encontrada: {caminhoMusica}");
                return;
            }

            try
            {
                _player = new SoundPlayer(caminhoMusica);
                _player.PlayLooping();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Falha ao tocar musica: {ex.Message}");
                Parar();
            }
        }

        public static void Parar()
        {
            try
            {
                _player?.Stop();
            }
            finally
            {
                _player?.Dispose();
                _player = null;
            }
        }
    }
}