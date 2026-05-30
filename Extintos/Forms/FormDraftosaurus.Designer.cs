namespace Extintos
{
    partial class FormDraftosaurus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblVersao3 = new System.Windows.Forms.Label();
            this.lblVersaoTres = new System.Windows.Forms.Label();
            this.txtHistorico = new System.Windows.Forms.TextBox();
            this.frmTimer = new System.Windows.Forms.Timer(this.components);
            this.picDado = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDado)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersao3
            // 
            this.lblVersao3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVersao3.AutoSize = true;
            this.lblVersao3.Location = new System.Drawing.Point(847, 496);
            this.lblVersao3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVersao3.Name = "lblVersao3";
            this.lblVersao3.Size = new System.Drawing.Size(0, 13);
            this.lblVersao3.TabIndex = 52;
            // 
            // lblVersaoTres
            // 
            this.lblVersaoTres.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVersaoTres.AutoSize = true;
            this.lblVersaoTres.Location = new System.Drawing.Point(978, 597);
            this.lblVersaoTres.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVersaoTres.Name = "lblVersaoTres";
            this.lblVersaoTres.Size = new System.Drawing.Size(0, 13);
            this.lblVersaoTres.TabIndex = 61;
            // 
            // txtHistorico
            // 
            this.txtHistorico.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtHistorico.Location = new System.Drawing.Point(801, 291);
            this.txtHistorico.Multiline = true;
            this.txtHistorico.Name = "txtHistorico";
            this.txtHistorico.ReadOnly = true;
            this.txtHistorico.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHistorico.Size = new System.Drawing.Size(227, 258);
            this.txtHistorico.TabIndex = 67;
            // 
            // frmTimer
            // 
            this.frmTimer.Interval = 1000;
            this.frmTimer.Tick += new System.EventHandler(this.frmTimer_Tick);
            // 
            // picDado
            // 
            this.picDado.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picDado.BackColor = System.Drawing.Color.Transparent;
            this.picDado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picDado.Location = new System.Drawing.Point(850, 138);
            this.picDado.Name = "picDado";
            this.picDado.Size = new System.Drawing.Size(109, 104);
            this.picDado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDado.TabIndex = 74;
            this.picDado.TabStop = false;
            // 
            // FormDraftosaurus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Extintos.Properties.Resources.Mesa;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Controls.Add(this.picDado);
            this.Controls.Add(this.txtHistorico);
            this.Controls.Add(this.lblVersaoTres);
            this.Controls.Add(this.lblVersao3);
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormDraftosaurus";
            this.Load += new System.EventHandler(this.FormDraftosaurus_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmPaint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.picDado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label lblVersao3;
        private System.Windows.Forms.Label lblVersaoTres;
        private System.Windows.Forms.TextBox txtHistorico;
        private System.Windows.Forms.Timer frmTimer;
        private System.Windows.Forms.PictureBox picDado;
    }
}