using UnityEngine;
using UnityEngine.AI;
using Assets.Scripts.AI.Agents;

namespace Assets.Scripts.AI
{
    // Automatically add the NavMeshAgent to the GameObject
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentAI : MonoBehaviour
    {

        // Reference to NavMeshAgent component
        private NavMeshAgent _navAgent;

        [SerializeField] 
        private AgentType _agentType;

        private Agent _agent;

        
        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {

            GetComponents();

            SetAgentType();

            
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
        private void SetAgentType()
        {
            switch (_agentType)
            {
                case AgentType.Vehicle:
                {
                    _agent = new Car();
                    break;
                }
                    
                case AgentType.Pedestrian:
                {
                    _agent = new Ped();
                    break;
                }
        
                case AgentType.TrafficLight:
                {
                    _agent = new TrafficLight();
                    break;
                }
            
            }
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
    }

}
