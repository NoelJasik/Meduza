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
    public static Transform PlayerPosition;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (Camera.main != null)
        {
            cam = Camera.main.gameObject;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerPosition = transform;
        Walking();
        CameraMovement();
    }
    private void FixedUpdate()
    {
        rb.AddForce(-rb.velocity * friction);
    }
    
    void CameraMovement()
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
    void Walking()
    {
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(inputHorizontal, 0, inputVertical);
        movement = cam.transform.TransformDirection(movement);
        movement.Normalize();
        movement *= accelerationSpeed;
        movement *= Time.deltaTime;
        Vector3 finalMovement = new Vector3(movement.x, 0, movement.z);
        rb.AddForce(finalMovement, ForceMode.VelocityChange);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
