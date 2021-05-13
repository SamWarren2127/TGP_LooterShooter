using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    

    [SerializeField] private LayerMask _layerMask;


    private Enemy _target; //This will be the player when player is in range. Change the GameObject to player
    private EnemyState _currentState;
    private Vector3 _destination, _direction;
   
    private float m_moveSpeed = 10f;
    private Quaternion _desiredRotation;
    private float _attackRange = 10f;
    private float _rayDistance = 5f;

    private float _stoppingDist = 2f;
    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case EnemyState.Wander:
                {
                    if(NeedADestination()) //in this check also check if path is blocked.
                    {
                        GetDestination();
                    }
                    transform.rotation = _desiredRotation;

                    transform.Translate(Vector3.forward * Time.deltaTime * m_moveSpeed);

                    var rayColor = IsPathBlocked() ? Color.red : Color.green;
                    Debug.DrawRay(transform.position, _direction * _rayDistance, rayColor);


                    while (IsPathBlocked())
                    {
                        Debug.Log("Path Blocked");
                        GetDestination();
                    }

                    var targetToAggro = CheckForAggression();
                    if (targetToAggro != null)
                    {
                        _target = targetToAggro.GetComponent<Enemy>();
                        _currentState = EnemyState.Chase;
                    }
                    break;
                }
            case EnemyState.Chase:
                {
                    if(_target == null)
                    {
                        _currentState = EnemyState.Wander;
                        return;
                    }

                    transform.LookAt(_target.transform);
                    transform.Translate(Vector3.forward * Time.deltaTime * m_moveSpeed);

                    if(Vector3.Distance(transform.position, _target.transform.position) <_attackRange) //Check for line of sight
                    {
                        _currentState = EnemyState.Attack;
                    }
                    break;
                }

            case EnemyState.Attack:
                {
                    if(_target != null)
                    {
                        //Check Line of Sight, Shoot at player. Do damage to player.
                    }

                    if(_target == null)
                    {
                        _currentState = EnemyState.Wander;
                    }
                    if(_target != null && Vector3.Distance(transform.position, _target.transform.position) > _attackRange)
                    {
                        _currentState = EnemyState.Chase;
                    }
                    break;
                }
                
            case EnemyState.Patrol:
                {
                    //Get Nodes and travel between nodes till player spotted
                }
                break;
        }


    }

    private bool IsPathBlocked()
    {
        //RaycastHit hit;
        Debug.Log("IsPathBlocked");
        Ray ray = new Ray(transform.position, _direction);
        var hitSomething = Physics.RaycastAll(ray, _rayDistance, _layerMask);
        return hitSomething.Any();
        ////var hitObject = Physics.Raycast(ray, out hit, _rayDistance);
        //if(Physics.Raycast(ray, out hit, _rayDistance))
        //{
        //    Debug.Log("Hit");
        //    //return true;
        //}
        //return false; 
    }

    private bool NeedADestination()
    {
        if(_destination == Vector3.zero)
        {
            return true;
        }

        var distance = Vector3.Distance(transform.position, _destination);
        if(distance <= _stoppingDist)
        {
            return true;
        }

        return false;
    }


    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAggression()
    {
        float aggroRadius = 5f;

        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;

        for(int i = 0; i < 24; i++)
        {
            if(Physics.Raycast(pos, direction, out hit, aggroRadius))
            {
                //Will need to sort for player
                var enemy = hit.collider.GetComponent<Enemy>();
                if(enemy != null)
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


    //Wander GetNewDest
    //public bool NeedDestination()
    //{
    //    if(_destination == Vector3.zero)
    //    {
    //        return true;
    //    }

    //}

    private void GetDestination()
    {
        //Wander New Destination
        //UnityEngine.Random.Range or Random.Range
        Vector3 testPos = (transform.position + (transform.forward * 4f)) + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));

        _destination = new Vector3(testPos.x, 1f, testPos.z);

        _direction = Vector3.Normalize(_destination - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);

        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    public enum EnemyState
    {
        Wander,
        Chase,
        Attack,
        Patrol
    }
}
