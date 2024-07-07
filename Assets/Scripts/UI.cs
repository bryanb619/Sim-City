using System;
using Assets.Scripts.AI;
using UnityEngine;


public class UI : MonoBehaviour
{

#region Variables

    // Reference to the UI GameObject
    [SerializeField] private GameObject      _startUI;

    [SerializeField] private GameObject      _simUI;

    [SerializeField] private GameObject     _aiDirector;

    private AIDirector aIDirector;


    [Range(0, 50)]
    [SerializeField] private int        _carSlider = 25;

    [Range(0, 75)]
    [SerializeField] private int         _pedSlider = 30;

    [Range(0, 75)]
    [SerializeField] private int        _carTimeStoped = 10;

    [Range(0, 75)]
    [SerializeField] private int         _pedTimeStopped = 10;

    [Range(0, 75)]
    [SerializeField] private int         _maxTimeInAccident = 15;

    [Range(0, 75)]
    [SerializeField] private int         _maxTimeInCrazy = 30;


#endregion

#region Properties

    /// <summary>
    /// Read-only property reads the value of _maxCarSlider.
    /// Automatically converts slider.value to int.
    /// </summary>
    /// <value>Returns value of the maximum of cars slider.</value>
    public int MaxCars { 
        get => _carSlider;}

    /// <summary>
    /// Read-only property reads the value of _pedSlider
    /// </summary>
    /// <value>Returns value of the maximum of cars slider.</value>
    public int MaxPeds { 
        get => _pedSlider;}

    /// <summary>
    /// Read-only property reads the value of _carTimeStoped slider
    /// </summary>
    /// <value>Returns value of the maximum of peds slider.</value>
    public int CarStoppedTime { 
        get => _carTimeStoped;}

    /// <summary>
    /// Read-only property reads the value of _pedTimeStopped slider
    /// </summary>
    /// <value>Returns value of the maximum of cars slider.</value>
    public int PedStoppedTime { 
        get => _pedTimeStopped;}

    /// <summary>
    /// Read-only property reads the value of _maxTimeInAccident slider
    /// </summary>
    /// <value>Returns value of the maximum of cars slider.</value>
    public int MaxTimeInAccident { 
        get => _maxTimeInAccident;}

    /// <summary>
    /// Read-only property reads the value of _maxTimeInCrazy slider
    /// </summary>
    /// <value>Returns value of _maxTimeInCrazy slider.</value>
    public int MaxTimeInCrazy { 
        get => _maxTimeInCrazy;}

#endregion

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Awake()
    {   
        //_startUI.SetActive(true);
        //_simUI.SetActive(false);
        _aiDirector.SetActive(false);

        // get AIDirector component and pass it to aIDirector
        aIDirector = _aiDirector.GetComponent<AIDirector>();
    }

        /// <summary>
    /// 
    /// </summary>
    public void StartSim()
    {
        if(!aIDirector.SimStar)
        {
            _aiDirector.SetActive(true);
            _simUI.SetActive(true);
        }
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
}

// -----------------------------------------------------------------------------
