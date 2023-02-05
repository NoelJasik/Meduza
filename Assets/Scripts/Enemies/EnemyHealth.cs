using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    [SerializeField] private AudioClip petrifySound;
    
    protected override void Death()
    {
        if(gameObject.GetComponent<EnemyBehaviour>().enabled == false)
            return;
        
        Animator animator = GetComponentInChildren<Animator>();
        SoundManager.Instance.PlaySound(petrifySound, 1f, false, transform);
        if(animator != null)
        animator.SetTrigger("Petrify");
        EnemyCounter.EnemyCount--;
        gameObject.GetComponent<EnemyBehaviour>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<CapsuleCollider>().radius /= 2f;
        gameObject.layer = LayerMask.NameToLayer("Wall");
        gameObject.tag = "Wall";
        this.enabled = false;
    }
}
