using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public PlayerInput input;
    public Camera playerCamera;
    public Interactable playerFocus;
    public Transform playerTransform;

    public float awakeRadius = 5f;

    public Vector3 playerScreenCenter;

    private void Start() 
    {
        //find screen center
        playerScreenCenter.x = 0.5f * Screen.width;
        playerScreenCenter.y = 0.5f * Screen.height;
        playerScreenCenter.z = playerCamera.nearClipPlane;
    }

    private void OnDrawGizmosSelected() //make selectabilty radius visible
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, awakeRadius);
    }

    void OnValidate() //locks sliders in inspector
    {
        if (awakeRadius > 5f)
        {
            awakeRadius = 5f; // prevent sleep radius from being too large
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = playerTransform.position;

        //check to see if any objects are in range for objects that need to be awoken automatically
        Collider[] colliders = Physics.OverlapSphere(playerPosition, awakeRadius);
        {
            foreach (var collider in colliders)
            {
                Interactable interactable = collider.GetComponent<Interactable>();//if obj has interactable component


               
                if (interactable != null && !interactable.isAwake) //if object has interactable componenet and is not awake
                {
                    
                    // awaken those objects);
                    interactable.OnAwake(playerTransform);
                }
            }
        }

        //for objects that need to be interacted with manually
        if (input.interact) // if player presses interact key, bound to e by default
        {
            Ray ray = playerCamera.ScreenPointToRay(playerScreenCenter); //project ray from camera           
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) // ray hits
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                
                if (interactable != null) //if obj has interactable component
                {SetFocus(interactable);} //set object as current focus
                
                else
                {RemoveFocus();}
            }
            else
            {
                RemoveFocus();
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != playerFocus) 
        {
            if ( playerFocus != null) { playerFocus.Defocused(); }
            playerFocus = newFocus;
        }
        newFocus.Focused(playerTransform.transform);      
    }

    void RemoveFocus()
    {
        if (playerFocus != null) { playerFocus.Defocused(); }
        playerFocus = null;
    }
}
