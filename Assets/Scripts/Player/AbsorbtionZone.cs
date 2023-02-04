using UnityEngine;

public class AbsorbtionZone : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField] private Transform barrel;
    [SerializeField] private Transform hitPoint;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip[] swordFleshImpactSounds;
    [SerializeField] private AudioClip[] swordStoneImpactSounds;
    [SerializeField] private AudioClip[] parrySounds;
    [SerializeField] private AudioClip[] parryBlockedSounds;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit by:" + other.name + " " + other.tag);
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().ReceiveDamage(PlayerCombat.ActualSwingDamage);
            SoundManager.Instance.PlayRandom(swordFleshImpactSounds);
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Ground"))
        {
            SoundManager.Instance.PlayRandom(swordStoneImpactSounds);
        }

        if (other.tag != "Projectile") 
        {
           return;
        }

        if (PlayerCombat.IsSwinging && other.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            SoundManager.Instance.PlayRandom(parrySounds);
            GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation);
            projectile.GetComponent<Projectile>().Initialize(hitPoint.position, PlayerCombat.ActualReflectDamage , PlayerCombat.ActualProjectileDeflectSpeed, LayerMask.NameToLayer("PlayerProjectile"));
        }

        if (PlayerCombat.IsBlocking)
        {
            SoundManager.Instance.PlayRandom(parryBlockedSounds);
            PlayerCombat.IsHoldingProjectile = true;
        }
        
        Destroy(other.gameObject);

    }
}
