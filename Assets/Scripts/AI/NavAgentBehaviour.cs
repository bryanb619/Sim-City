using UnityEngine;
using UnityEngine.AI;
using LibGameAI.DecisionTrees;

namespace Assets.Scripts.AI
{
    public class NavAgentBehaviour : MonoBehaviour
    {
        // Current goal of navigation agent
        [SerializeField] private GameObject[] goal;

        // Reference to the NavMeshAgent component
        private NavMeshAgent agent;



        // The root of the decision tree
        private IDecisionTreeNode root;


        // 
        private void Awake()
        {

            goal = GameObject.FindGameObjectsWithTag("Dest");

            // Get reference to the NavMeshAgent component
            agent = GetComponent<NavMeshAgent>();

        }


        // Start is called before the first frame update
        private void Start()
        {
            
            // Set initial agent goal
            agent.SetDestination(goal[0].transform.position);


            RandomDecisionBehaviour rdb = new RandomDecisionBehaviour(
            () => Random.value,() => Time.time, 25);


            // stand action
            IDecisionTreeNode standStill = new ActionNode(Stand);

            // Move to target action
            IDecisionTreeNode GoToNextPont = new ActionNode(Move);

                                                        // if                   // true, false
            IDecisionTreeNode nodeA = new DecisionNode(rdb.RandomDecision,  GoToNextPont,  standStill); 
        }



        // Run the decision tree and execute the returned action
        private void Update()
        {
            // /////////////////////// //
            // Run decision tree here! //
            // /////////////////////// //

            ActionNode actionNode = root.MakeDecision() as ActionNode;
            //if(ActionNode)

            actionNode.Execute();

            //(root.MakeDecision() as ActionNode).Execute();

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

        private void Stand()
        {
            // Move towards player
            //MoveTowardsTarget(player.transform.position);
        }

        private void Move()
        {

            int pos = Random.Range(0, goal.Length);

            UpdateDestination(pos);
            
        }


        // Update destination
        private void UpdateDestination(int pos)
        {
            // Set destination to current goal position
            agent.SetDestination(goal[pos].transform.position);
        }
    }
}