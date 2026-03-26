using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Extintos
{
    internal class Config
    {
        public static void Fullscreen(Form form)
        {
            if (form == null) return;

          form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
          form.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
          form.WindowState = FormWindowState.Maximized;
          form.Show();
        }


        //public List<String> jogadores ()
    }
}