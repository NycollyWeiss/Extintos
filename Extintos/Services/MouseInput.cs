using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Extintos
{
    public static class MouseInput
    {
        private const uint INPUT_MOUSE = 0;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, [In] INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private static void SendMouseInput(uint flags, int dx, int dy)
        {
            try
            {
                var inputs = new INPUT[1];
                inputs[0].type = INPUT_MOUSE;
                inputs[0].mi = new MOUSEINPUT { dx = dx, dy = dy, mouseData = 0, dwFlags = flags, time = 0, dwExtraInfo = IntPtr.Zero };
                var result = SendInput(1, inputs, Marshal.SizeOf<INPUT>());
                if (result == 0) Debug.WriteLine($"[MouseInput] SendInput falhou: {Marshal.GetLastWin32Error()}");
            }
            catch (Exception ex) { Debug.WriteLine($"[MouseInput] Exceção: {ex.Message}"); }
        }

        public static void MoveTo(int x, int y) => SetCursorPos(x, y);
        public static void LeftDown() => SendMouseInput(MOUSEEVENTF_LEFTDOWN, 0, 0);
        public static void LeftUp() => SendMouseInput(MOUSEEVENTF_LEFTUP, 0, 0);

        public static async Task MoveToAnimated(Point start, Point end, int durationMs = 500,
            IProgress<float>? progress = null, CancellationToken cancellationToken = default)
        {
            const int steps = 40; // Mais passos = mais liso
            var delayPerStep = Math.Max(1, durationMs / steps);

            for (var i = 0; i <= steps; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var linearProgress = (float)i / steps;

                // Easing in-out
                var eased = linearProgress < 0.5f ? 2 * linearProgress * linearProgress : 1 - (float)Math.Pow(-2 * linearProgress + 2, 2) / 2;

                var x = (int)(start.X + (end.X - start.X) * eased);
                var y = (int)(start.Y + (end.Y - start.Y) * eased);
                MoveTo(x, y);
                progress?.Report(eased);

                // A MÁGICA CONTRA O LAG: Removido o ConfigureAwait(false). Mantém na Thread da UI!
                await Task.Delay(delayPerStep, cancellationToken);
            }
        }

        [StructLayout(LayoutKind.Sequential)] private struct INPUT { public uint type; public MOUSEINPUT mi; }
        [StructLayout(LayoutKind.Sequential)] private struct MOUSEINPUT { public int dx; public int dy; public uint mouseData; public uint dwFlags; public uint time; public IntPtr dwExtraInfo; }
    }
}