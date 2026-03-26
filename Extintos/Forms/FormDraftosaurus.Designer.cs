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
            this.lblJogadorEscolhido = new System.Windows.Forms.Label();
            this.lblFaceDado = new System.Windows.Forms.Label();
            this.bntExibirMao = new System.Windows.Forms.Button();
            this.lblCodDinossauro = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDadoSorteado = new System.Windows.Forms.Label();
            this.lstMao = new System.Windows.Forms.ListBox();
            this.lblVersao3 = new System.Windows.Forms.Label();
            this.lblMensagemInicioPartida = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblJogadorEscolhido
            // 
            this.lblJogadorEscolhido.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblJogadorEscolhido.AutoSize = true;
            this.lblJogadorEscolhido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJogadorEscolhido.Location = new System.Drawing.Point(265, 231);
            this.lblJogadorEscolhido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblJogadorEscolhido.Name = "lblJogadorEscolhido";
            this.lblJogadorEscolhido.Size = new System.Drawing.Size(0, 20);
            this.lblJogadorEscolhido.TabIndex = 43;
            // 
            // lblFaceDado
            // 
            this.lblFaceDado.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblFaceDado.AutoSize = true;
            this.lblFaceDado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFaceDado.Location = new System.Drawing.Point(498, 231);
            this.lblFaceDado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFaceDado.Name = "lblFaceDado";
            this.lblFaceDado.Size = new System.Drawing.Size(0, 20);
            this.lblFaceDado.TabIndex = 44;
            // 
            // bntExibirMao
            // 
            this.bntExibirMao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bntExibirMao.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.bntExibirMao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntExibirMao.Location = new System.Drawing.Point(182, 315);
            this.bntExibirMao.Margin = new System.Windows.Forms.Padding(4);
            this.bntExibirMao.Name = "bntExibirMao";
            this.bntExibirMao.Size = new System.Drawing.Size(132, 56);
            this.bntExibirMao.TabIndex = 45;
            this.bntExibirMao.Text = "Ver Mão";
            this.bntExibirMao.UseVisualStyleBackColor = false;
            this.bntExibirMao.Click += new System.EventHandler(this.bntExibirMao_Click);
            // 
            // lblCodDinossauro
            // 
            this.lblCodDinossauro.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCodDinossauro.AutoSize = true;
            this.lblCodDinossauro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodDinossauro.Location = new System.Drawing.Point(516, 193);
            this.lblCodDinossauro.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodDinossauro.Name = "lblCodDinossauro";
            this.lblCodDinossauro.Size = new System.Drawing.Size(0, 20);
            this.lblCodDinossauro.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(232, 196);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 18);
            this.label1.TabIndex = 48;
            this.label1.Text = "Jogador da vez:";
            // 
            // lblDadoSorteado
            // 
            this.lblDadoSorteado.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDadoSorteado.AutoSize = true;
            this.lblDadoSorteado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDadoSorteado.Location = new System.Drawing.Point(447, 196);
            this.lblDadoSorteado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDadoSorteado.Name = "lblDadoSorteado";
            this.lblDadoSorteado.Size = new System.Drawing.Size(127, 18);
            this.lblDadoSorteado.TabIndex = 50;
            this.lblDadoSorteado.Text = "Dado Sorteado:";
            // 
            // lstMao
            // 
            this.lstMao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstMao.FormattingEnabled = true;
            this.lstMao.ItemHeight = 16;
            this.lstMao.Location = new System.Drawing.Point(347, 292);
            this.lstMao.Margin = new System.Windows.Forms.Padding(4);
            this.lstMao.Name = "lstMao";
            this.lstMao.Size = new System.Drawing.Size(281, 116);
            this.lstMao.TabIndex = 51;
            this.lstMao.SelectedIndexChanged += new System.EventHandler(this.lstMao_SelectedIndexChanged);
            // 
            // lblVersao3
            // 
            this.lblVersao3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblVersao3.AutoSize = true;
            this.lblVersao3.Location = new System.Drawing.Point(745, 432);
            this.lblVersao3.Name = "lblVersao3";
            this.lblVersao3.Size = new System.Drawing.Size(0, 16);
            this.lblVersao3.TabIndex = 52;
            // 
            // lblMensagemInicioPartida
            // 
            this.lblMensagemInicioPartida.AutoSize = true;
            this.lblMensagemInicioPartida.Location = new System.Drawing.Point(28, 28);
            this.lblMensagemInicioPartida.Name = "lblMensagemInicioPartida";
            this.lblMensagemInicioPartida.Size = new System.Drawing.Size(44, 16);
            this.lblMensagemInicioPartida.TabIndex = 53;
            this.lblMensagemInicioPartida.Text = "label2";
            // 
            // FormDraftosaurus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblMensagemInicioPartida);
            this.Controls.Add(this.lblVersao3);
            this.Controls.Add(this.lstMao);
            this.Controls.Add(this.lblDadoSorteado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCodDinossauro);
            this.Controls.Add(this.bntExibirMao);
            this.Controls.Add(this.lblFaceDado);
            this.Controls.Add(this.lblJogadorEscolhido);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormDraftosaurus";
            this.Text = "FormDraftosaurus";
            this.Load += new System.EventHandler(this.FormDraftosaurus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblJogadorEscolhido;
        private System.Windows.Forms.Label lblFaceDado;
        private System.Windows.Forms.Button bntExibirMao;
        private System.Windows.Forms.Label lblCodDinossauro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDadoSorteado;
        private System.Windows.Forms.ListBox lstMao;
        private System.Windows.Forms.Label lblVersao3;
        private System.Windows.Forms.Label lblMensagemInicioPartida;
    }
}