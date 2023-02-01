using UnityEngine;

public class MeleeEnemyBehaviour : EnemyBehaviour
{
    protected override void Attack()
    {
        playerHealth.ReceiveDamage(damage);
    }
}