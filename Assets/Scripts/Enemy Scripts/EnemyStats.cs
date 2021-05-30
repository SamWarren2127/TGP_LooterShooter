using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
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




}
