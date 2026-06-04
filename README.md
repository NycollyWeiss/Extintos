# Extintos

Extintos é uma aplicação Windows Forms em C# baseada no jogo Draftosaurus.

O sistema permite criar e entrar em partidas, listar jogadores, iniciar a partida e jogar em um tabuleiro visual com dinossauros, cercados, dado e uma estratégia automatizada para o bot.

## Tecnologias

- C#
- Windows Forms
- .NET Framework 4.8
- DraftServer.dll
- System.Drawing
- System.Windows.Forms

## Requisitos

- Windows
- Visual Studio ou Rider
- .NET Framework 4.8 Developer Pack
- `DraftServer.dll` presente na pasta do projeto `Extintos`

## Como Executar

1. Abra o projeto no Visual Studio ou Rider.
2. Carregue o arquivo `Extintos.slnx`.
3. Defina o projeto `Extintos` como projeto de inicialização.
4. Execute em modo Debug.

A aplicação inicia em `Program.cs`, abrindo o formulário principal:


Application.Run(Forms.Form1);

## Fluxo Principal

- Form1
- Tela inicial.
- Permite criar uma nova partida.
- Permite acessar a tela de partidas existentes.
- FormLobby
- Lista partidas disponíveis.
- Permite entrar em uma partida informando jogador, id da partida e senha.
- FormJogadores
- Lista jogadores da partida.
- Exibe a senha do jogador.
- Inicia a partida e abre o tabuleiro.
- FormDraftosaurus
- Tela principal do jogo.
- Renderiza tabuleiro, mão, dado e dinossauros.
- Controla drag and drop manual.
- Atualiza histórico.
- Executa o bot por timer.
- Envia jogadas ao servidor.


## Estrutura do Projeto
```
Extintos/
├── Enumeration/
│   ├── Cercado.cs
│   ├── Dado.cs
│   ├── Dinossauros.cs
│   ├── AuxCercado.cs
│   └── AuxDinossauro.cs
│
├── Forms/
│   ├── Form1.cs
│   ├── FormLobby.cs
│   ├── FormJogadores.cs
│   └── FormDraftosaurus.cs
│
├── LeonKennedy/
│   ├── IEstrategia.cs
│   ├── EstrategiaGulosa.cs
│   ├── EstrategiaValidator.cs
│   ├── EstrategiaAnalizador.cs
│   └── TestesOffline.cs
│
├── Model/
│   ├── Jogador.cs
│   ├── InformacoesTurno.cs
│   ├── Partida.cs
│   └── PartidaInfo.cs
│
├── Util/
│   ├── DraftService.cs
│   ├── MouseInput.cs
│   ├── RandomHelper.cs
│   └── ConfigEstategia.cs
│
└── Resources/
```

## Estratégia do Bot
A estratégia principal está em Extintos/LeonKennedy/EstrategiaGulosa.cs.

Ela percorre todos os dinossauros disponíveis na mão e todos os cercados possíveis.

Para cada combinação, consulta o EstrategiaValidator para saber se a jogada é válida.

Depois, calcula um score com três fatores:
ganho de pontuação imediata;
bônus da jogada;
potencial futuro.

A jogada com maior score é escolhida.


## Validação de Jogadas
A validação fica em Extintos/LeonKennedy/EstrategiaValidator.cs.

Ela verifica:

se existe informação do turno;
se o dinossauro está disponível na mão;
se o cercado existe;
se a restrição do dado permite aquele cercado;
se a regra interna do cercado permite receber aquele dinossauro.
Análise de Jogadas
A análise auxiliar fica em Extintos/LeonKennedy/EstrategiaAnalizador.cs.

Esse arquivo não decide a jogada final.

Ele calcula bônus e potencial futuro para ajudar a estratégia principal a comparar jogadas válidas.

## Cercados
Os cercados estão definidos em Extintos/Enumeration/Cercado.cs.

Cercados disponíveis:

CD - Campina da Diferença
FI - Floresta da Igualdade
IS - Ilha Solitária
MT - Mata Tripla
PA - Pradaria do Amor
RI - Rio
RS - Rei da Selva


## Dinossauros
Os dinossauros estão definidos em Extintos/Enumeration/Dinossauros.cs.

Dinossauros disponíveis:

BR - Braquiossauro
EP - Espinossauro
ET - Estegossauro
PA - Parasaurolofo
TI - Tiranossauro
TR - Tricerátops

## Dado
As faces do dado estão definidas em Extintos/Enumeration/Dado.cs.

Faces disponíveis:

AL - Alimentação
FL - Floresta
PR - Pradaria
TI - Tiranossauro Rex
VZ - Cercado Vazio
WC - Banheiros


<h1>Interface do Tabuleiro</h1>
O tabuleiro visual fica concentrado em Extintos/Forms/FormDraftosaurus.cs.

Essa tela cuida de:

desenho do tabuleiro;
desenho da mão;
drag and drop dos dinossauros;
atualização do dado;
histórico da partida;
animação visual do bot;
envio da jogada para o servidor.
O drag and drop é manual, feito com:

frmMouseDown
frmMouseMove
frmMouseUp
Comunicação com o Servidor
A comunicação com a DLL do jogo é feita principalmente por Extintos/Util/DraftService.cs.

Esse arquivo encapsula chamadas como:

obter estado da partida;
obter mão do jogador;
jogar dinossauro em cercado;
listar jogadores;
listar cercados;
listar faces do dado.
Testes Offline
Existe uma estrutura de testes manuais/offline em Extintos/LeonKennedy/TestesOffline.cs.

Ela cria estados falsos de jogo e valida se a estratégia retorna jogadas possíveis e coerentes.
