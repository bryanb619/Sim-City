using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// 
    /// </summary>
    public class AIDirector : MonoBehaviour
    {

        #region Parameters

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

        [Range(0, 100)]
        [SerializeField] 
        private float         _chaosChance = 5f;

        #endregion

        // list of agents

        // car list
        private List<GameObject> _carList = new List<GameObject>();

        // ped list
        private List<GameObject> _pedList = new List<GameObject>();


        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            print("Simulation Started");

            // car spawn
            SpawnAgents(_car, _cars);

            // ped spawn
            SpawnAgents(_ped, _peds);
        }


        #region Car & Ped Spawn

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAI"></param>
        /// <param name="quantity"></param>
        private void SpawnAgents(GameObject objAI, int quantity)
        {   
            // ref to nav agent
            NavAgentBehaviour agent;

            // random point value
            int rp = 0;

            // loop
            for (int i = 0; i < quantity + 1; i++)
            {

                if (objAI == _car) 
                {   

                    //StartCoroutine(WaitForSeconds(3f)); 

                    // get random point
                    rp = Random.Range(0, _carSpawnPoints.Count);

                    // spawn car
                    Instantiate(objAI, _carSpawnPoints[rp].transform.position, 
                    transform.rotation);
                
                    // get NavAgentBehaviour component from spawned car
                    agent = objAI.GetComponent<NavAgentBehaviour>();

                    // set parameters
                    agent.SetParameters(_carTimeStoped, 
                    _maxTimeInAccident, _maxTimeInCrazy,_chaosChance);

                    // add car to list
                    _carList.Add(objAI); 

                    
                }

                else 
                {   

                    // get random point
                    rp = Random.Range(0, _pedSpawnPoints.Count);

                    // spawn ped
                    Instantiate(objAI, _pedSpawnPoints[rp].transform.position, 
                    transform.rotation);


                    // get NavAgentBehaviour component from spawned ped
                    agent = objAI.GetComponent<NavAgentBehaviour>();
                    
                    // set parameters
                    agent.SetParameters(_pedTimeStopped,
                    _maxTimeInAccident, _maxTimeInCrazy, _chaosChance);

                    // add ped to list
                    _pedList.Add(objAI); 

                    //print($"Ped {i} spawned");

                    //StartCoroutine(WaitForSeconds(1f)); 
                }

            }

        }


        private IEnumerator WaitForSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        public void SelectCarCrazyMode()
        {

            // random car value
            int i = 0;
            
            // get random value
            i = Random.Range(0, _carList.Count);

            // get NavAgentBehaviour component from car list index
            _carList[i].GetComponent<NavAgentBehaviour>();

            //TODO: set car to crazy mode
            //_carList[i].GetComponent<NavAgentBehaviour>().State = AgentState.Crazy;
        }

        public void SelectPedCrazyMode()
        {

            int i = 0;
             
            i = Random.Range(0, _pedList.Count);

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