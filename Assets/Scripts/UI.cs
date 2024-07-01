using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.AI
{
    public class UI : MonoBehaviour
    {
        // Reference to the UI GameObject

        [Tooltip("Ref to UI. Can be null. If Null it will automatically " 
        +"find the first Image component in the children.")]
        [SerializeField] private GameObject      _ui;

        // Key to toggle the UI on/off
        [Tooltip("Key to toggle the UI on/off")]
        [SerializeField] private KeyCode         _key;


        [SerializeField] private Slider         _maxCarSlider;
        [SerializeField] private Slider         _maxPedSlider;
        [SerializeField] private Slider         _maxCarTimeStoped;
        [SerializeField] private Slider         _maxPedTimeStopped;
        [SerializeField] private Slider         _maxTimeInAccident;
        [SerializeField] private Slider         _maxTimeInCrazy;



        /// <summary>
        /// Start is called before the first frame update.
        /// If _ui is null, Start will automatically find the first Image component in the children.
        /// </summary>
        private void Start()
        {
            _ui =  GetComponentInChildren<Image>().gameObject;
        }


        /// <summary>
        /// Update is called once per frame
        /// Detects if the key is pressed and toggles the UI on/off.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(_key))
            {
                DetectClick();
            }
        }


        /// <summary>
        /// Public method can be used as a button or key press to toggle the UI on/off.
        /// </summary>
        public void DetectClick()
        {
            if (_ui.activeSelf)
            {
                _ui.SetActive(false);
            }
            else
            {
                _ui.SetActive(true);
            }
        }
    }
}
// -----------------------------------------------------------------------------
