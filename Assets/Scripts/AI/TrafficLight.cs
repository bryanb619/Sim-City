using System;
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

#region Variables
        private StateMachine _fsm;

        [SerializeField]
        private GameObject[] _lightMat;

        [SerializeField]
        private GameObject triggerBox, crosswalkColliders;

#endregion

        public LightState Light { get; private set; }


        /// <summary>
        /// Start is called before the first frame update.
        /// Initiates all available signal states. 
        /// Signal transitions follow this pattern (green -> yellow -> red).
        /// Starts a coroutine updated every (1s default) that checks the state
        /// and updates time.
        /// </summary>
        private void Start()
        {

#region  FSM Initialization

            // -------------------- States -------------------------------------

            State redState = new State("Red",
                RedLight, null,
                () => Debug.Log("left red Light"));


            State greenState = new State("Green",
                GreenLight, null,
                () => Debug.Log("left green Light"));



            // -------------------- Transitions --------------------------------

            redState.AddTransition(new Transition(
                () => Light == LightState.green,
                () => Debug.Log("enter green Light"),
                 greenState));


            greenState.AddTransition(new Transition(
                () => Light == LightState.red,
                () => Debug.Log("enter red Light"),
                redState));


            _fsm = new StateMachine(redState);

        }
#endregion


        /// <summary>
        /// Update is called once per frame
        /// Updates actions & invokes the update of the FSM.
        /// </summary>
        private void Update()
        {
            Action actions = _fsm.Update();
            actions?.Invoke();
        }


#region Light State Control

        public void SetInitialState(LightState state)
        {
            Light = state;
        }


        /// <summary>
        /// 
        /// </summary>
        public void SwapLightState()
        {

            switch (Light)
            {
                case LightState.green:
                    {
                        Light = LightState.red;
                        break;
                    }

                case LightState.red:
                    {
                        Light = LightState.green;
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

           // triggerBox.gameObject.SetActive(false);
            //crosswalkColliders.gameObject.SetActive(true);

        }

        /// <summary>
        /// 
        /// </summary>
        private void RedLight()
        {
            SwitchMatLight();

            //triggerBox.gameObject.SetActive(true);
            //crosswalkColliders.gameObject.SetActive(false);
        }

#endregion
    

#region Visual Light change

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

