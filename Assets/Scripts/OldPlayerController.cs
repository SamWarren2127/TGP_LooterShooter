using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;

    // Character Movement
    [SerializeField] private float playerSpeed;
    [SerializeField] private float stoppingForce;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float moveMult;
    [SerializeField] private float sprintMult;
    private bool isSprinting = false;
    private Vector3 characterMoveDirection;

    // Mouse Movement
    private float mouseX;
    private float mouseY;
    private float xRotation;
    [SerializeField] private float mouseSensitivity;
    private CameraController _camera;

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

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Gets the players keyboard input
        SetCharacterMoveDirection();

        // Gets the players mouse input
        SetCharacterLookDirection();

        Sprint();

        Crouch();

        Jump();
    }

    private void FixedUpdate()
    {
        //------------------Movement Logic-----------------------------

        if (characterMoveDirection.magnitude > 0.1f)
        {
            if (isSprinting)
            {
                _rigidbody.AddRelativeForce(characterMoveDirection * playerSpeed * moveMult * sprintMult, ForceMode.VelocityChange);

                if (_rigidbody.velocity.magnitude > (maxSpeed * sprintMult))
                {
                    _rigidbody.velocity = _rigidbody.velocity.normalized * (maxSpeed * sprintMult);
                }
            }
            else
            {
                _rigidbody.AddRelativeForce(characterMoveDirection * playerSpeed * moveMult, ForceMode.VelocityChange);

                if (_rigidbody.velocity.magnitude > maxSpeed)
                {
                    _rigidbody.velocity = _rigidbody.velocity.normalized * maxSpeed;
                }

            }
        }
        else if (_rigidbody.velocity.magnitude > 2f || _rigidbody.velocity.magnitude < -2f)
        {
            _rigidbody.AddRelativeForce(-_rigidbody.velocity.normalized * stoppingForce, ForceMode.VelocityChange);
        }
        else if (_rigidbody.velocity.magnitude != 0f)
        {
            _rigidbody.velocity = Vector3.zero;
        }

        //--------------------Look Logic------------------------------

        // Rotates the rigidbody on the Y axis to follow the mouse going left and right

        if (mouseX > 0.1f || mouseX < -0.1f)
        {
            _rigidbody.AddTorque(Vector3.up * mouseX * mouseSensitivity);
        }
        else if (_rigidbody.angularVelocity.magnitude > 0.3f)
        {
            _rigidbody.AddTorque(-_rigidbody.angularVelocity.normalized * mouseSensitivity);
        }
        else if (_rigidbody.angularVelocity.magnitude != 0f)
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }

        //Rotates the camera on the X axis to look up and down following the mouse
        //_camera.RotateCamera(xRotation);
    }

    void SetCharacterMoveDirection()
    {
        characterMoveDirection.x = Input.GetAxis("Horizontal");
        characterMoveDirection.z = Input.GetAxis("Vertical");
    }

    void SetCharacterLookDirection()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        xRotation -= mouseY;
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
                //_camera.LowerCamera(crou);
            }

            if (Input.GetButtonUp("Crouch"))
            {
                //_camera.RaiseCamera();
            }
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _rigidbody.AddForce(transform.up * jumpForce, ForceMode.Force);
            Debug.Log("Jump");
        }
    }
}
