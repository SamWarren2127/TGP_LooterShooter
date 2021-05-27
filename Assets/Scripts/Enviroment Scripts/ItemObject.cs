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
        Debug.Log("Picking Up Item: " + item.ItemName);

        FindObjectOfType<PlayerInventory>().AddItem(item);

        Destroy(gameObject);
    }   
}
