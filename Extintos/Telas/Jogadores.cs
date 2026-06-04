using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Draft;
using Extintos.Model;

namespace Extintos.Telas
{
    public partial class Jogadores : Form
    {
        private readonly Jogador dadosJogador; //*******

        public Jogadores()
        {
            InitializeComponent();
            lblVersao2.Text = Jogo.versao;

            FormBorderStyle = FormBorderStyle.Sizable;
            Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            WindowState = FormWindowState.Maximized;

            dgvJogadores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJogadores.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvJogadores.AllowUserToAddRows = false;
            dgvJogadores.AllowUserToResizeColumns = false;
            dgvJogadores.AllowUserToResizeRows = false;
            dgvJogadores.RowHeadersVisible = false;
        }

        internal Jogadores(Jogador dadosJogador) : this() //Construtor com parâmetro 
        {
            this.dadosJogador = dadosJogador;
            lblSenhaGeradaa.Text = dadosJogador.Senha;
        }


        private void bntListaJogadores_Click(object sender, EventArgs e)
        {
            dgvJogadores.DataSource = Partida.ListarJogadores(dadosJogador.idPartida);
        }


        private void bntEntrar_Click(object sender, EventArgs e)
        {
            //teste do GPT pra arrumar essa bosta de botão
            var form = new TelaPartida("", dadosJogador);
            form.Show();
            Hide();

            Task.Run(() =>
            {
                var retorno = Partida.IniciarPartida(
                    dadosJogador.IdJogador,
                    dadosJogador.Senha,
                    dadosJogador.idPartida
                );
            });

            /*string retornoEntrar = Partida.IniciarPartida(dadosJogador.IdJogador, dadosJogador.Senha, dadosJogador.idPartida);
            TelaPartida TelaPartida = new TelaPartida(retornoEntrar, dadosJogador);
            TelaPartida.Show();
            this.Hide();
            */
        }

        private void btnCadastrarNovoJogador_Click(object sender, EventArgs e)
        {
            Forms.Lobby.Show();
            Hide();
        }

        private void btnVoltar2_Click_1(object sender, EventArgs e)
        {
            var lobby = new Lobby();
            lobby.Show();
        }
    }
}