using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private int killedEnemiesToSpawn = 0;
    
    

    // Update is called once per frame
    void Update() {
        if (EnemyCounter.MaxEnemyCount - EnemyCounter.EnemyCount >= killedEnemiesToSpawn)
        {
            Invoke("Spawning", spawnDelay);
        }
    }

    void Spawning()
    {
        CancelInvoke("Spawning");
        Instantiate(enemyPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
