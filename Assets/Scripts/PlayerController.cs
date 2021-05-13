using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;
    CameraController _camera;
    CharacterController _character;
    Collider _collider;

    [Header("Player Controller Stats")]
    [SerializeField]
    float speed;
    [SerializeField]
    Vector3 jumpForce;
    [SerializeField]
    Vector3 moveDirection;
    bool isSprinting = false;
    [SerializeField]
    float sprintMult;
    float distanceToGround;
    bool groundedPlayer;
    float startPosY;
    [SerializeField]
    float jumpTime;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.Log("_rigidbody is a null reference");
        }

        _camera = GetComponentInChildren<CameraController>();
        if (_camera == null)
        {
            Debug.Log("_camera is a null reference");
        }

        _collider = GetComponent<Collider>();
        {
            if(_collider == null)
            {
                Debug.Log("_collider is a null reference");
            }
        }

        distanceToGround = _collider.bounds.extents.y;
        startPosY = transform.position.y;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = IsGrounded();

        //if (groundedPlayer && moveDirection.y > 0f)
        //{
        //    moveDirection.y = 0f;
        //}

        //if (groundedPlayer)
        {
            SetCharacterMoveDirection();
            Crouch();
            Sprint();
            Jump();

            if (isSprinting)
            {
                transform.Translate(moveDirection * sprintMult);
            }
            else
            {
                transform.Translate(moveDirection);
            }
        }
    }

    private void SetCharacterMoveDirection()
    {
        moveDirection.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        moveDirection.z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
    }

    private void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
        }

        if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
        }
    }

    private void Crouch()
    {
        if (!isSprinting)
        {
            if (Input.GetButtonDown("Crouch"))
            {
                speed /= 3;
                _camera.LowerCamera();
            }

            if (Input.GetButtonUp("Crouch"))
            {
                speed *= 3;
                _camera.RaiseCamera();
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            //moveDirection += jumpForce;
            _rigidbody.velocity += jumpForce;
            Debug.Log("Jump");
        }

        //if(Input.GetKeyUp(KeyCode.Space))
        //{
        //    moveDirection.y = 0f;
        //}
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
    }
}
