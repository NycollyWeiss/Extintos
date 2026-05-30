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
            this.lblVersao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVersao.AutoSize = true;
            this.lblVersao.Location = new System.Drawing.Point(1008, 563);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(0, 13);
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
            this.dgvPartida.Location = new System.Drawing.Point(581, 125);
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
            this.dgvPartida.Size = new System.Drawing.Size(471, 422);
            this.dgvPartida.TabIndex = 2;
            this.dgvPartida.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPartida_CellContentClick);
            // 
            // lblTituloSelecionePartida
            // 
            this.lblTituloSelecionePartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloSelecionePartida.AutoSize = true;
            this.lblTituloSelecionePartida.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloSelecionePartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloSelecionePartida.ForeColor = System.Drawing.Color.White;
            this.lblTituloSelecionePartida.Location = new System.Drawing.Point(335, 39);
            this.lblTituloSelecionePartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTituloSelecionePartida.Name = "lblTituloSelecionePartida";
            this.lblTituloSelecionePartida.Size = new System.Drawing.Size(349, 33);
            this.lblTituloSelecionePartida.TabIndex = 4;
            this.lblTituloSelecionePartida.Text = "Selecione a Sua Partida";
            this.lblTituloSelecionePartida.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnListarAsPartidas
            // 
            this.btnListarAsPartidas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnListarAsPartidas.BackColor = System.Drawing.Color.Tomato;
            this.btnListarAsPartidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListarAsPartidas.ForeColor = System.Drawing.Color.White;
            this.btnListarAsPartidas.Location = new System.Drawing.Point(403, 83);
            this.btnListarAsPartidas.Margin = new System.Windows.Forms.Padding(2);
            this.btnListarAsPartidas.Name = "btnListarAsPartidas";
            this.btnListarAsPartidas.Size = new System.Drawing.Size(172, 40);
            this.btnListarAsPartidas.TabIndex = 5;
            this.btnListarAsPartidas.Text = "Listar Partidas";
            this.btnListarAsPartidas.UseVisualStyleBackColor = false;
            this.btnListarAsPartidas.Click += new System.EventHandler(this.btnListarAsPartidas_Click);
            // 
            // txtNomeDoJogador
            // 
            this.txtNomeDoJogador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNomeDoJogador.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeDoJogador.Location = new System.Drawing.Point(134, 226);
            this.txtNomeDoJogador.Margin = new System.Windows.Forms.Padding(2);
            this.txtNomeDoJogador.Name = "txtNomeDoJogador";
            this.txtNomeDoJogador.Size = new System.Drawing.Size(178, 32);
            this.txtNomeDoJogador.TabIndex = 6;
            this.txtNomeDoJogador.Text = "Verssel";
            // 
            // lblTituloNomeDoJogador
            // 
            this.lblTituloNomeDoJogador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloNomeDoJogador.AutoSize = true;
            this.lblTituloNomeDoJogador.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloNomeDoJogador.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloNomeDoJogador.ForeColor = System.Drawing.Color.White;
            this.lblTituloNomeDoJogador.Location = new System.Drawing.Point(130, 187);
            this.lblTituloNomeDoJogador.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTituloNomeDoJogador.Name = "lblTituloNomeDoJogador";
            this.lblTituloNomeDoJogador.Size = new System.Drawing.Size(184, 24);
            this.lblTituloNomeDoJogador.TabIndex = 7;
            this.lblTituloNomeDoJogador.Text = "Nome do Jogador:";
            // 
            // lblTituloIDDaPartida
            // 
            this.lblTituloIDDaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloIDDaPartida.AutoSize = true;
            this.lblTituloIDDaPartida.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloIDDaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloIDDaPartida.ForeColor = System.Drawing.Color.White;
            this.lblTituloIDDaPartida.Location = new System.Drawing.Point(130, 280);
            this.lblTituloIDDaPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTituloIDDaPartida.Name = "lblTituloIDDaPartida";
            this.lblTituloIDDaPartida.Size = new System.Drawing.Size(134, 24);
            this.lblTituloIDDaPartida.TabIndex = 8;
            this.lblTituloIDDaPartida.Text = "ID da Partida:";
            // 
            // txtIdDaPartida
            // 
            this.txtIdDaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtIdDaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdDaPartida.Location = new System.Drawing.Point(134, 321);
            this.txtIdDaPartida.Margin = new System.Windows.Forms.Padding(2);
            this.txtIdDaPartida.Name = "txtIdDaPartida";
            this.txtIdDaPartida.Size = new System.Drawing.Size(178, 32);
            this.txtIdDaPartida.TabIndex = 9;
            // 
            // lblTituloSenhaDaPartida
            // 
            this.lblTituloSenhaDaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloSenhaDaPartida.AutoSize = true;
            this.lblTituloSenhaDaPartida.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloSenhaDaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloSenhaDaPartida.ForeColor = System.Drawing.Color.White;
            this.lblTituloSenhaDaPartida.Location = new System.Drawing.Point(130, 387);
            this.lblTituloSenhaDaPartida.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTituloSenhaDaPartida.Name = "lblTituloSenhaDaPartida";
            this.lblTituloSenhaDaPartida.Size = new System.Drawing.Size(175, 24);
            this.lblTituloSenhaDaPartida.TabIndex = 10;
            this.lblTituloSenhaDaPartida.Text = "Senha da Partida:";
            // 
            // txtSenhaDaPartida
            // 
            this.txtSenhaDaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSenhaDaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaDaPartida.Location = new System.Drawing.Point(134, 434);
            this.txtSenhaDaPartida.Margin = new System.Windows.Forms.Padding(2);
            this.txtSenhaDaPartida.Name = "txtSenhaDaPartida";
            this.txtSenhaDaPartida.Size = new System.Drawing.Size(178, 32);
            this.txtSenhaDaPartida.TabIndex = 11;
            this.txtSenhaDaPartida.Text = "1234";
            // 
            // btnEntrarNaPartida
            // 
            this.btnEntrarNaPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEntrarNaPartida.BackColor = System.Drawing.Color.Tomato;
            this.btnEntrarNaPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrarNaPartida.ForeColor = System.Drawing.Color.White;
            this.btnEntrarNaPartida.Location = new System.Drawing.Point(358, 314);
            this.btnEntrarNaPartida.Margin = new System.Windows.Forms.Padding(2);
            this.btnEntrarNaPartida.Name = "btnEntrarNaPartida";
            this.btnEntrarNaPartida.Size = new System.Drawing.Size(139, 49);
            this.btnEntrarNaPartida.TabIndex = 12;
            this.btnEntrarNaPartida.Text = "Entrar na Partida";
            this.btnEntrarNaPartida.UseVisualStyleBackColor = false;
            this.btnEntrarNaPartida.Click += new System.EventHandler(this.btnEntrarNaPartida_Click);
            // 
            // btnVoltar1
            // 
            this.btnVoltar1.BackColor = System.Drawing.Color.Tomato;
            this.btnVoltar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar1.ForeColor = System.Drawing.Color.White;
            this.btnVoltar1.Location = new System.Drawing.Point(22, 20);
            this.btnVoltar1.Margin = new System.Windows.Forms.Padding(2);
            this.btnVoltar1.Name = "btnVoltar1";
            this.btnVoltar1.Size = new System.Drawing.Size(26, 40);
            this.btnVoltar1.TabIndex = 13;
            this.btnVoltar1.Text = "❮";
            this.btnVoltar1.UseVisualStyleBackColor = false;
            this.btnVoltar1.Click += new System.EventHandler(this.btnVoltar1_Click);
            // 
            // FormLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Extintos.Properties.Resources.FundoGeral;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1028, 598);
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
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "FormLobby";
            this.Load += new System.EventHandler(this.FormLobby_Load);
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