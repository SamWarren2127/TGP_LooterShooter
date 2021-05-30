using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
 
    public float sleepRadius = 20f;
    public bool isFocused = false;
    public bool isAwake = false;
    Transform playerTransform;


    private void OnDrawGizmosSelected() //make selectabilty radius visible
    {  
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sleepRadius);
    }

    void OnValidate() //locks sliders in inspector
    {        
        if (sleepRadius < 10) 
        {
            sleepRadius = 10f; // prevent sleep radius from being too small
        }
    }

    public virtual void Update()
    {
        if (isAwake)
        {
            float playerDistance = Vector3.Distance(playerTransform.position, transform.position);
            {                
                if (playerDistance > sleepRadius)
                {                   
                    OnSleep();  
                }
            }
        }
    }

    public void OnAwake(Transform transform)
    {
        playerTransform = transform;
        isAwake = true;        
        AwakeObject();
    }

    public void OnSleep()
    {
        isAwake = false;
        SleepObject();
    }

    public void Focused(Transform transform)
    {
        playerTransform = transform;
        isFocused = true;
        InteractObject();
    }

    public virtual void InteractObject()
    {
        //interact functionality
    }

    public virtual void AwakeObject()
    {
        //awake functionality
    }

    public virtual void SleepObject()
    {
        // sleep functionality
    }

    public void Defocused()
    {
        isFocused = false;
        playerTransform = null;
    }
}
