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
    public partial class FormLobby: Form
    {
        public FormLobby()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = FormWindowState.Maximized;
        }

        private void btnListarAsPartidas_Click(object sender, EventArgs e)
        {
            dgvPartida.DataSource = Partida.ListarPartidas("T"); //Lista para dentro do dgvPartidas
        }
    }
}
