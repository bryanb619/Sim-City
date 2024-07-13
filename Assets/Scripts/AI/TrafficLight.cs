using System;
using UnityEngine;
using LibGameAI.FSMs;


namespace Assets.Scripts.AI
{
    /// <summary>
    /// A simple traffic light.
    /// Uses libGameAI (FSM) library to implement states & transitions for the
    /// light signals.
    /// </summary>
    public class TrafficLight : MonoBehaviour
    {

        private StateMachine _fsm;

        [Header("Light Materials")]
        [SerializeField]
        private GameObject[] _lightMat;

        [Header("Initial Light State")]
        [SerializeField]
        private LightState _startLightState;

        private LightState _light;

        [Header("Crosswalk Colliders")]
        [SerializeField]
        private GameObject _crossWalkColls;

        [Header("Vehicle Colliders")]
        [SerializeField]
        private GameObject _carColls;

        /// <summary>
        /// Start is called before the first frame update.
        /// Calls the FSM Start method.
        /// </summary>
        private void Start()
        {
            StartFSM();
        }

        /// <summary>
        /// Initiates all available signal states. 
        /// Signal transitions follow this pattern (green -> red -> green).
        /// Starts a coroutine updated every (1s default),
        /// </summary>
        private void StartFSM()
        {

            // -------------------- States -------------------------------------

            // represents red state
            State redState = new State(
                "Red",
                RedLight, null, null);


            // represents green state
            State greenState = new State(
                "Green",
                GreenLight, null,
                null);


            // -------------------- Transitions --------------------------------

            redState.AddTransition(new Transition(
                () => _light == LightState.green,
                null,
                 greenState));


            greenState.AddTransition(new Transition(
                () => _light == LightState.red,
                null,
                redState));

            // check the initial state of the traffic light

            if (_startLightState == LightState.green)
            {
                _light = _startLightState;
                _fsm = new StateMachine(greenState);
            }
            else if (_startLightState == LightState.red)
            {
                _light = _startLightState;
                _fsm = new StateMachine(redState);
            }


            else
            {
                Debug.LogError("Invalid initial start state");
            }

            // apply a initial state light material
            SwitchMatLight();

        }

        /// <summary>
        /// Method should be called by IntersectionBrain class.
        /// this method swaps the light state in this traffic light.
        /// that checks the state and updates time.
        /// </summary>
        public void SwapLightState()
        {
            // swap the light state
            switch (_light)
            {
                // if light is green, change to red
                case LightState.green:
                    {
                        _light = LightState.red;
                        break;
                    }

                // if light is red, change to green
                case LightState.red:
                    {
                        _light = LightState.green;
                        break;
                    }
            }

        }

        /// <summary>
        /// Represents the Action of the traffic light when it is green.
        /// </summary>
        private void GreenLight()
        {
            SwitchMatLight();

            _carColls.SetActive(false);
            _crossWalkColls.SetActive(true);
        }

        /// <summary>
        /// Represents the Action of the traffic light when it is red.
        /// </summary>
        private void RedLight()
        {
            SwitchMatLight();

            _carColls.SetActive(true);
            _crossWalkColls.SetActive(false);

        }

        /// <summary>
        /// Method switches the material of the traffic light. this method only 
        /// aids visualy to the simulation having no actual effect on the AI.
        /// Green = 0 element of the array.
        /// Red = 1 element of the array.
        /// </summary>
        private void SwitchMatLight()
        {
            // switch the light material
            switch (_light)
            {
                case LightState.green:
                    {
                        // alternate the materials
                        _lightMat[0].SetActive(true);
                        _lightMat[1].SetActive(false);

                        break;
                    }

                case LightState.red:
                    {
                        // alternate the materials
                        _lightMat[0].SetActive(false);
                        _lightMat[1].SetActive(true);

                        break;
                    }

                default: { break; }
            }
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
    }
}

