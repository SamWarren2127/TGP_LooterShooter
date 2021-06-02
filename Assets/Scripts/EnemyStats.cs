using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable<float>
{
    public float health;

    private bool isDead = false;

    public void Damage(float _damageAmount)
    {
        health -= _damageAmount;

        if (health <= 0)
        {
            //Drop Items

            //kill the enemy
            Destroy(gameObject);

        }
        return;
        throw new System.NotImplementedException();
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