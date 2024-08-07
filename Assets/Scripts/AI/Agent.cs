using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using LibGameAI.FSMs;
using Random = UnityEngine.Random;

namespace Assets.Scripts.AI
{
    public class Agent : MonoBehaviour
    {
    
        public AgentState NavState { get; private set; } = AgentState.Idle;

        // -----------------------------Agent-----------------------------------

        // Current goal of navigation agent
        [SerializeField] private GameObject[] goal;

        private int randPos;

        // Reference to the NavMeshAgent component
        private NavMeshAgent agent;

        [SerializeField]
        private GameObject _model;

        private float initialAgentSpeed;

        private int _stopTime, _accidentTime, _chaosTime;

        public bool Chaotic { get; private set; }

        private float _chaosChance;


        private Rigidbody _rb;

        private bool canMove = true;

        // ------------- FSM ---------------------------------------------------

        private StateMachine _fsm;

        [SerializeField]
        private MeshRenderer[] meshToChangeColor;

        [SerializeField]
        private Material materialForAccident, materialForChaos, originalMaterial;


        //----------------------------------------------------------------------

        

        // 
        private void Awake()
        {
            if (CompareTag("Pedestrian"))
                goal = GameObject.FindGameObjectsWithTag("Dest");
            else
            {
                goal = GameObject.FindGameObjectsWithTag("CarDest");
            }

            // Get reference to the NavMeshAgent component
            agent = GetComponent<NavMeshAgent>();

            initialAgentSpeed = agent.speed;
        }


        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

            randPos = Random.Range(0, goal.Length);


            StartFSM();
        }


        private void StartFSM()
        {
            // --------------------- STATES ------------------------------------

            // The root of the decision tree
            State IdleState = new State(
                "Idle", Idle,
                null,
                null);

            State MovingState = new State(
                "Moving",
                null,
                Move,
                null);

            State AccidentState = new State("Accident", Accident, null, null);


            // ------------------------ TRANSITIONS ----------------------------

            // arrive at destination => idle
            // idle => invisível, desativa NavMeshAgent

            // IDLE => Move
            IdleState.AddTransition(new Transition(
                () => NavState == AgentState.Move,
                null,
                MovingState));


            // Move => Idle
            MovingState.AddTransition(new Transition(
                () => NavState == AgentState.Idle,
                null,
                IdleState));


            // Move => Accident
            MovingState.AddTransition(new Transition(
                () => NavState == AgentState.Accident,
                () => StopAgentMovement(true),
                AccidentState));

            // Accident => Move
            AccidentState.AddTransition(new Transition(
                () => NavState == AgentState.Move,
                () => StopAgentMovement(false),
                MovingState));

            // ---------------------------------------

            _fsm = new StateMachine(MovingState);

        }

        // Run the decision tree and execute the returned action
        private void Update()
        {
            // Update FSM 
            Action actions = _fsm.Update();
            actions?.Invoke();
        }

        private void SetIdle()
        {
            NavState = AgentState.Idle;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!Chaotic)
            {
                if (IsCar())
                {                    
                    if (other.CompareTag("Vehicle"))
                    {
                        agent.speed = Mathf.Lerp(agent.speed,
                        other.GetComponent<NavMeshAgent>().speed,
                        Time.deltaTime * 20);
                    }

                    else if (other.CompareTag("PedTrigger"))
                    {
                        bool coll = other.GetComponent<CarTrigger>().HasPed;
                        
                        if (coll)
                        {
                            canMove = false;
                            StopAgentMovement(true);
                        }
                        else 
                        {
                            canMove = true;
                            StopAgentMovement(false);
                        }

                    }

                    else if (other.CompareTag("Pedestrian")
                    || other.CompareTag("RedLight"))
                    {
                        StopAgentMovement(true);
                    }

                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!Chaotic)
            {
                if (IsCar())
                {
                    if (other.CompareTag("Vehicle") 
                        || other.CompareTag("Pedestrian")
                        || other.CompareTag("RedLight"))
                    {
                        StopAgentMovement(false);
                    }
                }

                else
                {
                    if (other.CompareTag("PedTrigger"))
                    {
                        StopAgentMovement(false);
                    }

                }
            }

        }

        private void OnCollisionEnter(Collision other)
        {

            if (IsCar())
            {
                if (other.gameObject.CompareTag("Vehicle") && 
                    NavState != AgentState.Accident)
                {
                    NavState = AgentState.Accident;
                }

                else if (other.gameObject.CompareTag("Pedestrian"))
                {
                    NavState = AgentState.Accident;
                }
            }

            else
            {
                if (other.gameObject.CompareTag("Vehicle"))
                {
                    NavState = AgentState.Accident;
                }
            }


        }

