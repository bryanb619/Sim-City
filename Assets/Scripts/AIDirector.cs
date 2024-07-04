using Assets.Scripts.AI;
using UnityEngine;

public class AIDirector : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _agents;

    private NavAgentBehaviour[] navAgent;

    [SerializeField] private UI _ui;

    [SerializeField] private GameObject _car;  

    [SerializeField] private GameObject _ped;

    public bool SimStar { get; private set; } = false; 


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {

        //navAgent = new NavAgentBehaviour[_ui.]
        // car spawn 
        SpawnAgents(_car, _ui.MaxCars);

        SimStar = true;
        
        print("Simulation Started");

        // ped spawn
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objAI"></param>
    private void SpawnAgents(GameObject objAI, int quantity)
    {   

        // loop
        for(int i = 0; quantity < i; i++)
        {
            Instantiate(objAI, transform.position, transform.rotation);

            _agents[i] = objAI;

            navAgent[i] = _agents[i].GetComponent<NavAgentBehaviour>();
        }

    }


    /// <summary>
    /// 
    /// </summary>
    public void ActivateCrazyAgent()
    {
        
        /*
        int i = Random.Range(0, _agents.Length);

        
        if(navAgent[i].State == AgentState.Move)
        {
            navAgent[i].State = AgentState.Crazy;
        }
        */
    }
}
