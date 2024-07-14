using UnityEngine;


/// <summary>
/// Class responsible for detecting if a pedestrian is inside the trigger
/// and store this information in a boolean variable that can be accessed by other classes 
/// such as a car
/// </summary>
public class CarTrigger: MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    /// <summary>
    /// Bool value that stores if a pedestrian is inside the trigger.
    /// </summary>
    /// <value>
    ///  True: has a pedestrian inside the trigger
    /// False: no pedestrian inside the trigger
    /// </value>
    public bool HasPed { get; private set;}


    [SerializeField] 
    private bool displayMesh = false;  

    /// <summary>
    /// Start is called before the first frame update
    /// Sets has ped to false and disables the mesh renderer
    /// </summary>
    private void Start()
    {
        HasPed = false;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;

    }

    /// <summary>
    /// Detects if a pedestrian is inside the trigger
    /// if so, it sets hasPed to true
    /// </summary>
    /// <param name="other">collideer to be received</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {
            if(displayMesh) _meshRenderer.enabled = true;

            HasPed = true;
        }
    }
    
    /// <summary>
    /// Detects if a pedestrian has left the trigger
    /// if so, it sets hasPed to false
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {   
            if(displayMesh)  _meshRenderer.enabled = false;
                
            HasPed = false;
        }
    }
    
}
