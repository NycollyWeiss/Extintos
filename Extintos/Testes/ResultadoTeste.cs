using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Extintos.LeonKennedy
{
    public class FormResultadoTeste : Form
    {
        private Button btnExecutar;
        private Button btnFechar;
        private Panel panelBotoes;
        private Panel panelDireita;
        private Panel panelEsquerda;
        private FlowLayoutPanel panelLogs;
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
            Text = "🫦🫦🫦Leon Kennedy Guloso🫦🫦🫦";
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
                BackColor = Color.Transparent
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


            panelLogs = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent
            };

            panelDireita.Controls.Add(panelLogs);


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
            panelLogs.Controls.Clear();

            Log("Iniciando testes do Guloso...\n");

            await Task.Run(() => { TestRunnerOffline.ExecutarTodos(Log); });

            Log("\nFinalizado!");
            btnExecutar.Text = "▶ Executar Novamente";
            btnExecutar.Enabled = true;
        }

        private void Log(string msg)
        {
            if (panelLogs.InvokeRequired) Console.WriteLine(msg);

            var texto = msg.Trim();
            if (string.IsNullOrEmpty(texto))
                return;

            var cor = Color.White;

            if (texto.Contains("❌"))
                cor = Color.Red;
            else if (texto.Contains("✔"))
                cor = Color.LimeGreen;
            else if (texto.Contains("[TESTE]"))
                cor = Color.DeepSkyBlue;
            else if (texto.Contains("🚀") || texto.Contains("🏁"))
                cor = Color.Gold;

            var lbl = new Label
            {
                Text = texto,
                AutoSize = true,
                ForeColor = cor,
                BackColor = Color.Transparent,
                Font = new Font("Consolas", 10F, FontStyle.Bold),
                Margin = new Padding(0, 2, 0, 2)
            };
        }
    }
}