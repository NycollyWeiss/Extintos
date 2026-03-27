namespace Extintos
{
    partial class FormJogadores
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
            this.latJogadores = new System.Windows.Forms.ListBox();
            this.lblSenhaGeradaJogador = new System.Windows.Forms.Label();
            this.lblIDJogadorGerado = new System.Windows.Forms.Label();
            this.bntListaJogadores = new System.Windows.Forms.Button();
            this.lblSenhaGeradaa = new System.Windows.Forms.Label();
            this.btnProximo = new System.Windows.Forms.Button();
            this.btnCadastrarNovoJogador = new System.Windows.Forms.Button();
            this.lblIdGeradoJogador = new System.Windows.Forms.Label();
            this.lblTituloAddJogadores = new System.Windows.Forms.Label();
            this.lblVersao2 = new System.Windows.Forms.Label();
            this.btnVoltar2 = new System.Windows.Forms.Button();
            this.dgvJogadores = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJogadores)).BeginInit();
            this.SuspendLayout();
            // 
            // latJogadores
            // 
            this.latJogadores.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.latJogadores.FormattingEnabled = true;
            this.latJogadores.ItemHeight = 16;
            this.latJogadores.Location = new System.Drawing.Point(346, 150);
            this.latJogadores.Name = "latJogadores";
            this.latJogadores.Size = new System.Drawing.Size(180, 260);
            this.latJogadores.TabIndex = 0;
            // 
            // lblSenhaGeradaJogador
            // 
            this.lblSenhaGeradaJogador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSenhaGeradaJogador.AutoSize = true;
            this.lblSenhaGeradaJogador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenhaGeradaJogador.Location = new System.Drawing.Point(543, 237);
            this.lblSenhaGeradaJogador.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSenhaGeradaJogador.Name = "lblSenhaGeradaJogador";
            this.lblSenhaGeradaJogador.Size = new System.Drawing.Size(208, 18);
            this.lblSenhaGeradaJogador.TabIndex = 44;
            this.lblSenhaGeradaJogador.Text = "Senha da Partida Jogador:";
            // 
            // lblIDJogadorGerado
            // 
            this.lblIDJogadorGerado.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblIDJogadorGerado.AutoSize = true;
            this.lblIDJogadorGerado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIDJogadorGerado.Location = new System.Drawing.Point(543, 173);
            this.lblIDJogadorGerado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIDJogadorGerado.Name = "lblIDJogadorGerado";
            this.lblIDJogadorGerado.Size = new System.Drawing.Size(117, 18);
            this.lblIDJogadorGerado.TabIndex = 45;
            this.lblIDJogadorGerado.Text = "Id do Jogador:";
            // 
            // bntListaJogadores
            // 
            this.bntListaJogadores.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bntListaJogadores.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.bntListaJogadores.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntListaJogadores.Location = new System.Drawing.Point(362, 103);
            this.bntListaJogadores.Name = "bntListaJogadores";
            this.bntListaJogadores.Size = new System.Drawing.Size(141, 31);
            this.bntListaJogadores.TabIndex = 46;
            this.bntListaJogadores.Text = "Listar Jogadores";
            this.bntListaJogadores.UseVisualStyleBackColor = false;
            this.bntListaJogadores.Click += new System.EventHandler(this.bntListaJogadores_Click);
            // 
            // lblSenhaGeradaa
            // 
            this.lblSenhaGeradaa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSenhaGeradaa.AutoSize = true;
            this.lblSenhaGeradaa.Location = new System.Drawing.Point(546, 278);
            this.lblSenhaGeradaa.Name = "lblSenhaGeradaa";
            this.lblSenhaGeradaa.Size = new System.Drawing.Size(0, 16);
            this.lblSenhaGeradaa.TabIndex = 47;
            // 
            // btnProximo
            // 
            this.btnProximo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProximo.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnProximo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProximo.Location = new System.Drawing.Point(750, 471);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(110, 35);
            this.btnProximo.TabIndex = 49;
            this.btnProximo.Text = "Próximo >";
            this.btnProximo.UseVisualStyleBackColor = false;
            this.btnProximo.Click += new System.EventHandler(this.btnProximo_Click);
            // 
            // btnCadastrarNovoJogador
            // 
            this.btnCadastrarNovoJogador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCadastrarNovoJogador.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCadastrarNovoJogador.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastrarNovoJogador.Location = new System.Drawing.Point(346, 441);
            this.btnCadastrarNovoJogador.Name = "btnCadastrarNovoJogador";
            this.btnCadastrarNovoJogador.Size = new System.Drawing.Size(180, 26);
            this.btnCadastrarNovoJogador.TabIndex = 51;
            this.btnCadastrarNovoJogador.Text = "Cadastrar novo jogador";
            this.btnCadastrarNovoJogador.UseVisualStyleBackColor = false;
            this.btnCadastrarNovoJogador.Click += new System.EventHandler(this.btnCadastrarNovoJogador_Click);
            // 
            // lblIdGeradoJogador
            // 
            this.lblIdGeradoJogador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblIdGeradoJogador.AutoSize = true;
            this.lblIdGeradoJogador.Location = new System.Drawing.Point(549, 201);
            this.lblIdGeradoJogador.Name = "lblIdGeradoJogador";
            this.lblIdGeradoJogador.Size = new System.Drawing.Size(0, 16);
            this.lblIdGeradoJogador.TabIndex = 52;
            // 
            // lblTituloAddJogadores
            // 
            this.lblTituloAddJogadores.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTituloAddJogadores.AutoSize = true;
            this.lblTituloAddJogadores.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloAddJogadores.Location = new System.Drawing.Point(292, 50);
            this.lblTituloAddJogadores.Name = "lblTituloAddJogadores";
            this.lblTituloAddJogadores.Size = new System.Drawing.Size(280, 36);
            this.lblTituloAddJogadores.TabIndex = 53;
            this.lblTituloAddJogadores.Text = "Veja os Jogadores";
            // 
            // lblVersao2
            // 
            this.lblVersao2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVersao2.AutoSize = true;
            this.lblVersao2.Location = new System.Drawing.Point(826, 558);
            this.lblVersao2.Name = "lblVersao2";
            this.lblVersao2.Size = new System.Drawing.Size(0, 16);
            this.lblVersao2.TabIndex = 54;
            // 
            // btnVoltar2
            // 
            this.btnVoltar2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnVoltar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar2.Location = new System.Drawing.Point(21, 24);
            this.btnVoltar2.Name = "btnVoltar2";
            this.btnVoltar2.Size = new System.Drawing.Size(33, 49);
            this.btnVoltar2.TabIndex = 55;
            this.btnVoltar2.Text = "❮";
            this.btnVoltar2.UseVisualStyleBackColor = false;
            this.btnVoltar2.Click += new System.EventHandler(this.btnVoltar2_Click_1);
            // 
            // dgvJogadores
            // 
            this.dgvJogadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJogadores.Location = new System.Drawing.Point(66, 150);
            this.dgvJogadores.Name = "dgvJogadores";
            this.dgvJogadores.RowHeadersWidth = 51;
            this.dgvJogadores.RowTemplate.Height = 24;
            this.dgvJogadores.Size = new System.Drawing.Size(240, 260);
            this.dgvJogadores.TabIndex = 56;
            // 
            // FormJogadores
            // 
            this.ClientSize = new System.Drawing.Size(891, 583);
            this.Controls.Add(this.dgvJogadores);
            this.Controls.Add(this.btnVoltar2);
            this.Controls.Add(this.lblVersao2);
            this.Controls.Add(this.lblTituloAddJogadores);
            this.Controls.Add(this.lblIdGeradoJogador);
            this.Controls.Add(this.btnCadastrarNovoJogador);
            this.Controls.Add(this.btnProximo);
            this.Controls.Add(this.lblSenhaGeradaa);
            this.Controls.Add(this.bntListaJogadores);
            this.Controls.Add(this.lblIDJogadorGerado);
            this.Controls.Add(this.lblSenhaGeradaJogador);
            this.Controls.Add(this.latJogadores);
            this.Name = "FormJogadores";
            this.Load += new System.EventHandler(this.FormJogadores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvJogadores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bntIniciar;
        private System.Windows.Forms.TextBox txtIdJogador;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblIdJogador;
        private System.Windows.Forms.Label lblSenhaJogador;
        private System.Windows.Forms.ListBox latJogadores;
        private System.Windows.Forms.Label lblSenhaGeradaJogador;
        private System.Windows.Forms.Label lblIDJogadorGerado;
        private System.Windows.Forms.Button bntListaJogadores;
        private System.Windows.Forms.Label lblSenhaGeradaa;
        private System.Windows.Forms.Label lblIdGeradoJogado;
        private System.Windows.Forms.Button btnProximo;
        private System.Windows.Forms.Button btnCadastrarNovoJogador;
        private System.Windows.Forms.Label lblIdGeradoJogador;
        private System.Windows.Forms.Label lblTituloAddJogadores;
        private System.Windows.Forms.Label lblVersao2;
        private System.Windows.Forms.Button btnVoltar2;
        private System.Windows.Forms.DataGridView dgvJogadores;
    }
}