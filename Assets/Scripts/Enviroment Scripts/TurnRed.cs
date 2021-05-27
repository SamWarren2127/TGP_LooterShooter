using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRed : Interactable
{
    public Material red;
    public Material grey;
    MeshRenderer meshRenderer;

    public void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
   
    }

    public override void AwakeObject()
    {

        meshRenderer.material = red;
    }

    public override void SleepObject()
    {
        meshRenderer.material = grey;
    }

}