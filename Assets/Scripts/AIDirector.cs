using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.AI;
using UnityEngine;

public class AIDirector : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _agents;

    private NavAgentBehaviour[] navAgent;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {

        for(int i = 0; _agents.Length < i; i++)
        {
            Instantiate(_agents[i], transform.position, transform.rotation);

            navAgent[i] = GetComponent<NavAgentBehaviour>();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public void ActivateCrazyAgent()
    {
        
        int i = Random.Range(0, _agents.Length);

        
        if(navAgent[i].State == AgentState.Move)
        {
            // TODO : CALL CRAZY METHOD IN THIS AI AGENT

        }
    }
}
