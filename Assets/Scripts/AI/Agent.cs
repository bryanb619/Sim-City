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
        // ---------------- interface ------------------------------------------

        public AgentState NavState { get; private set; } = AgentState.Move;
        // -----------------------------Agent-----------------------------------

        // Current goal of navigation agent
        [SerializeField] private GameObject[] goal;

        private int randPos;

        // Reference to the NavMeshAgent component
        private NavMeshAgent agent;

        [SerializeField]
        private GameObject _car;

        private float initialAgentSpeed;

        private Vector2Int _stopTime, _accidentTime, _chaosTime;

        public bool Chaotic { get; private set; }

        private float _chaosChance;

        // ------------- FSM ---------------------------------------------------

        private StateMachine _fsm;

        [SerializeField]
        private MeshRenderer[] meshToChangeColor;

        [SerializeField]
        private GameObject[] meshToDeactivate;

        [SerializeField]
        private Material materialForAccident, materialForChaos;
        private Material originalMaterial;

        private Rigidbody _rb;

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
                originalMaterial = meshToChangeColor[0].material;
            }

            // Get reference to the NavMeshAgent component
            agent = GetComponent<NavMeshAgent>();

            initialAgentSpeed = agent.speed;
        }

        #region Start methods
        // Start is called before the first frame update
        private void Start()
        {

            StartFSM();

            _rb = GetComponent<Rigidbody>();

            randPos = Random.Range(0, goal.Length);
        }


        private void StartFSM()
        {
            // --------------------- STATES ------------------------------------

            State IdleState = new State(
                "Idle",
                Idle,
                null,
                null);

            State MovingState = new State(
                "Moving",
                null,
                Move,
                null);

            State AccidentState = new State(
                "Accident",
                () => Debug.Log("Enter Accident"),
                  Accident,
                () => Debug.Log("Left Accident"));


            // ------------------------ TRANSITIONS ----------------------------

            // arrive at destination => idle
            // idle => invisel, desativa NavMeshagent

            // IDLE => Move
            IdleState.AddTransition(new Transition(
                () => NavState == AgentState.Move,
                () => Debug.Log("Transition Idle"),
                MovingState));


            // Move => Idle
            MovingState.AddTransition(new Transition(
                () => NavState == AgentState.Idle,
                () => Debug.Log("Transition Move"),
                IdleState));


            // Move => Accident
            MovingState.AddTransition(new Transition(
                () => NavState == AgentState.Accident,
                () => Debug.Log("Transition Accident"),
                AccidentState));

            // Move => Accident
            AccidentState.AddTransition(new Transition(
                () => NavState == AgentState.Move,
                () => Debug.Log("Transition Move"),
                MovingState));



            // ---------------------------------------

            _fsm = new StateMachine(MovingState);

        }

        #endregion
        // Run the decision tree and execute the returned action
        private void Update()
        {

            // Update FSM 
            Action actions = _fsm.Update();
            actions?.Invoke();

        }



        // Method called when agent collides with something
        private void OnTriggerEnter(Collider other)
        {
            //if (other.CompareTag("CarDest"))
            //  Invoke("SetIdle", 0.1f);

            // Debug.Log("penetrated");


        }

        private void SetIdle()
        {
            NavState = AgentState.Idle;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!Chaotic)
            {
                if (other.CompareTag("Vehicle"))
                    agent.speed = Mathf.Lerp(agent.speed,
                    other.GetComponent<NavMeshAgent>().speed,
                    Time.deltaTime);

                else if (other.CompareTag("Pedestrian") || 
                other.CompareTag("RedLight"))
                {
                    agent.speed = Mathf.Lerp(agent.speed, 0, Time.deltaTime * 50);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!Chaotic)
            {
                if (other.CompareTag("Vehicle") || other.CompareTag("Pedestrian")
                    || other.CompareTag("RedLight"))
                {
                    agent.speed = initialAgentSpeed;
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Pedestrian")
            || other.gameObject.CompareTag("Vehicle"))
            {
                NavState = AgentState.Accident;
            }


        }


        // ----------------------- ACTIONS -------------------------------------

        #region  Idle
        private void Idle()
        {

            ResetRB();

            if (_car.activeSelf) _car.SetActive(false);

            StopAgentMovement(true);

            agent.ResetPath();


            int randTime = Random.Range(_stopTime.x, _stopTime.y);

            StartCoroutine(TimeWait(randTime, AgentState.Move));



            randPos = Random.Range(0, goal.Length);


            // TODO: UPDATE TIME

            //foreach (GameObject m in meshToDeactivate)
            //{
            //  m.SetActive(false);
            //}

            //if (agent.GetComponent<BoxCollider>() != null)
            //   agent.GetComponent<BoxCollider>().enabled = false;
            //else
            //    agent.GetComponent<CapsuleCollider>().enabled = false;
        }

#endregion


#region Move

        private void Move()
        {

            StopAgentMovement(false);



            if (!_car.activeSelf) _car.SetActive(true);

            UpdateDestination(randPos);


            if (!agent.pathPending && agent.remainingDistance < 1f)
            {
                NavState = AgentState.Idle;
            }

        }

        #endregion

        #region  Chaos
        private void Chaos()
        {
            float time = Random.Range(10F, 60F);
            agent.speed *= 3;
            StartCoroutine(HitFlash(Random.Range(_chaosTime.x, _chaosTime.x)));


        }

        // receive time of flash 
        private IEnumerator HitFlash(float time)
        {
            int timer = 0;

            while (true)
            {
                timer++;


                if (timer < time)
                {
                    foreach (MeshRenderer m in meshToChangeColor)
                        m.material = materialForChaos;

                    yield return new WaitForSeconds(0.2f);

                    foreach (MeshRenderer m in meshToChangeColor)
                        m.material = originalMaterial;
                }

            }

        }

        private IEnumerator TimeWait(int time, AgentState state)
        {

            yield return new WaitForSecondsRealtime(time);

            NavState = state;
        }

        #endregion

        #region Accident
        private void Accident()
        {

            ResetRB();

            StopAgentMovement(true);
            //foreach (MeshRenderer m in meshToChangeColor)
            //  m.material = materialForAccident;

            StartCoroutine(
                TimeWait(Random.Range(_accidentTime.x, _accidentTime.y),
                 AgentState.Move));

        }


        #endregion

        // ---------------------------------------------------------------------


        // Update destination
        private void UpdateDestination(int pos)
        {
            // Set destination to current goal position
            agent.SetDestination(goal[pos].transform.position);
        }


        private void StopAgentMovement(bool stop)
        {
            if (stop)
            {
                agent.speed = 0;
            }
            else
            {
                agent.speed = initialAgentSpeed;

            }

        }


        private void ResetRB()
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        public void SetParameters(Vector2Int timeStopped,
                                  Vector2Int timeInAccident,
                                  Vector2Int timeInChaos,
                                  int chaosChance)
        {


            _stopTime = timeStopped;
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

            if (!Chaotic)
            {

                //
                Chaotic = true;

                // timer function that enables chaos movement

                // 
                Chaotic = false;



            }


        }
    }

}

