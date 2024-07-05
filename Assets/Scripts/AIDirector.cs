using System.Collections.Generic;
using Assets.Scripts.AI;
using UnityEngine;

public class AIDirector : MonoBehaviour
{
    [SerializeField] private UI _ui;

    [SerializeField] private GameObject _car;  

    [SerializeField] private GameObject _ped;

    private List<GameObject> _carList = new List<GameObject>();

    private List<GameObject> _pedList = new List<GameObject>();

    public bool SimStar { get; private set; } = false; 


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {

        SimStar = true;

        print("Simulation Started");

        print(_ui.MaxCars);

        SpawnAgents(_car, _ui.MaxCars);

        // ped spawn
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="objAI"></param>
    private void SpawnAgents(GameObject objAI, int quantity)
    {   

        // loop
        for(int i = 0;  i < quantity ; i++)
        {
            Instantiate(objAI, transform.position, transform.rotation);

            print("Agent " + i + " spawned");

            if (objAI == _car) { _carList.Add(objAI); }

            else { _pedList.Add(objAI); }
            
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
