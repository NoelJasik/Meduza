using System.Collections;
using UnityEngine;

public class DistanceBurstEnemyBehaviour : DistanceEnemyBehaviour
{
    [SerializeField] private float timeBetweenProjectilesInBurst;
    [SerializeField] private float amountOfProjectileInBurst;
    
    protected override void Attack()
    {
        StartCoroutine(SpawnBurst());
    }

    IEnumerator SpawnBurst()
    {
        for (int i = 0; i < amountOfProjectileInBurst; i++)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(timeBetweenProjectilesInBurst);
        }                    
    }
}