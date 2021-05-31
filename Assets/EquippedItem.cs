using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItem : MonoBehaviour
{
    public Mesh equippedObject;
    public ItemData equippedItem;

    public bool itemEquipped; 

    public void SetEquipped(ItemData item, Mesh mesh)
    {
        itemEquipped = true;
        equippedItem = item;
        equippedObject = mesh;

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        GetComponent<MeshFilter>().mesh = equippedObject;
    }

    public void RemoveEquipped()
    {
        itemEquipped = false;
        equippedItem = null;
        equippedObject = null;

        UpdateVisuals();
    }
}
