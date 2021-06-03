using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigidbody;
    CameraController _camera;
    Collider _collider;
    CapsuleCollider capsuleCollider;
    HUDManager hudManager;
    Camera playerCamera;

    public float stepRate = 0.5f;
    public float sprintRate = 0.5f;
    public float stepCooldown;
    public float sprintCooldown;
    public float slidingSpeed;
    public float slideHeight;
    float originalHeight;
    [SerializeField] bool canSlide;


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

    [HideInInspector]
    public bool doubleJump;

    private bool isGrounded;

    private Vector3 weaponPosition;
    private Quaternion weaponRotation;
    [SerializeField] GameObject[] gunTemplates;
    [SerializeField] private Transform gunObj;
    [SerializeField] private Transform GunPosition;

    private IGunDisplayable gunType;

    [SerializeField] TutorialUI tutorialUI;

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
            if (_collider == null)
            {
                Debug.Log("_collider is a null reference");
            }
        }

        playerCamera = GetComponentInChildren<Camera>();

        distanceToGround = _collider.bounds.extents.y;
        doubleJump = false;
        
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalHeight = capsuleCollider.height;
        canSlide = true;

        Cursor.lockState = CursorLockMode.Locked;

        hudManager = GetComponent<PlayerStats>().hudManager;

        weaponPosition = gunObj.transform.position;
        weaponRotation = gunObj.transform.rotation;
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
        sprintCooldown -= Time.deltaTime;
        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && stepCooldown < 0f && isGrounded == true && isSprinting == true)
        {
            FindObjectOfType<AudioManager>().Play("Footstep");
            stepCooldown = stepRate;
        }

        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && sprintCooldown < 0f && isGrounded == true && isSprinting == false)
        {
            FindObjectOfType<AudioManager>().Play("Footstep");
            sprintCooldown = sprintRate;
        }

        if (Input.GetKeyDown(KeyCode.C) && canSlide)
        {
            StartCoroutine(Slide());
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeGun(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeGun(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeGun(2);
        }
    }

    private void SetCharacterMoveDirection()
    {
        moveDirection.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        moveDirection.z = Input.GetAxis("Vertical") * speed * Time.deltaTime;
    }

    private void ChangeGun(int _gun)
    {
        Destroy(gunObj.gameObject);

        gunObj = Instantiate<GameObject>(gunTemplates[_gun], GunPosition.position, GunPosition.rotation, playerCamera.gameObject.transform).transform;
        gunType = gunObj.GetComponent<IGunDisplayable>();
        hudManager.UpdateEquippedGunText(gunType.GetGunName());
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
                //_camera.LowerCamera();
            }

            if (Input.GetButtonUp("Crouch"))
            {
                speed *= 3;
                //_camera.RaiseCamera();
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
        if(doubleJump && !groundedPlayer)
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

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }

    }

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name == "CrouchCollider")
        {
            tutorialUI.ShowCrouch();
        }
        else if(col.gameObject.name == "AbilityCollider")
        {
            tutorialUI.ShowAbility();
            Destroy(col.gameObject);
            Debug.Log("collided");
        }
        else if(col.gameObject.name == "JumpCollider")
        {
            tutorialUI.ShowJump();
        }
        else if(col.gameObject.name == "DoubleJumpCollider")
        {
            tutorialUI.ShowDoubleJump();
        }
        else if(col.gameObject.name == "ObjectiveCollider")
        {
            tutorialUI.ShowObjective();
        }
    }

    IEnumerator Slide()
    {
        canSlide = false;
        capsuleCollider.height = slideHeight;
        _rigidbody.AddForce(transform.forward * slidingSpeed, ForceMode.VelocityChange);
        yield return new WaitForSeconds(3);
        capsuleCollider.height = originalHeight;
        canSlide = true;
    }
}
