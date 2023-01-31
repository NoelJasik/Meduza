using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float triggerDistance;

    private Vector3 target;
    private NavMeshAgent agent;
    private EnemyWeapon enemyWeapon;

    public bool IsInMeleeMode { get; private set; }
    
    public static float minDistanceToPlayer = 1.5f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyWeapon = GetComponent<EnemyWeapon>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(PlayerMovement.PlayerTransform.position, transform.position);

        if (distanceToPlayer > triggerDistance)
        {
            agent.SetDestination(transform.position);
            return;
        }
        
        if (distanceToPlayer < enemyWeapon.GetMeleeDistance())
        {
            IsInMeleeMode = true;
        }

        if (IsInMeleeMode)
        {
            MoveToAttack(distanceToPlayer);
        }
        else
        {
            MoveToShoot(distanceToPlayer);
        }
        
        agent.SetDestination(target);
    }

    private void MoveToAttack(float distanceToPlayer)
    {
        if (distanceToPlayer > enemyWeapon.GetMeleeDistance())
        {
            target = PlayerMovement.PlayerTransform.position;
        }
        else
        {
            target = transform.position;
        }
    }

    private void MoveToShoot(float distanceToPlayer)
    {
        if (distanceToPlayer < triggerDistance)
        {
            if (distanceToPlayer > enemyWeapon.GetShootDistance())
            {
                target = PlayerMovement.PlayerTransform.position;
            }
            else
            {
                target = transform.position;
            }
        }
        else
        {
            target = transform.position;
        }
    }
}