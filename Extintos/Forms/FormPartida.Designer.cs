namespace Extintos
{
    partial class FormPartida
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
            this.lblDraftosaurus = new System.Windows.Forms.Label();
            this.btnListarPartidas = new System.Windows.Forms.Button();
            this.lstPartidas = new System.Windows.Forms.ListBox();
            this.lblTituloIdPartida = new System.Windows.Forms.Label();
            this.lblTituloNomePartida = new System.Windows.Forms.Label();
            this.lblTituloDataPartida = new System.Windows.Forms.Label();
            this.btnEntrarPartida = new System.Windows.Forms.Button();
            this.lblPartida = new System.Windows.Forms.Label();
            this.lblNomePartida = new System.Windows.Forms.Label();
            this.lblDataPartida = new System.Windows.Forms.Label();
            this.txtSenhaPartida = new System.Windows.Forms.TextBox();
            this.lblSenhadaPartida = new System.Windows.Forms.Label();
            this.txtIDdaPartida = new System.Windows.Forms.TextBox();
            this.lblIddaPartida = new System.Windows.Forms.Label();
            this.txtNomeJogador = new System.Windows.Forms.TextBox();
            this.lblNomeJogador = new System.Windows.Forms.Label();
            this.lblVersao1 = new System.Windows.Forms.Label();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.dgvPartidas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDraftosaurus
            // 
            this.lblDraftosaurus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDraftosaurus.AutoSize = true;
            this.lblDraftosaurus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDraftosaurus.Location = new System.Drawing.Point(270, 84);
            this.lblDraftosaurus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDraftosaurus.Name = "lblDraftosaurus";
            this.lblDraftosaurus.Size = new System.Drawing.Size(262, 26);
            this.lblDraftosaurus.TabIndex = 23;
            this.lblDraftosaurus.Text = "Selecione a sua partida";
            // 
            // btnListarPartidas
            // 
            this.btnListarPartidas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnListarPartidas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnListarPartidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListarPartidas.Location = new System.Drawing.Point(534, 126);
            this.btnListarPartidas.Margin = new System.Windows.Forms.Padding(2);
            this.btnListarPartidas.Name = "btnListarPartidas";
            this.btnListarPartidas.Size = new System.Drawing.Size(122, 23);
            this.btnListarPartidas.TabIndex = 24;
            this.btnListarPartidas.Text = "Listar Partidas";
            this.btnListarPartidas.UseVisualStyleBackColor = false;
            this.btnListarPartidas.Click += new System.EventHandler(this.btnListarPartidas_Click);
            // 
            // lstPartidas
            // 
            this.lstPartidas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstPartidas.FormattingEnabled = true;
            this.lstPartidas.Location = new System.Drawing.Point(275, 144);
            this.lstPartidas.Margin = new System.Windows.Forms.Padding(2);
            this.lstPartidas.Name = "lstPartidas";
            this.lstPartidas.Size = new System.Drawing.Size(213, 225);
            this.lstPartidas.TabIndex = 25;
            this.lstPartidas.SelectedIndexChanged += new System.EventHandler(this.lstPartidas_SelectedIndexChanged);
            // 
            // lblTituloIdPartida
            // 
            this.lblTituloIdPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloIdPartida.AutoSize = true;
            this.lblTituloIdPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloIdPartida.Location = new System.Drawing.Point(629, 352);
            this.lblTituloIdPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTituloIdPartida.Name = "lblTituloIdPartida";
            this.lblTituloIdPartida.Size = new System.Drawing.Size(106, 17);
            this.lblTituloIdPartida.TabIndex = 26;
            this.lblTituloIdPartida.Text = "Id da Partida:";
            // 
            // lblTituloNomePartida
            // 
            this.lblTituloNomePartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloNomePartida.AutoSize = true;
            this.lblTituloNomePartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloNomePartida.Location = new System.Drawing.Point(629, 408);
            this.lblTituloNomePartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTituloNomePartida.Name = "lblTituloNomePartida";
            this.lblTituloNomePartida.Size = new System.Drawing.Size(134, 17);
            this.lblTituloNomePartida.TabIndex = 27;
            this.lblTituloNomePartida.Text = "Nome da Partida:";
            // 
            // lblTituloDataPartida
            // 
            this.lblTituloDataPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloDataPartida.AutoSize = true;
            this.lblTituloDataPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloDataPartida.Location = new System.Drawing.Point(629, 465);
            this.lblTituloDataPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTituloDataPartida.Name = "lblTituloDataPartida";
            this.lblTituloDataPartida.Size = new System.Drawing.Size(127, 17);
            this.lblTituloDataPartida.TabIndex = 28;
            this.lblTituloDataPartida.Text = "Data da Partida:";
            // 
            // btnEntrarPartida
            // 
            this.btnEntrarPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEntrarPartida.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEntrarPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrarPartida.Location = new System.Drawing.Point(114, 323);
            this.btnEntrarPartida.Margin = new System.Windows.Forms.Padding(2);
            this.btnEntrarPartida.Name = "btnEntrarPartida";
            this.btnEntrarPartida.Size = new System.Drawing.Size(145, 50);
            this.btnEntrarPartida.TabIndex = 29;
            this.btnEntrarPartida.Text = "Entrar na partida";
            this.btnEntrarPartida.UseVisualStyleBackColor = false;
            this.btnEntrarPartida.Click += new System.EventHandler(this.btnEntrarPartida_Click);
            // 
            // lblPartida
            // 
            this.lblPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPartida.AutoSize = true;
            this.lblPartida.Location = new System.Drawing.Point(631, 382);
            this.lblPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPartida.Name = "lblPartida";
            this.lblPartida.Size = new System.Drawing.Size(0, 13);
            this.lblPartida.TabIndex = 30;
            // 
            // lblNomePartida
            // 
            this.lblNomePartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNomePartida.AutoSize = true;
            this.lblNomePartida.Location = new System.Drawing.Point(631, 438);
            this.lblNomePartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNomePartida.Name = "lblNomePartida";
            this.lblNomePartida.Size = new System.Drawing.Size(0, 13);
            this.lblNomePartida.TabIndex = 31;
            // 
            // lblDataPartida
            // 
            this.lblDataPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDataPartida.AutoSize = true;
            this.lblDataPartida.Location = new System.Drawing.Point(631, 492);
            this.lblDataPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDataPartida.Name = "lblDataPartida";
            this.lblDataPartida.Size = new System.Drawing.Size(0, 13);
            this.lblDataPartida.TabIndex = 32;
            // 
            // txtSenhaPartida
            // 
            this.txtSenhaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSenhaPartida.Location = new System.Drawing.Point(114, 269);
            this.txtSenhaPartida.Margin = new System.Windows.Forms.Padding(2);
            this.txtSenhaPartida.Name = "txtSenhaPartida";
            this.txtSenhaPartida.Size = new System.Drawing.Size(125, 20);
            this.txtSenhaPartida.TabIndex = 37;
            // 
            // lblSenhadaPartida
            // 
            this.lblSenhadaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSenhadaPartida.AutoSize = true;
            this.lblSenhadaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenhadaPartida.Location = new System.Drawing.Point(114, 244);
            this.lblSenhadaPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSenhadaPartida.Name = "lblSenhadaPartida";
            this.lblSenhadaPartida.Size = new System.Drawing.Size(122, 15);
            this.lblSenhadaPartida.TabIndex = 36;
            this.lblSenhadaPartida.Text = "Senha da Partida:";
            // 
            // txtIDdaPartida
            // 
            this.txtIDdaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtIDdaPartida.Location = new System.Drawing.Point(114, 209);
            this.txtIDdaPartida.Margin = new System.Windows.Forms.Padding(2);
            this.txtIDdaPartida.Name = "txtIDdaPartida";
            this.txtIDdaPartida.Size = new System.Drawing.Size(125, 20);
            this.txtIDdaPartida.TabIndex = 35;
            // 
            // lblIddaPartida
            // 
            this.lblIddaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblIddaPartida.AutoSize = true;
            this.lblIddaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIddaPartida.Location = new System.Drawing.Point(114, 184);
            this.lblIddaPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIddaPartida.Name = "lblIddaPartida";
            this.lblIddaPartida.Size = new System.Drawing.Size(95, 15);
            this.lblIddaPartida.TabIndex = 34;
            this.lblIddaPartida.Text = "ID da Partida:";
            // 
            // txtNomeJogador
            // 
            this.txtNomeJogador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNomeJogador.Location = new System.Drawing.Point(114, 151);
            this.txtNomeJogador.Margin = new System.Windows.Forms.Padding(2);
            this.txtNomeJogador.Name = "txtNomeJogador";
            this.txtNomeJogador.Size = new System.Drawing.Size(122, 20);
            this.txtNomeJogador.TabIndex = 39;
            // 
            // lblNomeJogador
            // 
            this.lblNomeJogador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNomeJogador.AutoSize = true;
            this.lblNomeJogador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeJogador.Location = new System.Drawing.Point(114, 126);
            this.lblNomeJogador.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNomeJogador.Name = "lblNomeJogador";
            this.lblNomeJogador.Size = new System.Drawing.Size(125, 15);
            this.lblNomeJogador.TabIndex = 38;
            this.lblNomeJogador.Text = "Nome do Jogador:";
            // 
            // lblVersao1
            // 
            this.lblVersao1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVersao1.AutoSize = true;
            this.lblVersao1.Location = new System.Drawing.Point(665, 411);
            this.lblVersao1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVersao1.Name = "lblVersao1";
            this.lblVersao1.Size = new System.Drawing.Size(0, 13);
            this.lblVersao1.TabIndex = 40;
            // 
            // btnVoltar
            // 
            this.btnVoltar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(14, 10);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(2);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(63, 28);
            this.btnVoltar.TabIndex = 41;
            this.btnVoltar.Text = "< Voltar";
            this.btnVoltar.UseVisualStyleBackColor = false;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // dgvPartidas
            // 
            this.dgvPartidas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartidas.Location = new System.Drawing.Point(519, 162);
            this.dgvPartidas.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPartidas.Name = "dgvPartidas";
            this.dgvPartidas.RowHeadersWidth = 51;
            this.dgvPartidas.RowTemplate.Height = 24;
            this.dgvPartidas.Size = new System.Drawing.Size(202, 171);
            this.dgvPartidas.TabIndex = 42;
            // 
            // FormPartida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 498);
            this.Controls.Add(this.dgvPartidas);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.lblVersao1);
            this.Controls.Add(this.txtNomeJogador);
            this.Controls.Add(this.lblNomeJogador);
            this.Controls.Add(this.txtSenhaPartida);
            this.Controls.Add(this.lblSenhadaPartida);
            this.Controls.Add(this.txtIDdaPartida);
            this.Controls.Add(this.lblIddaPartida);
            this.Controls.Add(this.lblDataPartida);
            this.Controls.Add(this.lblNomePartida);
            this.Controls.Add(this.lblPartida);
            this.Controls.Add(this.btnEntrarPartida);
            this.Controls.Add(this.lblTituloDataPartida);
            this.Controls.Add(this.lblTituloNomePartida);
            this.Controls.Add(this.lblTituloIdPartida);
            this.Controls.Add(this.lstPartidas);
            this.Controls.Add(this.btnListarPartidas);
            this.Controls.Add(this.lblDraftosaurus);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormPartida";
            this.Text = "FormPartida";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartidas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDraftosaurus;
        private System.Windows.Forms.Button btnListarPartidas;
        private System.Windows.Forms.ListBox lstPartidas;
        private System.Windows.Forms.Label lblTituloIdPartida;
        private System.Windows.Forms.Label lblTituloNomePartida;
        private System.Windows.Forms.Label lblTituloDataPartida;
        private System.Windows.Forms.Button btnEntrarPartida;
        private System.Windows.Forms.Label lblPartida;
        private System.Windows.Forms.Label lblNomePartida;
        private System.Windows.Forms.Label lblDataPartida;
        private System.Windows.Forms.TextBox txtSenhaPartida;
        private System.Windows.Forms.Label lblSenhadaPartida;
        private System.Windows.Forms.TextBox txtIDdaPartida;
        private System.Windows.Forms.Label lblIddaPartida;
        private System.Windows.Forms.TextBox txtNomeJogador;
        private System.Windows.Forms.Label lblNomeJogador;
        private System.Windows.Forms.Label lblVersao1;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.DataGridView dgvPartidas;
    }
}