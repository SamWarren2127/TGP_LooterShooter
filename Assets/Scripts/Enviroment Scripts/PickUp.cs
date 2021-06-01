using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp
    : Interactable
{
    public float speed = 1.5f , height = 4f;
    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        PickUpVFX();
        
    }


    void PickUpVFX() 
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 100f, Space.World);


        float objectY;
        Vector3 position = transform.position;
        objectY = Mathf.Sin(Time.time * speed) + height;
        transform.position = new Vector3(position.x, objectY, position.z);
    }
}
