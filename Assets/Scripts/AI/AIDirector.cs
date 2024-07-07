using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// 
    /// </summary>
    public class AIDirector : MonoBehaviour
    {

        [Header("== Car ==")]
        [Range(0, 50)]
        [SerializeField] 
        private int        _cars = 25;

        [SerializeField] 
        private List<Transform> _carSpawnPoints = new List<Transform>();

        [Range(0, 75)]
        [SerializeField] 
        private int        _carTimeStoped = 10;

        [SerializeField] 
        private GameObject _car;

        [Header("== Ped ==")]
        [Range(0, 75)]
        [SerializeField] 
        private int         _peds = 30;

        [SerializeField] 
        private List<Transform> _pedSpawnPoints = new List<Transform>();

        [Range(0, 75)]

        [SerializeField] 
        private int         _pedTimeStopped = 10;
                
        [SerializeField] 
        private GameObject _ped;


        
        [Header("== Accident ==")]
        [Range(0, 75)]
        [SerializeField] 
        private int         _maxTimeInAccident = 15;


        [Header("== Chaos ==")]
        [Range(0, 75)]
        [SerializeField] 
        private int         _maxTimeInCrazy = 30;

        [Range(0, 75)]
        [SerializeField] 
        private int         _chaosChance = 5;



 


        private List<GameObject> _carList = new List<GameObject>();

        private List<GameObject> _pedList = new List<GameObject>();


        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {

            print("Simulation Started");

            SpawnAgents(_car, _cars);

            // ped spawn
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAI">Game object</param>
        private void SpawnAgents(GameObject objAI, int quantity)
        {

            // loop
            for (int i = 0; i < quantity; i++)
            {
                Instantiate(objAI, transform.position, transform.rotation);

                NavAgentBehaviour agent = 
                objAI.GetComponent<NavAgentBehaviour>();

                if (objAI == _car) 
                {
                    _carList.Add(objAI); 

                    agent.SetParameters(_carTimeStoped, 
                    _maxTimeInAccident, _maxTimeInCrazy);


                    //print($"Car {i} spawned");
                }

                else 
                { 
                    _pedList.Add(objAI); 
                    agent.SetParameters(_pedTimeStopped,
                    _maxTimeInAccident, _maxTimeInCrazy);

                    //print($"Ped {i} spawned");
                }

            }

        }


        /// <summary>
        /// 
        /// </summary>
        public void SelectCarCrazyMode()
        {

            int i = Random.Range(0, _carList.Count);

            _carList[i].GetComponent<NavAgentBehaviour>();



            /*
            int i = Random.Range(0, _agents.Length);

            if(navAgent[i].State == AgentState.Move)
            {
                navAgent[i].State = AgentState.Crazy;
            }
            */
        }

        public void SelectPedCrazyMode()
        {

            int i = Random.Range(0, _pedList.Count);

            _pedList[i].GetComponent<NavAgentBehaviour>();


            /*
            int i = Random.Range(0, _agents.Length);


            if(navAgent[i].State == AgentState.Move)
            {
                navAgent[i].State = AgentState.Crazy;
            }
            */
        }
    }
}