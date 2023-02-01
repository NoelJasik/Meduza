using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHitPoints;
    [SerializeField] private AudioClip hurtSound;
    
    protected float currentHitPoints;
    protected virtual void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    public virtual void ReceiveDamage(float dmg)
    {
        print(gameObject.name + " received " + dmg + " damage");
        SoundManager.Instance.PlaySound(hurtSound);
        currentHitPoints -= dmg;
        if (currentHitPoints <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        
    }
}