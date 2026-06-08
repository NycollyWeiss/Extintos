namespace Extintos.Telas
{
    partial class TelaInicial
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblNomedaPartida = new System.Windows.Forms.Label();
            this.txtNomedaPartida = new System.Windows.Forms.TextBox();
            this.lblSenhadaPartida = new System.Windows.Forms.Label();
            this.txtSenhadaPartida = new System.Windows.Forms.TextBox();
            this.lblNomedoGrupo = new System.Windows.Forms.Label();
            this.txtNomedoGrupo = new System.Windows.Forms.TextBox();
            this.btnCriarPartida = new System.Windows.Forms.Button();
            this.lblIdGerado = new System.Windows.Forms.Label();
            this.lblVersao = new System.Windows.Forms.Label();
            this.btnIrPartidaExistente = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNomedaPartida
            // 
            this.lblNomedaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNomedaPartida.AutoSize = true;
            this.lblNomedaPartida.BackColor = System.Drawing.Color.Transparent;
            this.lblNomedaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomedaPartida.ForeColor = System.Drawing.Color.White;
            this.lblNomedaPartida.Location = new System.Drawing.Point(90, 141);
            this.lblNomedaPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNomedaPartida.Name = "lblNomedaPartida";
            this.lblNomedaPartida.Size = new System.Drawing.Size(165, 24);
            this.lblNomedaPartida.TabIndex = 13;
            this.lblNomedaPartida.Text = "Nome da Partida";
            // 
            // txtNomedaPartida
            // 
            this.txtNomedaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNomedaPartida.Location = new System.Drawing.Point(94, 167);
            this.txtNomedaPartida.Margin = new System.Windows.Forms.Padding(2);
            this.txtNomedaPartida.Name = "txtNomedaPartida";
            this.txtNomedaPartida.Size = new System.Drawing.Size(141, 20);
            this.txtNomedaPartida.TabIndex = 14;
            this.txtNomedaPartida.Text = "SleepToken";
            // 
            // lblSenhadaPartida
            // 
            this.lblSenhadaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSenhadaPartida.AutoSize = true;
            this.lblSenhadaPartida.BackColor = System.Drawing.Color.Transparent;
            this.lblSenhadaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenhadaPartida.ForeColor = System.Drawing.Color.White;
            this.lblSenhadaPartida.Location = new System.Drawing.Point(86, 200);
            this.lblSenhadaPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSenhadaPartida.Name = "lblSenhadaPartida";
            this.lblSenhadaPartida.Size = new System.Drawing.Size(169, 24);
            this.lblSenhadaPartida.TabIndex = 15;
            this.lblSenhadaPartida.Text = "Senha da Partida";
            // 
            // txtSenhadaPartida
            // 
            this.txtSenhadaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSenhadaPartida.Location = new System.Drawing.Point(94, 226);
            this.txtSenhadaPartida.Margin = new System.Windows.Forms.Padding(2);
            this.txtSenhadaPartida.Name = "txtSenhadaPartida";
            this.txtSenhadaPartida.Size = new System.Drawing.Size(141, 20);
            this.txtSenhadaPartida.TabIndex = 16;
            this.txtSenhadaPartida.Text = "1234";
            // 
            // lblNomedoGrupo
            // 
            this.lblNomedoGrupo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNomedoGrupo.AutoSize = true;
            this.lblNomedoGrupo.BackColor = System.Drawing.Color.Transparent;
            this.lblNomedoGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomedoGrupo.ForeColor = System.Drawing.Color.White;
            this.lblNomedoGrupo.Location = new System.Drawing.Point(90, 256);
            this.lblNomedoGrupo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNomedoGrupo.Name = "lblNomedoGrupo";
            this.lblNomedoGrupo.Size = new System.Drawing.Size(160, 24);
            this.lblNomedoGrupo.TabIndex = 17;
            this.lblNomedoGrupo.Text = "Nome do Grupo";
            // 
            // txtNomedoGrupo
            // 
            this.txtNomedoGrupo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNomedoGrupo.BackColor = System.Drawing.SystemColors.Menu;
            this.txtNomedoGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomedoGrupo.Location = new System.Drawing.Point(90, 282);
            this.txtNomedoGrupo.Margin = new System.Windows.Forms.Padding(2);
            this.txtNomedoGrupo.Name = "txtNomedoGrupo";
            this.txtNomedoGrupo.Size = new System.Drawing.Size(145, 21);
            this.txtNomedoGrupo.TabIndex = 18;
            this.txtNomedoGrupo.Text = "Extintos";
            this.txtNomedoGrupo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCriarPartida
            // 
            this.btnCriarPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCriarPartida.BackColor = System.Drawing.Color.Tomato;
            this.btnCriarPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCriarPartida.ForeColor = System.Drawing.Color.White;
            this.btnCriarPartida.Location = new System.Drawing.Point(39, 339);
            this.btnCriarPartida.Margin = new System.Windows.Forms.Padding(2);
            this.btnCriarPartida.Name = "btnCriarPartida";
            this.btnCriarPartida.Size = new System.Drawing.Size(114, 50);
            this.btnCriarPartida.TabIndex = 19;
            this.btnCriarPartida.Text = "Criar Partida";
            this.btnCriarPartida.UseVisualStyleBackColor = false;
            this.btnCriarPartida.Click += new System.EventHandler(this.btnCriarPartida_Click);
            // 
            // lblIdGerado
            // 
            this.lblIdGerado.AutoSize = true;
            this.lblIdGerado.Location = new System.Drawing.Point(443, 451);
            this.lblIdGerado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIdGerado.Name = "lblIdGerado";
            this.lblIdGerado.Size = new System.Drawing.Size(0, 13);
            this.lblIdGerado.TabIndex = 21;
            // 
            // lblVersao
            // 
            this.lblVersao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVersao.AutoSize = true;
            this.lblVersao.BackColor = System.Drawing.Color.White;
            this.lblVersao.ForeColor = System.Drawing.Color.Black;
            this.lblVersao.Location = new System.Drawing.Point(778, 469);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(0, 13);
            this.lblVersao.TabIndex = 23;
            // 
            // btnIrPartidaExistente
            // 
            this.btnIrPartidaExistente.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnIrPartidaExistente.BackColor = System.Drawing.Color.Tomato;
            this.btnIrPartidaExistente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIrPartidaExistente.ForeColor = System.Drawing.Color.White;
            this.btnIrPartidaExistente.Location = new System.Drawing.Point(176, 339);
            this.btnIrPartidaExistente.Margin = new System.Windows.Forms.Padding(2);
            this.btnIrPartidaExistente.Name = "btnIrPartidaExistente";
            this.btnIrPartidaExistente.Size = new System.Drawing.Size(113, 50);
            this.btnIrPartidaExistente.TabIndex = 24;
            this.btnIrPartidaExistente.Text = "Ir para o lobby";
            this.btnIrPartidaExistente.UseVisualStyleBackColor = false;
            this.btnIrPartidaExistente.Click += new System.EventHandler(this.btnIrPartidaExistente_Click);
            // 
            // TelaInicialTelaInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Extintos.Properties.Resources.Capa_Extintos;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(865, 500);
            this.Controls.Add(this.btnIrPartidaExistente);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.lblIdGerado);
            this.Controls.Add(this.btnCriarPartida);
            this.Controls.Add(this.txtNomedoGrupo);
            this.Controls.Add(this.lblNomedoGrupo);
            this.Controls.Add(this.txtSenhadaPartida);
            this.Controls.Add(this.lblSenhadaPartida);
            this.Controls.Add(this.txtNomedaPartida);
            this.Controls.Add(this.lblNomedaPartida);
            this.DoubleBuffered = true;
            this.Location = new System.Drawing.Point(15, 15);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TelaInicial";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label lblNomedaPartida;
        private System.Windows.Forms.TextBox txtNomedaPartida;
        private System.Windows.Forms.Label lblSenhadaPartida;
        private System.Windows.Forms.TextBox txtSenhadaPartida;
        private System.Windows.Forms.Label lblNomedoGrupo;
        private System.Windows.Forms.TextBox txtNomedoGrupo;
        private System.Windows.Forms.Button btnCriarPartida;
        private System.Windows.Forms.Label lblIdGerado;
        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.Button btnIrPartidaExistente;
    }
}

