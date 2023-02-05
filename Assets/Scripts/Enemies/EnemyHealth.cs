using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : Health
{
    protected override void Death()
    {
        Animator animator = GetComponentInChildren<Animator>();
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
