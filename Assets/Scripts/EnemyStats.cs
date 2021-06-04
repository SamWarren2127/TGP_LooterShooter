using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable<float>
{
    public float health;
    HUDManager hudManager;

    private bool isDead = false;

    Mission mission;

    void Start()
    {
        mission = FindObjectOfType<Mission>();
        hudManager = FindObjectOfType<HUDManager>();
    }

    public void Damage(float _damageAmount)
    {
        health -= _damageAmount;

        hudManager.damageGiven += (int)_damageAmount * 100;

        if (health <= 0)
        {
            //Drop Items

            //kill the enemy
            mission.enemiesKilled++;
            hudManager.kills++;
            Destroy(gameObject);
        }
        return;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0 && !isDead)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
        isDead = true;
    }
}


/*public class EnemyStats : MonoBehaviour
{
    private float HP;

    private void Start()
    {
        HP = 1;
    }


    public void Damage(float x)
    {
        HP -= x;

        if(HP < 0)
        {

        }

    }




}*/