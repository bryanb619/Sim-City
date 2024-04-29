using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.AI
{
    // Automatically add the NavMeshAgent to the GameObject
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentCar : MonoBehaviour
    {   
        [Tooltip("The maximum speed of the agent in units / sec.")]
        [Range(0, 100)]
        [SerializeField] 
        private float           speed;

        [Tooltip("The maximum acceleration of the agent in units / sec^2.")]
        [Range(0, 100)]
        [SerializeField] 
        private float           acceleration;

        [Tooltip("The maximum turning speed in degrees per second that the agent can rotate.")]
        [Range(0, 999)]
        [SerializeField] 
        private float           angularSpeed;

        [Tooltip("Size of the agent. This value influences the size of navmesh agent")]
        [Range(5, 200)]
        [SerializeField] 
        private float           size; 


        [Tooltip("Value of the mass.")]
        [Range(5, 200)]
        [SerializeField] 
        private float           mass; 


        // Reference to NavMeshAgent component
        private NavMeshAgent    _navAgent;

        private Agent           _agent;

        
        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            GetComponents();

            SetAgent();

            //_navAgent.SetDestination(Target.position);
            
        }



        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
            
        }

        /// <summary>
        /// Sets the agent type according to the AgentType enumerator defined 
        /// in the inspector with a dropdown menu.
        /// Agent type can be:
        /// 1. Vehicle
        /// 2. Pedestrian
        /// 3. TrafficLight
        /// </summary>
        private void SetAgent()
        {

            _agent = new Car(speed, acceleration, size, mass);
               
            Debug.Log($"{_agent}");
        }


        /// <summary>
        /// Method attempts to get the NavMeshAgent component
        /// 
        /// </summary>
        private void GetComponents()
        {
            try
            {
                _navAgent = GetComponent<NavMeshAgent>();

            }
            catch (System.Exception e)
            {
                Debug.LogError("component not found: " + e.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="target"></param>
        private void SetDestination(Vector3 destination, Transform target)
        {

        }
    }

}
