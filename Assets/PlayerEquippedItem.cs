using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquippedItem : MonoBehaviour
{
    public GameObject equippedObject;
    public GameObject weaponObject;

    public Quaternion setRotation;

    public ItemData itemData;

    public bool itemEquippedBool;

    public void SetEquipped(ItemData item, GameObject weaponPrefab)
    {

        Debug.Log("Setting equipped weapon");

        RemoveEquipped();

        itemEquippedBool = true;
        itemData = item;
        weaponObject = weaponPrefab;
        UpdateEquipped();
    }

    private void UpdateEquipped()
    {

        Debug.Log("Update Equpped item");


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
