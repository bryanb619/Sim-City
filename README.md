# Sim City

## Autoria

### Elementos do grupo

- Diogo Freire  22104684
- Steven Hall   2200173
  
### Report

#### Diogo Freire

- Código
  - NavAgentBehaviour:
    - Melhoria do comportamento do Agent (melhorar isso!)
    - Sistema de Waypoints
    - Deteção de agentes
    - Deteção de Colisões
  - TrafficLight
  - AIDirector
- Scene:
  - _NavMesh Bake_
  - _NavMesh Link_
  - _Waypoints_ automóveis e peões
- Relatório:
  - Conclusão
  - Verificação ortográfica
- _Bug fixing_  

#### Steven Hall

- Código:
  - Implementação de FSM
  - AgentState enum
  - AIDirector (Implementação de sistema e transição do estado de luzes)
  - TrafficLight System implementation:
    - IntersectionBrain
    - LightState enum
    - TrafficLight
  - UI
    - Contador de Agentes

- Scene
  - Posicionamento de sinais de trânsito, transição de cor, _colliders_ e locais de destino
- Relatório:
  - Artigos
  - Imagens e explicação
  - UML
  - Diagramas
- _Bug fixing_

## Introdução

 O projeto desenvolvido retrata um modelo tráfego urbano """ . Foi desenvolvido utilizando o motor de jogo [_Unity Engine_ 2022.3.1 _LTS_](https://unity.com/releases/editor/whats-new/2022.3.1#release-notes) e para definir as ações dos agentes, utilizamos as FSM (finite-state machine) [[1]](https://nunofachada.github.io/libgameai/api/LibGameAI.FSMs.html) e o objetivo principal foi demonstrar as técnicas de Inteligência Artificial em uma espécie de _Sim City_ não jogável, com automóveis, peões e sinais de trânsito como semáforos (sinalização luminosa).

- Objetivos desta simulação:
  - Fazer com que os automóveis respeitem as regras de trânsito, como sinais luminosos, passadeiras e outros veículos na via.
  - Permitir que os pedestres utilizem passeios e passadeiras quando o sinal luminoso permitir.
  - Simular acidentes entre agentes.
  - Implementar um modo de descontrole que escolhe um agente aleatório e aumenta o seu nível de "insanidade".
  
- Objetivos alcançados:
  - Implementação de sinais luminosos utilizando máquina de estados (_FSM_).
  - Transição entre estado de agentes (automóveis e peões).
  - Obedecer regras de trânsito
  - Estado descontrolo
  - Abrandar a para evitar colisões
  
## Estudo da Arte

Nesta secção, será apresentada uma pesquisa sobre simulações relacionadas ao nosso projeto, na qual faremos uma resumida descrição de cada uma e as compararemos com o nosso trabalho desenvolvido ao fazer uma análise de onde há semelhanças como diferenças entre as simulações.

### **_Traffic3D: An Open-Source Traffic-based Interactive Framework to Train AI Agents_**

Este artigo analisa o uso de _Traffic3D_, uma poderosa ferramenta [_open source_](https://en.wikipedia.org/wiki/Open_source), capaz de testar... ...  e treinar agentes com I.A.  . O objetivo principal é explorar os problemas de tráfego, bem como o congestionamento em interseções operadas por semáforos (sinais luminosos).

**Comparação entre projetos** :

- **Parametrização da Simulação**
  
  Ambos os projetos permitem a edição de parâmetros relevantes a simulação, tanto o nosso projeto como este projeto que estamos a analisar permitem popular o cenário da simulação com tráfego multi-modal, isto é tráfego de veículos e peões, posicionar a posição de instanciação dos agentes móveis (criar agentes num local pré-definido). Entretanto _Traffic3D_ vai ainda mais longe e permite múltiplas escolhas de (parametrização):

  - Peões: alterações mais a nível estético como género, idade, aparência (roupa do peão). Existem também outras parametrizações como andar ou correr e comportamentos como esperar em sinais vermelhos, atravessar nos sinais verdes, atravessar a estrada em pontos não designados para peões. Este poderoso simulador permite ainda estender os comportamentos e funcionalidades de peões.

  - Veículos: Existem diversos veículos nesta simulação como civil, emergência,     taxi e autocarros. Ainda é fornecida a opção de veículos escolherem a condução pelo lado esquerdo ou direito da estrada, algo que é importante para países como que se conduz no lado esquerdo da estrada, bem como a Inglaterra e outros.

- **Componentes da simulação :**
  
  _Traffic3D_ possui diversas características semelhantes com o _engine_ utilizado no nosso projeto que permitem fazer o cenário da simulação parecer o mais realístico como: iluminação global em tempo real, luzes, sombras, texturas e objetos nativos como veículos, céu e edifícios.
  
  [[2]](https://papers.ssrn.com/sol3/papers.cfm?abstract_id=4015243)

### **_Unity based Urban Environment Simulation for Autonomous Vehicle Stereo Vision Evaluation_**

Este estudo explora o desenvolvimento e experimento de uma simulação 3D no _Unity Engine_ para testar veículos  veículos autónomos registarem registarem dados com _stereo cameras_ representados nesta simulação por câmaras do próprio _engine_ situadas na frente do veículo. O objetivo deste experimento é demonstrar as capacidades de um veículo autónomo equipado com sensores(_omnipresent stereo cameras_). A terceira câmara ou a câmara central é bastante interessante porque, faz uso de um shader chamado _Z-buffer_ para representar a gravação de dados em profundidade.

**Comparação entre projetos :**
[[2]](https://ieeexplore.ieee.org/abstract/document/8756805)

### **_Unity 3D Simulator of Autonomous Motorway Traffic Applied to Emergency Corridor Building_**

Este trabalho aborda o desenvolvimento de um simulador de trânsito numa auto-estrada com veículos autónomos, ...  O objetivo de OUTRO é demonstrar a eficácia de agentes autónomos em certos cenários e permitir com mais sucesso e mais rapidamente a chegada de equipas de emergência ao local de acidentes em auto-estradas.

A principal diferença ou inovação entre este projetos e outros é o facto deste focar-se na construção de uma faixa de emergência em auto-estradas realizada por veículos autónomos e assim Demonstrar a eficácia de veículos autónomos em situações de emergência e intenso trânsito.

**Comparação entre projetos** :

- **Abordagem e objetivos**:  

  Ambos os trabalhos procuram melhorar simulações de tráfego, entretanto o nosso projeto implementa uma abordagem que foca em simular comportamentos de tráfego de veículos e peões, suas respetivas interações como deteção de agentes móveis(carros), agentes fixos (sinais luminosos) e finalmente terem uma ação designada em caso de acidentes num cenário urbano. Enquanto OUTRO PROJETO desenvolveu um sistema focado em segurança rodoviária, mais especificamente em auto-estradas.  
  Para efetuar a sua abordagem utiliza veículos autónomos capazes de se comunicarem com o objetivo de regular a velocidade dos agentes, criar faixas de emergência, procurando assim garantir ao máximo possível a segurança em estradas para todos. [[3]](https://www.researchgate.net/profile/Jurij-Kuzmic/publication/341470027_Unity_3D_Simulator_of_Autonomous_Motorway_Traffic_Applied_to_Emergency_Corridor_Building/links/60119d42299bf1b33e2d26f5/Unity-3D-Simulator-of-Autonomous-Motorway-Traffic-Applied-to-Emergency-Corridor-Building.pdf)

## Metodologia

A simulação desenvolvida é em 3D e as técnicas de Inteligência Artificial utilizadas foram, respetivamente, _FSM's_ (uma biblioteca essencial para a realização de transições entre estados) e _A* (Unity NavMesh)_, que é essencial para o _Pathfinding_ de agentes como carros e peões.

### Controlos da simulação

Os seguintes comandos são representados por teclas (teclado):

- Mover para frente: **W**
- Mover para esquerda: **A**
- Mover para trás: **S**
- Mover para direita: **D**

O seguinte comando é realizado através de um rato

- Rodar câmara: rato

### Elementos visuais

Na simulação existem também elementos visuais (_UI_) como informação e ações.

- **Informação**
  
  Para informar o número de agentes ativos, existe portando, um contador no canto superior direito com o número total de peões e carros ativos na simulação.

  ![SimCity counter](./Images/Agents.png)

- **Ação**
  
  No campo das ações existem 2 ações possíveis, sendo estas respecitvamente aumentar a probabilidade de um carro em modo caos, como também aumentar essa probabilidade no peão.

  ![S](./Images/Buttons.png)
  
#### Agentes Móveis

As imagens nesta secção apresentam visualmente o código desenvolvido para efetuar as transições e estados tanto de agentes móveis (carros e peões) como também agentes fixos (sinais luminosos de transito)

### Diagramas _FSM_

![SimCity FSM](./Images/agentDrawio.png)

#### Agente Fixo (semáforo)

![SimCity FSM](./Images/traffic.drawio.png)

### Carros

Agentes móveis

O carro é integrado por componentes como [_NavMeshAgent_](https://docs.unity3d.com/560/Documentation/Manual/class-NavMeshAgent.html), [_Rigidbody_](https://docs.unity3d.com/560/Documentation/Manual/class-Rigidbody.html) e [Box colliders](https://docs.unity3d.com/560/Documentation/Manual/class-BoxCollider.html) para garantir o seu funcionamento respectivamente a nível de _pathfinding_, colisões e deteção de outros componetes ou objetos na simulação.

O carro possui movimento dinâmico.

![SimCity car](./Images/car.png)

### Peões

Assim como os carros, os peões possuem os mesmos componentes descritos na secção: [Carros](#carros) com exatamente os mesmos objetivos.

A principal diferença entre peões e carros é que peões utilizam o movimento cinemático.

![SimCity car](./Images/ped.png)

### Semáforos (sinais luminosos)

Os semáforos são agentes fixos na simulação alternando apenas entre 2 estados, verde e vermelho. O seu estado inicial é definido no próprio _gameObject_ . Estes estados alternam os estados de 2 _colliders_. Quando o estado é vermelho, o _collider_ de carros passa a estar ativo enquanto, o _collider_ de peões é desativo e assim vice-versa.

Como referido, existem 2 tipos de _colliders_ numa interseção, sendo demonstrados a título de exemplo na seguinte imagem.

- **_Collider_ Vermelho**: _collider_ para veículos que quando detetado impede a passagem de veículos na mesma faixa.

- **_Collider_ Verde**: _collider_ para peões que quando detetado impede a passagem de um peão na passadeira.

![SimCity trafficLight](./Images/trafficLights.png)

### _Traffic Light_

Cada semáforo na simulação contém um componente _Traffic Light_, responsável por guardar o _LightState_ (estado de luz atual) e ativar e desativar _colliders_ como referido anteriormente.

É possível editar alguns parâmetros como:

- **_Light Mat_**: guarda os materiais (serve apenas para efeitos visuais da simulação).  
  
- **_Start Light State_**: define o estado de luz inicial de cada semáforo.
  
- **_Cross Walk Colls_**: guarda os _colliders_ de peões.

- **_Car Colls_**: guarda os _colliders_ de veículos.

![SimCity trafficLightScript](./Images/TrafficLighScript.png)

### _Intersection Brain_

Existe apenas 1 "cérebro" por interseção e este componente é responsável pelo tempo (tempo do temporizador em segundos) para mudar o estado dos sinais.

Possui valores parametrizáveis como:

- **_Light Max Time_**: responsável por definir o tempo (em segundos) de transição do estado dos semáforos.
  
- **_Control Points_**: define cada semáforo na atual interseção.

![SimCity Intersection Brain](./Images/IB.png)

Exemplo de uma interseção com 4 conexões, isto é um componente _Intersection Brain_ com 4 compentes
_Traffic Light_.

![Sim city Intersection Prefab](./Images/intersection.png)

### Passadeiras

Um sistema a parte dos componentes _Traffic Light_ e _Intersection Brain_ é um colisor responsável por detetar se há um peão na passadeira ou não. Isto é importante para os veículos poderem parar em zonas onde não existem semáforos.

![SimCity crossWalk](./Images/croswalk.png)

### AI Director

É um _Game Object_ que permite o controlo  ....  da simulação. Aqui é permitido que a simulação seja personalizada com parâmetros ajustáveis, sendo estes:

- Carros
  - **_Cars_**: número de veículos a serem instanciados no início da simulação.
  - **_Car Spawn points_**: Lista de posições onde carros serão instanciados no início da simulação.
  - **_Car Time Stopped_**: tempo máximo (segundos) para veículo estar estacionário no seu destino
  - **_Cars_**: _prefab_ de veículo a ser instanciado

![SimCity AIDirector](./Images/carAI.png)

- Peões
  - **_Peds_**: número de pões a serem instanciados no início da simulação.
  - **_Ped Spawn points_**:Lista de posições onde peões serão instanciados no início da simulação.
  - **_Ped Time Stopped_**: tempo máximo (segundos) para peão estar parado no seu destino
  - **_Ped_**: _prefab_ de peão a ser instanciado.

![SimCity AIDirector](./Images/pedAI.png)

- Acidente
  - **_Max Time In Accident_**: tempo máximo (segundos) em que agente pode estar em modo acidente.
  
- Caos
  - **_Max Time in Chaos_**: tempo máximo (segundos) em que agente pode estar em modo caos
  - **_Chaos Chance_**: probabilidade de um agente começar em modo caos.
  
![SimCity AIDirector](./Images/generalAIsetts.png)

### Diagrama UML

O seguinte diagrama contém apenas os algoritmos mais significativos e desenvolvidos pelo [autores](#autoria) deste projeto

```mermaid

classDiagram

    LightState             <--  TrafficLight
    TrafficLight           --*  IntersectionBrain 

    AgentState             <-- Agent

    Agent                  <.. AIDirector
                 
    class LightState
    <<enumeration>> LightState 

    class AgentState
    <<enumeration>> AgentState 
```

## Resultados e discussão

Ao analisar a simulação, é possível observar que os agentes se deslocam para os seus alvos. Os semáforos (sinal luminoso) podem impedi-los de avançar e, se houver uma passadeira, os pedestres podem prosseguir, caso esta seja a situação na zona da simulação.

## Conclusões

Para concluir, o projeto desenvolvido consistiu em implementar uma simulação de trânsito com automóveis, peões e sinais luminosos. Os resultados obtidos na simulação foram:

- Respeito pelas regras de trânsito
- Uso com sucesso do NavMesh Link para cruzamentos
- Transições do estado dos agentes
- Alterações de estados os agentes a meio do programa
- Múltiplos locais como destino
- Escolha ao calhas dos locais para destino
- Desaparecimento visual do agente, porem este mantém-se na cena

## Referências

### IAs generativas

O uso de IAs generativas foi usado e neste tópico explicaremos como:

- O _Chat Bing_ (_Chat GPT-4_) foi utilizado para tirar dúvidas e explicar itens da [documentação](https://learn.microsoft.com/en-us/dotnet/api/?view=netstandard-2.1) de forma mais clara e simples, erros, exemplos e também para obter de forma mais rápida _links_ com código útil, mas nunca diretamente utilizado no código do projeto.

A realização deste projeto consistiu essencialmente em pesquisa própria, conhecimento adquirido por meio de trabalhos e ensino fornecido por professores em diversas unidades curriculares lecionadas na [licenciatura de Videojogos](https://www.ulusofona.pt/lisboa/licenciaturas/videojogos).

### Código, tutoriais, planeamento utilizados e pesquisa

Technologies, U. (n.d.). Unity - Scripting API: Random.Range. <https://docs.unity3d.com/ScriptReference/Random.Range.html>
  
Sunny Valley Studio. (2020, August 26). Project setup - City Builder Unity tutorial P3 [Video]. YouTube. <https://www.youtube.com/watch?v=uBWEjqtdcgM>

Kink3d. (n.d.). GitHub - Kink3d/SimpleTraffic: A simple traffic simulation using Unity’s Nav Mesh Components. GitHub. <https://github.com/Kink3d/SimpleTraffic?tab=readme-ov-file>

Mike, V. a. P. B. (2012, October 1). SimCity: traffic system, public transportation and international airports. Simcitizens. <https://simcitizens.com/simcity-traffic-system-public-transportation-and-international-airports/>

Technologies, U. (n.d.-a). Unity - manual: making an agent patrol between a set of points. <https://docs.unity3d.com/560/Documentation/Manual/nav-AgentPatrol.html>

### _Assets_ de terceiros
  
SimplePoly - Town Pack | 3D Environments | Unity Asset Store. (2024, January 7). Unity Asset Store. <https://assetstore.unity.com/packages/3d/environments/simplepoly-town-pack-62400>

O pacote acima disponibiliza um controlo de câmara (_fly over_) sendo utilizado no projeto.

HD Low Poly Racing Car No.1201 | 3D Land | Unity Asset Store. (2024, March 16). Unity Asset Store. <https://assetstore.unity.com/packages/3d/vehicles/land/hd-low-poly-racing-car-no-1201-118603>
