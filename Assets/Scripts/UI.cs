using UnityEngine;


public class UI : MonoBehaviour
{

#region Variables

    // Reference to the UI GameObject
    [SerializeField] private GameObject      _simUI;

    [SerializeField] private GameObject     _aiDirector;

#endregion

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
}

// -----------------------------------------------------------------------------
