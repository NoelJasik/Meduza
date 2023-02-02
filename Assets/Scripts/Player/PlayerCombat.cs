using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Shared Stats")]
    [SerializeField] private GameObject absorbtionZone;
    [Header("Swing")]
    [SerializeField] private float swingDamage;
    [SerializeField] private float reflectDamage;
    [SerializeField] private float swingCooldown;
    [SerializeField] private float swingTime;
    [SerializeField] private float projectileDeflectSpeed;
    [Header("Block")]
    [SerializeField] private float blockCooldown;
    [SerializeField] private float projectileHoldTime;
    [SerializeField] private float holdedProjectileSpeed;
    [SerializeField] private float maxBlockingSpeed;

    [Header("Private Variables")]
    private float swingCooldownTimer;
    private float blockCooldownTimer;
    private float swingDurationTimer;
    private float projectileHoldTimer;
    private float playerBaseSpeed;

    [Header("References")] 
    [SerializeField] Animator anim;
    
    [Header("Stuff to export")]
    public static bool IsBlocking;
    public static bool IsSwinging;
    public static bool IsHoldingProjectile;
    public static float ActualSwingDamage;
    public static float ActualReflectDamage;
    public static float ActualProjectileDeflectSpeed;


    private void Awake()
    {
        ActualSwingDamage = swingDamage;
        ActualReflectDamage = reflectDamage;
        ActualProjectileDeflectSpeed = projectileDeflectSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBaseSpeed = PlayerMovement.ActualMaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Swing();
        Block();
    }

    void Swing()
    {
        if (Input.GetButtonDown("Fire1") && swingCooldownTimer < 0 && !IsSwinging && !IsBlocking)
        {
            anim.Play("Slice");
            IsSwinging = true;
            swingDurationTimer = swingTime;
            swingCooldownTimer = swingCooldown;
        }

        absorbtionZone.SetActive(IsSwinging);
        swingDurationTimer -= Time.deltaTime;
        swingCooldownTimer -= Time.deltaTime;

        if (swingDurationTimer < 0)
        {
            IsSwinging = false;
        }
    }
    
    void Block()
    {
        anim.SetBool("Block", IsBlocking);
        IsBlocking = Input.GetButton("Fire2") && !IsHoldingProjectile && !IsSwinging;
        PlayerMovement.ActualMaxSpeed = IsBlocking ? maxBlockingSpeed : playerBaseSpeed;
    }
}
