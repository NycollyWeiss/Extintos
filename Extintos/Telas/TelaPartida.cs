﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Draft;
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
        #region Cleanup

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _timerRobozinho?.Stop();
            _timerRobozinho?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion


        private async Task LoopRobozinhoAsync()
        {
            try
            {
                _cts?.Cancel();
                _cts = new CancellationTokenSource();

                var token = _cts.Token;

                if (_dadosJogador == null)
                {
                    Console.WriteLine("Jogador nulo.");
                    return;
                }

                if (_dadosJogador.idPartida <= 0 ||
                    string.IsNullOrWhiteSpace(_dadosJogador.Senha))
                {
                    Console.WriteLine("Partida ou senha inválida.");
                    return;
                }

                var decisoes = new InformacoesTurno(
                    _dadosJogador.IdJogador,
                    _dadosJogador.idPartida,
                    _dadosJogador.Senha,
                    _dadosJogador);

                turnoAtual = decisoes.NumeroTurno;

                Console.WriteLine();
                Console.WriteLine("======================================");
                Console.WriteLine($"Turno atual: {turnoAtual}");
                Console.WriteLine("======================================");

                if (_ultimoTurnoJogado == turnoAtual)
                {
                    Console.WriteLine($"Já joguei no turno {turnoAtual}.");
                    return;
                }
                if (_ultimoTurnoProcessado != turnoAtual)
                {
                    var quantidadeJogadores = nomesJogadores.Count;

                    if ((turnoAtual <= quantidadeJogadores)||(turnoAtual > 6 && turnoAtual <= 6 + quantidadeJogadores))
                    {
                        DinossaurosNoUniverso.AddRange(decisoes.MaoJogador);
                    }

                    if ((turnoAtual <= quantidadeJogadores + 1) || (turnoAtual > 6 && turnoAtual <= 7 + quantidadeJogadores)) { 
                        try
                        {
                            var retornoTurnoAnterior =
                                Jogo.VerificarTurno(
                                    _dadosJogador.idPartida,
                                    turnoAtual - 1);

                            var dinosOponente =
                                DadosOponete.ParserDinosOponente(
                                    retornoTurnoAnterior,
                                    _dadosJogador.IdJogador);


                            DinossaurosNoUniverso.AddRange(
                                dinosOponente);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(
                                $"Falha ao obter dinos do oponente: {ex.Message}");
                        }
                    }

                     AuxUniverso.ConsolidarDino(DinossaurosNoUniverso);

                    //DinossaurosNoUniverso.Clear();
                   // DinossaurosNoUniverso.AddRange(consolidado);

                    _ultimoTurnoProcessado = turnoAtual;
                   
                }

                Console.WriteLine($"Jogador: {_dadosJogador.IdJogador}");
                Console.WriteLine($"Turno: {decisoes.NumeroTurno}");
                Console.WriteLine($"Dado: {decisoes.DadoAtual}");

                Console.WriteLine("MÃO:");

                foreach (var d in decisoes.MaoJogador)
                    Console.WriteLine(
                        $"{d.Dinossauro} x{d.QuantidadeDinossauros}");

                Console.WriteLine("CERCADOS:");

                foreach (var c in decisoes.CercadosJogador)
                    Console.WriteLine(
                        $"{c.Cercados} -> {c.Dinossauros.Count}");

                if (decisoes.MaoJogador == null ||
                    decisoes.MaoJogador.Count == 0)
                {
                    Console.WriteLine("Mão vazia.");
                    return;
                }

                if (decisoes.CercadosJogador == null ||
                    decisoes.CercadosJogador.Count == 0)
                {
                    Console.WriteLine("Nenhum cercado carregado.");
                    return;
                }

                var jogada = estrategia.Avaliar(decisoes);

                if (!jogada.HasValue)
                {
                    Console.WriteLine(
                        "Estratégia não encontrou jogada válida.");
                    return;
                }

                var escolha = jogada.Value;

                Console.WriteLine(
                    $"Escolha: {escolha.dino.PegaNome()} -> {escolha.cercado.PegaNome()}");

                await Task.Delay(
                    Helper.Next(2000, 3000),
                    token);

                var codigoDino =
                    escolha.dino.PegaCodigo();

                var codigoCercado =
                    escolha.cercado.PegaCodigo();

                Console.WriteLine(
                    $"Enviando: {codigoDino} -> {codigoCercado}");

                proximoTurno =
                    await DraftService.JogarAsync(
                        _dadosJogador.IdJogador,
                        _dadosJogador.Senha,
                        codigoDino,
                        codigoCercado,
                        token);

                Console.WriteLine(
                    $"Servidor retornou: {proximoTurno}");

                if (proximoTurno == decisoes.NumeroTurno)
                {
                    Console.WriteLine(
                        "Servidor recusou ou não processou a jogada.");

                    return;
                }

                Console.WriteLine(
                    "Jogada confirmada.");

                _dadosJogador.ColocarDinossauro(
                    escolha.dino,
                    escolha.cercado);

                _ultimoTurnoJogado =
                    decisoes.NumeroTurno;

                ultimoTurnoExibido =
                    decisoes.NumeroTurno;

                try
                {
                    await ExecutarMovimentoVisual(
                        codigoDino,
                        codigoCercado);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(
                        $"Erro na animação: {ex.Message}");
                }

                JaJogueiNesseTurno(
                    decisoes.NumeroTurno);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine(
                    "Operação cancelada.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"ERRO COMPLETO: {ex}");

                Console.WriteLine(
                    $"STACK: {ex.StackTrace}");
            }

            try
            {
                await Task.Delay(2000);

                var historico = DraftService.ObterHistoricoBruto(_dadosJogador.idPartida);

                Console.WriteLine(
                    "Histórico atualizado? " +
                    historico.Contains(
                        _dadosJogador.IdJogador.ToString()));
            }
            catch
            {
            }
        }

        private bool JaJogueiNesseTurno(int turnoAtual)
        {
            var historico = DraftService.ObterHistoricoBruto(_dadosJogador.idPartida);
            if (string.IsNullOrWhiteSpace(historico)) return false;

            var termoTurno = $"Turno {turnoAtual}";
            var termoJogador = _dadosJogador.IdJogador.ToString();

            var achouTurno = historico.IndexOf(termoTurno, StringComparison.OrdinalIgnoreCase) >= 0;
            var achouJogador = historico.IndexOf(termoJogador, StringComparison.OrdinalIgnoreCase) >= 0;

            if (achouTurno && achouJogador)
            {
                Console.WriteLine($"Já joguei neste turno (histórico): Turno {turnoAtual}");
                return true;
            }

            return false;
        }

        private async Task<bool> AguardarAtualizacaoHistoricoAsync(int turno, int idJogador, int tentativas = 5,
            int delayMs = 1500)
        {
            for (var i = 0; i < tentativas; i++)
            {
                await Task.Delay(delayMs);

                var historicoBruto = DraftService.ObterHistoricoBruto(_dadosJogador.idPartida);
                Console.WriteLine($"🔍 Verificando histórico ({i + 1}/{tentativas})...");

                if (historicoBruto.Contains($"Turno {turno}") && historicoBruto.Contains(idJogador.ToString()))
                {
                    Console.WriteLine("Histórico atualizado com sucesso!");
                    return true;
                }
            }

            Console.WriteLine("⚠ Histórico não refletiu após todas as tentativas.");
            return false;
        }

        private int ContarTotalDinos(InformacoesTurno decisoes)
        {
            var total = 0;
            foreach (var c in decisoes.CercadosJogador) total += c.Dinossauros.Count;
            return total;
        }

        #region Classe Auxiliar

        public class DinoNoTabuleiro
        {
            public string Tipo { get; set; }
            public string Cercado { get; set; }
            public Rectangle Area { get; set; }
        }

        #endregion

        #region Campos e Propriedades

        private readonly Jogador _dadosJogador;
        private string _retornoEntrar;
        private bool maoAberta;
        private int rodadaAtual = 1;
        private int turnoAtual = 1;
        private int proximoTurno;
        private string nomeJogadorDado = "";
        private int ultimoTurnoExibido = -1;
        private readonly List<AuxDinossauro> DinossaurosNoUniverso = new();
        private readonly IEstategia estrategia = new EstrategiaGulosa();
        private int _ultimoTurnoProcessado = -1;


        private CancellationTokenSource _cts;
        private bool _trabalhando;
        private Timer _timerRobozinho;

        private ConfigEstrategia _config;

        private int _ultimoTurnoJogado = -1;
        private int _totalDinosUltimaLeitura = -1;
        private bool _movimentoDoBot; // Flag para saber se o movimento é do bot

        // Dicionários e listas
        private readonly Dictionary<int, string> nomesJogadores = new();
        private readonly List<DinoNoTabuleiro> dinosFixosNoTabuleiro = new();
        private readonly Dictionary<string, List<Point>> posicoesCercados = new();
        private readonly List<DinossauroVisual> dinos = new();
        private DinossauroVisual dinoSelecionado;
        private List<AuxDinossauro> ultimaMaoRecebida = new();

        // Posições e tamanhos na tela
        private int tabX, tabY;
        private readonly int tabTamanho = 600;
        private int maoX, maoY;
        private readonly int maoLargura = 450;
        private readonly int maoAltura = 400;

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

        internal TelaPartida(string retornoEntrar, Jogador dadosJogador) : this()
        {
            _retornoEntrar = retornoEntrar;
            _dadosJogador = dadosJogador;
        }

        private void ConfigurarSistema()
        {
            _config = new ConfigEstrategia();
            var estrategia = new EstrategiaGulosa(_config);
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
            // Configurar posição do histórico
            txtHistorico.Left = ClientSize.Width - txtHistorico.Width - 120;
            txtHistorico.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            // Iniciar timer principal
            frmTimer.Interval = 3000;
            frmTimer.Start();

            // Iniciar timer do robozinho
            _timerRobozinho = new Timer { Interval = 5000 };
            _timerRobozinho.Tick += TimerRobozinho_Tick;
            _timerRobozinho.Start();

            CarregarNomesJogadores();

            picDado.BringToFront();
            picDado.Visible = true;
        }

        #endregion

        #region Comunicação com Servidor

        public void bntExibirMao_Click(object sender, EventArgs e)
        {
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
            dinos.Clear();
            ultimaMaoRecebida = maoJogador;
            maoAberta = true;


            var centroX = maoX + 105;
            var centroY = maoY + 160;


            var tamanhoDino = 100;
            var espacamentoX = 95;
            var espacamentoY = 95;

            var index = 0;
            foreach (var item in maoJogador)
                for (var i = 0; i < item.QuantidadeDinossauros; i++)
                {
                    if (index >= 6) break; // Limita a 6 dinos na mão

                    var coluna = index % 3;
                    var linha = index / 3;

                    var posX = centroX + coluna * espacamentoX;
                    var posY = centroY + linha * espacamentoY;

                    var d = new DinossauroVisual(PegarImagemDinossauro(item.Dinossauro), posX, posY)
                    {
                        Tipo = item.Dinossauro.ToString(),
                        largura = tamanhoDino,
                        altura = tamanhoDino,
                        area = new Rectangle(posX, posY, tamanhoDino, tamanhoDino)
                    };

                    dinos.Add(d);
                    index++;
                }

            Invalidate();
        }

        private void frmPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;


            tabX = (ClientSize.Width - tabTamanho) / 2;
            tabY = (ClientSize.Height - tabTamanho) / 2;
            InicializarPosicoesCercados();
            maoX = 20;
            maoY = ClientSize.Height - maoAltura - 10;
            maoX = (ClientSize.Width - maoLargura) / 16;
            maoY = ClientSize.Height - maoAltura;


            g.DrawImage(imgTabuleiro, tabX, tabY, tabTamanho, tabTamanho);

#if DEBUG

            var cercados = ObterCercadosMapeados();

            foreach (var c in cercados)
            {
                g.DrawRectangle(
                    Pens.Red,
                    c.Value);

                g.DrawString(
                    c.Key,
                    Font,
                    Brushes.Red,
                    c.Value.X,
                    c.Value.Y);
                
                foreach (var lista in posicoesCercados)
                {
                    foreach (var p in lista.Value)
                    {
                        g.DrawRectangle(
                            Pens.Blue,
                            p.X,
                            p.Y,
                            20,
                            20);
                    }
                }
            }

#endif
            

            foreach (var dinoFixo in dinosFixosNoTabuleiro)
            {
                var tipoEnum = (Dinossauro)Enum.Parse(typeof(Dinossauro), dinoFixo.Tipo.ToUpper());
                var img = PegarImagemDinossauro(tipoEnum);
                g.DrawImage(img, dinoFixo.Area.X, dinoFixo.Area.Y, dinoFixo.Area.Width, dinoFixo.Area.Height);
            }


            var imgMao = maoAberta ? imgMaoAberta : imgMaoFechada;
            g.DrawImage(imgMao, maoX, maoY, maoLargura, maoAltura);


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
            var areaMao = new Rectangle(maoX, maoY, maoLargura, maoAltura);
            if (areaMao.Contains(e.Location) && !maoAberta)
            {
                bntExibirMao_Click(null, null);
                return;
            }

            SelecionarDinossauro(e.Location);
        }


        private void AtualizarPosicaoDinoSelecionado(Point posicao)
        {
            dinoSelecionado.posicao.X = posicao.X - dinoSelecionado.largura / 2;
            dinoSelecionado.posicao.Y = posicao.Y - dinoSelecionado.altura / 2;
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
            var posicao = ObterPosicaoLivre(cercado);
            
            Console.WriteLine(
                $"FIXADO EM {cercado}: X={posicao.X}, Y={posicao.Y}");
            
            Console.WriteLine(
                $"DINO FIXADO -> {cercado} | X={posicao.X} Y={posicao.Y}");

            dinosFixosNoTabuleiro.Add(new DinoNoTabuleiro
            {
                Tipo = dinoSelecionado.Tipo,
                Cercado = cercado,
                Area = new Rectangle(
                    posicao.X - 20,
                    posicao.Y - 20,
                    60,
                    60
                )
            });

            dinos.Remove(dinoSelecionado);
            dinoSelecionado = null;

            Invalidate();
        }

        private void frmMouseUp(object sender, MouseEventArgs e)
        {
            if (dinoSelecionado != null)
            {
                var cercadosMapeados = ObterCercadosMapeados();

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
                            var codigoDino = ConverterParaCodigoDino(dinoSelecionado.Tipo);
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

        private async Task ExecutarMovimentoVisual(string codigoDino, string siglaCercado)
        {
            try
            {
                _movimentoDoBot = true;
                Console.WriteLine($"Iniciando animação visual: {codigoDino} → {siglaCercado}");


                DinossauroVisual dinoParaMover = null;
                foreach (var d in dinos)
                {
                    var codigoAtual = ConverterParaCodigoDino(d.Tipo);
                    if (codigoAtual.Equals(codigoDino, StringComparison.OrdinalIgnoreCase))
                    {
                        dinoParaMover = d;
                        break;
                    }
                }

                if (dinoParaMover == null)
                {
                    Console.WriteLine($" Dinossauro {codigoDino} não encontrado na mão!");
                    return;
                }

                //ObterCercadosMapeados está declarado ao final do código, sendo um dicionário dos cercados. 
                var cercadosMapeados = ObterCercadosMapeados();


                if (!cercadosMapeados.ContainsKey(siglaCercado))
                {
                    Console.WriteLine($" Cercado {siglaCercado} não encontrado no mapeamento!");
                    return;
                }

                var slotDestino = ObterPosicaoLivre(siglaCercado);
                
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

                // Durante a animação, dispara eventos MouseMove para atualizar a UI
                var passos = 20;
                for (var i = 0; i <= passos; i++)
                {
                    var progresso = (float)i / passos;
                    var x = (int)(dinoParaMover.posicao.X +
                                  (slotDestino.X - dinoParaMover.posicao.X)
                                  * progresso);

                    var y = (int)(dinoParaMover.posicao.Y +
                                  (slotDestino.Y - dinoParaMover.posicao.Y)
                                  * progresso);

                    var mouseMoveArgs = new MouseEventArgs(MouseButtons.Left, 0, x, y, 0);
                    Invoke(new Action(() => frmMouseMove(this, mouseMoveArgs)));

                    await Task.Delay(15);
                }

                Console.WriteLine($" Dino chegou ao cercado {siglaCercado}");

                //Solta o mouse (MouseUp)
                await Task.Delay(1000);

                // Calcula a posição local do mouse dentro do cercado
                var posicaoLocalFinal = new Point(
                    slotDestino.X,
                    slotDestino.Y
                );
                
                var mouseUpArgs = new MouseEventArgs(
                    MouseButtons.Left,
                    1,
                    posicaoLocalFinal.X,
                    posicaoLocalFinal.Y,
                    0
                );

                MouseInput.LeftUp();

                // Atualiza manualmente o estado visual
                Invoke(new Action(() =>
                {
                    if (dinoSelecionado != null)
                    {
                        FixarDinoSelecionadoNoTabuleiro(siglaCercado);
                    }
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
                _movimentoDoBot = false; // Sempre reseta a flag
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
                    bntExibirMao_Click(null, null);

                    var imagemDado = PegarImagemDado(info.faceDado.Trim());
                    if (imagemDado != null) picDado.Image = imagemDado;
                    ultimoTurnoExibido = info.numeroTurno;
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
                    await LoopRobozinhoAsync();
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

        #region Métodos Auxiliares

        private string ConverterParaCodigoDino(string tipo)
        {
            var t = tipo.ToUpper();
            if (t.Contains("TIRANOSSAURO") || t == "TI") return "Ti";
            if (t.Contains("BRAQUIOSSAURO") || t == "BR") return "Br";
            if (t.Contains("ESTEGOSSAURO") || t == "ET") return "Et";
            if (t.Contains("PARASAUROLOFO") || t == "PA") return "Pa";
            if (t.Contains("ESPINOSSAURO") || t == "EP") return "Ep";
            if (t.Contains("TRICERATOPS") || t == "TR") return "Tr";
            return tipo;
        }

        private Image PegarImagemDinossauro(Dinossauro dino)
        {
            switch (dino)
            {
                case Dinossauro.TI: return Resources.Tiranossauro;
                case Dinossauro.BR: return Resources.Braquiossauro;
                case Dinossauro.ET: return Resources.Estegossauro;
                case Dinossauro.PA: return Resources.Parasaurolofo;
                case Dinossauro.EP: return Resources.Espinossauro;
                case Dinossauro.TR: return Resources.Triceratops;
                default: return null;
            }
        }

        private Dictionary<string, Rectangle> ObterCercadosMapeados()
        {
            return new Dictionary<string, Rectangle>
            {
                { "FI", new Rectangle(tabX + 30, tabY + 30, 200, 135) },
                { "MT", new Rectangle(tabX + 30, tabY + 225, 145, 135) },
                { "PA", new Rectangle(tabX + 60, tabY + 410, 155, 160) },
                { "RS", new Rectangle(tabX + 400, tabY + 50, 60, 60) },
                { "CD", new Rectangle(tabX + 367, tabY + 230, 197, 150) },
                { "IS", new Rectangle(tabX + 470, tabY + 415, 60, 60) },
                { "RI", new Rectangle(tabX + 270, tabY + 445, 130, 150) }
            };
        }
        
        private Point ObterPosicaoLivre(string cercado)
        {
            if (!posicoesCercados.ContainsKey(cercado))
                return Point.Empty;

            var ocupados =
                dinosFixosNoTabuleiro.Count(
                    d => d.Cercado == cercado);

            var lista = posicoesCercados[cercado];

            if (ocupados >= lista.Count)
                ocupados = lista.Count - 1;

            return lista[ocupados];
        }
        
        private void InicializarPosicoesCercados()
        {
            posicoesCercados.Clear();

            posicoesCercados["FI"] = new List<Point>
            {
                new(tabX + 35, tabY + 80),
                new(tabX + 65, tabY + 80),
                new(tabX + 100, tabY + 80),
                new(tabX + 135, tabY + 80),
                new(tabX + 170, tabY + 80),
                new(tabX + 200, tabY + 80)
            };

            posicoesCercados["MT"] = new List<Point>
            {
                new(tabX + 65, tabY + 270),
                new(tabX + 95, tabY + 270),
                new(tabX + 125, tabY + 270)
            };

            posicoesCercados["PA"] = new List<Point>
            {
                new(tabX + 110, tabY + 430),
                new(tabX + 140, tabY + 430),

                new(tabX + 110, tabY + 460),
                new(tabX + 140, tabY + 460),

                new(tabX + 110, tabY + 490),
                new(tabX + 140, tabY + 490)
            };

            posicoesCercados["RS"] = new List<Point>
            {
                new(tabX + 420, tabY + 70)
            };

            posicoesCercados["CD"] = new List<Point>
            {
                new(tabX + 368, tabY + 310),
                new(tabX + 398, tabY + 310),
                new(tabX + 433, tabY + 310),
                new(tabX + 468, tabY + 310),
                new(tabX + 498, tabY + 310),
                new(tabX + 535, tabY + 310)
            };

            posicoesCercados["RI"] = new List<Point>
            {
                new(tabX + 290, tabY + 465),
                new(tabX + 325, tabY + 465),
                new(tabX + 360, tabY + 465),

                new(tabX + 290, tabY + 495),
                new(tabX + 325, tabY + 495),
                new(tabX + 360, tabY + 495),

                new(tabX + 290, tabY + 525),
                new(tabX + 325, tabY + 525),
                new(tabX + 360, tabY + 525),
                
                new(tabX + 290, tabY + 555),
                new(tabX + 325, tabY + 555),
                new(tabX + 360, tabY + 555)
            };

            posicoesCercados["IS"] = new List<Point>
            {
                new(tabX + 490, tabY + 435)
            };
        }

        private Image PegarImagemDado(string face)
        {
            switch (face)
            {
                case "AL": return Resources.Dado_Alimentacao;
                case "FL": return Resources.Dado_Floresta;
                case "PR": return Resources.Dado_Pradaria;
                case "TI": return Resources.Dado_ReiSelva;
                case "VZ": return Resources.Dado_CercadoVazio;
                case "WC": return Resources.Dado_Banheiro;
                default: return null;
            }
        }

        #endregion
    }
}