using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemData> inventoryItems = new List<ItemData>();

    public int inventorySpace = 19;

    public bool AddItem (ItemData item)
    {
        if (inventoryItems.Count <= inventorySpace)
        {
            inventoryItems.Add(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItem(ItemData item)
    {

        inventoryItems.Remove(item);
    }
}
