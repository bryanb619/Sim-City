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

        private GameObject currentAI;

        [SerializeField, BoxGroup("Cars"), Label("Car amount"),
        Range(0, 50)]
        private int _cars = 25;

        [SerializeField, BoxGroup("Cars"),
        MinMaxSlider(0, 75)]
        // x = minimum time, y = maximum time
        private Vector2Int _carTimeStopped;

        [SerializeField, BoxGroup("Cars")]
        private GameObject _car;

        [SerializeField, BoxGroup("Pedestrian"), Label("Ped amount"),
        Range(0, 100)]
        private int _peds = 30;

        [SerializeField, BoxGroup("Pedestrian"),
        MinMaxSlider(0, 75)]
        // x = minimum time, y = maximum time
        private Vector2Int _pedTimeStopped;

        [SerializeField, BoxGroup("Pedestrian")]
        private GameObject _ped;


        [SerializeField, BoxGroup("Accident"),
        MinMaxSlider(0, 75)]
        private Vector2Int _timeInAccident;


        [SerializeField, BoxGroup("Chaos"),
        MinMaxSlider(0, 75)]
        // x = minimum time, y = maximum time
        private Vector2Int _timeInChaos;

        [SerializeField, BoxGroup("Chaos"),
        Range(0, 100)]
        private int _chaosChance = 5;

        [SerializeField, BoxGroup("Pedestrian")]
        private List<Transform> _pedSpawnPoints = new List<Transform>();

        [SerializeField, BoxGroup("Cars")]
        private List<Transform> _carSpawnPoints = new List<Transform>();


        #endregion

        // list of agents

        // car list
        private List<GameObject> _carList = new List<GameObject>();

        // ped list
        private List<GameObject> _pedList = new List<GameObject>();


        [SerializeField] private UI _ui;


        [SerializeField] private bool doubleTimeScale;

        private bool coroutineStarted = false;


        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            // car spawn
            SpawnAgents(_car, _cars);

            // ped spawn
            SpawnAgents(_ped, _peds);

            if (doubleTimeScale) Time.timeScale = 2f;
        }


        #region Car & Ped Spawn

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAI"></param>
        /// <param name="quantity"></param>
        private void SpawnAgents(GameObject objAI, int quantity)
        {

            // random point value
            int rp = 0;

            // loop
            for (int i = 0; i < quantity; i++)
            {

                Agent agent;

                if(!coroutineStarted)
                {   
                    Invoke("WaitTime",2f);
                }

                if (objAI == _car)
                {
                    

                    // get random point
                    rp = Random.Range(0, _carSpawnPoints.Count);

                    // spawn car
                    currentAI = Instantiate(objAI,
                     _carSpawnPoints[rp].transform.position,
                    transform.rotation);

                    // get NavAgentBehaviour component from spawned car
                    agent = currentAI.GetComponent<Agent>();

                    // add car to list
                    _carList.Add(currentAI);

                    _ui.UpdateCarCount();

                    // set parameters
                    agent.SetParameters(_carTimeStopped, _timeInAccident, _timeInChaos, _chaosChance);


                }

                else
                {

                    // get random point
                    rp = Random.Range(0, _pedSpawnPoints.Count);

                    // spawn ped
                    currentAI = Instantiate(objAI, _pedSpawnPoints[rp].transform.position,
                    transform.rotation);


                    // get NavAgentBehaviour component from spawned ped
                    agent = currentAI.GetComponent<Agent>();

                    // set parameters
                    agent.SetParameters(_pedTimeStopped, _timeInAccident, _timeInChaos, _chaosChance);

                    // add ped to list
                    _pedList.Add(objAI);

                    _ui.UpdatePedCount();
                    //print($"Ped {i} spawned");
                }

            }

        }

        private IEnumerator WaitTime(float seconds)
        {
            coroutineStarted = true;

            yield return new WaitForSecondsRealtime(seconds);

            coroutineStarted = false;
        }

        #endregion


        /// <summary>
        /// 
        /// </summary>
        public void SelectCarChaosMode()
        {

            // var to hold random value
            int i = 0;

            // get random value within car list
            i = Random.Range(0, _carList.Count);

            // get NavAgentBehaviour component from car list index
            Agent agent = _carList[i].GetComponent<Agent>();

            while (agent.Chaotic)
            {
                i = Random.Range(0, _carList.Count);

                agent = _carList[i].GetComponent<Agent>();

                print("Car is already in chaos mode");
            }

            agent.SetChaosAgent();

            print($"Car {i} is in chaos mode");
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