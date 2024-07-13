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
        [Header("Max time for each light signal")]
        [Range(5, 10)]
        [SerializeField]
        private int lightMaxTime = 5;

        [Header("List of Traffic lights for the selected intersection\n" +
        "Traffic light 0: Up\nTraffic light 1: Down\nTraffic light 2: Left\n" + 
        "Traffic light: Right")]

        [Tooltip("This is a list this intersection's traffic lights.")]

        [SerializeField]
        private List<TrafficLight> controlPoints = new List<TrafficLight>();


        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Awake()
        {
            CheckLightConfig();
        }

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {
            StartCoroutine(UpdateLightState());
        }


        private void CheckLightConfig()
        {
            if (controlPoints.Count == 0 || controlPoints == null)
            {
                Debug.LogError("No traffic lights found or configured!");
            }
        }


        /// <summary>
        /// Coroutine updates time, checks and changes the single states while 
        /// also resetting the time
        /// </summary>
        /// <returns>
        ///  waits for seconds
        /// </returns>
        private IEnumerator UpdateLightState()
        {
            int time = 0;

            while (true)
            {
                time++;

                if (time >= lightMaxTime)
                {

                    foreach (TrafficLight trafficLight in controlPoints)
                    {

                        if(trafficLight != null)
                        {
                            trafficLight.SwapLightState();
                            
                        }

                    }

                    // reset time
                    time = 0;
                }

#if UNITY_EDITOR // DEBUG TIME

                //print(time);
#endif

                // wait 1 second
                yield return new WaitForSeconds(1);
            }
        }
    }
}