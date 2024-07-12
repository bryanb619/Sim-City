using UnityEngine;
using TMPro;
public class PlaceCounter : MonoBehaviour
{

    private int count = 0; 

    [SerializeField] 
    private TMP_Text _text;


    private void OnTriggerEnter(Collider other)
    {

        
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
}
