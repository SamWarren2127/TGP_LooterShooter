using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;
    [SerializeField]
    float speed;
    [SerializeField]
    float jumpForce;
    Vector3 moveDirection;
    [HideInInspector]
    bool isGrounded = true;

    float oldPos;

    public float stepRate = 0.5f;

    public float stepCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.Log("_rigidbody is a null reference");
        }
        Cursor.lockState = CursorLockMode.Locked;

        oldPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        moveDirection.z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(moveDirection);

        stepCoolDown -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCoolDown < 0f)
        {
            FindObjectOfType<AudioManager>().Play("Footstep");
            stepCoolDown = stepRate;
        }


            /*if(_rigidbody.velocity.magnitude > 0)
            {
                //Debug.Log("Moving");
            }

            if(oldPos < transform.position.x || oldPos > transform.position.x)
            {
                FindObjectOfType<AudioManager>().Play("Footstep");
                Debug.Log("Sound playing");
            }*/

            /*if(isGrounded == true)
            {
                FindObjectOfType<AudioManager>().Play("Footstep");
            }*/
        }

    /*void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Ground")
        {
            isGrounded = true;
            Debug.Log("Collided");
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.name == "Ground")
        {
            isGrounded = false;
        }
    }*/
}
