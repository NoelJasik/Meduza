using UnityEngine;

public class DistanceEnemyBehaviour : EnemyBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform shootPoint;
    [SerializeField] protected float projectileSpeed;
    
    [SerializeField] private AudioClip[] roarSounds;

    protected override void Attack()
    {
        SpawnProjectile();
    }

    protected void SpawnProjectile()
    {
        SoundManager.Instance.PlayRandom(roarSounds);
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(PlayerMovement.PlayerTransform.position, 
            damage, projectileSpeed, LayerMask.NameToLayer("EnemyProjectile"));
    }
    
}