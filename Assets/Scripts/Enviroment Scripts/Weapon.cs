using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapon")]
public class Weapon : ItemData
{

    public int ItemQuality = 0;
    public GameObject player;
    public GameObject weaponObject;

    public override void Use()
    {
        Debug.Log("Use called");

        player = GameObject.FindGameObjectWithTag("Player");

        base.Use();

        Debug.Log(weaponObject.name);



        if (weaponObject != null)
        {
            player.GetComponent<PlayerEquipping>().PlayerEquip(this, weaponObject);
        }
        Debug.Log("Equipped Weapon");
    }
}

