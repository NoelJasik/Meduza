using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    private float damage;
    private float speed;
    
    private Health targetHealth;
    
    // Projectile layer 
    // There is a layer for enemies projectiles and for players projectiles
    // When spawning projectiles you need to pass the correct layer in Initialize()
    // The layers interaction is in the Project settings -> Physics2D 
    public void Initialize(Vector3 targetPos, float dmg, float projSpeed, int projectileLayer)
    {
        direction = (targetPos - transform.position).normalized;
        speed = projSpeed;
        damage = dmg;
        gameObject.layer = projectileLayer;
    }

    private void Update()
    {
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out targetHealth))
        {
            targetHealth.ReceiveDamage(damage);
        }
        // else if hits mirror then ... (and without DestroyBullet())
        
        DestroyBullet();
    }
    
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}