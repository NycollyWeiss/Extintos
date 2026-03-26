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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Extintos
{
    public partial class FormPartida : Form
    {
       
        public FormPartida()
        {
            InitializeComponent();
            lblVersao1.Text = Jogo.versao;
            txtNomeJogador.Clear();
            txtIDdaPartida.Clear();
            txtSenhaPartida.Clear();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = FormWindowState.Maximized;
        }
      
        private void btnListarPartidas_Click(object sender, EventArgs e)
        {
        dgvPartidas.DataSource = Partida.ListarPartidas("T"); //Lista para dentro do dgvPartidas
        }
        private void lstPartidas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selecaoPartida = lstPartidas.SelectedItem.ToString(); // **
            string[] dadosPartida = selecaoPartida.Split(',');

            int idPartida = Convert.ToInt32(dadosPartida[0]); //Conversão para int devido os dados das partidas estarem em string. **
            string nomePartida = dadosPartida[1];
            string dataPartida = dadosPartida[2];

            lblPartida.Text = idPartida.ToString();
            lblNomePartida.Text = nomePartida;
            lblDataPartida.Text = dataPartida;
        }

        private void btnEntrarPartida_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomeJogador.Text) ||
                string.IsNullOrEmpty(txtIDdaPartida.Text) ||
                string.IsNullOrEmpty(txtSenhaPartida.Text))
            {
                MessageBox.Show("Todos os campos devem ser preechidos!!\n\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nomeJogador = txtNomeJogador.Text;
            string idDaPartida = txtIDdaPartida.Text;
            string senhaDaPartida = txtSenhaPartida.Text;
               
            int idPartida = Convert.ToInt32(idDaPartida);

           
            string jogadores = Jogo.ListarJogadores(idPartida);
            string[] ativos = jogadores.Split(',');
            for (int i = 0; i < ativos.Length; i++)
            {
                if (nomeJogador.Equals(ativos[i]))
                {
                    MessageBox.Show("Jogador já existente!! Digite outro nome\n\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNomeJogador.Clear();
                    nomeJogador = txtNomeJogador.Text;
                    return;
                }
            }


          ///  FormJogadores formJogadores = new FormJogadores(idDaPartida, senhaJogador, idJogador);
         ///   formJogadores.Show();

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Forms.Form1.Show();
            this.Hide();
        }

        private void FormPartida_Load(object sender, EventArgs e)
        {

        }
    }
    }

