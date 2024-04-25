using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    [SerializeField]
    private float panSpeed = 20f;

    private float _panBorderThickness = 10f;

    

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
        MovInput();
    }

    /// <summary>
    /// 
    /// </summary>
    private void MovInput()
    {

        Vector3 pos = transform.position;


        if(Input.GetKey("w") || Input.mousePosition.y >= 
        Screen.height - _panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        {
            pos.z += panSpeed * Time.deltaTime;
        }


        transform.position = pos;
    }
}
