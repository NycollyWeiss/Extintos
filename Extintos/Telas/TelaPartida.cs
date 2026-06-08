using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extintos.Auxiliares;
using Extintos.Enumeration;
using Extintos.Interfaces;
using Extintos.Services;
using Extintos.Model;
using Extintos.Properties;
using Extintos.LeonKennedy;
using Timer = System.Windows.Forms.Timer;

namespace Extintos.Telas
{
    public partial class TelaPartida : Form
    {
        #region Campos e Propriedades

        private readonly Jogador _dadosJogador;
        private bool _maoAberta;
        private int _rodadaAtual = 1;
        private int _turnoAtual = 1;
        private string _nomeJogadorDado = "";
        private int _ultimoTurnoExibido = -1;
        private bool _animacaoBotEmAndamento;
        private bool _jogandoAgora;

        private readonly TabuleiroLayout _layout = new();
        private readonly FabricaDinosService _fabricaDinos = new();
        private BotService _botService;
        private IEstategia _estrategia;
        private ConfigEstrategia _config;

        private CancellationTokenSource _cts;
        private bool _trabalhando;
        private Timer _timerRobozinho;
        private bool _movimentoDoBot;

        private readonly Dictionary<int, string> _nomesJogadores = new();
        private readonly List<DinoNoTabuleiro> _dinosFixosNoTabuleiro = new();
        private readonly List<DinossauroVisual> _dinos = new();
        private DinossauroVisual _dinoSelecionado;
        private List<AuxDinossauro> _ultimaMaoRecebida = new();

        private readonly Image _imgTabuleiro = Resources.TabuleiroDrafto;
        private readonly Image _imgMaoAberta = Resources.MaoAberta;
        private readonly Image _imgMaoFechada = Resources.MaoFechada;

        #endregion

        #region Construtores e Inicialização

        public TelaPartida()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            lblVersaoTres.Text = DraftService.Versao;

            ConfigurarSistema();
            ConfigurarJanela();
        }

        internal TelaPartida(Jogador dadosJogador) : this()
        {
            _dadosJogador = dadosJogador;
        }

        private void ConfigurarSistema()
        {
            _config = new ConfigEstrategia();
            _estrategia = new EstrategiaGulosa(_config);
        }

