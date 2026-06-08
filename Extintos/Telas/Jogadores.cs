using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Draft;
using Extintos.Model;
using Extintos.Services;

namespace Extintos.Telas
{
    public partial class Jogadores : Form
    {
        private readonly Jogador _dadosJogador;
        public Jogadores()
        {
            InitializeComponent();
            lblVersao2.Text = DraftService.Versao;

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
            this._dadosJogador = dadosJogador;
            lblSenhaGeradaa.Text = dadosJogador.Senha;
        }


        private void bntListaJogadores_Click(object sender, EventArgs e)
        {
            dgvJogadores.DataSource = Partida.ListarJogadores(_dadosJogador.idPartida);
        }


        private void bntEntrar_Click(object sender, EventArgs e)
        {
            //teste do GPT pra arrumar essa droga de botão
            var form = new TelaPartida(_dadosJogador);            
            form.Show();
            Hide();

            Task.Run(() =>
            {
                var retorno = Partida.IniciarPartida(
                    _dadosJogador.IdJogador,
                    _dadosJogador.Senha,
                    _dadosJogador.idPartida
                );
            });

            /*string retornoEntrar = Partida.IniciarPartida(dadosJogador.IdJogador, dadosJogador.Senha, dadosJogador.idPartida);
            TelaPartida TelaPartida = new TelaPartida(retornoEntrar, dadosJogador);
            TelaPartida.Show();
            this.Hide();
            */
        }

        private void btnVoltar2_Click_1(object sender, EventArgs e)
        {
            var lobby = new Lobby();
            lobby.Show();
        }
    }
}