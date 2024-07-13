using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    /// <summary>
    /// Represents a class responsable for handling Traffic light intersections
    /// in the simulation.
    /// Intersection Brain is also reponsible for setting the timer for the swap 
    /// of the traffic lights state.
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

        /// <summary>
        /// Represents a list of traffic lights for the current intersection.
        /// </summary>
        /// <typeparam name="TrafficLight">Traffic light component</typeparam>
        /// <returns>returns a list of Traffic Light components</returns>
        private List<TrafficLight> controlPoints = new List<TrafficLight>();

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Calls the method to check if the traffic lights are properly configured.
        /// </summary>
        private void Awake()
        {
            CheckLightConfig();
        }
        
        /// <summary>
        /// Method checks if the traffic lights are properly configured
        /// if not configured, it will throw an error to the unity console.
        /// </summary>
        private void CheckLightConfig()
        {
            if (controlPoints.Count == 0 || controlPoints == null)
            {
                Debug.LogError("No traffic lights found or configured!");
            }
        }

        /// <summary>
        /// Start is called before the first frame update
        /// Calls the coroutine to update the light state
        /// </summary>
        private void Start()
        {
            StartCoroutine(UpdateLightState());
        }

        /// <summary>
        /// Coroutine updates time, checks and changes the single states while 
        /// also resetting the time after the max time is reached.
        /// </summary>
        /// <returns> waits for seconds.</returns>
        private IEnumerator UpdateLightState()
        {
            int time = 0;

            while (true)
            {
                time++;

                // check if time is greater than max time
                if (time >= lightMaxTime)
                {
                    
                    // swap the light state for each traffic light in the list
                    foreach (TrafficLight trafficLight in controlPoints)
                    {
                        // check if traffic light is not null
                        if(trafficLight != null)
                        {   
                            // swap the light state
                            trafficLight.SwapLightState();
                        }

                    }

                    // reset time
                    time = 0;
                }

                // DEBUG TIME
                //print(time);

                // wait 1 second
                yield return new WaitForSeconds(1);
            }
        }
    }
}