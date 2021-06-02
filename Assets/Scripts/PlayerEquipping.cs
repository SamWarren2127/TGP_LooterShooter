﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipping : MonoBehaviour
{
    public EquippedItem equippedItem;
    
    public void PlayerEquip(ItemData item, GameObject weaponObject )
    {
        equippedItem.SetEquipped(item, weaponObject);
    }

    public void PlayerRemove()
    {
        equippedItem.RemoveEquipped();
    }

}