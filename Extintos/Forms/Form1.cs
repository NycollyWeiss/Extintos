using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Draft;


namespace Extintos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblVersao.Text = Jogo.versao;
            Config.Fullscreen(Forms.Form1);
        }
        

        private void btnCriarPartida_Click(object sender, EventArgs e)
        {
            txtNomedoGrupo.Text = "Extintos";
            string nomePartida = txtNomedaPartida.Text;
            string senhaPartida = txtSenhadaPartida.Text;
            string nomeGrupo = txtNomedoGrupo.Text;

            if (string.IsNullOrEmpty(nomePartida) ||
                string.IsNullOrEmpty(senhaPartida) ||
                string.IsNullOrEmpty(nomeGrupo))
            {
                MessageBox.Show("Todos os campos devem ser preechidos!!\n\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           Jogo.CriarPartida(nomePartida, senhaPartida, nomeGrupo);

            //Referenciando o próximo formulário
            Forms.FormLobby.Show();
            this.Hide();
        }

        private void btnIrPartidaExistente_Click(object sender, EventArgs e)
        {
            Forms.FormLobby.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    }

