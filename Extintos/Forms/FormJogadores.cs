using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Draft;
using Extintos.Model;

namespace Extintos
{
    public partial class FormJogadores : Form
    {
        private readonly Jogador dadosJogador; //*******

        public FormJogadores()
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

        internal FormJogadores(Jogador dadosJogador) : this() //Construtor com parâmetro 
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
            var form = new FormDraftosaurus("", dadosJogador);
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
            FormDraftosaurus FormDraftosaurus = new FormDraftosaurus(retornoEntrar, dadosJogador);
            FormDraftosaurus.Show();
            this.Hide();
            */
        }

        private void btnCadastrarNovoJogador_Click(object sender, EventArgs e)
        {
            Forms.FormLobby.Show();
            Hide();
        }

        private void btnVoltar2_Click_1(object sender, EventArgs e)
        {
            var lobby = new FormLobby();
            lobby.Show();
        }
    }
}