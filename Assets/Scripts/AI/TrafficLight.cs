using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LibGameAI.FSMs;
using System.Data.Common;


namespace Assets.Scripts.AI
{
    /// <summary>
    /// A simple traffic light.
    /// Uses libGameAI (FSM) library to implement states & transitions for the
    /// signals.
    /// </summary>
    public class TrafficLight : MonoBehaviour, ITrafficLight
    {

#region Variables

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

        [SerializeField]
        private GameObject triggerBox, crosswalkColliders;

        float time = 0; 

        [SerializeField]
        private LightState initialLightState;

#endregion

        [SerializeField]
        public LightState Light { get; private set; }

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

            //print(i);


            // -------------------- States -------------------------------------

            State redState = new State("Red", RedLight, null, null);
            State greenState = new State("Green", GreenLight, null, null);


            List<State> states = new List<State>
            {
                redState, greenState
            };


            // -------------------- Transitions --------------------------------

            redState.AddTransition(new Transition(
                () => Light == LightState.green,
                null, greenState));
            

           
            if (initialLightState == LightState.red)
            {
                Light = LightState.red;
                _fsm = new StateMachine(redState);
            }
            else if (initialLightState == LightState.green)
            {
                Light = LightState.green;
                _fsm = new StateMachine(greenState);
            }

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
                            if(IsGreaterThan(_greenTime)) 
                            {

# if UNITY_EDITOR 
                                _currentLightState = LightState.green;
# endif

                                Light = LightState.red;
                                time = 0; 
                            }
                            
                            break;
                        }

                    case LightState.red:
                        {
                            if(IsGreaterThan(_redTime)) 
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
        private bool IsGreaterThan(int signalTimer)
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

            triggerBox.gameObject.SetActive(false);
            crosswalkColliders.gameObject.SetActive(true);
            
        }

        /// <summary>
        /// 
        /// </summary>
        private void RedLight()
        {
            SwitchMatLight();

            triggerBox.gameObject.SetActive(true);
            crosswalkColliders.gameObject.SetActive(false);
        }

        /// <summary>
        /// 0 = Green
        /// 1 = Red
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

