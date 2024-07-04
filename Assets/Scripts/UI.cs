using System;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.AI
{
    public class UI : MonoBehaviour
    {

#region Variables

        // Reference to the UI GameObject
        [SerializeField] private GameObject      _ui;

        [SerializeField] private GameObject     _aiDirector;

        private AIDirector aIDirector;

        //  UI Sliders
        //Stores value of user set value of vehicles.
        [SerializeField] private Slider         _carSlider;
        [SerializeField] private Slider         _pedSlider;
        [SerializeField] private Slider         _carTimeStoped;
        [SerializeField] private Slider         _pedTimeStopped;
        [SerializeField] private Slider         _maxTimeInAccident;
        [SerializeField] private Slider         _maxTimeInCrazy;

#endregion

#region Properties

        /// <summary>
        /// Read-only property reads the value of _maxCarSlider.
        /// Automatically converts slider.value to int.
        /// </summary>
        /// <value>Returns value of the maximum of cars slider.</value>
        public int MaxCars { 
            get => Convert.ToInt32(_carSlider.value);}

        /// <summary>
        /// Read-only property reads the value of _pedSlider
        /// </summary>
        /// <value>Returns value of the maximum of cars slider.</value>
        public int MaxPeds { 
            get => Convert.ToInt32(_pedSlider.value);}

        /// <summary>
        /// Read-only property reads the value of _carTimeStoped slider
        /// </summary>
        /// <value>Returns value of the maximum of cars slider.</value>
        public int CarStoppedTime { 
            get => Convert.ToInt32(_carTimeStoped.value);}

        /// <summary>
        /// Read-only property reads the value of _pedTimeStopped slider
        /// </summary>
        /// <value>Returns value of the maximum of cars slider.</value>
        public int PedStoppedTime { 
            get => Convert.ToInt32(_pedTimeStopped.value);}

        /// <summary>
        /// Read-only property reads the value of _maxTimeInAccident slider
        /// </summary>
        /// <value>Returns value of the maximum of cars slider.</value>
        public int MaxTimeInAccident { 
            get => Convert.ToInt32(_maxTimeInAccident.value);}

        /// <summary>
        /// Read-only property reads the value of _maxTimeInCrazy slider
        /// </summary>
        /// <value>Returns value of _maxTimeInCrazy slider.</value>
        public int MaxTimeInCrazy { 
            get => Convert.ToInt32(_maxTimeInCrazy.value);}

#endregion

        /// <summary>
        /// Start is called before the first frame update
        /// </summary>
        private void Start()
        {   
            _ui.SetActive(true);

            // get AIDirector component and pass it to aIDirector
            aIDirector = _aiDirector.GetComponent<AIDirector>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartSim()
        {
            _ui.SetActive(false);
            _aiDirector.SetActive(true);
            
            if(!aIDirector.SimStar)
            {
                
            }

        }
    }
}
// -----------------------------------------------------------------------------
