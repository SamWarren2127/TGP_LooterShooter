using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNew : MonoBehaviour
{
    [SerializeField] private Side _side;
    [SerializeField] private GameObject _laserVisual;


    public Transform m_target { get; private set; }

    public Side Side => _side;

    public StateMachine StateMachine => GetComponent<StateMachine>();


    private void Awake()
    {
        InitaliseStateMachine();
    }
    
    private void InitaliseStateMachine()
    {
        Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>()
        {//can use Enums instead of typeof
            {typeof(WanderState), new WanderState(this) },
            {typeof(ChaseState), new ChaseState(this) },
            {typeof(AttackState), new AttackState(this) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }


    public void SetTarget(Transform target)
    {
        m_target = target;
    }



    public void FireWeapon()
    {
        _laserVisual.transform.position = (m_target.position + transform.position) / 2f;
        float distance = Vector3.Distance(m_target.position, transform.position);
        _laserVisual.transform.localScale = new Vector3(0.1f, 0.1f, distance);
        _laserVisual.SetActive(true);

        StartCoroutine(TurnOffLaser());
    }

    private IEnumerator TurnOffLaser()
    {
        yield return new WaitForSeconds(0.25f);
        _laserVisual.SetActive(false);

        if(m_target != null)
        {
            GameObject.Destroy(m_target.gameObject);
        }
    }

}


public enum Side
{
    Enemy,
    Neutral,
    Player,
}