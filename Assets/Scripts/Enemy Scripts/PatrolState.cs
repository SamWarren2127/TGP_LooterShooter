using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public Vector3? _destination = null;
    private float stopDistance = 5f;
    
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Walls");
    private float _rayDistance = 4f;
    private Quaternion _desiredRotation;
    private Vector3 _direction;

    GameObject[] m_Enodes;
    private EnemyNew _enemyNew;
    private EnodeSetUp m_enodeSetUp;

    public float m_Speed = 10.0f;
    public float turnSpeed = 10f;

    private bool started;

    private void Awake()
    {
        m_enodeSetUp = GameObject.FindGameObjectWithTag("ESetUp").GetComponent<EnodeSetUp>();
        m_Enodes = m_enodeSetUp.Enodes;
        _destination = m_transform.position;
        started = false;
    }


    public PatrolState(EnemyNew enemyNew) : base(enemyNew.gameObject)
    {
        _enemyNew = enemyNew;
        
    }


    public override Type Tick()
    {
        Transform chaseTarget = CheckForAgro();
        if (chaseTarget != null)
        {
            _enemyNew.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }

        if (_destination == null || Vector3.Distance(m_transform.position, _destination.Value) <= stopDistance) //transform.positon
        {
            FindNextDestination();
        }
        m_transform.rotation = Quaternion.Slerp(m_transform.rotation, _desiredRotation, Time.deltaTime * turnSpeed);

        if (IsForwardBlocked())
        {
            m_transform.rotation = Quaternion.Lerp(m_transform.rotation, _desiredRotation, 3.2f);
        }
        else
        {
            m_transform.Translate(Vector3.forward * Time.deltaTime * m_Speed); //GameSettings.EnemySpeed
        }
        Debug.DrawRay(m_transform.position, _direction * _rayDistance, Color.red);
        while (IsPathBlocked())
        {
            FindNextDestination();
            Debug.Log("Wall");
        }



        return null;
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


    // Update is called once per frame
    void Update()
    {
        
    }



    private void FindNextDestination()
    {
        m_enodeSetUp = GameObject.FindGameObjectWithTag("ESetUp").GetComponent<EnodeSetUp>();
        m_Enodes = m_enodeSetUp.Enodes;
        if (_destination == null || !started)
        {
            _destination = m_Enodes[0].transform.position;
            started = true;
        }
        //else
        //{
        for (int i = 0; i < m_Enodes.Length; i++)
            {
                if (Vector3.Distance(m_transform.position, m_Enodes[i].transform.position) < 10f)
                {
                    if (i+1 == m_Enodes.Length)
                    {
                        _destination = m_Enodes[0].transform.position;
                    }
                    else
                    {
                        _destination = m_Enodes[i + 1].transform.position;
                    }
                }
            }
        //}

        _direction = Vector3.Normalize(_destination.Value - m_transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }



    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAgro()
    {
        float aggroRadius = 20f;

        RaycastHit hit;
        Quaternion angle = m_transform.rotation * startingAngle;
        Vector3 direction = angle * Vector3.forward;
        Vector3 pos = m_transform.position;

        for (int i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, aggroRadius))
            {
                //Will need to sort for player
                //Get Player


                PlayerStats m_playerStats = hit.collider.GetComponent<PlayerStats>();

                //EnemyNew enemy = hit.collider.GetComponent<EnemyNew>();
                //if (enemy != null)

                if (m_playerStats != null)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return m_playerStats.transform;
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
}
