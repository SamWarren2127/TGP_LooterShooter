using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class InventorySlot : MonoBehaviour
{
    public ItemData item;
    public Sprite itemImage = null;
    public Image slotImage;
    public Button removeButton;
    public Sprite emptyImage;

    PlayerInventory inventory;

    public EquippedItem equippedItemScript;

    public void Start()
    {

        emptyImage = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Icons/empty slot icon.png", typeof(Sprite));

        item = null;
        inventory = FindObjectOfType<PlayerInventory>();

        equippedItemScript = FindObjectOfType<EquippedItem>();

    }

    public void AddItem(ItemData newItem)
    {
        item = newItem;
        if (newItem.ItemIcon != null) 
        {
            itemImage = newItem.ItemIcon;
            slotImage.sprite = itemImage;
        }
    }

    public void RemoveItem()
    {
            item = null;
            slotImage.sprite = emptyImage;
    }

    public void RemoveButtonPress()
    {
 
        if (item != null)
        {
            inventory.RemoveItem(item);

            if (equippedItemScript.itemData.ItemID == item.ItemID)
            {
               equippedItemScript.RemoveEquipped();
            }
        }     
    }

    public void UseButtonPress()
    {
       
        if (item != null)
        {
            item.Use();
        }
    }
}
