using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// 
    /// </summary>
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
            for (int i = 0; i < quantity; i++)
            {
                Instantiate(objAI, transform.position, transform.rotation);

                NavAgentBehaviour agent = 
                objAI.GetComponent<NavAgentBehaviour>();

                if (objAI == _car) 
                {
                    _carList.Add(objAI); 

                    agent.SetParameters(_ui.CarStoppedTime, 
                    _ui.MaxTimeInAccident, _ui.MaxTimeInCrazy);


                    //print($"Car {i} spawned");
                }

                else 
                { 
                    _pedList.Add(objAI); 
                    agent.SetParameters(_ui.PedStoppedTime,
                    _ui.MaxTimeInAccident, _ui.MaxTimeInCrazy);

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