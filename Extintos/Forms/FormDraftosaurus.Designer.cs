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
            this.bntExibirMao = new System.Windows.Forms.Button();
            this.lblVersao3 = new System.Windows.Forms.Label();
            this.lblMensagemInicioPartida = new System.Windows.Forms.Label();
            this.lblNomeDino = new System.Windows.Forms.Label();
            this.btnJogar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRetornoInicio = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bntExibirMao
            // 
            this.bntExibirMao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bntExibirMao.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.bntExibirMao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntExibirMao.Location = new System.Drawing.Point(70, 147);
            this.bntExibirMao.Name = "bntExibirMao";
            this.bntExibirMao.Size = new System.Drawing.Size(99, 46);
            this.bntExibirMao.TabIndex = 45;
            this.bntExibirMao.Text = "Ver Mão";
            this.bntExibirMao.UseVisualStyleBackColor = false;
            this.bntExibirMao.Click += new System.EventHandler(this.bntExibirMao_Click);
            // 
            // lblVersao3
            // 
            this.lblVersao3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVersao3.AutoSize = true;
            this.lblVersao3.Location = new System.Drawing.Point(777, 457);
            this.lblVersao3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVersao3.Name = "lblVersao3";
            this.lblVersao3.Size = new System.Drawing.Size(0, 13);
            this.lblVersao3.TabIndex = 52;
            // 
            // lblMensagemInicioPartida
            // 
            this.lblMensagemInicioPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMensagemInicioPartida.AutoSize = true;
            this.lblMensagemInicioPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensagemInicioPartida.Location = new System.Drawing.Point(45, 10);
            this.lblMensagemInicioPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMensagemInicioPartida.Name = "lblMensagemInicioPartida";
            this.lblMensagemInicioPartida.Size = new System.Drawing.Size(193, 18);
            this.lblMensagemInicioPartida.TabIndex = 53;
            this.lblMensagemInicioPartida.Text = "Mensagem Inicio Partida";
            // 
            // lblNomeDino
            // 
            this.lblNomeDino.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNomeDino.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeDino.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblNomeDino.Location = new System.Drawing.Point(45, 210);
            this.lblNomeDino.Name = "lblNomeDino";
            this.lblNomeDino.Size = new System.Drawing.Size(165, 87);
            this.lblNomeDino.TabIndex = 54;
            // 
            // btnJogar
            // 
            this.btnJogar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnJogar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnJogar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJogar.Location = new System.Drawing.Point(70, 371);
            this.btnJogar.Name = "btnJogar";
            this.btnJogar.Size = new System.Drawing.Size(99, 46);
            this.btnJogar.TabIndex = 56;
            this.btnJogar.Text = "Jogar";
            this.btnJogar.UseVisualStyleBackColor = false;
            this.btnJogar.Click += new System.EventHandler(this.btnJogar_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(281, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 518);
            this.panel1.TabIndex = 57;
            // 
            // lblRetornoInicio
            // 
            this.lblRetornoInicio.Location = new System.Drawing.Point(26, 73);
            this.lblRetornoInicio.Name = "lblRetornoInicio";
            this.lblRetornoInicio.Size = new System.Drawing.Size(193, 71);
            this.lblRetornoInicio.TabIndex = 58;
            // 
            // FormDraftosaurus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1028, 578);
            this.Controls.Add(this.lblRetornoInicio);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnJogar);
            this.Controls.Add(this.lblNomeDino);
            this.Controls.Add(this.lblMensagemInicioPartida);
            this.Controls.Add(this.lblVersao3);
            this.Controls.Add(this.bntExibirMao);
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormDraftosaurus";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnJogar;

        private System.Windows.Forms.Label lblNomeDino;

        private System.Windows.Forms.RichTextBox richTextBox1;

        #endregion

        private System.Windows.Forms.Button bntExibirMao;
        private System.Windows.Forms.Label lblRetornoInicio;
        private System.Windows.Forms.Label lblVersao3;
        private System.Windows.Forms.Label lblMensagemInicioPartida;
        private System.Windows.Forms.Panel panel1;
    }
}