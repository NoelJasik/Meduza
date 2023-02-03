using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float triggerDistance;
    [SerializeField] private float attackDistance;
    [SerializeField] private float rechargeTime;
    [SerializeField] protected float damage;

    private Vector3 target;
    private NavMeshAgent agent;
    private float timeElapsedSinceLastAttack = 0f;
    protected PlayerHealth playerHealth;
    private const float ERROR = 0.1f;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHealth = PlayerMovement.PlayerTransform.GetComponent<PlayerHealth>();
    }
    
    private void Update()
    {
        Movement();
        
        timeElapsedSinceLastAttack += Time.deltaTime;
        if (timeElapsedSinceLastAttack < rechargeTime) return;
        
        float distanceToPlayer = Vector3.Distance(PlayerMovement.PlayerTransform.position, transform.position);
        if (distanceToPlayer - attackDistance < ERROR)  // can be negative
        {
            Attack();
            timeElapsedSinceLastAttack = 0f;
        }
    }

    protected virtual void Movement()
    {
        float distanceToPlayer = Vector3.Distance(PlayerMovement.PlayerTransform.position, transform.position);

        if (distanceToPlayer > triggerDistance)
        {
            target = transform.position;
        }
        else if (distanceToPlayer > attackDistance)
        {
            target = PlayerMovement.PlayerTransform.position;
        }
        else if (distanceToPlayer < attackDistance && IsThereAnObstacleOnTheWayToPlayer())
        {
            target = PlayerMovement.PlayerTransform.position;
        }

        agent.SetDestination(target);
        Vector3 lookVector = PlayerMovement.PlayerTransform.position - transform.position;
        lookVector.y = 0;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }

    private bool IsThereAnObstacleOnTheWayToPlayer()
    {
        Vector3 raycastDirection = PlayerMovement.PlayerTransform.position - transform.position;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, raycastDirection);

        if (hits.Length == 0) return false;
        if (hits[0].collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            return true;
        }

        return false;
    }
    
    protected abstract void Attack();
    
    // added gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}