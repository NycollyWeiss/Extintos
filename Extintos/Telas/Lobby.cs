using System;
using System.Drawing;
using System.Windows.Forms;
using Draft;
using Extintos.Model;

namespace Extintos.Telas
{
    public partial class Lobby : Form
    {
        public Lobby()
        {
            InitializeComponent();
            lblVersao.Text = Jogo.versao;
            FormBorderStyle = FormBorderStyle.Sizable;
            Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            WindowState = FormWindowState.Maximized;
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
                MessageBox.Show("Todos os campos devem ser preechidos!!\n\n", "ERRO", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var nomeJogador = txtNomeDoJogador.Text;

            var idDaPartida = txtIdDaPartida.Text;
            var senhaDaPartida = txtSenhaDaPartida.Text;

            var idPartida = Convert.ToInt32(idDaPartida);

            //Verifica se o jogador colocado já está na partida
            var jogadores = Jogo.ListarJogadores(idPartida);
            var ativos = jogadores.Split(',');
            for (var i = 0; i < ativos.Length; i++)
                if (nomeJogador.Equals(ativos[i]))
                {
                    MessageBox.Show("Jogador já existente!! Digite outro nome\n\n", "ERRO", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtNomeDoJogador.Clear();
                    nomeJogador = txtNomeDoJogador.Text;
                    return;
                }

            // string verificaSenha = Jogo.ListarPartidas

            // if (txtSenhaDaPartida.Text != senhaDaPartida)


            var DadosJogador = Jogador.EntrarNaPartida(idPartida, nomeJogador, senhaDaPartida);
            //string[] dadosJogador = DadosJogador.Split(',');
            //int idJogador = int.Parse(dadosJogador[0]);
            //string senhaJogador = dadosJogador[1];

            if (txtSenhaDaPartida.Text != senhaDaPartida)
            {
                MessageBox.Show("A senha digitada não corresponde à da partida selecionada.\n\n", "ERRO",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenhaDaPartida.Clear();
            }


            //  Partida p = (Partida)dgvPartida.SelectedRows[0].DataBoundItem;

            var formJogadores = new Jogadores(DadosJogador);

            formJogadores.Show();
            Hide();
        }

        private void btnVoltar1_Click(object sender, EventArgs e)
        {
            Forms.TelaInicial.Show();
            Hide();
        }

        private void dgvPartida_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIdDaPartida.Text = dgvPartida.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnVoltarParaPartida_Click(object sender, EventArgs e)
        {
        }

        private void btnVoltarParaPartida_Click_1(object sender, EventArgs e)
        {
            lblTituloNomeDoJogador.Text = "ID da Partida: ";
            //txtNomeDoJogador.Name = "txtIdDaPartida";

            lblTituloIDDaPartida.Text = "Nome do Jogador: ";
            //txtIdDaPartida.Name = "txtNomeDoJogador";

            lblTituloSenhaDaPartida.Text = "Senha do Jogador: ";
            // txtSenhaDaPartida.Name = "txtSenhaDoJogador";

            btnEntrarNaPartida.Text = "Voltar para para partida";

            var idPartidaJogando = Convert.ToInt32(txtNomeDoJogador.Text);
            var jogador = txtIdDaPartida.Text;
            var senhaJogador = txtSenhaDaPartida.Text;

            var jogadores = Jogo.ListarJogadores(idPartidaJogando);
            var ativos = jogadores.Split(',');
            for (var i = 0; i < ativos.Length; i++)
            {
                //  if (jogador.Equals(ativos[i]))
                // {

                // }
            }
        }

        private void FormLobby_Load(object sender, EventArgs e)
        {
        }

        private void txtNomeDoJogador_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}