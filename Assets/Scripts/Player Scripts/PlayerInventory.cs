using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemData> inventory = new List<ItemData>();

    public void AddItem (ItemData item)
    {
        inventory.Add(item);
    }

    public void RemoveItem(ItemData item)
    {

        inventory.Remove(item);
    }
}
