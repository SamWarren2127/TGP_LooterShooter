using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNew : MonoBehaviour
{

    public RaycastHit rayHit;
    public LayerMask layerMask;
    public float range = 10.0f;
    public float damage = 0.08f;

    public Transform m_target { get; private set; }


    public StateMachine StateMachine => GetComponent<StateMachine>();


    private void Awake()
    {
        Debug.Log("EnemyNewAwake");
        InitaliseStateMachine();
    }
    
    private void InitaliseStateMachine()
    {
        Debug.Log("Making Dictionary");
        Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>()
        {//can use Enums instead of typeof
            {typeof(WanderState), new WanderState(this) },
            {typeof(ChaseState), new ChaseState(this) },
            {typeof(AttackState), new AttackState(this) },
            {typeof(PatrolState), new PatrolState(this) }
        };



        GetComponent<StateMachine>().SetStates(states);
    }


    public void SetTarget(Transform target)
    {
        m_target = target;
    }



    public void FireWeapon()
    {
        //Debug.Log("Ray Activated");
        Vector3 direction = Vector3.Normalize(m_target.position - transform.position);
        //Debug.DrawRay(transform.position, direction * 25f, Color.red);

        if (Physics.Raycast(transform.position, direction, out rayHit, range, layerMask))
        {
            //Debug.Log("Ray Activated");
            //Debug.DrawRay(transform.position, direction * 25f, Color.red);
            Debug.Log(rayHit.collider.name);
            //Debug.DrawRay(transform.position, direction * 25f, Color.red);

            if (rayHit.collider.CompareTag("Player"))
            {
                Debug.Log("Ray Activated");
                Debug.DrawRay(transform.position, direction * 25f, Color.red);
                PlayerStats health = rayHit.collider.GetComponent<PlayerStats>();
                IDamageable<float> eInterface = health.gameObject.GetComponent<IDamageable<float>>();

                if (eInterface != null)
                {
                    FindObjectOfType<AudioManager>().Play("gunshot");
                    eInterface.Damage(damage);
                }
                Debug.Log("Hit Player");
            }

            
        }

    }


}
