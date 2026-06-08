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
using Extintos.Model;

namespace Extintos.Services
{ //arrumar os metodos, validar oq faz sentido ficar nas outras claases
    public class DraftService
    {
        public static List<AuxDinossauro> ObterMao(int idJogador, string senhaJogador)
        {
            var raw = Jogo.ExibirMao(idJogador, senhaJogador);
            return ParserMao.Parse(raw);
        }
        
        public static string EntrarPartida(int idPartida, string nomeJogador, string senhaPartida)
        {
            return Jogo.Entrar(idPartida, nomeJogador, senhaPartida);
        }
        
        public static string CriarPartida(string nomePartida, string senhaPartida, string nomeGrupo)
        {
            return Jogo.CriarPartida(nomePartida, senhaPartida, nomeGrupo);
        }

        public static string ListarJogadores(int idPartida)
        {
            return Jogo.ListarJogadores(idPartida);
        }
        
        public static string ObterHistoricoBruto(int idPartida)
        {
            return Jogo.ListarHistorico(idPartida);
        }
        
        public static string VerificarPartidaBruto(
            int idPartida)
        {
            return Jogo.VerificarPartida(idPartida);
        }
        public static string Jogar(int idJogador, string senha, string codigoDino, string codigoCercado)
        {
            return Jogo.Jogar(idJogador, senha, codigoDino, codigoCercado);
        }
        public static string Versao => Jogo.versao;
        
        public static string ObterTurnos(int idPartida, int quantidade)
        {
            return Jogo.VerificarTurno(idPartida, quantidade);
        }
        
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
        
        public static async Task<PartidasInfo.PartidaInfo> ObterEstadoAsync(int idPartida, CancellationToken token)
        {
            var raw = await ChamarSeguroAsync(() => Jogo.VerificarPartida(idPartida), token, 3000);
            return new PartidasInfo.PartidaInfo(raw);
        }
        
        public static async Task<List<AuxDinossauro>> PegarMaoAsync(int idJogador, string senha,
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
            var match = Regex.Match(retorno, @"\d+"); //esse treco aqui é pra pegar um padrao dentro do 
            //texto e retorna quando acha o primeiro, esse @\d+ é pra pegar um digito de 0-9 é 
            //pra ter 100% de certeza que vai ter o retorno numerico,pra n dar aquele bug do carai 
            //eu achei q era igual o Matcher do java por isso tava dando erro, foi mal :/
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


        public static async Task<List<Jogador>> PegaJogadoresAsync(int idPartida, CancellationToken token)
        {

            var raw = await ChamarSeguroAsync(
                () => ListarJogadores(idPartida), token, 3000);

            return ParserJogadores.Parse(raw);
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


    public static class ParserJogadores
    {
            public static List<Jogador> Parse(string raw)
            {
                var list = new List<Jogador>();
                foreach (var linha in raw.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    String[] p = linha.Split(',');
            
                    
                    if (p.Length >= 2 && int.TryParse(p[0], out var id) &&  int.TryParse(p[2], out  int pts))                    {
                    
                        list.Add(new Jogador { IdJogador = id, NomeJogador = p[1], Pontuacao = pts });
                    }
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

        public static List<Tabuleiro.JogadaOponente> ParserJogadaOponente(string raw, int meuId, int turnoSelecionado)
        {
            var list = new List<Tabuleiro.JogadaOponente>();

            foreach (var linha in raw.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Skip(1))
            {
                var p = linha.Split(',');

                if (p.Length >= 3
                    && int.TryParse(p[0].Trim(), out var idJogador)
                    && idJogador != meuId
                    && Enum.TryParse<Cercados>(p[1].Trim(), true, out var cercado)
                    && Enum.TryParse<Dinossauro>(p[2].Trim(), true, out var dino))
                  
                {
                    list.Add(new Tabuleiro.JogadaOponente(idJogador, dino, cercado, turnoSelecionado));
                }
            }
            return list;

        }
    }
}