using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class IntersectionBrain : MonoBehaviour
{   

    [Header("List of control points for the intersection\n" +
    "Element 0: Up\nElement 1: Down\nElement 2: Left\nElement 3: Right")]
    [SerializeField]
    private List<GameObject> controlPoints = new List<GameObject>();
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        
    }
}
