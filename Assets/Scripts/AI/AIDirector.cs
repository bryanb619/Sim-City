using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// 
    /// </summary>
    public class AIDirector : MonoBehaviour
    {

        #region Parameters

        [SerializeField, BoxGroup("Cars"), Label("Car amount"), 
        Range(0, 50)]
        private int             _cars = 25;

        [SerializeField, BoxGroup("Cars"), 
        MinMaxSlider(0, 75)] 
        // x = minimum time, y = maximum time
        private Vector2Int      _carTimeStopped;

        [SerializeField, BoxGroup("Cars")] 
        private GameObject      _car;

        [SerializeField, BoxGroup("Pedestrian"), Label("Ped amount"), 
        Range(0, 75)]
        private int             _peds = 30;

        [SerializeField, BoxGroup("Pedestrian"), 
        MinMaxSlider(0, 75)] 
        // x = minimum time, y = maximum time
        private Vector2Int      _pedTimeStopped;
                
        [SerializeField, BoxGroup("Pedestrian")] 
        private GameObject      _ped;


        [SerializeField, BoxGroup("Accident"), 
        MinMaxSlider(0, 75)] 
        private Vector2Int      _timeInAccident;


        [SerializeField, BoxGroup("Chaos"), 
        MinMaxSlider(0, 75)]
        // x = minimum time, y = maximum time
        private Vector2Int      _timeInChaos;

        [SerializeField, BoxGroup("Chaos"), 
        Range(0, 100)]
        private int             _chaosChance = 5;

        [SerializeField, Foldout("Spawn Points")] 
        private List<Transform> _pedSpawnPoints = new List<Transform>();
        
        [SerializeField, Foldout("Spawn Points")]
        private List<Transform> _carSpawnPoints = new List<Transform>();
        
        
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
                    agent.SetParameters(_carTimeStopped, _timeInAccident, _timeInChaos,_chaosChance);

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
                    agent.SetParameters(_pedTimeStopped, _timeInAccident, _timeInChaos,_chaosChance);

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
        public void SelectCarChaosMode()
        {

            // random car value
            int i = 0;
            
            // get random value
            i = Random.Range(0, _carList.Count);

            // get NavAgentBehaviour component from car list index
            _carList[i].GetComponent<NavAgentBehaviour>();

            //TODO: set car to chaos mode
            //_carList[i].GetComponent<NavAgentBehaviour>().State = AgentState.Chaos;
        }

        public void SelectPedChaosMode()
        {

            int i = 0;
             
            i = Random.Range(0, _pedList.Count);

            _pedList[i].GetComponent<NavAgentBehaviour>();

            /*
            int i = Random.Range(0, _agents.Length);


            if(navAgent[i].State == AgentState.Move)
            {
                navAgent[i].State = AgentState.Chaos;
            }
            */
        }
    }
}