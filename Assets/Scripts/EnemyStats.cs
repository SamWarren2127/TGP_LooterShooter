using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    private bool isDead = false;

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