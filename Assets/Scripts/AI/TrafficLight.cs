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

        [SerializeField]
        private GameObject[] _lightMat;

        [SerializeField] 
        private LightState _startLightState;

        private LightState _light;

        [SerializeField] 
        private GameObject _crossWalkColls;

        [SerializeField] 
        private GameObject _carColls;

        private Action actions;




        /// <summary>
        /// Start is called before the first frame update.
        /// Initiates all available signal states. 
        /// Signal transitions follow this pattern (green -> yellow -> red).
        /// Starts a coroutine updated every (1s default) that checks the state
        /// and updates time.
        /// </summary>
        private void Start()
        {
            StartFSM();
        }


    #region  FSM Initialization
        private void StartFSM()
        {
        
            // -------------------- States -------------------------------------

            State redState = new State("Red",
                RedLight, null,
                () => Debug.Log("left red Light"));


            State greenState = new State("Green",
                GreenLight, null,
                () => Debug.Log("left green Light"));


            // -------------------- Transitions --------------------------------

            redState.AddTransition(new Transition(
                () => _light == LightState.green,
                () => Debug.Log("enter green Light"),
                 greenState));


            greenState.AddTransition(new Transition(
                () => _light == LightState.red,
                () => Debug.Log("enter red Light"),
                redState));


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

            SwitchMatLight();

        }
#endregion


        /// <summary>
        /// Update is called once per frame
        /// Updates actions & invokes the update of the FSM.
        /// </summary>
        private void Update()
        {
            actions = _fsm.Update();
            actions?.Invoke();
        }


#region Light State Control

        /// <summary>
        /// 
        /// </summary>
        public void SwapLightState()
        {

            switch (_light)
            {
                case LightState.green:
                    {
                        _light = LightState.red;
                        break;
                    }

                case LightState.red:
                    {
                        _light = LightState.green;
                        break;
                    }
            }

        }
#endregion


#region  Action Methods
        /// <summary>
        /// 
        /// </summary>
        private void GreenLight()
        {
            SwitchMatLight();

            _carColls.SetActive(false);
            _crossWalkColls.SetActive(true);
        }

        /// <summary>
        /// 
        /// </summary>
        private void RedLight()
        {
            SwitchMatLight();

            _carColls.SetActive(true);
            _crossWalkColls.SetActive(false);
        }

#endregion
    

#region Visual Light change

        /// <summary>
        /// 0 = Green
        /// 1 = Red
        /// </summary>
        private void SwitchMatLight()
        {

            switch (_light)
            {
                case LightState.green:
                    {

                        _lightMat[0].SetActive(true);
                        _lightMat[1].SetActive(false);

                        break;
                    }

                case LightState.red:
                    {
                        _lightMat[0].SetActive(false);
                        _lightMat[1].SetActive(true);

                        break;
                    }

                default: { break; }
            }

        }

#endregion
    }
}

