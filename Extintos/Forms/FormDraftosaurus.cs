using Draft;
using Extintos.Enumeration;
using Extintos.Model;
using Extintos.Model;
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
    public partial class FormDraftosaurus : Form
    {
        private string retorno;
        private Jogador dadosJogador; //*******
        public FormDraftosaurus()
        {
            
            InitializeComponent();
            lblVersao3.Text = Jogo.versao;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = FormWindowState.Maximized;
        }

        internal FormDraftosaurus(string retorno, Jogador dadosJogador) : this() //Construtor com parâmetros
        {
            this.retorno = retorno;
            this.dadosJogador = dadosJogador;
            lblMensagemInicioPartida.Text = retorno;
        }
        

        private void bntExibirMao_Click(object sender, EventArgs e)
        {
            string retornoMao = Jogo.ExibirMao(dadosJogador.IdJogador,dadosJogador.Senha);

            retornoMao = retornoMao.Replace("\r", "");
            retornoMao = retornoMao.Substring(0, retornoMao.Length - 1);
            string[] dinossauros = retornoMao.Split('\n');

            lstMao.Items.Clear();
            for (int i = 0; i < dinossauros.Length; i++)
            {
                lstMao.Items.Add(dinossauros[i]);

            }
            
        }

        //Dinossauros.Dinossauro dinossaurio = (Dinossauros.Dinossauro)Enum.Parse(Dinossauros.Dinossauro);
        //dinossaurio.PegaNome()
        //dinossaurio.PegaCor();

        private void FormDraftosaurus_Load(object sender, EventArgs e)
        {

        }

        private void lstMao_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
