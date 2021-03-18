using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
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

    void damagePlayer(float damage)
    {
        if (armor > 0)
        {
            armor -= damage;
            if (armor < 0)
            {
                health += armor;
            }
        }
        else if (health > 0 )
        {
            health -= damage;
            UpdateHealthBar();
        }
        else
        {
            print("dead");
        }

    }
    void healPlayer(float heal)
    {
        health += heal;
    }
    void armorHeal(float heal)
    {
        armor += heal;
    }
}
