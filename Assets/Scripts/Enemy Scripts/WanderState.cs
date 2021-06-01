using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : BaseState
{
    public Vector3? _destination; //Question mark means its nullable
    private float stopDistance = 1f;
    private float turnSpeed = 1f;
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Walls");
    private float _rayDistance = 4f;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private EnemyNew _enemyNew;


    private float m_Speed = 4.0f;

    public WanderState(EnemyNew enemyNew) : base(enemyNew.gameObject)
    {
        _enemyNew = enemyNew;
    }


    public override Type Tick()
    {
        Transform chaseTarget = CheckForAgro();
        if(chaseTarget != null)
        {
            _enemyNew.SetTarget(chaseTarget);
            //return typeof(ChaseState)
        }

        if(_destination.HasValue == false || Vector3.Distance(m_transform.position, _destination.Value) <= stopDistance) //transform.positon
        {
            FindRandomDestination();
        }
        m_transform.rotation = Quaternion.Slerp(m_transform.rotation, _desiredRotation, Time.deltaTime * turnSpeed);

        if(IsForwardBlocked())
        {
            m_transform.rotation = Quaternion.Lerp(m_transform.rotation, _desiredRotation, 0.2f);
        }
        else
        {
            m_transform.Translate(Vector3.forward * Time.deltaTime * m_Speed); //GameSettings.EnemySpeed
        }
        Debug.DrawRay(m_transform.position, _direction * _rayDistance, Color.red);
        while(IsPathBlocked())
        {
            FindRandomDestination();
            Debug.Log("Wall");
        }

        return null;


        //return typeof(WanderState);
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(m_transform.position, m_transform.forward);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(m_transform.position, _direction);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
    }

    private void FindRandomDestination()
    {
        Vector3 testPosition = (m_transform.position + m_transform.forward * 4f) + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, UnityEngine.Random.Range(-4.5f, 4.5f));
        _destination = new Vector3(testPosition.x, 1f, testPosition.z);

        _direction = Vector3.Normalize(_destination.Value - m_transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
        Debug.Log("Got Direction");
    }

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAgro()
    {
        float aggroRadius = 5f;

        RaycastHit hit;
        Quaternion angle = m_transform.rotation * startingAngle;
        Vector3 direction = angle * Vector3.forward;
        Vector3 pos = m_transform.position;

        for (int i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, aggroRadius))
            {
                //Will need to sort for player
                EnemyNew enemy = hit.collider.GetComponent<EnemyNew>();
                if (enemy != null)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return enemy.transform;
                }
                else
                {
                    //Debugging purposes
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * aggroRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
