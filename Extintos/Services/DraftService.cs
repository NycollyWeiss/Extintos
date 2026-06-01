using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Draft;
using Extintos.Enumeration;
using Extintos.Auxiliares;

namespace Extintos.Services
{
    public class DraftService
    {
        public static async Task<T> ChamarSeguroAsync<T>(Func<T> metodoDll, CancellationToken token,
            int timeoutMs = 5000, bool requerUiThread = true)
        {
            if (requerUiThread && Application.MessageLoop)
            {
                T resultado = default!;
                Exception? excecao = null;
                await Task.Run(() =>
                {
                    try
                    {
                        var ctrl = new Control();
                        ctrl.CreateControl();
                        ctrl.Invoke((MethodInvoker)(() =>
                        {
                            try
                            {
                                Debug.WriteLine($"[DLL CALL] Executando: {metodoDll.Method.Name}");
                                resultado = metodoDll();
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"[DLL ERROR] {ex.Message}");
                                excecao = ex;
                            }
                        }));
                    }
                    catch (Exception ex)
                    {
                        excecao = ex;
                    }
                }, token);
                if (excecao != null) throw excecao;
                return resultado;
            }

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(token);
            cts.CancelAfter(timeoutMs);
            try
            {
                return await Task.Run(metodoDll, cts.Token);
            }
            catch (OperationCanceledException) when (cts.IsCancellationRequested)
            {
                throw new TimeoutException($"Timeout DLL ({timeoutMs}ms)");
            }
        }


        public static async Task<PartidaInfo> ObterEstadoAsync(int idPartida, CancellationToken token)
        {
            var raw = await ChamarSeguroAsync(() => Jogo.VerificarPartida(idPartida), token, 3000);
            return PartidaInfo.Parse(raw);
        }


        public static async Task<List<AuxDinossauro>> ObterMaoAsync(int idJogador, string senha,
            CancellationToken token)
        {
            var raw = await ChamarSeguroAsync(() => Jogo.ExibirMao(idJogador, senha), token, 2000);
            return ParserMao.Parse(raw);
        }

        public static async Task<int> JogarAsync(
            int idJogador,
            string senha,
            string codigoDino,
            string codigoCercado,
            CancellationToken token)
        {
            var retorno = await ChamarSeguroAsync(
                () => Jogo.Jogar(idJogador, senha, codigoDino, codigoCercado),
                token,
                4000);

            Debug.WriteLine($"[JOGAR RETORNO] {retorno}");

            var match = Regex.Match(retorno, @"\d+");
            if (match.Success && int.TryParse(match.Value, out var proximoTurno))

                return proximoTurno;
            if (string.IsNullOrWhiteSpace(retorno))
                throw new Exception("Resposta vazia da DLL.");


            if (retorno.IndexOf("não está em andamento", StringComparison.OrdinalIgnoreCase) >= 0)
                throw new InvalidOperationException("Partida não iniciada ou já encerrada.");

            if (retorno.IndexOf("não é sua vez", StringComparison.OrdinalIgnoreCase) >= 0)
                throw new InvalidOperationException("Não é o turno do jogador.");

            if (retorno.IndexOf("jogada inválida", StringComparison.OrdinalIgnoreCase) >= 0)
                throw new InvalidOperationException("Jogada inválida.");

            throw new Exception($"Resposta inesperada da DLL: {retorno}");
        }


        public static async Task<List<JogadorInfo>> ObterJogadoresAsync(int idPartida, CancellationToken token)
        {
            // A DLL retorna List<Jogador> diretamente. Forçamos o tipo para o compilador entender.
            var listaDll = await ChamarSeguroAsync(
                () => Partida.ListarJogadores(idPartida), token, 3000);

            return listaDll.Select(j => new JogadorInfo
            {
                Id = j.IdJogador,
                Nome = j.NomeJogador ?? "Bot",
                Pontuacao = j.Pontuacao
            }).ToList();
        }


        public static async Task<string> ListarCercadosAsync(CancellationToken token)
        {
            return await ChamarSeguroAsync(Jogo.ListarCercados, token);
        }

        public static async Task<string> ListarFacesAsync(CancellationToken token)
        {
            return await ChamarSeguroAsync(Jogo.ListarFacesDado, token);
        }
    }


    public class PartidaInfo
    {
        public char StatusPartida { get; set; } // J ou E
        public int TurnoAtual { get; set; }
        public char StatusTurno { get; set; } // A ou F
        public int IdJogadorDaVez { get; set; }
        public string FaceDado { get; set; } = string.Empty;

        public static PartidaInfo Parse(string csv)
        {
            var p = csv.Split(',');
            return new PartidaInfo
            {
                StatusPartida = p[0][0],
                TurnoAtual = int.Parse(p[1]),
                StatusTurno = p[2][0],
                IdJogadorDaVez = int.Parse(p[3]),
                FaceDado = p[4].Trim()
            };
        }
    }

    public class JogadorInfo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Pontuacao { get; set; }
    }

    public static class ParserJogadores
    {
        public static List<JogadorInfo> Parse(string raw)
        {
            var list = new List<JogadorInfo>();
            foreach (var linha in raw.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var p = linha.Split(',');
                if (p.Length >= 3 && int.TryParse(p[0], out var id) && int.TryParse(p[2], out var pts))
                    list.Add(new JogadorInfo { Id = id, Nome = p[1], Pontuacao = pts });
            }

            return list;
        }
    }

    public static class ParserMao
    {
        public static List<AuxDinossauro> Parse(string raw)
        {
            var list = new List<AuxDinossauro>();
            foreach (var linha in raw.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var p = linha.Split(',');
                if (p.Length == 2 && Enum.TryParse<Dinossauro>(p[0].Trim(), true, out var dino) &&
                    int.TryParse(p[1], out var qtd))
                    list.Add(new AuxDinossauro(dino, qtd));
            }

            return list;
        }
    }

    public static class ParseTurno
    {
        public static List<AuxDinossauro> ParserDinossauros(string raw, int turno)
        {
            var list = new List<AuxDinossauro>();

            foreach (var linha in raw.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Skip(1))
            {
                var p = linha.Split(',');

                if (p.Length >= 3 && Enum.TryParse<Dinossauro>(p[0].Trim(), true, out var dino))
                    list.Add(new AuxDinossauro(dino, 1));
            }

            return list;
        }
    }

    public class DadosOponete
    {
        public static List<AuxDinossauro> ParserDinosOponente(string raw, int meuId)
        {
            var list = new List<AuxDinossauro>();

            foreach (var linha in raw.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Skip(1))
            {
                var p = linha.Split(',');

                if (p.Length >= 3
                    && int.TryParse(p[0].Trim(), out var idJogador)
                    && idJogador != meuId
                    && Enum.TryParse<Dinossauro>(p[1].Trim(), true, out var dino))
                    list.Add(new AuxDinossauro(dino, 1));
            }

            return list;
        }
    }
}