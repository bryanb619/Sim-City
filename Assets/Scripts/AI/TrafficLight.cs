using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LibGameAI.FSMs;


namespace Assets.Scripts.AI
{
    /// <summary>
    /// A simple traffic light.
    /// Uses libGameAI (FSM) library to implement states & transitions for the
    /// signals.
    /// </summary>
    public class TrafficLight : MonoBehaviour, ITrafficLight
    {

        public LightState Light { get; private set; }


        [Header("Green light timer")]
        [Range(0, 50f)]
        [SerializeField] private int _greenTime = 5;

        [Header("Yellow light timer")]
        [Range(0, 50f)]
        [SerializeField] private int _yellowTime = 10;

        [Header("Red light timer")]
        [Range(0, 50f)]
        [SerializeField] private int _redTime = 10;

        private StateMachine _fsm;

        [SerializeField]
        private GameObject[] _lightMat;

        float time = 0; 



# if UNITY_EDITOR 

        // -------------------- DEBUG/TEST --------------------------------------
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

            //print(i);


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

# if UNITY_EDITOR // DEBUG TIME

               // print(time);
# endif

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
# endif

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
# endif
                                Light = LightState.green; 
                                time = 0; 
                            }

                            break;
                        }
                }

                yield return new WaitForSeconds(1);
            }
        }

        /// <summary>
        /// Checks if time is greater than a passed value
        /// </summary>
        /// <returns>
        /// Returns true if time is greater
        /// Returns false if time is not greater
        /// </returns>
        private bool IsGraterThan(int signalTimer)
        {
            if(time > signalTimer) return true; 

            else return false; 
        }


        /// <summary>
        /// 
        /// </summary>
        private void GreenLight()
        {
            SwitchMatLight();

            Debug.Log($"Current light is: {Light}");
        }

        /// <summary>
        /// 
        /// </summary>
        private void YellowLight()
        {
            SwitchMatLight();

            Debug.Log($"Current light is: {Light}");
        }

        /// <summary>
        /// 
        /// </summary>
        private void RedLight()
        {
            SwitchMatLight();

            Debug.Log($"Current light is: {Light}");
        }

        /// <summary>
        /// 0 = Green
        /// 1 = Yellow
        /// 2 = Red
        /// </summary>
        private void SwitchMatLight()
        {   

            switch (Light)
            {
                case LightState.green:
                    {
                        
                        _lightMat[0].SetActive(true);

                        _lightMat[1].SetActive(false);
                        _lightMat[2].SetActive(false);

                        break;
                    }

                case LightState.yellow:
                    {

                        _lightMat[0].SetActive(false);
                        _lightMat[1].SetActive(true);

                        _lightMat[2].SetActive(false);

                        break;
                    }
                    
                case LightState.red:
                    {

                        _lightMat[0].SetActive(false);
                        _lightMat[1].SetActive(false);

                        _lightMat[2].SetActive(true);

                        break;
                    }
                default : { break; }
            }

        }
    }
}

