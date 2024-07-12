using UnityEngine;
using TMPro;
public class PlaceCounter : MonoBehaviour
{

    private int count = 0; 

    [SerializeField] 
    private TMP_Text _text;

    private Camera _mainCam; 


    private void Start()
    {
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
        _text.text = $"Agent(s): {count}";

    }

    /// <summary>
    /// 
    /// </summary>
    public void DecreaseCounter()
    {
        count--; 
        _text.text = $"Agent(s): {count}";
    }


    private void OnTriggerEnter(Collider other)
    {


    }
}
