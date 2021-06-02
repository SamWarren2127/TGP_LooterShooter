using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquippedItem : MonoBehaviour
{
    public EquippedItem equippedItem;
    
    public void PlayerEquip(ItemData item, Mesh mesh)
    {
        equippedItem.SetEquipped(item, mesh);
    }

    public void PlayerRemove()
    {
        equippedItem.RemoveEquipped();
    }

}
