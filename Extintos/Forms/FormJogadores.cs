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
        private string idPartida = "";
        private string senhaJogador = "";
        private int idJogador = 0;

        public FormJogadores(string idPartida, string senhaJogador, int idJogador)
        {
            this.idPartida = idPartida;
            this.senhaJogador = senhaJogador;
            this.idJogador = idJogador;

            InitializeComponent();
            lblVersao2.Text = Jogo.versao;

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = FormWindowState.Maximized;


          
            lblSenhaGeradaa.Text = senhaJogador;
            lblIdGeradoJogador.Text = idJogador.ToString();
        }
      

        private void bntListaJogadores_Click(object sender, EventArgs e)
        {
            int idPa = int.Parse(idPartida);
            string retornoJogadores = Jogo.ListarJogadores(idPa);

            retornoJogadores = retornoJogadores.Replace("\r", "");
            retornoJogadores = retornoJogadores.Substring(0, retornoJogadores.Length - 1);
            string[] jogadores = retornoJogadores.Split('\n');

            latJogadores.Items.Clear();
            for (int i = 0; i < jogadores.Length; i++)
            {
                latJogadores.Items.Add(jogadores[i]);

            }
        }

      

        private void btnSim_Click(object sender, EventArgs e)
        {
            Forms.FormPartida.Show();
            this.Hide();
        }

        private void btnNão_Click(object sender, EventArgs e)
        {
            FormDraftosaurus formDraftosaurus = new FormDraftosaurus(idPartida, senhaJogador,idJogador);
            formDraftosaurus.Show();
            this.Hide();
        }

        private void FormJogadores_Load(object sender, EventArgs e)
        {

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






