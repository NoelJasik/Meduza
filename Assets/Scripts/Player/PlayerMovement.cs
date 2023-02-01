using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Base Stats")]
    public float accelerationSpeed;
    public float maxSpeed;
    public float jumpForce;
    public float friction;
    [Header("References")]
    private Rigidbody rb;
    private GameObject cam;
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
    }
    private void FixedUpdate()
    {
        Walking();
        Friction();
    }

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
    
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
