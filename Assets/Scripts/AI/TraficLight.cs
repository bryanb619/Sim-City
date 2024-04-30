using System.Collections.Generic;
using UnityEngine;
using LibGameAI.FSMs;


public class TraficLight : MonoBehaviour
{
    

    private StateMachine _fsm;

    [SerializeField] private enum lightState
    {
        yellow, red, green
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        System.Random rand = new System.Random(); 

        int i = rand.Next(0, 2);

        State yellowState = new State(  "Yellow", YellowLight,  null, null);
        State redState = new State(     "Red",    RedLight,     null, null);
        State greenState = new State(   "Green",  GreenLight,   null, null);


        List <State> states = new List<State>
        {
            yellowState, redState, greenState
        };

        _fsm = new StateMachine(states[i]);


        // Transitions

        
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        
    }


    private void YellowLight()
    {

    }

    private void RedLight()
    {

    }

    private void GreenLight()
    {

    }
}
