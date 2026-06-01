using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Extintos.LeonKennedy
{

    public class TransparenteRichTextBox : RichTextBox
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr LoadLibrary(string lpFileName);

        protected override CreateParams CreateParams
        {
            get
            {

                LoadLibrary("Msftedit.dll");
                CreateParams cp = base.CreateParams;
                cp.ClassName = "RICHEDIT50W";
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        public TransparenteRichTextBox()
        {
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }
    }

    public class FormResultadoTeste : Form
    {
        private Button btnExecutar;
        private Button btnFechar;
        private Panel panelBotoes;
        private Panel panelDireita;
        private Panel panelEsquerda;
        private TransparenteRichTextBox txtLogs;
        private PictureBox pictureBox1;

        public FormResultadoTeste()
        {
            InitializeComponents();
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            UpdateStyles();
        }

        private void InitializeComponents()
        {
            Text = "🫦🫦🫦Leon Kennedy Guloso🫦🫦🫦"; //aqui
            Size = new Size(900, 600);
            MinimumSize = new Size(700, 500);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9F);
            DoubleBuffered = true;

            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "leon.jpg");
                BackgroundImage = Image.FromFile(path);
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch
            {
                BackColor = Color.FromArgb(20, 20, 30);
            }

            panelEsquerda = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Color.Transparent
            };
            
            panelDireita = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(140, 15, 15, 20)
            };

            pictureBox1 = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };
            try
            {
                var foto1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "leonCoelinho.jpg");
                pictureBox1.Image = Image.FromFile(foto1);
            }
            catch
            {
                pictureBox1.Image = SystemIcons.Application.ToBitmap();
            }

            panelEsquerda.Controls.Add(pictureBox1);

            // CONFIGURAÇÃO DO LOG
            txtLogs = new TransparenteRichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                ForeColor = Color.FromArgb(240, 240, 240),
                Font = new Font("Consolas", 11F, FontStyle.Bold), // Fonte ligeiramente maior melhora a leitura sobre imagens
                Margin = new Padding(20),
                ScrollBars = RichTextBoxScrollBars.Vertical
            };

            panelDireita.Controls.Add(txtLogs);

            panelBotoes = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.Transparent
            };

            btnExecutar = new Button
            {
                Text = "▶ Executar Testes",
                Size = new Size(160, 30),
                Location = new Point(20, 10),
                BackColor = Color.White,
                ForeColor = Color.MidnightBlue,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            btnExecutar.FlatAppearance.BorderSize = 0;
            btnExecutar.Click += BtnExecutar_Click;

            btnFechar = new Button
            {
                Text = "✖ Fechar",
                Size = new Size(100, 30),
                Location = new Point(190, 10),
                BackColor = Color.FromArgb(190, 30, 45),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.Click += (s, e) => Close();

            panelBotoes.Controls.Add(btnExecutar);
            panelBotoes.Controls.Add(btnFechar);

            panelDireita.Controls.Add(panelBotoes);
            panelBotoes.BringToFront();

            Controls.Add(panelDireita);
            Controls.Add(panelEsquerda);
        }

        private async void BtnExecutar_Click(object sender, EventArgs e)
        {
            btnExecutar.Enabled = false;
            btnExecutar.Text = "Executando...";
            txtLogs.Clear();

            Log("Iniciando testes do Guloso...\n");

            await Task.Run(() =>
            {
                PartidaSimulada.ExecutarPartida4Jogadores(Log);
            });

            Log("\nFinalizado!");
            btnExecutar.Text = "▶ Executar Novamente";
            btnExecutar.Enabled = true;
        }

        // NOVO MÉTODO DE LOG: Paleta de cores de alto contraste calibrada para fundos escuros e artísticos
        private void Log(string msg)
        {
            if (txtLogs.InvokeRequired)
            {
                txtLogs.Invoke(new Action(() => Log(msg)));
                return;
            }

            if (string.IsNullOrEmpty(msg))
                return;

            var cor = Color.FromArgb(240, 240, 240);

            if (msg.Contains("❌"))
                cor = Color.FromArgb(255, 80, 80);       // Vermelho vivo para erros
            else if (msg.Contains("✔"))
                cor = Color.FromArgb(50, 255, 130);     // Verde limão brilhante para sucessos
            else if (msg.Contains("[TESTE]"))
                cor = Color.FromArgb(0, 210, 255);      // Neon Cyber Azul para identificadores
            else if (msg.Contains("🚀") || msg.Contains("🏁"))
                cor = Color.FromArgb(255, 215, 0);       // Ouro vibrante para marcos importantes

            txtLogs.SelectionStart = txtLogs.TextLength;
            txtLogs.SelectionLength = 0;
            txtLogs.SelectionColor = cor;
            txtLogs.AppendText(msg + Environment.NewLine);
            txtLogs.SelectionColor = txtLogs.ForeColor;

            txtLogs.Invalidate();
        }
    }
}