        private void ConfigurarJanela()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            Size = new Size(
                Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height);
            WindowState = FormWindowState.Maximized;
        }

        private void FormDraftosaurus_Load(object sender, EventArgs e)
        {
            txtHistorico.Left = ClientSize.Width - txtHistorico.Width - 120;
            txtHistorico.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            frmTimer.Interval = 3000;
            frmTimer.Tick += frmTimer_Tick;  // ← minúsculo
            frmTimer.Start();

            _timerRobozinho = new Timer { Interval = 5000 };
            _timerRobozinho.Tick += TimerRobozinho_Tick;
            _timerRobozinho.Start();

            _botService = new BotService(_dadosJogador, _estrategia);
            _botService.JogadaConfirmada += ExecutarMovimentoVisual;

            _ = CarregarNomesJogadoresAsync();

            picDado.BringToFront();
            picDado.Visible = true;

            Console.WriteLine("[TelaPartida] Inicializado com sucesso");
        }

        private async void frmTimer_Tick(object sender, EventArgs e)  // ← minúsculo
        {
            frmTimer.Stop();
            try
            {
                await AtualizarDadoAsync();
            }
            finally
            {
                frmTimer.Start();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            frmTimer.Stop();
            _timerRobozinho?.Stop();
            _cts?.Cancel();
            _cts?.Dispose();
            _timerRobozinho?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion

        #region Comunicação com Servidor

        public void bntExibirMao_Click(object sender, EventArgs e)
        {
            if (_animacaoBotEmAndamento)
            {
                Console.WriteLine("[ExibirMao] Animação em andamento, ignorando");
                return;
            }

            try
            {
                Console.WriteLine("[ExibirMao] Solicitando mão do servidor...");
                var dinossaurosJogador = DraftService.ObterMao(
                    _dadosJogador.IdJogador,
                    _dadosJogador.Senha);

                if (dinossaurosJogador == null || dinossaurosJogador.Count == 0)
                {
                    Console.WriteLine($"[ExibirMao] Mão vazia ou nula! Count={dinossaurosJogador?.Count ?? -1}");
                    return;
                }

                Console.WriteLine($"[ExibirMao] Recebidos {dinossaurosJogador.Count} dinossauros");
                CriarDinos(dinossaurosJogador);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExibirMao ERROR] {ex.Message}");
                Console.WriteLine($"[ExibirMao STACK] {ex.StackTrace}");
            }
        }

        private async Task CarregarNomesJogadoresAsync()
        {
            try
            {
                var jogadores = await DraftService.PegaJogadoresAsync(
                    _dadosJogador.IdPartida,
                    CancellationToken.None);

                _nomesJogadores.Clear();
                foreach (var j in jogadores)
                    _nomesJogadores[j.IdJogador] = j.NomeJogador;

                Console.WriteLine($"[Nomes] Carregados {_nomesJogadores.Count} jogadores");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Nomes ERROR] {ex.Message}");
            }
        }

        private void ListarHistorico()
        {
            try
            {
                var retorno = DraftService.ObterHistoricoBruto(_dadosJogador.IdPartida);
                txtHistorico.Text = retorno.Replace(".", ".\r\n");
                txtHistorico.ScrollBars = ScrollBars.Both;
                txtHistorico.Multiline = true;
                txtHistorico.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Historico ERROR] {ex.Message}");
            }
        }

        #endregion

        #region Desenho e Interface Visual

        private void CriarDinos(List<AuxDinossauro> maoJogador)
        {
            _ultimaMaoRecebida = maoJogador;
            _maoAberta = true;

            var centroX = _layout.MaoX + 105;
            var centroY = _layout.MaoY + 160;

            _dinos.Clear();
            _dinos.AddRange(_fabricaDinos.Criar(maoJogador, centroX, centroY));

            Invalidate();
        }

        private void frmPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            _layout.Atualizar(ClientSize.Width, ClientSize.Height);
            g.DrawImage(_imgTabuleiro, _layout.TabX, _layout.TabY, _layout.TabTamanho, _layout.TabTamanho);
            
            var cercados = _layout.ObterCercadosMapeados();
            foreach (var c in cercados)
            {
                g.DrawRectangle(Pens.Red, c.Value);
                g.DrawString(c.Key, Font, Brushes.Red, c.Value.X, c.Value.Y);
                foreach (var lista in _layout.PosicoesCercados)
                    foreach (var p in lista.Value)
                        g.DrawRectangle(Pens.Blue, p.X, p.Y, 20, 20);
            }


            foreach (var dinoFixo in _dinosFixosNoTabuleiro)
            {
                var tipoEnum = (Dinossauro)Enum.Parse(typeof(Dinossauro), dinoFixo.Tipo.ToUpper());
                var img = ProvedorDeImagens.PegarImagemDinossauro(tipoEnum);
                g.DrawImage(img, dinoFixo.Area.X, dinoFixo.Area.Y, dinoFixo.Area.Width, dinoFixo.Area.Height);
            }

            var imgMao = _maoAberta ? _imgMaoAberta : _imgMaoFechada;
            g.DrawImage(imgMao, _layout.MaoX, _layout.MaoY, _layout.MaoLargura, _layout.MaoAltura);

            g.DrawString($"Rodada: {_rodadaAtual}  -  Turno: {_turnoAtual}",
                new Font("Segoe UI", 20, FontStyle.Bold), Brushes.White, 30, 30);

            if (!string.IsNullOrEmpty(_nomeJogadorDado) && picDado.Visible)
            {
                g.DrawString($"{_nomeJogadorDado} jogou o dado",
                    new Font("Segoe UI", 18, FontStyle.Bold), Brushes.White,
                    picDado.Location.X - 20, picDado.Location.Y - 60);
            }

            foreach (var d in _dinos)
                g.DrawImage(d.imagem, d.posicao.X, d.posicao.Y, d.largura, d.altura);
        }

        #endregion

        #region Lógica de Mouse

        private void SelecionarDinossauro(Point posicao)
        {
            for (var i = _dinos.Count - 1; i >= 0; i--)
            {
                if (!_dinos[i].area.Contains(posicao)) continue;

                _dinoSelecionado = _dinos[i];
                _dinoSelecionado.ativo = true;
                _dinos.RemoveAt(i);
                _dinos.Add(_dinoSelecionado);
                break;
            }
        }

        private void frmMouseDown(object sender, MouseEventArgs e)
        {
            var areaMao = new Rectangle(_layout.MaoX, _layout.MaoY, _layout.MaoLargura, _layout.MaoAltura);
            if (areaMao.Contains(e.Location) && !_maoAberta)
            {
                bntExibirMao_Click(null, null);
                return;
            }

            SelecionarDinossauro(e.Location);
        }

        private void AtualizarPosicaoDinoSelecionado(Point posicao)
        {
            if (_dinoSelecionado == null) return;

            _dinoSelecionado.posicao.X = posicao.X - _dinoSelecionado.largura / 2;
            _dinoSelecionado.posicao.Y = posicao.Y - _dinoSelecionado.altura / 2;
            _dinoSelecionado.area = new Rectangle(
                _dinoSelecionado.posicao.X,
                _dinoSelecionado.posicao.Y,
                _dinoSelecionado.largura,
                _dinoSelecionado.altura);
        }

        private void frmMouseMove(object sender, MouseEventArgs e)
        {
            if (_dinoSelecionado == null) return;
            AtualizarPosicaoDinoSelecionado(e.Location);
            Invalidate();
        }

        private void FixarDinoSelecionadoNoTabuleiro(string cercado)
        {
            if (_dinoSelecionado == null) return;

            var posicao = _layout.ObterPosicaoLivre(cercado, _dinosFixosNoTabuleiro);
            Console.WriteLine($"[Fixar] {cercado} | X={posicao.X} Y={posicao.Y}");

            _dinosFixosNoTabuleiro.Add(new DinoNoTabuleiro
            {
                Tipo = _dinoSelecionado.Tipo,
                Cercado = cercado,
                Area = new Rectangle(posicao.X - 20, posicao.Y - 20, 60, 60)
            });

            _dinos.Remove(_dinoSelecionado);
            _dinoSelecionado = null;
            Invalidate();
        }

        private async void frmMouseUp(object sender, MouseEventArgs e)
        {
            if (_dinoSelecionado == null) return;
            if (_jogandoAgora) return;

            var dinoParaJogar = _dinoSelecionado;
            _dinoSelecionado = null;

            var cercadosMapeados = _layout.ObterCercadosMapeados();
            var jogadaRealizada = false;

            try
            {
                foreach (var cercado in cercadosMapeados)
                {
                    if (!cercado.Value.Contains(e.Location)) continue;

                    if (_movimentoDoBot)
                    {
                        jogadaRealizada = true;
                        break;
                    }

                    _jogandoAgora = true;
                    var codigoDino = ConversorDinos.ConverterParaCodigo(dinoParaJogar.Tipo);

                    Console.WriteLine($"[Jogar] dino={codigoDino} cercado={cercado.Key}");

                    await DraftService.JogarAsync(
                        _dadosJogador.IdJogador,
                        _dadosJogador.Senha,
                        codigoDino,
                        cercado.Key,
                        CancellationToken.None);

                    _dinoSelecionado = dinoParaJogar;
                    FixarDinoSelecionadoNoTabuleiro(cercado.Key);
                    _dinoSelecionado = null;

                    jogadaRealizada = true;
                    bntExibirMao_Click(null, null);
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Jogar ERROR] {ex.Message}");
                MessageBox.Show("Erro ao jogar: " + ex.Message);
            }
            finally
            {
                _jogandoAgora = false;
            }

            if (!jogadaRealizada)
                CriarDinos(_ultimaMaoRecebida);

            Invalidate();
        }

        #endregion

        #region Bot - Animação Visual

        private DinossauroVisual BuscarDinoVisual(string codigoDino)
        {
            foreach (var d in _dinos)
            {
                var codigoAtual = ConversorDinos.ConverterParaCodigo(d.Tipo);
                if (codigoAtual.Equals(codigoDino, StringComparison.OrdinalIgnoreCase))
                    return d;
            }
            return null;
        }

        private void FinalizarMovimentoBot(DinossauroVisual dino, string cercado)
        {
            _dinoSelecionado = dino;
            FixarDinoSelecionadoNoTabuleiro(cercado);
        }

        private async Task ExecutarMovimentoVisual(string codigoDino, string siglaCercado)
        {
            try
            {
                _animacaoBotEmAndamento = true;
                _movimentoDoBot = true;
                Console.WriteLine($"[Bot] Animação: {codigoDino} → {siglaCercado}");

                var dinoParaMover = BuscarDinoVisual(codigoDino);
                if (dinoParaMover == null)
                {
                    Console.WriteLine($"[Bot] Dino {codigoDino} não encontrado na mão!");
                    return;
                }

                var cercadosMapeados = _layout.ObterCercadosMapeados();
                if (!cercadosMapeados.ContainsKey(siglaCercado))
                {
                    Console.WriteLine($"[Bot] Cercado {siglaCercado} não encontrado!");
                    return;
                }

                var slotDestino = _layout.ObterPosicaoLivre(siglaCercado, _dinosFixosNoTabuleiro);
                var posicaoInicial = new Point(
                    PointToScreen(dinoParaMover.posicao).X + dinoParaMover.largura / 2,
                    PointToScreen(dinoParaMover.posicao).Y + dinoParaMover.altura / 2);

                MouseInput.MoveTo(posicaoInicial.X, posicaoInicial.Y);
                await Task.Delay(2000);

                Invoke(new Action(() => frmMouseDown(this, new MouseEventArgs(
                    MouseButtons.Left, 1,
                    dinoParaMover.posicao.X + dinoParaMover.largura / 2,
                    dinoParaMover.posicao.Y + dinoParaMover.altura / 2, 0))));

                MouseInput.LeftDown();
                await Task.Delay(3000);

                const int passos = 20;
                for (var i = 0; i <= passos; i++)
                {
                    var progresso = (float)i / passos;
                    var x = (int)(dinoParaMover.posicao.X + (slotDestino.X - dinoParaMover.posicao.X) * progresso);
                    var y = (int)(dinoParaMover.posicao.Y + (slotDestino.Y - dinoParaMover.posicao.Y) * progresso);

                    Invoke(new Action(() =>
                        frmMouseMove(this, new MouseEventArgs(MouseButtons.Left, 0, x, y, 0))));

                    await Task.Delay(15);
                }

                await Task.Delay(1000);
                MouseInput.LeftUp();

                var dinoMovido = dinoParaMover;
                Invoke(new Action(() => FinalizarMovimentoBot(dinoMovido, siglaCercado)));

                Console.WriteLine("[Bot] Animação concluída!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Bot ERROR] {ex.Message}");
                Console.WriteLine($"[Bot STACK] {ex.StackTrace}");
            }
            finally
            {
                _animacaoBotEmAndamento = false;
                _movimentoDoBot = false;
            }
        }

        #endregion

        #region Timers e Atualização

        private async void FrmTimer_Tick(object sender, EventArgs e)
        {
            frmTimer.Stop();
            try
            {
                await AtualizarDadoAsync();
            }
            finally
            {
                frmTimer.Start();
            }
        }

        private async Task AtualizarDadoAsync()
        {
            if (_dadosJogador == null) return;

            try
            {
                var info = await Task.Run(() => Partida.VerificaPartida(_dadosJogador.IdPartida));

                Console.WriteLine($"[Turno] info.numeroTurno={info.numeroTurno} _ultimoTurnoExibido={_ultimoTurnoExibido} animacao={_animacaoBotEmAndamento}");

                if (info.numeroTurno == _ultimoTurnoExibido) return;

                if (!_animacaoBotEmAndamento)
                    bntExibirMao_Click(null, null);

                var imagemDado = ProvedorDeImagens.PegarImagemDado(info.faceDado.Trim());
                if (imagemDado != null)
                    picDado.Image = imagemDado;

                _ultimoTurnoExibido = info.numeroTurno;
                _turnoAtual = info.numeroTurno;
                _rodadaAtual = ((_turnoAtual - 1) / 6) + 1;

                if (_nomesJogadores.ContainsKey(info.idJogador))
                    _nomeJogadorDado = _nomesJogadores[info.idJogador];

                ListarHistorico();
                Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AtualizarDado ERROR] {ex.Message}");
            }
        }

        private async void TimerRobozinho_Tick(object sender, EventArgs e)
        {
            if (_trabalhando) return;

            _timerRobozinho.Stop();
            _trabalhando = true;

            try
            {
                var dadosVerificacao = DraftService.VerificarPartidaBruto(_dadosJogador.IdPartida);
                var status = dadosVerificacao.Split(',')[0];

                Console.WriteLine($"[Bot] Status retornado: '{status}' (raw: '{dadosVerificacao}')");

                if (status == "J")
                {
                    _cts?.Cancel();
                    _cts = new CancellationTokenSource();
                    await _botService.ExecutarTurnoAsync(_cts.Token);
                }
                else
                {
                    Console.WriteLine("[Bot] Aguardando turno...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Bot TIMER ERROR] {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"[Bot INNER] {ex.InnerException.Message}");
            }
            finally
            {
                _trabalhando = false;
                _timerRobozinho.Start();
            }
        }

        #endregion
    }
}
