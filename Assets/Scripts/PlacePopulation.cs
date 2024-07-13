using UnityEngine;
using TMPro;
using NaughtyAttributes;
public class PlacePopulation : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private int count = 0; 

    private TMP_Text _text;

    private Camera _mainCam; 


    private void Start()
    {
        _text = transform.GetChild(0).
                transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
                
        _mainCam = Camera.main;
    }


    private void Update()
    {
        transform.LookAt(_mainCam.transform);
    }


    /// <summary>
    /// 
    /// </summary>
    public void IncreaseCounter()
    {

        count++;
        _text.text = $"{count}";

    }

    /// <summary>
    /// 
    /// </summary>
    public void DecreaseCounter()
    {
        count--; 
        _text.text = $"{count}";
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pedestrian") || other.CompareTag("Vehicle"))
        {
            IncreaseCounter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pedestrian") || other.CompareTag("Vehicle"))
        {
            DecreaseCounter();
        }
    }
}
