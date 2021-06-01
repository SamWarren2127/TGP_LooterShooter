using System;
using UnityEngine;

public class ItemObject : Interactable
{
    public ItemData item;
    public override void InteractObject()
    {
        base.InteractObject();

        AddToInventory();
    }

    private void AddToInventory()
    {
        bool itemAdded = FindObjectOfType<PlayerInventory>().AddItem(item); //return true if enough inventory space

        if (itemAdded)
        {
            Destroy(gameObject);
        }
    }   
}
