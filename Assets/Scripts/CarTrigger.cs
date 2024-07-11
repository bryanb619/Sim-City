using UnityEngine;

public class CarTrigger: MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public bool HasPed { get; private set;}


    [SerializeField] 
    private bool displayMesh = false;  

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {

            if(displayMesh) _meshRenderer.enabled = true;

            HasPed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pedestrian"))
        {   
            if(displayMesh)  _meshRenderer.enabled = false;
                
            HasPed = false;
        }
    }
    
}
