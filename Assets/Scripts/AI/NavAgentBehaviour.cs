using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using LibGameAI.FSMs;
using Random = UnityEngine.Random;


namespace Assets.Scripts.AI
{
    // Automatically add the NavMeshAgent to the GameObject
    public class NavAgentBehaviour : MonoBehaviour, IAgent
    {   
        // ---------------- interface ------------------------------------------

        public AgentState State { get; private set; }
        // -----------------------------Agent-----------------------------------

        // Current goal of navigation agent
        [SerializeField] private GameObject[] goal;

        // Reference to the NavMeshAgent component
        private NavMeshAgent agent;
        private float initialAgentSpeed;

        private Vector2Int _stopTime, _accidentTime, _chaosTime;
        private float _chaosChance;

        // ------------- FSM ---------------------------------------------------

        private StateMachine _fsm;

        private Color _color;
        
        [SerializeField]
        private MeshRenderer[] meshToChangeColor;
        [SerializeField]
        private GameObject[] meshToDeactivate;

        private Action actions;

        //----------------------------------------------------------------------

        // The root of the decision tree

        // 
        private void Awake()
        {
            if (CompareTag("Pedestrian"))
                goal = GameObject.FindGameObjectsWithTag("Dest");
            else
            {
                goal = GameObject.FindGameObjectsWithTag("CarDest");
                //_color = meshToChangeColor[0].material.color;
            }

            // Get reference to the NavMeshAgent component
            agent = GetComponent<NavMeshAgent>();
            initialAgentSpeed = agent.speed;
        }


        // Start is called before the first frame update
        private void Start()
        {   

            // --------------------- STATES ------------------------------------
            State IdleState     = new State("Iddle",Idle, null, null);
            State MovingState   = new State("Moving",Move, null, null);
            State AccidentState = new State("Accident",Accident, null, null);
            State ChaosState    = new State("Chaos", Chaos, null, null);

            // ------------------------ TRANSITIONS ----------------------------

            // arrive at destination => idle
            // idle => invisel, desativa NavMeshagent

            // IDLE => Move
            IdleState.AddTransition(new Transition(
                () => State == AgentState.Move,
                () => Move(), MovingState));


            // ----------------------------------------

            // Move => Idle
            MovingState.AddTransition(new Transition(
                () => State == AgentState.Idle,
                () => Idle(), 
                IdleState));
            // Move => Chaos
            MovingState.AddTransition(new Transition(
                () => State == AgentState.Chaos,
                () => Chaos(), 
                ChaosState));

            // Move => Accident
            MovingState.AddTransition(new Transition(
                () => State == AgentState.Idle,
                () => Accident(), IdleState));

            // ---------------------------------------

            // Chaos => Accident
            ChaosState.AddTransition(new Transition(
                () => State == AgentState.Accident,
                () => Accident(), AccidentState));

            // Chaos => Move
            ChaosState.AddTransition(new Transition(
                () => State == AgentState.Move,
                () => Move(), MovingState));

            // Chaos => Idle
            ChaosState.AddTransition(new Transition(
                () => State == AgentState.Idle,
                () => Idle(), MovingState));


            _fsm = new StateMachine(MovingState);
            //------------------------------------------------------------------

            // Set initial agent goal
            agent.SetDestination(
                goal[Random.Range(0, goal.Length)].transform.position);

        }



        // Run the decision tree and execute the returned action
        private void Update()
        {

            //----------------- Update FSM -------------------------------------
            actions = _fsm.Update();
            actions?.Invoke();

            //------------------------------------------------------------------
         
        }


        // Method called when agent collides with something
        private void OnTriggerEnter(Collider other)
        { 
            if (other.CompareTag("CarDest"))
                Invoke("Idle", 0.1f);
        }

        private void OnTriggerStay(Collider other)
        {
            if (State != AgentState.Chaos)
            {
                if (other.CompareTag("Vehicle"))
                    agent.speed = Mathf.Lerp(agent.speed, 
                                            other.GetComponent<NavMeshAgent>().speed, 
                                            Time.deltaTime);
                else if (other.CompareTag("Pedestrian") || other.CompareTag("RedLight"))
                {
                    agent.speed = Mathf.Lerp(agent.speed, 0, Time.deltaTime * 50);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (State != AgentState.Chaos)
            {
                if(other.CompareTag("Vehicle") || other.CompareTag("Pedestrian")
                    || other.CompareTag("RedLight"))
                {
                    agent.speed = initialAgentSpeed;
                }
            }
        }




        // ----------------------- ACTIONS -------------------------------------

#region  Idle & Move
        private void Idle()
        {
            SetAgentMovement(true);

            foreach (GameObject m in meshToDeactivate)
            {
                m.SetActive(false);
            }
  
            if (agent.GetComponent<BoxCollider>() != null)
                agent.GetComponent<BoxCollider>().enabled = false;
            else
                agent.GetComponent<CapsuleCollider>().enabled = false;
        }

        private void Move()
        {
            SetAgentMovement(false);
            agent.speed = initialAgentSpeed;

            foreach (GameObject m in meshToDeactivate)
            {
                m.SetActive(false);
            }

            if (agent.GetComponent<BoxCollider>() != null)
                agent.GetComponent<BoxCollider>().enabled = true;
            else
                agent.GetComponent<CapsuleCollider>().enabled = true;

            int pos = Random.Range(0, goal.Length);

            UpdateDestination(pos);
            
        }

#endregion

#region  Chaos
        private void Chaos()
        {
          
            float time = Random.Range(10F, 60F);
            agent.speed *= 3;
            StartCoroutine(HitFlash(time));

        }

        // receive time of flash 
        private IEnumerator HitFlash(float time)
        {
            int timer = 0; 

            while (true)
            {
                timer ++;


                if(timer< time)
                {
                    foreach (MeshRenderer m in meshToChangeColor)
                        m.material.color = Color.yellow;

                    yield return new WaitForSeconds(0.2f);
                    
                    foreach (MeshRenderer m in meshToChangeColor)
                        m.material.color = _color;
                }
              
            }
            
        }

#endregion

#region Accident Actions
        private void Accident()
        {
            SetAgentMovement(true);
            foreach (MeshRenderer m in meshToChangeColor)
                m.material.color = Color.red;
        
        }

      
#endregion

        // ---------------------------------------------------------------------


        // Update destination
        private void UpdateDestination(int pos)
        {
            // Set destination to current goal position
            agent.SetDestination(goal[pos].transform.position);
        }


        private void SetAgentMovement(bool stop)
        {
            if(stop)
            {
                agent.speed = 0; 
            }
            else
            {   
                // TODO ADD A default speed
                agent.speed = 3.5f;
            }

        }

        public void SetParameters(Vector2Int timeStoped, 
                                  Vector2Int timeInAccident, 
                                  Vector2Int timeInChaos,
                                  int chaosChance)
        {
            _stopTime = timeStoped;
            _accidentTime = timeInAccident;
            _chaosTime = timeInChaos;
            _chaosChance = chaosChance;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetChaosAgent()
        {
            // Set the agent to be a chaos agent
        }
    }

}
