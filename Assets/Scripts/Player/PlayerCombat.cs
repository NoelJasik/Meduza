using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Shared Stats")]
    [SerializeField] private GameObject absorbtionZone;

    [SerializeField] private float inputErrorRoom;
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
    [SerializeField] private float holdedProjectileDamage;
    [SerializeField] private float maxBlockingSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform hitPoint;

    [Header("Private Variables")]
    private float swingCooldownTimer;
    private float blockCooldownTimer;
    private float swingDurationTimer;
    private float projectileHoldTimer;
    private float playerBaseSpeed;
    private float inputErrorRoomTimer;

    [Header("References")] 
    [SerializeField] Animator anim;

    [SerializeField] private Animator swordAnim;
    
    [Header("Stuff to export")]
    public static bool IsBlocking;
    public static bool IsSwinging;
    public static bool IsHoldingProjectile;
    public static float ActualSwingDamage;
    public static float ActualReflectDamage;
    public static float ActualProjectileDeflectSpeed;
    
    private AudioSource playerSizzleBackgroundSource;
    [SerializeField] private GameObject particlesForSwordSizzle;

    private void Awake()
    {
        ActualSwingDamage = swingDamage;
        ActualReflectDamage = reflectDamage;
        ActualProjectileDeflectSpeed = projectileDeflectSpeed;
    }

    void Start()
    {
        playerBaseSpeed = PlayerMovement.ActualMaxSpeed;
        projectileHoldTimer = projectileHoldTime;
        playerSizzleBackgroundSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Swing();
        Block();
        absorbtionZone.SetActive(IsSwinging || IsBlocking);
    }

    void Swing()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            inputErrorRoomTimer = inputErrorRoom;
        }
        inputErrorRoomTimer -= Time.deltaTime;
        if (inputErrorRoomTimer > 0 && swingCooldownTimer < 0 && swingDurationTimer < 0 && !IsSwinging && !IsBlocking)
        {
            swingDurationTimer = swingTime;
            swingCooldownTimer = swingCooldown;
            anim.Play("Slice");
            IsSwinging = true;
            if (IsHoldingProjectile)
            {
                GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
                projectile.GetComponent<Projectile>().Initialize(hitPoint.position, holdedProjectileDamage , holdedProjectileSpeed, LayerMask.NameToLayer("PlayerProjectile"));
                IsHoldingProjectile = false;
                playerSizzleBackgroundSource.Stop();
                particlesForSwordSizzle.SetActive(false);
                projectileHoldTimer = projectileHoldTime;
            }
        }
        
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
        swordAnim.SetBool("Hold", IsHoldingProjectile);
        IsBlocking = Input.GetButton("Fire2") && !IsHoldingProjectile && !IsSwinging && blockCooldownTimer < 0;
        PlayerMovement.ActualMaxSpeed = IsBlocking ? maxBlockingSpeed : playerBaseSpeed;
        
        if (IsBlocking && Input.GetButtonUp("Fire2"))
        {
            blockCooldownTimer = blockCooldown;
        }
        else
        {
            blockCooldownTimer -= Time.deltaTime;
        }
        
        if(IsHoldingProjectile)
        {
            projectileHoldTimer -= Time.deltaTime;
            if (projectileHoldTimer < 0)
            {
                playerSizzleBackgroundSource.Stop();
                particlesForSwordSizzle.SetActive(false);
                IsHoldingProjectile = false;
                projectileHoldTimer = projectileHoldTime;
            }
        }
    }
}
