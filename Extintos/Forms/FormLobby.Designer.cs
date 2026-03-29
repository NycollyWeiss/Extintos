namespace Extintos
{
    partial class FormLobby
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblVersao = new System.Windows.Forms.Label();
            this.dgvPartida = new System.Windows.Forms.DataGridView();
            this.lblTituloSelecionePartida = new System.Windows.Forms.Label();
            this.btnListarAsPartidas = new System.Windows.Forms.Button();
            this.txtNomeDoJogador = new System.Windows.Forms.TextBox();
            this.lblTituloNomeDoJogador = new System.Windows.Forms.Label();
            this.lblTituloIDDaPartida = new System.Windows.Forms.Label();
            this.txtIdDaPartida = new System.Windows.Forms.TextBox();
            this.lblTituloSenhaDaPartida = new System.Windows.Forms.Label();
            this.txtSenhaDaPartida = new System.Windows.Forms.TextBox();
            this.btnEntrarNaPartida = new System.Windows.Forms.Button();
            this.btnVoltar1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartida)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersao
            // 
            this.lblVersao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersao.AutoSize = true;
            this.lblVersao.Location = new System.Drawing.Point(1316, 701);
            this.lblVersao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(0, 16);
            this.lblVersao.TabIndex = 3;
            // 
            // dgvPartida
            // 
            this.dgvPartida.AllowUserToAddRows = false;
            this.dgvPartida.AllowUserToDeleteRows = false;
            this.dgvPartida.AllowUserToOrderColumns = true;
            this.dgvPartida.AllowUserToResizeColumns = false;
            this.dgvPartida.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvPartida.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvPartida.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPartida.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvPartida.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkGreen;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.MediumSeaGreen;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPartida.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPartida.ColumnHeadersHeight = 40;
            this.dgvPartida.Location = new System.Drawing.Point(775, 154);
            this.dgvPartida.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPartida.Name = "dgvPartida";
            this.dgvPartida.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.MediumAquamarine;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.DarkCyan;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPartida.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPartida.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.MediumAquamarine;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Teal;
            this.dgvPartida.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPartida.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPartida.Size = new System.Drawing.Size(628, 519);
            this.dgvPartida.TabIndex = 2;
            this.dgvPartida.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPartida_CellContentClick);
            // 
            // lblTituloSelecionePartida
            // 
            this.lblTituloSelecionePartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloSelecionePartida.AutoSize = true;
            this.lblTituloSelecionePartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloSelecionePartida.Location = new System.Drawing.Point(458, 35);
            this.lblTituloSelecionePartida.Name = "lblTituloSelecionePartida";
            this.lblTituloSelecionePartida.Size = new System.Drawing.Size(366, 38);
            this.lblTituloSelecionePartida.TabIndex = 4;
            this.lblTituloSelecionePartida.Text = "Selecione a Sua Partida";
            this.lblTituloSelecionePartida.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnListarAsPartidas
            // 
            this.btnListarAsPartidas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnListarAsPartidas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnListarAsPartidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListarAsPartidas.Location = new System.Drawing.Point(539, 91);
            this.btnListarAsPartidas.Name = "btnListarAsPartidas";
            this.btnListarAsPartidas.Size = new System.Drawing.Size(230, 49);
            this.btnListarAsPartidas.TabIndex = 5;
            this.btnListarAsPartidas.Text = "Listar Partidas";
            this.btnListarAsPartidas.UseVisualStyleBackColor = false;
            this.btnListarAsPartidas.Click += new System.EventHandler(this.btnListarAsPartidas_Click);
            // 
            // txtNomeDoJogador
            // 
            this.txtNomeDoJogador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNomeDoJogador.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeDoJogador.Location = new System.Drawing.Point(179, 278);
            this.txtNomeDoJogador.Name = "txtNomeDoJogador";
            this.txtNomeDoJogador.Size = new System.Drawing.Size(236, 38);
            this.txtNomeDoJogador.TabIndex = 6;
            // 
            // lblTituloNomeDoJogador
            // 
            this.lblTituloNomeDoJogador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloNomeDoJogador.AutoSize = true;
            this.lblTituloNomeDoJogador.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloNomeDoJogador.Location = new System.Drawing.Point(174, 230);
            this.lblTituloNomeDoJogador.Name = "lblTituloNomeDoJogador";
            this.lblTituloNomeDoJogador.Size = new System.Drawing.Size(190, 25);
            this.lblTituloNomeDoJogador.TabIndex = 7;
            this.lblTituloNomeDoJogador.Text = "Nome do Jogador:";
            // 
            // lblTituloIDDaPartida
            // 
            this.lblTituloIDDaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloIDDaPartida.AutoSize = true;
            this.lblTituloIDDaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloIDDaPartida.Location = new System.Drawing.Point(174, 345);
            this.lblTituloIDDaPartida.Name = "lblTituloIDDaPartida";
            this.lblTituloIDDaPartida.Size = new System.Drawing.Size(144, 25);
            this.lblTituloIDDaPartida.TabIndex = 8;
            this.lblTituloIDDaPartida.Text = "ID da Partida:";
            // 
            // txtIdDaPartida
            // 
            this.txtIdDaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtIdDaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdDaPartida.Location = new System.Drawing.Point(179, 395);
            this.txtIdDaPartida.Name = "txtIdDaPartida";
            this.txtIdDaPartida.Size = new System.Drawing.Size(236, 38);
            this.txtIdDaPartida.TabIndex = 9;
            // 
            // lblTituloSenhaDaPartida
            // 
            this.lblTituloSenhaDaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloSenhaDaPartida.AutoSize = true;
            this.lblTituloSenhaDaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloSenhaDaPartida.Location = new System.Drawing.Point(174, 476);
            this.lblTituloSenhaDaPartida.Name = "lblTituloSenhaDaPartida";
            this.lblTituloSenhaDaPartida.Size = new System.Drawing.Size(186, 25);
            this.lblTituloSenhaDaPartida.TabIndex = 10;
            this.lblTituloSenhaDaPartida.Text = "Senha da Partida:";
            // 
            // txtSenhaDaPartida
            // 
            this.txtSenhaDaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSenhaDaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaDaPartida.Location = new System.Drawing.Point(179, 534);
            this.txtSenhaDaPartida.Name = "txtSenhaDaPartida";
            this.txtSenhaDaPartida.Size = new System.Drawing.Size(236, 38);
            this.txtSenhaDaPartida.TabIndex = 11;
            // 
            // btnEntrarNaPartida
            // 
            this.btnEntrarNaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEntrarNaPartida.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnEntrarNaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrarNaPartida.Location = new System.Drawing.Point(478, 387);
            this.btnEntrarNaPartida.Name = "btnEntrarNaPartida";
            this.btnEntrarNaPartida.Size = new System.Drawing.Size(185, 60);
            this.btnEntrarNaPartida.TabIndex = 12;
            this.btnEntrarNaPartida.Text = "Entrar na Partida";
            this.btnEntrarNaPartida.UseVisualStyleBackColor = false;
            this.btnEntrarNaPartida.Click += new System.EventHandler(this.btnEntrarNaPartida_Click);
            // 
            // btnVoltar1
            // 
            this.btnVoltar1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnVoltar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar1.Location = new System.Drawing.Point(29, 24);
            this.btnVoltar1.Name = "btnVoltar1";
            this.btnVoltar1.Size = new System.Drawing.Size(35, 49);
            this.btnVoltar1.TabIndex = 13;
            this.btnVoltar1.Text = "❮";
            this.btnVoltar1.UseVisualStyleBackColor = false;
            this.btnVoltar1.Click += new System.EventHandler(this.btnVoltar1_Click);
            // 
            // FormLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 736);
            this.Controls.Add(this.btnVoltar1);
            this.Controls.Add(this.btnEntrarNaPartida);
            this.Controls.Add(this.txtSenhaDaPartida);
            this.Controls.Add(this.lblTituloSenhaDaPartida);
            this.Controls.Add(this.txtIdDaPartida);
            this.Controls.Add(this.lblTituloIDDaPartida);
            this.Controls.Add(this.lblTituloNomeDoJogador);
            this.Controls.Add(this.txtNomeDoJogador);
            this.Controls.Add(this.btnListarAsPartidas);
            this.Controls.Add(this.lblTituloSelecionePartida);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.dgvPartida);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormLobby";
            this.Text = "FormLobby";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartida)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.DataGridView dgvPartida;
        private System.Windows.Forms.Label lblTituloSelecionePartida;
        private System.Windows.Forms.Button btnListarAsPartidas;
        private System.Windows.Forms.TextBox txtNomeDoJogador;
        private System.Windows.Forms.Label lblTituloNomeDoJogador;
        private System.Windows.Forms.Label lblTituloIDDaPartida;
        private System.Windows.Forms.TextBox txtIdDaPartida;
        private System.Windows.Forms.Label lblTituloSenhaDaPartida;
        private System.Windows.Forms.TextBox txtSenhaDaPartida;
        private System.Windows.Forms.Button btnEntrarNaPartida;
        private System.Windows.Forms.Button btnVoltar1;
    }
}