using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Extintos.LeonKennedy;


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

        protected override void OnPaintBackground(PaintEventArgs e) { }
    }

    public class FormResultadoTeste : Form
    {
        private Button btnExecutarLocal;
        private Button btnTestarOnline;
        private Button btnFechar;
        private TextBox txtIdPartida;
        private TextBox txtSenhaPartida;
        private ComboBox cmbQtdBots;
        private Panel panelBotoes;
        private Panel panelDireita;
        private Panel panelEsquerda;
        private TransparenteRichTextBox txtLogs;
        private PictureBox pictureBox1;

        public FormResultadoTeste()
        {
            InitializeComponents();
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        private void InitializeComponents()
        {
            Text = "🫦🫦🫦Leon Kennedy Guloso🫦🫦🫦"; 
            Size = new Size(1000, 600);
            MinimumSize = new Size(800, 500);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 9F);

            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "leon.jpg");
                BackgroundImage = Image.FromFile(path);
                BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch { BackColor = Color.FromArgb(20, 20, 30); }

            panelEsquerda = new Panel { Dock = DockStyle.Left, Width = 250, BackColor = Color.Transparent };
            panelDireita = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(140, 15, 15, 20) };

            pictureBox1 = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.Transparent };
            try { pictureBox1.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "leonCoelinho.jpg")); }
            catch { pictureBox1.Image = SystemIcons.Application.ToBitmap(); }
            panelEsquerda.Controls.Add(pictureBox1);

            txtLogs = new TransparenteRichTextBox { Dock = DockStyle.Fill, ReadOnly = true, BorderStyle = BorderStyle.None, ForeColor = Color.FromArgb(240, 240, 240), Font = new Font("Consolas", 11F, FontStyle.Bold), Margin = new Padding(20), ScrollBars = RichTextBoxScrollBars.Vertical };
            panelDireita.Controls.Add(txtLogs);

            panelBotoes = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.Transparent };

            btnExecutarLocal = new Button { Text = "Teste Local", Size = new Size(120, 30), Location = new Point(20, 15), BackColor = Color.White, ForeColor = Color.MidnightBlue, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnExecutarLocal.FlatAppearance.BorderSize = 0;
            btnExecutarLocal.Click += BtnExecutarLocal_Click;

            Label lblId = new Label { Text = "ID:", Location = new Point(145, 22), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
            txtIdPartida = new TextBox { Location = new Point(165, 20), Width = 40 };

            Label lblSenha = new Label { Text = "Senha:", Location = new Point(210, 22), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
            txtSenhaPartida = new TextBox { Location = new Point(255, 20), Width = 50 };

            Label lblBots = new Label { Text = "Bots:", Location = new Point(310, 22), AutoSize = true, ForeColor = Color.White, BackColor = Color.Transparent };
            cmbQtdBots = new ComboBox { Name = "cmbBots", Location = new Point(345, 20), Width = 40, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbQtdBots.Items.AddRange(new object[] { "2", "3", "4" });
            cmbQtdBots.SelectedIndex = 2; 

            btnTestarOnline = new Button { Text = "Testar Online", Size = new Size(160, 30), Location = new Point(395, 15), BackColor = Color.FromArgb(0, 210, 255), ForeColor = Color.Black, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnTestarOnline.FlatAppearance.BorderSize = 0;
            btnTestarOnline.Click += BtnTestarOnline_Click;

            btnFechar = new Button { Text = "Fechar", Size = new Size(100, 30), Location = new Point(565, 15), BackColor = Color.FromArgb(190, 30, 45), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.Click += (s, e) => Close();

            panelBotoes.Controls.AddRange(new Control[] { btnExecutarLocal, lblId, txtIdPartida, lblSenha, txtSenhaPartida, lblBots, cmbQtdBots, btnTestarOnline, btnFechar });
            panelDireita.Controls.Add(panelBotoes);
            panelBotoes.BringToFront();
            Controls.Add(panelDireita);
            Controls.Add(panelEsquerda);
        }

        private async void BtnExecutarLocal_Click(object sender, EventArgs e)
        {
            TravarBotoes(true);
            txtLogs.Clear();
            Log("Iniciando testes locais do Guloso...\n");
            await Task.Run(() => PartidaSimulada.ExecutarPartida4Jogadores(Log));
            Log("\nFinalizado!");
            TravarBotoes(false);
        }

        private async void BtnTestarOnline_Click(object sender, EventArgs e)
        {
         
            if (string.IsNullOrWhiteSpace(txtIdPartida.Text) || !int.TryParse(txtIdPartida.Text, out int idPartida))
            {
                MessageBox.Show("ID inválido.");
                return;
            }

            var cmb = (ComboBox)panelBotoes.Controls["cmbBots"];
            int totalJogadores = int.Parse(cmb.SelectedItem.ToString());

            TravarBotoes(true);
            txtLogs.Clear();
            
            
            await TestRunnerOnline.ExecutarPartidaAsync(idPartida, txtSenhaPartida.Text, totalJogadores, Log);
            
            TravarBotoes(false);
        }

        private void TravarBotoes(bool travado)
        {
            btnExecutarLocal.Enabled = !travado;
            btnTestarOnline.Enabled = !travado;
            txtIdPartida.Enabled = !travado;
            txtSenhaPartida.Enabled = !travado;
        }

        private void Log(string msg)
        {
            if (txtLogs.InvokeRequired) { txtLogs.Invoke(new Action(() => Log(msg))); return; }
            if (string.IsNullOrEmpty(msg)) return;

            var cor = Color.FromArgb(240, 240, 240);
            if (msg.Contains("❌") || msg.Contains("BURRO")) cor = Color.FromArgb(255, 80, 80);
            else if (msg.Contains("✔") || msg.Contains("TOME")) cor = Color.FromArgb(50, 255, 130);
            else if (msg.Contains("[VAR]") || msg.Contains("🧠")) cor = Color.FromArgb(0, 210, 255);
            else if (msg.Contains("🚀") || msg.Contains("🏁") || msg.Contains("🏆")) cor = Color.FromArgb(255, 215, 0);

            txtLogs.SelectionStart = txtLogs.TextLength;
            txtLogs.SelectionColor = cor;
            txtLogs.AppendText(msg + Environment.NewLine);
            txtLogs.Invalidate();
        }
    }
