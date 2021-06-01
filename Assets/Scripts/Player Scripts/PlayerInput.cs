using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float mouseX,mouseY;
    public float horizontal;
    public float vertical;
    public bool jump; //true while holding
    public bool interact; //true once per press

    // Update is called once per frame
    void Update()
    {
        // mouse controls
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;



        // button controls
        if (Input.GetButton("Jump")){jump = true;} else { jump = false; }; //jump
        
        if (Input.GetButtonDown("Interact")) { interact = true;} else { interact = false; }; //interact

        horizontal = Input.GetAxis("Horizontal") * Time.deltaTime; //movement
        vertical = Input.GetAxis("Vertical") * Time.deltaTime; 
    }
}
