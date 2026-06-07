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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _timerRobozinho?.Stop();
            _timerRobozinho?.Dispose();
            base.OnFormClosing(e);
        }

        #region Campos e Propriedades

        private readonly Jogador _dadosJogador;
        private bool maoAberta;
        private int rodadaAtual = 1;
        private int turnoAtual = 1;
        private string nomeJogadorDado = "";
        private int ultimoTurnoExibido = -1;
        private bool _animacaoBotEmAndamento;
        private readonly TabuleiroLayout _layout = new();
        private readonly FabricaDinosService _fabricaDinos = new();
        private BotService _botService;
        private IEstategia estrategia;
        private ConfigEstrategia _config;

        private CancellationTokenSource _cts;
        private bool _trabalhando;
        private Timer _timerRobozinho;
        
        private bool _movimentoDoBot; // Flag para saber se o movimento é do bot

        // Dicionários e listas
        private readonly Dictionary<int, string> nomesJogadores = new();
        private readonly List<DinoNoTabuleiro> dinosFixosNoTabuleiro = new();
        private readonly List<DinossauroVisual> dinos = new();
        private DinossauroVisual dinoSelecionado;
        private List<AuxDinossauro> ultimaMaoRecebida = new();

        // Imagens em cache
        private readonly Image imgTabuleiro = Resources.TabuleiroDrafto;
        private readonly Image imgMaoAberta = Resources.MaoAberta;
        private readonly Image imgMaoFechada = Resources.MaoFechada;

        #endregion

        #region Construtores e Inicialização

        public TelaPartida()
        {
            InitializeComponent();
            
            DoubleBuffered = true;
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer,
                true);
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

            estrategia =
                new EstrategiaGulosa(_config);
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
            frmTimer.Start();

            _timerRobozinho = new Timer { Interval = 5000 };
            _timerRobozinho.Tick += TimerRobozinho_Tick;
            _timerRobozinho.Start();

            _botService = new BotService(_dadosJogador, estrategia);

            _botService.JogadaConfirmada += ExecutarMovimentoVisual;

            CarregarNomesJogadores();

            picDado.BringToFront();
            picDado.Visible = true;
        }

        #endregion

        #region Comunicação com Servidor

        public void bntExibirMao_Click(object sender, EventArgs e)
        {
            if (_animacaoBotEmAndamento)
                return;
            try
            {
                var dinossaurosJogador = DraftService.ObterMao(_dadosJogador.IdJogador, _dadosJogador.Senha);

                if (dinossaurosJogador == null || dinossaurosJogador.Count == 0)
                    return;

                CriarDinos(dinossaurosJogador);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        private void CarregarNomesJogadores()
        {
            try
            {
                var retorno = DraftService.ListarJogadoresBruto(_dadosJogador.idPartida);
                var linhas = retorno.Replace("\r", "")
                    .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                nomesJogadores.Clear();
                foreach (var linha in linhas)
                {
                    var dados = linha.Split(',');
                    if (dados.Length >= 2) nomesJogadores.Add(int.Parse(dados[0].Trim()), dados[1].Trim());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ListarHistorico()
        {
            try
            {
                var retorno = DraftService.ObterHistoricoBruto(_dadosJogador.idPartida);
                txtHistorico.Text = retorno.Replace(".", ".\r\n");
                txtHistorico.ScrollBars = ScrollBars.Both;
                txtHistorico.Multiline = true;
                txtHistorico.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        #endregion

        #region Desenho e Interface Visual

        private void CriarDinos(List<AuxDinossauro> maoJogador)
        {
            ultimaMaoRecebida = maoJogador;
            maoAberta = true;

            var centroX = _layout.MaoX + 105;
            var centroY = _layout.MaoY + 160;

            dinos.Clear();
            dinos.AddRange(_fabricaDinos.Criar(maoJogador, centroX, centroY));

            Invalidate();
        }

        private void frmPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            _layout.Atualizar(ClientSize.Width, ClientSize.Height);

            g.DrawImage(imgTabuleiro, _layout.TabX, _layout.TabY, _layout.TabTamanho, _layout.TabTamanho);

#if DEBUG

            var cercados = _layout.ObterCercadosMapeados();

            //Aqui está os retângulos e quadrados dos cercados e slots onde os dinos vão aparecer!
            //Se quiser que aquilo suma, só comentar!! Evitar apagar por conta dos testes!!
            foreach (var c in cercados)
            {
                g.DrawRectangle(Pens.Red, c.Value);

                g.DrawString(c.Key, Font, Brushes.Red, c.Value.X, c.Value.Y);
                
                foreach (var lista in _layout.PosicoesCercados)
                {
                    foreach (var p in lista.Value)
                    {
                        g.DrawRectangle(Pens.Blue, p.X, p.Y, 20, 20);
                    }
                }
            }

#endif
            
            foreach (var dinoFixo in dinosFixosNoTabuleiro)
            {
                var tipoEnum = (Dinossauro)Enum.Parse(typeof(Dinossauro), dinoFixo.Tipo.ToUpper());
                var img = ProvedorDeImagens.PegarImagemDinossauro(tipoEnum);
                g.DrawImage(img, dinoFixo.Area.X, dinoFixo.Area.Y, dinoFixo.Area.Width, dinoFixo.Area.Height);
            }


            var imgMao = maoAberta ? imgMaoAberta : imgMaoFechada;
            g.DrawImage(imgMao, _layout.MaoX, _layout.MaoY, _layout.MaoLargura, _layout.MaoAltura);


            g.DrawString($"Rodada: {rodadaAtual}  -  Turno: {turnoAtual}",
                new Font("Segoe UI", 20, FontStyle.Bold), Brushes.White, 30, 30);


            if (!string.IsNullOrEmpty(nomeJogadorDado) && picDado.Visible)
            {
                var textX = picDado.Location.X - 20;
                var textY = picDado.Location.Y - 60;
                g.DrawString($"{nomeJogadorDado} jogou o dado",
                    new Font("Segoe UI", 18, FontStyle.Bold), Brushes.White, textX, textY);
            }


            foreach (var d in dinos) g.DrawImage(d.imagem, d.posicao.X, d.posicao.Y, d.largura, d.altura);
        }
        #endregion

        #region Lógica de Mouse (Clicar e Arrastar)

        private void SelecionarDinossauro(Point posicao)
        {
            for (var i = dinos.Count - 1; i >= 0; i--)
                if (dinos[i].area.Contains(posicao))
                {
                    dinoSelecionado = dinos[i];
                    dinoSelecionado.ativo = true;

                    dinos.RemoveAt(i);
                    dinos.Add(dinoSelecionado);
                    break;
                }
        }

        private void frmMouseDown(object sender, MouseEventArgs e)
        {
            var areaMao = new Rectangle(_layout.MaoX, _layout.MaoY, _layout.MaoLargura, _layout.MaoAltura);
            if (areaMao.Contains(e.Location) && !maoAberta)
            {
                bntExibirMao_Click(null, null);
                return;
            }

            SelecionarDinossauro(e.Location);
        }

        private void AtualizarPosicaoDinoSelecionado(Point posicao)
        {
            if (dinoSelecionado == null)
                return;

            dinoSelecionado.posicao.X =
                posicao.X - dinoSelecionado.largura / 2;

            dinoSelecionado.posicao.Y =
                posicao.Y - dinoSelecionado.altura / 2;

            dinoSelecionado.area = new Rectangle(
                dinoSelecionado.posicao.X,
                dinoSelecionado.posicao.Y,
                dinoSelecionado.largura,
                dinoSelecionado.altura);
        }

        private void frmMouseMove(object sender, MouseEventArgs e)
        {
            if (dinoSelecionado != null)
            {
                AtualizarPosicaoDinoSelecionado(e.Location);
                Invalidate();
            }
        }
        
        private void FixarDinoSelecionadoNoTabuleiro(string cercado)
        {
            if (dinoSelecionado == null)
                return;

            var posicao = _layout.ObterPosicaoLivre(cercado, dinosFixosNoTabuleiro);
            
            Console.WriteLine(
                $"FIXADO EM {cercado}: X={posicao.X}, Y={posicao.Y}");
            
            Console.WriteLine(
                $"DINO FIXADO -> {cercado} | X={posicao.X} Y={posicao.Y}");

            dinosFixosNoTabuleiro.Add(new DinoNoTabuleiro
            {
                Tipo = dinoSelecionado.Tipo,
                Cercado = cercado,
                Area = new Rectangle(posicao.X - 20, posicao.Y - 20, 60, 60)
            });

            dinos.Remove(dinoSelecionado);
            dinoSelecionado = null;

            Invalidate();
        }

        private void frmMouseUp(object sender, MouseEventArgs e)
        {
            if (dinoSelecionado != null)
            {
                var cercadosMapeados = _layout.ObterCercadosMapeados();

                var jogadaRealizada = false;

                foreach (var cercado in cercadosMapeados)
                    if (cercado.Value.Contains(e.Location))
                    {
                        if (_movimentoDoBot)
                        {
                            Console.WriteLine("Movimento do bot detectado - pulando envio ao servidor");
                            jogadaRealizada = true;
                            break;
                        }

                        try
                        {
                            var codigoDino = ConversorDinos.ConverterParaCodigo(dinoSelecionado.Tipo);
                            var retorno = DraftService.Jogar(_dadosJogador.IdJogador, _dadosJogador.Senha, codigoDino, cercado.Key);

                            if (!retorno.Contains("ERRO"))
                            {
                                MessageBox.Show("Jogada realizada com sucesso!");

                                FixarDinoSelecionadoNoTabuleiro(cercado.Key);

                                jogadaRealizada = true;

                                bntExibirMao_Click(null, null);
                                break;
                            }

                            MessageBox.Show("O Servidor recusou: " + retorno);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro técnico: " + ex.Message);
                        }
                    }

                if (!jogadaRealizada)
                    CriarDinos(ultimaMaoRecebida);
            }
            dinoSelecionado = null;
            Invalidate();
        }
        #endregion


        #region Timers e Atualização

        private async void frmTimer_Tick(object sender, EventArgs e)
        {
            frmTimer.Stop();
            await AtualizarDadoAsync();
            frmTimer.Start();
        }
        
        private DinossauroVisual BuscarDinoVisual(
            string codigoDino)
        {
            foreach (var d in dinos)
            {
                var codigoAtual =
                    ConversorDinos.ConverterParaCodigo(d.Tipo);

                if (codigoAtual.Equals(
                        codigoDino,
                        StringComparison.OrdinalIgnoreCase))
                {
                    return d;
                }
            }
            return null;
        }
        
        private Point ObterDestinoLivre(
            string siglaCercado)
        {
            return _layout.ObterPosicaoLivre(
                siglaCercado,
                dinosFixosNoTabuleiro);
        }
        
        private void FinalizarMovimentoBot(
            DinossauroVisual dino,
            string cercado)
        {
            dinoSelecionado = dino;

            FixarDinoSelecionadoNoTabuleiro(
                cercado);
        }
        
        private async Task ExecutarMovimentoVisual(string codigoDino, string siglaCercado)
        {
            try
            {
                _animacaoBotEmAndamento = true;
                _movimentoDoBot = true;
                Console.WriteLine($"Iniciando animação visual: {codigoDino} → {siglaCercado}");

                var dinoParaMover = BuscarDinoVisual(codigoDino);

                if (dinoParaMover == null)
                {
                    Console.WriteLine($" Dinossauro {codigoDino} não encontrado na mão!");
                    return;
                }

                var cercadosMapeados = _layout.ObterCercadosMapeados();

                if (!cercadosMapeados.ContainsKey(siglaCercado))
                {
                    Console.WriteLine($" Cercado {siglaCercado} não encontrado no mapeamento!");
                    return;
                }

                var slotDestino = ObterDestinoLivre(siglaCercado);
                
                Console.WriteLine(
                    $"SLOT DESTINO: X={slotDestino.X}, Y={slotDestino.Y}");

                var posicaoInicial = new Point(
                    PointToScreen(dinoParaMover.posicao).X + dinoParaMover.largura / 2,
                    PointToScreen(dinoParaMover.posicao).Y + dinoParaMover.altura / 2
                );

                var posicaoFinal = new Point(
                    PointToScreen(slotDestino).X + 30,
                    PointToScreen(slotDestino).Y + 30
                );    

                Console.WriteLine($" Origem: ({posicaoInicial.X}, {posicaoInicial.Y})");
                Console.WriteLine($" Destino: ({posicaoFinal.X}, {posicaoFinal.Y})");

                MouseInput.MoveTo(posicaoInicial.X, posicaoInicial.Y);
                await Task.Delay(2000);

                var mouseDownArgs = new MouseEventArgs(MouseButtons.Left, 1,
                    dinoParaMover.posicao.X + dinoParaMover.largura / 2,
                    dinoParaMover.posicao.Y + dinoParaMover.altura / 2, 0);

                Invoke(new Action(() => frmMouseDown(this, mouseDownArgs)));

                MouseInput.LeftDown();
                await Task.Delay(3000);

                Console.WriteLine($"Mouse pressionado sobre {codigoDino}");

                var passos = 20;

                for (var i = 0; i <= passos; i++)
                {
                    var progresso = (float)i / passos;

                    var x = (int)(
                        dinoParaMover.posicao.X +
                        (slotDestino.X - dinoParaMover.posicao.X)
                        * progresso);

                    var y = (int)(
                        dinoParaMover.posicao.Y +
                        (slotDestino.Y - dinoParaMover.posicao.Y)
                        * progresso);

                    var mouseMoveArgs =
                        new MouseEventArgs(
                            MouseButtons.Left,
                            0,
                            x,
                            y,
                            0);

                    Invoke(new Action(() =>
                        frmMouseMove(this, mouseMoveArgs)));

                    await Task.Delay(15);
                }

                Console.WriteLine($" Dino chegou ao cercado {siglaCercado}");

                //solta o mouse (MouseUp)
                await Task.Delay(1000);

                MouseInput.LeftUp();
                
                var dinoMovido = dinoParaMover;

                Invoke(new Action(() =>
                {
                    FinalizarMovimentoBot(dinoMovido, siglaCercado);
                }));

                Console.WriteLine(" Animação visual concluída!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na animação visual: {ex.Message}");
                Console.WriteLine($" Stack: {ex.StackTrace}");
            }
            finally
            {
                _animacaoBotEmAndamento = false;
                _movimentoDoBot = false;
            }
        }

        private async Task AtualizarDadoAsync()
        {
            if (_dadosJogador == null) return;

            try
            {
                var info = await Task.Run(() => Partida.VerificaPartida(_dadosJogador.idPartida));

                if (info.numeroTurno != ultimoTurnoExibido)
                {
                    if (!_animacaoBotEmAndamento)
                    {
                        bntExibirMao_Click(null, null);
                    }

                    var imagemDado = ProvedorDeImagens.PegarImagemDado(info.faceDado.Trim());
                    if (imagemDado != null) picDado.Image = imagemDado;
                    ultimoTurnoExibido = info.numeroTurno;
                    turnoAtual = info.numeroTurno;
                    rodadaAtual = ((info.numeroTurno - 1) / 6) + 1;
                    if (nomesJogadores.ContainsKey(info.idJogador)) nomeJogadorDado = nomesJogadores[info.idJogador];

                    ListarHistorico();
                    Invalidate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void TimerRobozinho_Tick(object sender, EventArgs e)
        {
            if (_trabalhando) return;

            _timerRobozinho.Stop();
            _trabalhando = true;

            try
            {
                var dadosVerificacao = DraftService.VerificarPartidaBruto(_dadosJogador.idPartida);
                var dados = dadosVerificacao.Split(',');
                var statusPartida = dados[0];


                if (statusPartida == "J")
                {
                    _cts?.Cancel();

                    _cts =
                        new CancellationTokenSource();

                    await _botService.ExecutarTurnoAsync(
                        _cts.Token);
                }
                else
                    Console.WriteLine("⏳ Aguardando próximo ciclo");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ERRO NO TIMER: {ex.Message}");
                Console.WriteLine($"STACK TRACE: {ex.StackTrace}");

                if (ex.InnerException != null)
                    Console.WriteLine($" INNER: {ex.InnerException.Message}");
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