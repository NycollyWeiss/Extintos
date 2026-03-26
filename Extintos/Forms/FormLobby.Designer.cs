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
            this.lblVersao = new System.Windows.Forms.Label();
            this.dgvPartida = new System.Windows.Forms.DataGridView();
            this.lblTituloSelecionePartida = new System.Windows.Forms.Label();
            this.btnListarAsPartidas = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartida)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersao
            // 
            this.lblVersao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersao.AutoSize = true;
            this.lblVersao.Location = new System.Drawing.Point(944, 519);
            this.lblVersao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(0, 16);
            this.lblVersao.TabIndex = 3;
            // 
            // dgvPartida
            // 
            this.dgvPartida.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvPartida.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartida.Location = new System.Drawing.Point(61, 114);
            this.dgvPartida.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPartida.Name = "dgvPartida";
            this.dgvPartida.RowHeadersWidth = 51;
            this.dgvPartida.Size = new System.Drawing.Size(421, 407);
            this.dgvPartida.TabIndex = 2;
            // 
            // lblTituloSelecionePartida
            // 
            this.lblTituloSelecionePartida.AutoSize = true;
            this.lblTituloSelecionePartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloSelecionePartida.Location = new System.Drawing.Point(352, 35);
            this.lblTituloSelecionePartida.Name = "lblTituloSelecionePartida";
            this.lblTituloSelecionePartida.Size = new System.Drawing.Size(383, 39);
            this.lblTituloSelecionePartida.TabIndex = 4;
            this.lblTituloSelecionePartida.Text = "Selecione a Sua Partida";
            this.lblTituloSelecionePartida.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnListarAsPartidas
            // 
            this.btnListarAsPartidas.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnListarAsPartidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListarAsPartidas.Location = new System.Drawing.Point(626, 114);
            this.btnListarAsPartidas.Name = "btnListarAsPartidas";
            this.btnListarAsPartidas.Size = new System.Drawing.Size(230, 49);
            this.btnListarAsPartidas.TabIndex = 5;
            this.btnListarAsPartidas.Text = "Listar Partidas";
            this.btnListarAsPartidas.UseVisualStyleBackColor = false;
            this.btnListarAsPartidas.Click += new System.EventHandler(this.btnListarAsPartidas_Click);
            // 
            // FormLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnListarAsPartidas);
            this.Controls.Add(this.lblTituloSelecionePartida);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.dgvPartida);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
    }
}