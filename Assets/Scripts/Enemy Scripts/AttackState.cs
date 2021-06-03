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

        //Debug.Log("Ray Activated");
        //Vector3 direction = Vector3.Normalize(_enemyNew.m_target.position - m_transform.position);
        //Debug.DrawRay(m_transform.position, direction * 25f, Color.red);
        if (_attackReadyTimer <= 0f)
        {
            Debug.Log("Attack!");
            _enemyNew.FireWeapon();
            _attackReadyTimer = 2f;
        }


        //Going out of range attack and changing to chase state
        if (Vector3.Distance(_enemyNew.m_target.transform.position, m_transform.position) > 10f)
        {
            return typeof(ChaseState);
        }

        
        return null;
    }
}
