using Draft;
using Extintos.Enumeration;
using Extintos.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Extintos
{
    public partial class FormDraftosaurus : Form
    {
        private string _retornoEntrar;
        private Jogador _dadosJogador;

        public FormDraftosaurus()
        {

            InitializeComponent();
            lblVersaoTres.Text = Jogo.versao;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = FormWindowState.Maximized;

        }

        internal FormDraftosaurus(string retornoEntrar, Jogador dadosJogador) : this() //Construtor com parâmetros
        {
            this._retornoEntrar = retornoEntrar;
            this._dadosJogador = dadosJogador;
            lblRetornoInicio.Text = _retornoEntrar;
        }

        //metodo de exibir a mao vai ir pro jogador depois

        public void bntExibirMao_Click(object sender, EventArgs e)
        {
            string retornoMao = Jogo.ExibirMao(_dadosJogador.IdJogador, _dadosJogador.Senha);
            string[] linhasRetorno = retornoMao.Replace("\r", "")
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            //esse new[] ta criando um novo array pra guardar por linha, pq eles vem tudo na mesma linha
            List<AuxDinossauro> dinossaurosJogador = new List<AuxDinossauro>();



            foreach (string linha in linhasRetorno)
            {
                string[] partes = linha.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (partes.Length == 2)
                {

                    string codigo = partes[0].ToUpper().Trim();
                    codigo.ToUpper();
                    int quantidade = int.Parse(partes[1].Trim());
                    Dinossauro dinossauro = (Dinossauro)Enum.Parse(typeof(Dinossauro), codigo);
                    dinossaurosJogador.Add(new AuxDinossauro(dinossauro, quantidade));

                }

                string mao = "";
                foreach (var item in dinossaurosJogador)
                {
                    mao += $"Dino: {item.Dinossauro.PegaNome()} | Qtd: {item.QuantidadeDinossauros}\n";
                }

                lblNomeDino.Text = mao;


            }
        }

        public void btnJogar_Click(object sender, EventArgs e)
        {
            var verificacao = Partida.VerificaPartida(_dadosJogador.idPartida);

            List<AuxCercado> cercadosJogador = new List<AuxCercado>();
            List<Cercados> todos = CercadosExtension.cercadosLista();
            if (verificacao.numeroTurno == 1 && verificacao.idJogador == _dadosJogador.IdJogador)
            {
                //depois fazer o jogador ter a lista de cercados nele 

                foreach (Cercados cercado in todos)
                {
                    cercadosJogador.Add(new AuxCercado(cercado, 0));
                }
            }

            Dado dadoAtual = (Dado)Enum.Parse(typeof(Dado), verificacao.faceDado);

            List<Cercados> cercadosOk = dadoAtual.ValidaCercados();


            string dinoEscolhido = txtDino.Text.ToUpper();

            string cercadoEscolhido = txtCercado.Text.ToUpper();

            if (!cercadosOk.Contains((Cercados)Enum.Parse(typeof(Cercados), cercadoEscolhido)))
            {
                MessageBox.Show(
                    $"Voce nao pode jogar nesse cercado devido ao dado do turno\n" +
                    $"Dado atual: {dadoAtual.PegaNome()}\n" +
                    $"A restição diz que {dadoAtual.PegaRestricao()}\n" +
                    $"Escolha outro cercado!!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(
                    $"Jogada realizada com sucesso!", "Boa!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //criar metodo pra atualizar dinos no cercado


            }


            string retornoMao = Jogo.ExibirMao(_dadosJogador.IdJogador, _dadosJogador.Senha);
            retornoMao = retornoMao.Substring(1);
            string[] linhasRetorno = retornoMao.Replace("\r", "")
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            //esse new[] ta criando um novo array pra guardar por linha, pq eles vem tudo na mesma linha
            List<AuxDinossauro> dinossaurosJogador = new List<AuxDinossauro>();



            foreach (string linha in linhasRetorno)
            {
                string[] partes = linha.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (partes.Length == 2)
                {

                    string codigo = partes[0].Trim();
                    int quantidade = int.Parse(partes[1].Trim());
                    Dinossauro dinossauro = (Dinossauro)Enum.Parse(typeof(Dinossauro), codigo.ToUpper());
                    dinossaurosJogador.Add(new AuxDinossauro(dinossauro, quantidade));

                }
               
            }
            
            Dinossauro dino = (Dinossauro)Enum.Parse(typeof(Dinossauro), dinoEscolhido);
            Cercados cerca = (Cercados)Enum.Parse(typeof(Cercados), cercadoEscolhido);
            foreach (var dinossauro in dinossaurosJogador)
            {
                if (dinossauro.Dinossauro.Equals(dino))
                {
                   //MessageBox.Show($"foi2", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    string retorno = Jogo.Jogar(_dadosJogador.IdJogador, _dadosJogador.Senha,dinoEscolhido,cercadoEscolhido);
                   // lblMensagemInicioPartida.Text= retorno;
                    foreach (var cercado in cercadosJogador)
                    {
                        if (cercado.Cercados.Equals(cerca))
                            
                        { var cercadin =  cercadosJogador.FirstOrDefault(p => p.Cercados == cerca);
                            cercadin.QuantidadeDinos += 1;

//depois faz o cercado ter lista de dinos git 
                        }
                    }
                }
                
                }
            
            
            
            
            
            
            
            
            }

 
    }
    }


