using TMPro;
using UnityEngine;


public class UI : MonoBehaviour
{



    // Reference to the UI GameObject
    [SerializeField] private GameObject      _simUI;

    [SerializeField] TMP_Text _carCountText;

    [SerializeField] TMP_Text _pedCountText;


    private int _carCount = 0 , _pedCount = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        ChangeUIState();
    }


    private void ChangeUIState()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
                switch(_simUI.activeSelf)
            {
                case true:
                    _simUI.SetActive(false);
                    break;
                case false:
                    _simUI.SetActive(true);
                    break;
            }
            
        }
    }

    public void UpdateCarCount()
    {
        _carCount++;
        _carCountText.text = $"Car count: {_carCount}";

    }

    public void UpdatePedCount()
    {
        _pedCount++;
        _pedCountText.text = $"Ped count: {_pedCount}";
    }
}

// -----------------------------------------------------------------------------
