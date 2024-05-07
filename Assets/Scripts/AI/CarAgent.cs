using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using LibGameAI.FSMs;
using Random = UnityEngine.Random;



namespace Assets.Scripts.AI
{
    // Automatically add the NavMeshAgent to the GameObject
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentCar : MonoBehaviour, IAgent
    {   
        // ---------------- interface ------------------------------------------

        public AgentState State { get; private set; }
        // ---------------------------------------------------------------------

        // Current goal of navigation agent
        [SerializeField] private GameObject[] goal;

        // Reference to the NavMeshAgent component
        private NavMeshAgent agent;


        // ------------- FSM ---------------------------------------------------

        private StateMachine _fsm;

        private Color _color;

        private MeshRenderer mesh;

        //----------------------------------------------------------------------


        // The root of the decision tree

        // 
        private void Awake()
        {
            if (this.tag == "Pedestrian")
                goal = GameObject.FindGameObjectsWithTag("Dest");
            else
                goal = GameObject.FindGameObjectsWithTag("CarDest");

            // Get reference to the NavMeshAgent component
            agent = GetComponent<NavMeshAgent>();

            MeshRenderer mesh = GetComponent<MeshRenderer>();
    

        }


        // Start is called before the first frame update
        private void Start()
        {   

            // --------------------- STATES ------------------------------------


            State IdleState     = new State("Iddle",Idle, null, null);
            State MovingState   = new State("Moving",Move, null, null);
            State AccidentState = new State("Accident",Accident, null, null);
            State CrazyState    = new State("Crazy", Crazy, null, null);

            // ------------------- Color ---------------------------------------
             _color = mesh.material.color;
           

            // ------------------------ TRANSITIONS ----------------------------


            // IDLE => Move
            IdleState.AddTransition(new Transition(
                () => State == AgentState.Move,
                null, MovingState));


            // ----------------------------------------

            // Move => Idle
            MovingState.AddTransition(new Transition(
                () => State == AgentState.Idle,
                null, IdleState));

            // Move => Crazy
            MovingState.AddTransition(new Transition(
                () => State == AgentState.Crazy,
                null, CrazyState));

            // Move => Accident
            MovingState.AddTransition(new Transition(
                () => State == AgentState.Idle,
                null, IdleState));

            // ---------------------------------------

            // Crazy => Accident
            CrazyState.AddTransition(new Transition(
                () => State == AgentState.Accident,
                null, AccidentState));

            CrazyState.AddTransition(new Transition(
                () => State == AgentState.Move,
                null, MovingState));



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
            Action actions = _fsm.Update();
            actions?.Invoke();

            //------------------------------------------------------------------
         
        }


        // Method called when agent collides with something
        private void OnTriggerEnter(Collider other)
        {
           
            // Did agent collide with goal?
            if (other.name == "Goal")
                // If so, update destination (let goal reposition itself first)

                // MUST ADD INT POS
                Invoke("Move", 0.1f);
        }




        // ----------------------- ACTIONS -------------------------------------

#region  Idle & Move
        private void Idle()
        {

            mesh.enabled = false;
            agent.enabled = false;
        }

        private void Move()
        {
            mesh.enabled = true;
            agent.enabled = true;

            int pos = Random.Range(0, goal.Length);

            UpdateDestination(pos);
            
        }

#endregion

#region  Crazy
        private void Crazy()
        {
          
            float time = Random.Range(10F, 60F);
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
                    mesh.material.color = Color.yellow;
                    yield return new WaitForSeconds(0.2f);
                    mesh.material.color = _color;
                }
              
            }
            
        }

#endregion


#region Accident Actions
        private void Accident()
        {
            SetAgentMovement(true);
            mesh.material.color = Color.red;
        
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
    }

}
