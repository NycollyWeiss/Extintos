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
            lblVersao3.Text = Jogo.versao;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = FormWindowState.Maximized;
        }

        internal FormDraftosaurus(string retornoEntrar, Jogador dadosJogador) : this() //Construtor com parâmetros
        {
            this._retornoEntrar = retornoEntrar;
            this._dadosJogador = dadosJogador;
            lblMensagemInicioPartida.Text = _retornoEntrar;
        }
        //metodo de exibir a mao vai ir pro jogador depois

        public void bntExibirMao_Click(object sender, EventArgs e)
        {
                string retornoMao = Jogo.ExibirMao(_dadosJogador.IdJogador,_dadosJogador.Senha);
                string[] maoJogador = retornoMao.Split(',');
                var tipoDino = (Dinossauro)Enum.Parse(typeof(Dinossauro), maoJogador[0]);
                string codDino = maoJogador[0];
                string nomeDino = tipoDino.PegaNome();
                string quantidadeDinos = maoJogador[1];
                
                var mao = (nomeDino, quantidadeDinos);
                lblNomeDino.Text = nomeDino;
                lblQtnDino.Text = quantidadeDinos;
                
                
        }
        
          

            private void btnJogar_Click(object sender, EventArgs e)
            {
               
                var verificacao = Partida.VerificaPartida(_dadosJogador.idPartida);
                if (verificacao.numeroTurno == 1 && verificacao.idJogador == _dadosJogador.IdJogador)
                {
                    //depois fazer o jogador ter a lista de cercados nele 
                   ;
                  
                    List<AuxCercado> cercadosJogador = new List<AuxCercado>();
                    List<Cercados> todos = CercadosExtension.cercadosLista();
                    foreach (Cercados cercado in todos)
                    {
                        cercadosJogador.Add(new AuxCercado(cercado,0));
                    }
                }
                Dado dadoAtual = (Dado)Enum.Parse(typeof(Dado), verificacao.faceDado);

                List<Cercados> cercadosOk = dadoAtual.ValidaCercados();
                
               
                string dinoEscolhido = "de onde ele for escolher, se vai ser txt ou checkbox";

                string cercadoEscolhido = "de onde ele for escolher, se vai ser txt ou checkbox";
               
                if (!cercadosOk.Contains((Cercados)Enum.Parse(typeof(Cercados), cercadoEscolhido)))
                {
                    MessageBox.Show(
                        $"Voce nao pode jogar nesse cercado devido ao dado do turno\n" +
                        $"Dado atual: {dadoAtual.PegaNome()}\n"+
                        $"A restição diz que {dadoAtual.PegaRestricao()}\n" +
                        $"Escolha outro cercado!!");
                    return;
                }
    
                string retornoMao = Jogo.ExibirMao(_dadosJogador.IdJogador, _dadosJogador.Senha);
                string[] maoJogador = retornoMao.Split('\n');
                maoJogador = retornoMao.Split(',');
                string[] dinossauros = maoJogador;
                List<AuxDinossauro> dinossaurosJogador = new List<AuxDinossauro>();
               
                for (int i = 0; i < dinossauros.Length; i++)// ta certo ou anda só uma vez ?
                { 
                    Dinossauro dinossauro = (Dinossauro)Enum.Parse(typeof(Dinossauro),dinossauros[0]);
                    int quant = int.Parse(dinossauros[1]);
                    dinossaurosJogador.Add(new AuxDinossauro(dinossauro,quant));

                }
                
                if(!dinossaurosJogador.Contains((AuxDinossauro)Enum.Parse(typeof(Dinossauro),dinoEscolhido)))

                {
                    MessageBox.Show(
                        $"Voce nao tem esse dinossauro escolha um dos seus\n" +
                        $"{dinossauros}");
                }
                //salva o dinossauro  e cercado escolhido como string pra n mexer mt
                //depois a gente deixa pra escolher ou o dino ou o cercado antes 
                
                if(dinossaurosJogador.Contains((AuxDinossauro)Enum.Parse(typeof(Dinossauro),dinoEscolhido)))
                {
                    if(cercadosOk.Contains((Cercados)Enum.Parse(typeof(Cercados), cercadoEscolhido)))
                    {
                        
                        string retorno = Jogo.Jogar(_dadosJogador.IdJogador,_dadosJogador.Senha, dinoEscolhido, cercadoEscolhido);
                        
                        MessageBox.Show(
                            $"Jogada efetuada com sucesso!");
                        //falta atualizar o cercado e a lista de dinossauro
                    }
                }
                
                
            }
    }
  
        
    }

