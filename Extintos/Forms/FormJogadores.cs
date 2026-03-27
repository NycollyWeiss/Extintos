using Draft;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Extintos
{
    public partial class FormJogadores : Form
    {
        private int idDaPartida = 0;
        private string senhaDoJogador = "";
        private int idDoJogador = 0;
        private Jogador dadosJogador;

        public FormJogadores()
        { 

            InitializeComponent();
            lblVersao2.Text = Jogo.versao;

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = FormWindowState.Maximized;
        }

        public FormJogadores(Jogador dadosJogador)
        {
            this.dadosJogador = dadosJogador;
        }

        private void bntListaJogadores_Click(object sender, EventArgs e)
        {
           dgvJogadores.DataSource = Partida.ListarJogadores(idDaPartida);

        }
     
        private void FormJogadores_Load(object sender, EventArgs e)
        {

        }

        private void btnProximo_Click(object sender, EventArgs e)
        {

        }

        private void btnCadastrarNovoJogador_Click(object sender, EventArgs e)
        {
            Forms.FormLobby.Show();
            this.Hide();

        }

        private void btnVoltar2_Click(object sender, EventArgs e)
        {
            Forms.FormLobby.Show();
            this.Hide();
        }

        private void btnVoltar2_Click_1(object sender, EventArgs e)
        {
            Forms.FormLobby.Show();
            this.Hide();
        }

        //private void latJogadores_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //  string selecaoJogadores = latJogadores.SelectedItem.ToString(); // **
        // string[] dadosJogadores = selecaoJogadores.Split(',');

        //   int idJogador = Convert.ToInt32(dadosJogadores[0]); //Conversão para int devido os dados das partidas estarem em string. **
        //   string nomJogador = dadosJogadores[1];
        //   int pontuacaoJogador = Convert.ToInt32(dadosJogadores[2]);

        //    lblIdGeradoJogado.Text = idJogador.ToString();
        //    lblSenhaGeradaa.Text = senhaJogador;
        //}
    }
    }






