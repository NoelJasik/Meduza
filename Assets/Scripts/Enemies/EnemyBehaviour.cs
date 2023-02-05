using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public float triggerDistance;
    public float attackDistance;
    [SerializeField] private float rechargeTime;
    [SerializeField] protected float damage;

    private Vector3 target;
    private NavMeshAgent agent;
    private float timeElapsedSinceLastAttack = 0f;
    protected PlayerHealth playerHealth;
    private const float ERROR = 0.1f;
    private const float turnLerpCoef = 0.05f;
    bool isThinking = true;

    private Animator animator;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerHealth = PlayerMovement.PlayerTransform.GetComponent<PlayerHealth>();
      //  Invoke("BeginThinking", 0.1f);
      //  transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
      animator = GetComponent<Animator>();
      if(animator != null)
      animator.SetFloat("Speed", 1f);
    }

    void BeginThinking()
    {
        isThinking = true;
    }
    
    void SpawnAnimation()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 1.5f);
        if (transform.localScale.x > 0.9f && transform.localScale.y > 0.9f && transform.localScale.z > 0.9f)
        {
            transform.localScale = Vector3.one;
        }
    }
    
    private void Update()
    {
        isThinking = PlayerMovement.HasMoved;

        if (!isThinking)
        {
         //   SpawnAnimation();
            return;
        }
        Movement();
        
        timeElapsedSinceLastAttack += Time.deltaTime;
        if (timeElapsedSinceLastAttack < rechargeTime) return;
        
        float distanceToPlayer = Vector3.Distance(PlayerMovement.PlayerTransform.position, transform.position);
        if (distanceToPlayer - attackDistance < ERROR)  // can be negative
        {
            AttackContainer();
            timeElapsedSinceLastAttack = 0f;
        }
    }

    protected virtual void Movement()
    {
        float distanceToPlayer = Vector3.Distance(PlayerMovement.PlayerTransform.position, transform.position);
        Debug.Log(IsThereAnObstacleOnTheWayToPlayer());
        if (distanceToPlayer > triggerDistance)
        {
            target = transform.position;
<<<<<<< HEAD
            //animator.SetFloat("Speed", 0f);
=======
            if(animator != null)
                animator.SetFloat("Speed", 0f);
>>>>>>> 3dd2256ff540868bfbe626330e5ec9316c3453d3
        }
        else if (distanceToPlayer > attackDistance)
        {
            target = PlayerMovement.PlayerTransform.position;
<<<<<<< HEAD
            //animator.SetFloat("Speed", 1f);
=======
            if(animator != null)
                animator.SetFloat("Speed", 1f);
>>>>>>> 3dd2256ff540868bfbe626330e5ec9316c3453d3
        }
        else
        {
<<<<<<< HEAD
            if (IsThereAnObstacleOnTheWayToPlayer())
            {
                target = PlayerMovement.PlayerTransform.position;
                //animator.SetFloat("Speed", 1f);
            }
            else
            {
                target = transform.position;
                //animator.SetFloat("Speed", 0f);
            }
=======
            target = PlayerMovement.PlayerTransform.position;
            if(animator != null)
                animator.SetFloat("Speed", 1f);
>>>>>>> 3dd2256ff540868bfbe626330e5ec9316c3453d3
        }

        agent.SetDestination(target);
        Vector3 lookVector = PlayerMovement.PlayerTransform.position - transform.position;
        lookVector.y = 0;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnLerpCoef);
    }

    private bool IsThereAnObstacleOnTheWayToPlayer()
    {
        Vector3 raycastDirection = PlayerMovement.PlayerTransform.position - transform.position;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, raycastDirection,
            Vector3.Distance(PlayerMovement.PlayerTransform.position, transform.position));

        if (hits.Length == 0) return false;
        if (hits[0].collider.gameObject.layer == LayerMask.NameToLayer("Wall") || 
            hits[0].collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            return true;
        }

        return false;
    }

    private void AttackContainer()
    {
<<<<<<< HEAD
        //animator.SetTrigger("Attack");
=======
        if(animator != null)
            animator.SetTrigger("Attack");
>>>>>>> 3dd2256ff540868bfbe626330e5ec9316c3453d3
        Attack();
    }
    
    protected abstract void Attack();
    
    // added gizmos
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}