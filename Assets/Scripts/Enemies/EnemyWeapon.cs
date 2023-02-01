using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private float meleeAttackDistance;
    [SerializeField] private float rechargeTime;
    [SerializeField] protected float meleeDamage;
    
    [SerializeField] private float shootDistance;
    [SerializeField] protected float shootDamage;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float projectileSpeed;

    private float timeElapsedSinceLastAttack = 0f;
    private int enemyProjectileLayer;
    private EnemyMovement enemyMovement;

    private void Start()
    {
        if (meleeAttackDistance < EnemyMovement.minDistanceToPlayer || shootDistance < EnemyMovement.minDistanceToPlayer)
        {
            Debug.LogError("Attack distance on: " + gameObject.name + " " +
                           "is less than minimal distance to player, this enemt won't hit the player ever");
        }
        
        enemyProjectileLayer = LayerMask.NameToLayer("EnemyProjectile");
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        timeElapsedSinceLastAttack += Time.deltaTime;
        if (timeElapsedSinceLastAttack < rechargeTime) return;
        
        float distanceToPlayer = Vector3.Distance(PlayerMovement.PlayerTransform.position, transform.position);
        if (distanceToPlayer < meleeAttackDistance)
        {
            Attack();
        }
        else if (distanceToPlayer < shootDistance && !enemyMovement.IsInMeleeMode)
        {
            Shoot();
        }
        
        timeElapsedSinceLastAttack = 0f;
    }

    private void Attack()
    {
        print("MeleeAttack");
    }

    private void Shoot()
    {
        
    }

    public float GetShootDistance() => shootDistance;
    public float GetMeleeDistance() => meleeAttackDistance;
}
