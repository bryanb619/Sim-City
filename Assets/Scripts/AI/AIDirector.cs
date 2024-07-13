using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// Represents the director of AI in the simulation.
    /// AI Director is responsable setting the parameters of agents
    /// instantiate of agents, adding to their respective list (car or ped list)
    /// while also implementing the logic for the chaos button.
    /// </summary>
    public class AIDirector : MonoBehaviour
    {

        private GameObject currentAI;

        [SerializeField, BoxGroup("Cars"), Label("Car amount"),
        Range(0, 50)]
        private int _cars = 25;

        [SerializeField, BoxGroup("Cars")]
        // x = minimum time, y = maximum time
        private int _maxCarTimeStopped;

        [SerializeField, BoxGroup("Cars")]
        private GameObject _car;

        [SerializeField, BoxGroup("Pedestrian"), Label("Ped amount"),
        Range(0, 100)]
        private int _peds = 30;

        [SerializeField, BoxGroup("Pedestrian")]
        private int _maxPedTimeStopped;

        [SerializeField, BoxGroup("Pedestrian")]
        private GameObject _ped;


        [SerializeField, BoxGroup("Accident")]
        private int _maxTimeInAccident;


        [SerializeField, BoxGroup("Chaos")]
        // x = minimum time, y = maximum time
        private int _maxTimeInChaos;

        [SerializeField, BoxGroup("Chaos"),
        Range(0, 100)]
        private int _chaosChance = 5;

        [SerializeField, BoxGroup("Pedestrian")]
        private List<Transform> _pedSpawnPoints = new List<Transform>();

        [SerializeField, BoxGroup("Cars")]
        private List<Transform> _carSpawnPoints = new List<Transform>();


        // list of agents

        /// <summary>
        /// Represents the car list of game objects
        /// </summary>
        private List<GameObject> _carList = new List<GameObject>();

        /// <summary>
        /// Represents the Ped list of game objects
        /// </summary>
        private List<GameObject> _pedList = new List<GameObject>();

        [SerializeField] 
        private UI _ui;

        [SerializeField] 
        private bool doubleTimeScale;

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            // car spawn
            StartCoroutine(SpawnAgents(_car, _cars));

            // ped spawn
            StartCoroutine(SpawnAgents(_ped, _peds));

            if (doubleTimeScale) Time.timeScale = 2f;
        }

        /// <summary>
        /// This IEnumerator spawns agents into the simulation while also 
        /// adding them to their respective list (car or ped).
        /// </summary>
        /// <param name="objAI">game object to be spawned</param>
        /// <param name="quantity">
        /// quantity of previous gameObject (objAI) to be spawned in sim
        /// </param>
        private IEnumerator SpawnAgents(GameObject objAI, int quantity)
        {
            int rp;
            Agent agent;

            if (objAI == _car)
            {
                for(int i = 0; i < quantity; i++)
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
                    agent.SetParameters(_maxCarTimeStopped, _maxTimeInAccident, _maxTimeInChaos, _chaosChance);

                    yield return new WaitForSeconds(0.7f);
                }
            }
            else if (objAI == _ped)
            {
                for(int i = 0; i < quantity; i++)
                {
                    // get random point
                    rp = Random.Range(0, _pedSpawnPoints.Count);

                    // spawn ped
                    currentAI = Instantiate(objAI, _pedSpawnPoints[rp].transform.position,
                    transform.rotation);


                    // get NavAgentBehaviour component from spawned ped
                    agent = currentAI.GetComponent<Agent>();

                    // set parameters
                    agent.SetParameters(_maxPedTimeStopped, _maxTimeInAccident, _maxTimeInChaos, _chaosChance);

                    // add ped to list
                    _pedList.Add(objAI);

                    _ui.UpdatePedCount();
                    //print($"Ped {i} spawned");

                    yield return new WaitForSeconds(0.7f);
                }
            }

        }

        /// <summary>
        /// Method attempts to sets a random agent in chaos mode.
        /// bool value must be set so the method can correctly set an agent 
        /// to chaos mode.
        /// Pass a true value and method will use the car list to randomnly 
        /// chose a vehicle.
        /// Pass a false value and method will use the ped list to radomnly
        /// chose a ped.
        /// When no agent is available to be ped, a debug message will appear in
        /// the Unity console.
        /// </summary>
        /// <param name="isVehicle">
        /// True: this agent is of type vehicle
        /// False: this is not of type vehicle (is a ped)
        /// </param>
        public void SelectChaosMode(bool isVehicle)
        {
            Agent agent;

            int r, attempts = 0;

            bool foundAgent = false;

            string unit;

            List<GameObject> units; 


            do
            {
                // check if is vehicle
                if (isVehicle)
                {
                    unit = "Car";
                    units = _carList;

                }

                // else is a pedestrian
                else 
                {
                    unit = "Ped";
                    units = _pedList;
                }

                // obtain a random value withing the list count
                r = Random.Range(0, units.Count);

                // obtain the agent class
                agent = units[r].GetComponent<Agent>();

                // is this agent a chaos candidate?
                if (agent.CanBeChaos())
                {   
                    // found a agent & break
                    foundAgent = true;
                    break;
                }

                // agent not valid, so increase attempts!
                attempts++;
            }

            // loop until attemps does not pass the list count
            while (attempts < units.Count);

            // use this agent
            if (foundAgent) 
            {   
                // set to chaos
                agent.SetChaosAgent();
                print($"{unit} {r}: Chaos mode");
            }

            // no viable agent found, display message to user
            else Debug.Log($"No {unit} available to be chaos");
            
        }
    }
}