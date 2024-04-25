using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    
    [SerializeField]
    private float panSpeed = 25f;

    private float panBorderThickness = 20f;

    
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
        //RotateInput();
    }

    /// <summary>
    /// 
    /// </summary>
    private void MovInput()
    {

        Vector3 pos = transform.position;

        // -------------------- Vertical Movement --------------------------- //

        if (Input.GetKey("w") || Input.mousePosition.y >= 
        Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        // -------------------- Horizontal Movement ------------------------- //

        if (Input.GetKey("d") || Input.mousePosition.x 
        >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.mousePosition.x 
        <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        // ---------------- UP and DOWN Movement ---------------------------- //

        if (Input.GetKey("q"))
        {
            pos.y -= 10 * Time.deltaTime;
        }

         if (Input.GetKey("e"))
        {
            pos.y += 10 * Time.deltaTime;
        }

        transform.position = pos;
    }


    /// <summary>
    /// 
    /// </summary>
    private void RotateInput()
    {

    }
}
