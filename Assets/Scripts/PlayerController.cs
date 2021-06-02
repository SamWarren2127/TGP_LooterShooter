using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;
    CameraController _camera;
    CapsuleCollider _collider;
    [SerializeField] AudioManager audioManager;

    public float stepRate = 0.5f;
    public float stepCooldown;

    [Header("Player Controller Stats")]
    [SerializeField]
    float speed;
    [SerializeField]
    Vector3 jumpForce;
    [SerializeField]
    float dashForce;
    [SerializeField]
    Vector3 moveDirection;
    bool isSprinting = false;
    [SerializeField]
    float sprintMult;
    float moveMult = 1.0f;
    float distanceToGround;
    bool groundedPlayer;
    [SerializeField]
    float jumpTime;
    private float crouchDifference = 0.5f;

    [HideInInspector]
    public bool doubleJump;

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

        _collider = GetComponent<CapsuleCollider>();
        {
            if (_collider == null)
            {
                Debug.Log("_collider is a null reference");
            }
        }

        distanceToGround = _collider.bounds.extents.y;
        doubleJump = false;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = IsGrounded();

        SetCharacterMoveDirection();
        Crouch();
        Sprint();
        Jump();

        if (isSprinting)
        {
            transform.Translate(moveDirection * sprintMult * moveMult);
        }
        else
        {
            transform.Translate(moveDirection * moveMult);
        }

        stepCooldown -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCooldown < 0f)
        {
            audioManager.Play("Footstep");
            stepCooldown = stepRate;
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
                _collider.height *= 0.5f;
                _camera.LowerCamera(crouchDifference);
            }

            if (Input.GetButtonUp("Crouch"))
            {
                speed *= 3;
                _collider.height *= 2f;
                _camera.RaiseCamera(crouchDifference);
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
            Debug.Log("Jump");
        }
    }
    
    public void DoubleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && doubleJump && !groundedPlayer)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            _rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }
    }

    public void Dash()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, 0f);
        _rigidbody.AddRelativeForce(Vector3.forward * dashForce, ForceMode.VelocityChange);
    }

     public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanceToGround + 0.1f);
    }

    public IEnumerator IncreaseMoveMultCoroutine(float _moveMult)
    {
        float originalMult = moveMult;
        moveMult = _moveMult;
        yield return new WaitForSeconds(3f);
        moveMult = originalMult;
    }
}
