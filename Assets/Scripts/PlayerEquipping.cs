using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipping : MonoBehaviour
{
    public EquippedItem equippedItem;

    public void Start()
    {

        equippedItem = FindObjectOfType<EquippedItem>();

        Debug.Log("equipped item component gained:" + equippedItem.name);

    }

    public void PlayerEquip(ItemData item, GameObject weaponObject)
    {

        Debug.Log("Player Equip called");

        equippedItem.SetEquipped(item, weaponObject);
    }

    public void PlayerRemove()
    {
        equippedItem.RemoveEquipped();
    }

}
