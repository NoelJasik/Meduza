using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Walking")]
    [SerializeField] 
    float accelerationSpeed;
    [SerializeField] 
    float maxSpeed;
    [SerializeField] 
    float friction;
    
    [Header("Jumping")]
    [SerializeField] 
    float jumpForce;
    [SerializeField] 
    float jumpStopForce;
    // Kayoote time is the time the player can jump after leaving the ground
    [SerializeField] 
    float kayooteTime;
    [SerializeField]
    float jumpInputEnabledTime;
    [SerializeField]
    Transform groundChecker;
    [SerializeField]
    float groundCheckRadius;
    [SerializeField]
    LayerMask whatIsGround;
    [Header("References")]
    private Rigidbody rb;
    private GameObject cam;
    [Header("Private Variables")] 
    private bool isJumping;
    private bool isButtonUp;
    private bool canJump;
    // isGrounded refers to the player "feet" being on the ground, while isTouchingGround is for the player being in contact with the ground
    // This is necessary to prevent the player jumping when being near the ground but not on it, which is common when bunny hopping
    // Raycast wouldn't work, because it would prevent jumping when standing on edges
    private bool isGrounded;
    private bool isTouchingGround;
    private float jumpTimer;
    private float kayooteTimer;



    [Header("Stuff to export")]
    public static Transform PlayerTransform;

    // In awake, because other scripts already need player transform in their start method
    private void Awake()
    {
        PlayerTransform = transform;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (Camera.main != null)
        {
            cam = Camera.main.gameObject;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraMovement();
        CanJump();
    }
    private void FixedUpdate()
    {
        Walking();
        if (isGrounded)
        {
            Friction();
        }

        Jump();

    }
    
    // Camera

    private void CameraMovement()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * Settings.Dpi, 0);
        cam.transform.Rotate(-Input.GetAxis("Mouse Y") * Settings.Dpi, 0, 0);
        if (cam.transform.localEulerAngles.x > 60 && cam.transform.localEulerAngles.x < 70)
        {
            cam.transform.localEulerAngles = new Vector3(60, 0, 0);
        }
        else if (cam.transform.localEulerAngles.x < 300 && cam.transform.localEulerAngles.x > 290)
        {
            cam.transform.localEulerAngles = new Vector3(300, 0, 0);
        }
    }
    
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    
    // Walking

    private void Friction()
    {
        Vector3 velocity = rb.velocity;
        Vector3 firctionForce = new Vector3(-velocity.x * friction, 0, -velocity.z * friction);
        rb.AddForce(firctionForce);
    }

    private void Walking()
    {
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(inputHorizontal, 0, inputVertical);
        movement = transform.TransformDirection(movement);
        movement = movement.normalized;
        movement *= accelerationSpeed;
        movement *= Time.deltaTime;
        Vector3 finalMovement = new Vector3(movement.x, 0, movement.z);
        rb.AddForce(finalMovement, ForceMode.VelocityChange);

        if (rb.velocity.magnitude > maxSpeed)
        {
            Vector3 velocity = rb.velocity;
            Vector3 maxVelocity = new Vector3(velocity.normalized.x * maxSpeed, velocity.y, velocity.normalized.z * maxSpeed);
            velocity = maxVelocity;
            rb.velocity = velocity;
        }
    }
    
    // Jumping

    private void CanJump()
    {
        Collider[] hitColliders = new Collider[1];
        isGrounded = Physics.OverlapSphereNonAlloc(groundChecker.position, groundCheckRadius, hitColliders,whatIsGround) == 1;

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimer = jumpInputEnabledTime;
        }
        if (isJumping)
        {
            jumpTimer -= Time.deltaTime;
        }
        if (isJumping && jumpTimer <= 0)
        {
            isJumping = false;
        }
        if (Input.GetButtonUp("Jump"))
        {
            isButtonUp = true;
        }
        if(isGrounded && isTouchingGround)
        {
            kayooteTimer = kayooteTime;
        }
        kayooteTimer -= Time.deltaTime;
        canJump = kayooteTimer > 0;
    }
    
    private void Jump()
    {
        if (isJumping && !isButtonUp && canJump)
        {
            rb.AddForce(jumpForce * transform.up);
            kayooteTimer = 0;
            isJumping = false;
        }

        if (!isButtonUp) return;
        if (rb.velocity.y >= 0)
        {
            Vector3 velocity = rb.velocity;
            rb.velocity = new Vector3(velocity.x, velocity.y / jumpStopForce, velocity.z);
        }
        isButtonUp = false;
        isJumping = false;
    }
    
    // Ground Check
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckRadius);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 10 || other.gameObject.layer == 11)
        {
            isTouchingGround = true;
        }
    }
    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.layer == 10 || other.gameObject.layer == 11)
        {
            isTouchingGround = true;
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.layer == 10 || other.gameObject.layer == 11)
        {
            isTouchingGround = false;
        }
    }
    
    

}
