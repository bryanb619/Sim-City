using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// 
    /// </summary>
    public class IntersectionBrain : MonoBehaviour
    {
        [Header("Time for the light to change state. Default is 10 seconds.\n" +
        "Min: 5 | Max: 25 seconds.")]
        [Range(5, 25)]
        [SerializeField]
        private int lightMaxTime = 10;

        [Header("Random Timer")]

        [Tooltip("If true, the light will have a random time to change states.")]

        [SerializeField] private bool randomTime = false;

        [Header("List of control points for the intersection\n" +
        "Element 0: Up\nElement 1: Down\nElement 2: Left\nElement 3: Right")]

        [Tooltip("This is a list this intersection's traffic lights.")]

        [SerializeField]
        private List<TrafficLight> controlPoints = new List<TrafficLight>();

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Awake()
        {
            SetLightsStates();
        }

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {

            if (randomTime)
            {
                lightMaxTime = GetRandomTime();
            }


            StartCoroutine(UpdateLightState());
        }


        private int GetRandomTime()
        {
            return Random.Range(5, 25);
        }


        private void SetLightsStates()
        {
            if (controlPoints.Count == 0 || controlPoints == null)
            {
                Debug.LogError("No traffic lights found.");
            }


            if (controlPoints[0] != null)
                controlPoints[0].SetInitialState(LightState.green);

            if (controlPoints[1] != null)
                controlPoints[1].SetInitialState(LightState.green);

            if (controlPoints[2] != null)
                controlPoints[2].SetInitialState(LightState.red);

            if (controlPoints[3] != null)
                controlPoints[3].SetInitialState(LightState.red);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerator UpdateLightState()
        {
            int time = 0;

            while (true)
            {
                time++;

                if (time >= lightMaxTime)
                {
                    // swap light state in up & down

                    foreach (TrafficLight trafficLight in controlPoints)
                    {
                        if(trafficLight != null)
                            trafficLight.SwapLightState();
                    }

                    time = 0;
                }

#if UNITY_EDITOR // DEBUG TIME

                print(time);
#endif

                // wait 1 second
                yield return new WaitForSeconds(1);




            }
        }


    }
}