using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Shared Stats")]
    [SerializeField] private float range;
    [Header("Swing")]
    [SerializeField] private float swingDamage;
    [SerializeField] private float swingCooldown;
    [SerializeField] private float swingTime;
    [SerializeField] private float deflectedProjectileSpeed;
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
    private bool isHoldingProjectile;
    private bool isBlocking;
    private bool isSwinging;
    private float playerBaseSpeed;

    [Header("References")] 
    [SerializeField] Animator anim;
    
    
    
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
        if (Input.GetButtonDown("Fire1") && swingCooldownTimer < 0 && !isSwinging && !isBlocking)
        {
            anim.Play("Slice");
            isSwinging = true;
            swingDurationTimer = swingTime;
            swingCooldownTimer = swingCooldown;
        }

        swingDurationTimer -= Time.deltaTime;
        swingCooldownTimer -= Time.deltaTime;

        if (swingDurationTimer < 0)
        {
            isSwinging = false;
        }
    }
    
    void Block()
    {
        anim.SetBool("Block", isBlocking);
        isBlocking = Input.GetButton("Fire2") && !isHoldingProjectile && !isSwinging;
        PlayerMovement.ActualMaxSpeed = isBlocking ? maxBlockingSpeed : playerBaseSpeed;
    }
}
