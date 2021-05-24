﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IHealable<float>
{
    public HUDManager hudManager;

    public float health = 1.0f;
    public float armor = 1.0f;
    public float maxHealth = 1.0f;
    public float maxArmor = 1.0f;
    public bool canDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("damagePlayer", 10, 10);
        hudManager.UpdateHealthBar(health);
        hudManager.UpdateArmorBar(armor);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateHealthBar()
    {
        hudManager.UpdateHealthBar(health);
    }

    void UpdateArmorBar()
    {
        hudManager.UpdateArmorBar(armor);
    }

    void damagePlayer()
    {
        if (health > 0)
        {
            health -= 0.2f;
            UpdateHealthBar();
        }
        else
        {
            print("dead");
        }
    }

    public void Heal(float _healAmount)
    {
        if(_healAmount < 0)
        {
            Debug.Log("Cannot heal a negative number.");
        }
        else if(health + _healAmount > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += _healAmount;
        }

        UpdateHealthBar();
    }
}
