using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPickUp : Interactable
{
    public PlayerStats playerStats;

    public float armor;
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();

        armor = 0.5f;
    }

    public override void AwakeObject()
    {
        if (ConsumePack())
        {
            Destroy(gameObject);
        }
    }

    private bool ConsumePack()
    {
        if (playerStats.IncreaseArmor(armor))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
