using TMPro;
using UnityEngine;

/// <summary>
/// Class that controls the Agent count in the simulation.
/// </summary>
public class UI : MonoBehaviour
{

    // Reference to the UI GameObject
    [SerializeField]
    private GameObject _simUI;

    [SerializeField] TMP_Text _carCountText;

    [SerializeField] TMP_Text _pedCountText;


    private int _carCount = 0, _pedCount = 0;

    /// <summary>
    /// This start sets the cursor to confined in the game view.
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// Update is called once per frame
    /// calls the method to change the UI state.
    /// </summary>
    private void Update()
    {
        ChangeUIState();
    }

    /// <summary>
    /// Controls the UI state in the simulation.
    /// If the space key is pressed, the UI will be toggled between active 
    /// and inactive.
    /// </summary>
    private void ChangeUIState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (_simUI.activeSelf)
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


    /// <summary>
    /// Updates the car count UI in simulation.
    /// </summary>
    public void UpdateCarCount()
    {
        _carCount++;
        _carCountText.text = $"Car count: {_carCount}";

    }

    /// <summary>
    /// Updates the pedestrian count UI in simulation.
    /// </summary>
    public void UpdatePedCount()
    {
        _pedCount++;
        _pedCountText.text = $"Ped count: {_pedCount}";
    }
}

// -----------------------------------------------------------------------------
