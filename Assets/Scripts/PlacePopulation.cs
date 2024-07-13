using UnityEngine;
using TMPro;
using NaughtyAttributes;

/// <summary>
/// Class responsible for counting the agents in each location.
/// </summary>
public class PlacePopulation : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private int count = 0; 

    private TMP_Text _text;

    private Camera _mainCam; 

    /// <summary>
    /// start is called before the first frame update
    /// Gets the main camera and text in child.
    /// </summary>
    private void Start()
    {
        _text = transform.GetChild(0).
                transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
                
        _mainCam = Camera.main;
    }

    /// <summary>
    /// update is called once per frame
    /// calls the method to update the text position.
    /// </summary>
    private void Update()
    {
        UpdateTextPos();
    }


    private void UpdateTextPos()
    {
        transform.LookAt(_mainCam.transform);
    }


    /// <summary>
    /// Increase UI counter 
    /// </summary>
    private void IncreaseCounter()
    {

        count++;
        _text.text = $"{count}";

    }

    /// <summary>
    /// Decrease UI counter
    /// </summary>
    private void DecreaseCounter()
    {
        count--; 
        _text.text = $"{count}";
    }

    /// <summary>
    /// Detect if a pedestrian or vehicle is inside the trigger
    /// </summary>
    /// <param name="other">Collider to check for on Stay</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pedestrian") || other.CompareTag("Vehicle"))
        {
            IncreaseCounter();
        }
    }

    /// <summary>
    /// Detect if a pedestrian or vehicle has exited the trigger
    /// </summary>
    /// <param name="other">Collider to check for exit</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pedestrian") || other.CompareTag("Vehicle"))
        {
            DecreaseCounter();
        }
    }
}