        private bool IsCar()
        {
            return tag == "Vehicle";
        }


        // ----------------------- ACTIONS -------------------------------------

        private void Idle()
        {
            ResetRBVelocities();

            agent.ResetPath();

            if (_model.activeSelf) _model.SetActive(false);

            StopAgentMovement(true);

            agent.speed = initialAgentSpeed;

            int randTime = Random.Range(0, _stopTime + 1);

            StartCoroutine(TimeWait(randTime, AgentState.Move));

           StopAgentMovement(false);

            randPos = Random.Range(0, goal.Length);


        }


        private void Move()
        {
            if(canMove)
            {   
                UpdateDestination(randPos);

                if (!agent.pathPending && agent.remainingDistance < 1f)
                {
                    NavState = AgentState.Idle;
                }
            }
        }


        private void Chaos()
        {
            float time = Random.Range(0, _chaosTime + 1);
            agent.speed *= 3;
            StartCoroutine(HitFlash(time, materialForChaos.color));
        }

        // receive time of flash 
        private IEnumerator HitFlash(float time, Color color)
        {
            time *= 2;
            for (int i = 0; i < time; i++)
            {
                foreach (MeshRenderer m in meshToChangeColor)
                {
                    if (m.material.color == originalMaterial.color)
                        m.material.color = color;
                    
                    else if (m.material.color == color)
                        m.material.color = originalMaterial.color;
                }
                yield return new WaitForSeconds(0.5f);
            }

            foreach (MeshRenderer m in meshToChangeColor)
                if (m.material.color == color)
                    m.material.color = originalMaterial.color;
        }

        private IEnumerator TimeWait(int time, AgentState state)
        {
            if (state == AgentState.Accident)
            {
                StartCoroutine(HitFlash(time, materialForChaos.color));
            }
            else
            {
                yield return new WaitForSecondsRealtime(time);

                if (!_model.activeSelf && NavState == AgentState.Idle)
                    _model.SetActive(true);
            }

            NavState = state;
        }

        private void Accident()
        {
            Debug.Log("Accident Movement");

            // reset dynamic movement
            ResetRBVelocities();

            // stop nav mesh
            StopAgentMovement(true);

            agent.ResetPath();

            int time = Random.Range(0, _accidentTime);


            StartCoroutine(HitFlash(time, materialForAccident.color));
            // wait random time
            StartCoroutine(
                TimeWait(Random.Range(0, _accidentTime),
                 AgentState.Move));
        }

        // ---------------------------------------------------------------------


        /// <summary>
        /// Update destination
        /// </summary>
        /// <param name="pos"></param>
        private void UpdateDestination(int pos)
        {
            // Set destination to current goal position
            agent.SetDestination(goal[pos].transform.position);
        }


        private void StopAgentMovement(bool stop)
        {
            if (stop)
            {
                agent.speed = Mathf.Lerp(agent.speed, 0,
                                        Time.deltaTime * 50);
            }
            else
            {   
                agent.speed = Mathf.Lerp(agent.speed, initialAgentSpeed,
                                        Time.deltaTime * 30);
            }

        }


        private void ResetRBVelocities()
        {
            if (!_rb.isKinematic)
            {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }

        public void SetParameters(int timeStopped,
                                  int timeInAccident,
                                  int timeInChaos,
                                  int chaosChance)
        {
            _stopTime = timeStopped;
            _accidentTime = timeInAccident;
            _chaosTime = timeInChaos;
            _chaosChance = chaosChance;
        }

        /// <summary>
        /// Method checks if agent is moving state and is also not in chaos mode.
        /// </summary>
        /// <returns>
        /// Reuturns the bool value of the check descripted above.
        /// true: means this agent is a candidate to be set in chaos mode.
        /// false: means this agent is not viable to set in chaos mode
        /// </returns>
        public bool CanBeChaos() => NavState == AgentState.Move && !Chaotic;
        
        /// <summary>
        /// Method sets agent to chaos mode.
        /// </summary>
        public void SetChaosAgent()
        {
            // Set the agent to be a chaos agent
            StartCoroutine(ChaosTimer()); 
        }


        private IEnumerator ChaosTimer()
        {   
            // chaos features
            Chaotic = true;
            agent.speed = initialAgentSpeed * 1.5f;


            yield return new WaitForSeconds(Random.Range(0,_chaosTime));

            // reset chaos features
            agent.speed = initialAgentSpeed;
            Chaotic = false;
            
        }

        public void IncreaseChaosChance(int value)
        {
            if(_chaosChance < 100)
                _chaosChance += value;
        }


    }

}

