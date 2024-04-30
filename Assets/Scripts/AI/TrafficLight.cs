using System;
using System.Collections.Generic;
using UnityEngine;
using LibGameAI.FSMs;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// A traffic light.
    /// </summary>{
    public class TrafficLight : MonoBehaviour, ITrafficLight
    {
        public float changeTime { get; private set; }

        private StateMachine _fsm;

        // -------------------- TEMP/TEST --------------------------------------
        private enum lightState
        {
            green, yellow, red
        }

        [Header("Traffic Light")]
        [SerializeField]
        private lightState _currentLightState;
        //----------------------------------------------------------------------



        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            System.Random rand = new System.Random(); 

            int i = rand.Next(0, 2);


            // -------------------- States -------------------------------------


            State yellowState = new State(  "Yellow", YellowLight,  null, null);
            State redState = new State(     "Red",    RedLight,     null, null);
            State greenState = new State(   "Green",  GreenLight,   null, null);


            List <State> states = new List<State>
            {
                yellowState, redState, greenState
            };


            _fsm = new StateMachine(states[i]);


            // -------------------- Transitions --------------------------------

            // change every 10 seconds
            //() => Time.time > 5, null, lightState

            greenState.AddTransition(new Transition(
                () => _currentLightState == lightState.yellow, 
                null, yellowState));

            yellowState.AddTransition(new Transition(
                () => _currentLightState == lightState.red, 
                null, redState));

            redState.AddTransition(new Transition(
                () => _currentLightState == lightState.green, 
                null, greenState));
        }


        /// <summary>
        /// Update is called once per frame
        /// </summary>
        private void Update()
        {
           

            Action actions = _fsm.Update();
            actions?.Invoke();
        }

        
        /// <summary>
        /// 
        /// </summary>
        private void GreenLight()
        {
            if(Time.time > 15)
            {
                _currentLightState = lightState.yellow;
                print("Yellow Light");
            }

            print(Time.time);

        }

        /// <summary>
        /// 
        /// </summary>
        private void YellowLight()
        {
            if(Time.time > 3)
            {
                _currentLightState = lightState.red;
                print("Red light");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void RedLight()
        {
            if(Time.time > 10)
            {
                _currentLightState = lightState.green;
                print("Green  light");
            }


        }

    }
}

