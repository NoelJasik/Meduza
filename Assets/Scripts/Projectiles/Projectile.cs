using Unity.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 Direction { get; set; }
    private float damage;
    private float speed;
    
    private Health targetHealth;
    private Mirror targetMirror;
    
    // Projectile layer 
    // There is a layer for enemies projectiles and for players projectiles
    // When spawning projectiles you need to pass the correct layer in Initialize()
    // The layers interaction is in the Project settings -> Physics2D 
    public void Initialize(Vector3 targetPos, float dmg, float projSpeed, int projectileLayer)
    {
        Direction = (targetPos - transform.position).normalized;
        speed = projSpeed;
        damage = dmg;
        gameObject.layer = projectileLayer;
    }

    private void Update()
    {
        transform.position += Direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out targetMirror))
        {
            targetMirror.Reflect(this);
            return;
        }
        
        if (col.TryGetComponent(out targetHealth))
        {
            targetHealth.ReceiveDamage(damage);
        }
        
        DestroyBullet();
    }
    
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}