# Sim City

## Autoria

### Elementos do grupo

- Diogo Freire  22104684
- Steven Hall   2200173
  
### Report

#### Diogo Freire

- Código
  - NavAgentBehaviour:
    - Melhoria do comportamento do Agent
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
  - IAgent interface
  - TrafficLight System implementation:
    - LightState enum
    - TrafficLight class
    - ITrafficLight interface
  - UI
  - AIDirector
- Scene
  - Posicionamento de sinais de trânsito, transição de cor, _colliders_ e locais de destino
- Relatório:
  - UML
  - Diagramas
- _Bug fixing_

## Introdução

 O projeto desenvolvido retrata o tráfego urbano """ . Foi desenvolvido utilizando o motor de jogo [_Unity Engine_ 2022.3.1 _LTS_](https://unity.com/releases/editor/whats-new/2022.3.1#release-notes) e para definir as ações dos agentes, utilizamos as FSM (finite-state machine) e objetivo principal foi demonstrar as técnicas de Inteligência Artificial em uma espécie de _SimCity_ não jogável, com automóveis, peões e sinais de trânsito (sinalização luminosa).

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

Um importante referencial para a realização deste projeto é o famoso jogo _SimCity_, que possui diversas edições. Este jogo apresenta uma inteligência artificial bastante interessante, incluindo comboios, veículos elétricos, autocarros, alguns outros veículos e peões.

![SimCity GIF](./Images/traffic-small.gif)

## Metodologia

A simulação desenvolvida é em 3D e as técnicas de Inteligência Artificial utilizadas foram, respectivamente, _FSM's_ (uma biblioteca essencial para a realização de transições entre estados) e _A* (Unity NavMesh)_, que é essencial para o _Pathfinding_ de agentes como carros e peões.

### Diagramas _FSM_

#### Agentes Móveis

As imagens nesta secção apresentam visualmente o código desenvolvido para efetuar as transições e estados tanto de agentes móveis (carros e peões) como também agentes fixos (sinais luminosos de transito)

![SimCity FSM](./Images/agent.drawio.png)

#### Agentes Fixos

![SimCity FSM](./Images/traffic.drawio.png)

### Carros

Agentes móveis

### Peões

Assim como os carros  

### Semáforos

Os semáfors são agentes fixos na simulação alternando apenas entre 2 estados, verde e vermelho. Estes estados alternam os estados de 2 colisores. Quando o estado é vermelho, o colisor de carros passa a estar ativo enquanto, o colisor de peões é desativo e assim vice-versa.

![SimCity AIDirecto](./Images/semaforo.png)

TODO: MOSTRAR COMO FUNCIONA

### AI Director

É um _Game Object_ que permite o controlo  ....  da simulação. Aqui é permitido que a simulação seja personalizada com parâmetros ajustáveis, sendo estes:

- Carros
  - **_Cars_**: número de veículos a serem instanciados no início da simulação.
  - **_Car Spawn points_**: Lista de posições onde carros serão instanciados no início da simulação.
  - **_Car Time Stoped_**: tempo máximo (segundos) para veículo estar estacionário no seu destino
  - **_Cars_**: _prefab_ de veículo a ser instanciado

![SimCity AIDirector](./Images/carAI.png)

- Peões
  - **_Peds_**: número de pões a serem instanciados no início da simulação.
  - **_Ped Spawn points_**:Lista de posições onde peões serão instanciados no início da simulação.
  - **_Ped Time Stoped_**: tempo máximo (segundos) para peão estar parado no seu destino
  - **_Ped_**: _prefab_ de peão a ser instanciado.

![SimCity AIDirector](./Images/pedAI.png)

- Acidente
  - **_Max Time In Accident_**: tempo máximo (segundos) em que agente pode estar em modo acidente.
  
- Caos
  - **_Max Time in Crazy_**: tempo máximo (segundos) em que agente pode estar em modo caos
  - **_Chaos Chance_**: probabilidade de um agente começar em modo caos.
  
![SimCity AIDirector](./Images/generalAIsetts.png)

### Diagrama Geral do Projeto em _UML_

```mermaid

classDiagram

    LightState             <--  TrafficLight
    NavAgentBehaviour      --*  AIDirector           
    UI                     <..  AIDirector

    class ITrafficLight
    <<interface>> ITrafficLight

    class LightState
    <<enumeration>> LightState 
```

## Resultados e discussão

Ao analisar a simulação, é possível observar que os agentes se deslocam para os seus alvos. Os semáforos (sinal luminoso) podem impedi-los de avançar e, se houver uma passadeira, os pedestres podem prosseguir, caso esta seja a situação na zona da simulação.

## Conclusões

Para concluir, o projeto desenvolvido consistiu em implementar uma simulação de trânsito com automóveis, peões e sinais luminosos. Os resultados obtidos na simulação foram:

- Respeito pelas regras de trânsito
- Uso com sucesso do NavMesh Link para cruzamentos
- Transições do estado dos agentes
- Alterações de estados os agentes a meio do programa
- Multiplos locais como destino
- Escolha ao calhas dos locais para destino
- Desaparecimento visual do agente, porem este mantem-se na cena

## Referências

### IAs generativas

O uso de IAs generativas foi usado e neste tópico explicaremos como:

- O _Chat Bing_ (_Chat GPT-4_) foi utilizado para tirar dúvidas e explicar itens da [documentação](https://learn.microsoft.com/en-us/dotnet/api/?view=netstandard-2.1) de forma mais clara e simples, erros, exemplos e também para obter de forma mais rápida _links_ com código útil, mas nunca diretamente utilizado no código do projeto.

A realização deste projeto consistiu essencialmente em pesquisa própria, conhecimento adquirido por meio de trabalhos e ensino fornecido por professores em diversas unidades curriculares lecionadas na [licenciatura de Videojogos](https://www.ulusofona.pt/lisboa/licenciaturas/videojogos).

### Código, tutoriais, planeamento utlizados e pesquisa

Technologies, U. (n.d.). Unity - Scripting API: Random.Range. <https://docs.unity3d.com/ScriptReference/Random.Range.html>
  
Sunny Valley Studio. (2020, August 26). Project setup - City Builder Unity tutorial P3 [Video]. YouTube. <https://www.youtube.com/watch?v=uBWEjqtdcgM>

Kink3d. (n.d.). GitHub - Kink3d/SimpleTraffic: A simple traffic simulation using Unity’s Nav Mesh Components. GitHub. <https://github.com/Kink3d/SimpleTraffic?tab=readme-ov-file>

Mike, V. a. P. B. (2012, October 1). SimCity: traffic system, public transportation and international airports. Simcitizens. <https://simcitizens.com/simcity-traffic-system-public-transportation-and-international-airports/>

### _Assets_ de terceiros
  
SimplePoly - Town Pack | 3D Environments | Unity Asset Store. (2024, January 7). Unity Asset Store. <https://assetstore.unity.com/packages/3d/environments/simplepoly-town-pack-62400>

School bus, simple, isometric | 3D Land | Unity Asset Store. (2020, November 16). Unity Asset Store. <https://assetstore.unity.com/packages/3d/vehicles/land/school-bus-simple-isometric-106250>

HD Low Poly Racing Car No.1201 | 3D Land | Unity Asset Store. (2024, March 16). Unity Asset Store. <https://assetstore.unity.com/packages/3d/vehicles/land/hd-low-poly-racing-car-no-1201-118603>

Microsimulation of traffic flow: Onramp. (n.d.). <https://www.traffic-simulation.de/>

Wikipedia contributors. (2024, March 15). Traffic simulation. Wikipedia. <https://en.wikipedia.org/wiki/Traffic_simulation>
