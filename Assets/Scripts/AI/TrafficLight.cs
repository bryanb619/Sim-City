using System;
using System.Collections.Generic;
using UnityEngine;
using LibGameAI.FSMs;
using System.Collections;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// A simple traffic light.
    /// Uses libGameAI (FSM) library to implement states & transitions for the
    /// signals.
    /// </summary>{
    public class TrafficLight : MonoBehaviour, ITrafficLight
    {

        public LightState Light { get; private set; }


        [Tooltip("Green light timer")]
        [Range(0, 50f)]
        [SerializeField] private float _greenTime = 5f;

        [Tooltip("Yellow light timer")]
        [Range(0, 50f)]
        [SerializeField] private float _yellowTime = 10f;

        [Tooltip("Red light timer")]
        [Range(0, 50f)]
        [SerializeField] private float _redTime = 10f;

        [SerializeField] private float _updateCheck = 1f;

        private StateMachine _fsm;

        float time = 0; 


# if UNITY_EDITOR 

        // -------------------- TEMP/TEST --------------------------------------
        [Header("Traffic Light.\nChanging it does not affect signal")]
        [SerializeField]
        private LightState _currentLightState;
        //----------------------------------------------------------------------
# endif


        /// <summary>
        /// Start is called before the first frame update.
        /// Initiates all available signal states. 
        /// Signal transitions follow this pattern (green -> yellow -> red).
        /// Starts a coroutine updated every (1s default) that checks the state
        /// and updates time.
        /// </summary>
        private void Start()
        {

            System.Random rand = new System.Random();

            int i = rand.Next(0, 2);


            // -------------------- States -------------------------------------

            State yellowState = new State("Yellow", YellowLight, null, null);
            State redState = new State("Red", RedLight, null, null);
            State greenState = new State("Green", GreenLight, null, null);


            List<State> states = new List<State>
            {
                yellowState, redState, greenState
            };


            // -------------------- Transitions --------------------------------

            greenState.AddTransition(new Transition(
                () => Light == LightState.yellow,
                null, yellowState));

            yellowState.AddTransition(new Transition(
                () => Light == LightState.red,
                null, redState));

            redState.AddTransition(new Transition(
                () => Light == LightState.green,
                null, greenState));

            _fsm = new StateMachine(states[i]);
            
            //_fsm = new StateMachine(greenState);

            StartCoroutine(UpdateLightState());
        }


        /// <summary>
        /// Update is called once per frame
        /// Updates actions & invokes the update of the FSM.
        /// </summary>
        private void Update()
        {
            Action actions = _fsm.Update();
            actions?.Invoke();
        }


        /// <summary>
        /// Coroutine updates time, checks and changes the singal states while 
        /// also reseting the time
        /// </summary>
        /// <returns>
        ///  waits for seconds
        /// </returns>
        private IEnumerator UpdateLightState()
        {

            while (true)
            {
                time += 1;
                print(time);

                switch (Light)
                {
                    case LightState.green:
                        {
                            if(IsGraterThan(_greenTime)) 
                            {

# if UNITY_EDITOR 
                                _currentLightState = LightState.yellow;
# endif

                                Light = LightState.yellow;
                                time = 0; 
                            }
                            
                            break;
                        }

                    case LightState.yellow:
                        {
                            if(IsGraterThan(_yellowTime)) 
                            {
# if UNITY_EDITOR 
                                _currentLightState = LightState.red;
#endif

                                Light = LightState.red; 
                                time = 0; 
                            }

                            break;
                        }

                    case LightState.red:
                        {
                            if(IsGraterThan(_redTime)) 
                            {
# if UNITY_EDITOR 
                                _currentLightState = LightState.green;
#endif
                                Light = LightState.green; 
                                time = 0; 
                            }

                            break;
                        }
                }

                yield return new WaitForSeconds(_updateCheck);
            }
        }

        /// <summary>
        /// Checks if time is greater than a passed value
        /// </summary>
        /// <returns>
        /// Returns true if time is greater
        /// Returns false if time is not greater
        /// </returns>
        private bool IsGraterThan(float signalTimer)
        {
            if(time > signalTimer) { return true; }

            else { return false; }
        }


        /// <summary>
        /// 
        /// </summary>
        private void GreenLight()
        {
            Debug.Log($"Current light is: {Light}");
        }

        /// <summary>
        /// 
        /// </summary>
        private void YellowLight()
        {
            Debug.Log($"Current light is: {Light}");
        }

        /// <summary>
        /// 
        /// </summary>
        private void RedLight()
        {
            Debug.Log($"Current light is: {Light}");
        }

    }
}

