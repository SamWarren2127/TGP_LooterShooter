using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private EnemyNew _enemyNew;
    private float m_Speed = 8.0f;
    private float turnSpeed = 10f;

    public ChaseState(EnemyNew enemyNew) : base(enemyNew.gameObject) //might need to be m_gameObject
    {
        _enemyNew = enemyNew;
    }

    public override Type Tick()
    {
        if(_enemyNew.m_target == null)
        {
            return typeof(WanderState);
        }

        m_transform.LookAt(_enemyNew.m_target);
        m_transform.Translate(Vector3.forward * Time.deltaTime * m_Speed); 

        float distance = Vector3.Distance(m_transform.position, _enemyNew.m_target.transform.position);
        if (distance <= 25f) 
        {
            return typeof(AttackState);
        }
        return null;
    }
}
