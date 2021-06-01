using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItem : MonoBehaviour
{
    public GameObject equippedObject;
    public GameObject weaponObject;

    public Quaternion setRotation;

    public ItemData itemData;

    public bool itemEquippedBool; 

    public void SetEquipped(ItemData item, GameObject weaponPrefab)
    {
        RemoveEquipped();

        itemEquippedBool = true;
        itemData = item;
        weaponObject = weaponPrefab;

        UpdateEquipped();
    }

    private void UpdateEquipped()
    {
       
        equippedObject = Instantiate(weaponObject, this.transform.position, Quaternion.identity) as GameObject;

        equippedObject.transform.parent = this.transform;

        equippedObject.transform.rotation = setRotation;

    }



    public void RemoveEquipped()
    {
        Destroy(equippedObject);
               
        itemEquippedBool = false;
        itemData = null;
        equippedObject = null;
        weaponObject = null;

    }
}
