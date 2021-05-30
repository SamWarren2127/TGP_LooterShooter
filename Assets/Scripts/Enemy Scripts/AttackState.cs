using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float _attackReadyTimer;
    private EnemyNew _enemyNew;

    //Potentiall add _enemy into the base state if adding more states

    public AttackState(EnemyNew enemyNew) : base(enemyNew.gameObject)
    {
        _enemyNew = enemyNew;
    }

    public override Type Tick()
    {
        if(_enemyNew.m_target == null)
        {
            return typeof(WanderState);
        }
        
        _attackReadyTimer -= Time.deltaTime;

        if(_attackReadyTimer <= 0f)
        {
            Debug.Log("Attack!");
            _enemyNew.FireWeapon();
        }
        //Implement going out of range attack and moving to chase state
        return null;
    }
}
