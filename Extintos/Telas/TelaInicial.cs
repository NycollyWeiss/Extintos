// Importações e namespaces utilizados no código
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
using Extintos.Services;


namespace Extintos.Telas
{

    public partial class TelaInicial : Form 
    {
        public TelaInicial() 
        {
            InitializeComponent();
            lblVersao.Text = DraftService.Versao;
            ConfigTela.Fullscreen(this);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = FormWindowState.Maximized;
        }
        
        private void btnCriarPartida_Click(object sender, EventArgs e) 
        {
            string nomePartida  = txtNomedaPartida.Text;
            string senhaPartida = txtSenhadaPartida.Text;
            string nomeGrupo    = txtNomedoGrupo.Text;

            if (string.IsNullOrEmpty(nomePartida)  ||
                string.IsNullOrEmpty(senhaPartida) ||
                string.IsNullOrEmpty(nomeGrupo))
            {
                MessageBox.Show("Todos os campos devem ser preenchidos!!\n\n",
                    "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string retorno = DraftService.CriarPartida(nomePartida, senhaPartida, nomeGrupo);

            // A DLL retorna "ERRO: Partida já existente" quando o nome já existe em partida Aberta ou Jogando
            if (retorno.StartsWith("ERRO", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(retorno, "Não foi possível criar a partida",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (!int.TryParse(retorno.Trim(), out int idPartidaCriada))
            {
                MessageBox.Show($"Resposta inesperada do servidor: {retorno}",
                    "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Lobby form = new Lobby();
            form.Show();
            this.Hide();

        }

        private void btnIrPartidaExistente_Click(object sender, EventArgs e) //evento de clique do botão ir para partida existente
        {
            Forms.Lobby.Show();
            this.Hide();
        }
        
    }
}