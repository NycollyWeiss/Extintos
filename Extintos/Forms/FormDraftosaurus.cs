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
    public partial class FormDraftosaurus : Form
    {
        private int idJogador = 0;
        private String senhaJogador = "";
        private string idPartida = "";
        public FormDraftosaurus(string idPartida, string senhaJogador, int idJogador)
        {
            
            this.senhaJogador = senhaJogador;
            this.idJogador = idJogador;
            this.idPartida = idPartida;
            InitializeComponent();
            lblVersao3.Text = Jogo.versao;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = FormWindowState.Maximized;
        }

        private void bntEntrar_Click(object sender, EventArgs e)
        {
            string retornoEntrar = Jogo.Iniciar(idJogador, senhaJogador);


            

        }

        private void bntExibirMao_Click(object sender, EventArgs e)
        {
            string retornoMao = Jogo.ExibirMao(idJogador,senhaJogador);

            retornoMao = retornoMao.Replace("\r", "");
            retornoMao = retornoMao.Substring(0, retornoMao.Length - 1);
            string[] dinossauros = retornoMao.Split('\n');

            lstMao.Items.Clear();
            for (int i = 0; i < dinossauros.Length; i++)
            {
                lstMao.Items.Add(dinossauros[i]);

            }
            
        }

       
    }
}
