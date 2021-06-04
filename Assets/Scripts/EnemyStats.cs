using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable<float>
{
    public float health;
    HUDManager hudManager;
    EnemyManager m_Emanager;


    Mission mission;

    void Start()
    {
        m_Emanager = FindObjectOfType<EnemyManager>();
        mission = FindObjectOfType<Mission>();
        hudManager = FindObjectOfType<HUDManager>();
    }

    public void Damage(float _damageAmount)
    {
        health -= _damageAmount;

        hudManager.damageGiven += (int)(_damageAmount * 100);

        if (health <= 0)
        {
            //Drop Items

            //kill the enemy
            mission.enemiesKilled++;
            hudManager.kills++;
            m_Emanager.EnemyDied();

            Destroy(gameObject);
        }
        return;
    }


}


