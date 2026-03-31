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
using Extintos.Model;

namespace Extintos
{
    public partial class FormLobby: Form
    {
        public FormLobby()
        {
            InitializeComponent();
            lblVersao.Text = Jogo.versao;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnListarAsPartidas_Click(object sender, EventArgs e)
        {
            dgvPartida.DataSource = Partida.ListarPartidas('T'); //Lista para dentro do dgvPartidas

            dgvPartida.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPartida.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvPartida.AllowUserToAddRows = false;
            dgvPartida.AllowUserToResizeColumns = false;
            dgvPartida.AllowUserToResizeRows = false;
            dgvPartida.RowHeadersVisible = false;

            //ALTERAÇÕES VISUAIS NAS COLUNAS

            dgvPartida.Columns[1].HeaderText = "Nome da Partida";
            dgvPartida.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvPartida.Columns[2].HeaderText = "Data da Partida";
            dgvPartida.Columns[3].HeaderText = "Status da Partida"; 

        }

        private void btnEntrarNaPartida_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomeDoJogador.Text) ||
               string.IsNullOrEmpty(txtIdDaPartida.Text) ||
               string.IsNullOrEmpty(txtSenhaDaPartida.Text))
            {
                MessageBox.Show("Todos os campos devem ser preechidos!!\n\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nomeJogador = txtNomeDoJogador.Text;
            string idDaPartida = txtIdDaPartida.Text; 
            int idPartida = Convert.ToInt32(idDaPartida); 

            string senhaDaPartida = txtSenhaDaPartida.Text;

            //Verifica se o jogador colocado já está na partida
            string jogadores = Jogo.ListarJogadores(idPartida);
            string[] ativos = jogadores.Split(',');
            for (int i = 0; i < ativos.Length; i++)
            {
                if (nomeJogador.Equals(ativos[i]))
                {
                    MessageBox.Show("Jogador já existente!! Digite outro nome\n\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNomeDoJogador.Clear();
                    nomeJogador = txtNomeDoJogador.Text;
                    return;
                }
            }

            // string verificaSenha = Jogo.ListarPartidas

            // if (txtSenhaDaPartida.Text != senhaDaPartida)


            Jogador DadosJogador = Jogador.EntrarNaPartida(idPartida, nomeJogador, senhaDaPartida);
            //string[] dadosJogador = DadosJogador.Split(',');
            //int idJogador = int.Parse(dadosJogador[0]);
            //string senhaJogador = dadosJogador[1];

            if (txtSenhaDaPartida.Text != senhaDaPartida)
            {
                MessageBox.Show("A senha digitada não corresponde à da partida selecionada.\n\n", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenhaDaPartida.Clear();
            }


            //  Partida p = (Partida)dgvPartida.SelectedRows[0].DataBoundItem;

            FormJogadores formJogadores = new FormJogadores(DadosJogador);
            formJogadores.Show();

            this.Hide();

        }

        private void btnVoltar1_Click(object sender, EventArgs e)
        {
            Forms.Form1.Show();
            this.Hide();
        }

        private void dgvPartida_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdDaPartida.Text = dgvPartida.CurrentRow.Cells[0].Value.ToString();
        }
    }
}
