using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : Interactable
{
    public int ammo;
    public WeaponSystem weaponSystem;



    // Start is called before the first frame update
    void Start()
    {
        weaponSystem = FindObjectOfType<WeaponSystem>();
        ammo = 30;
    }

    public override void AwakeObject()
    {
        weaponSystem.GainAmmo(ammo);
        Destroy(gameObject);
    }
      
}
