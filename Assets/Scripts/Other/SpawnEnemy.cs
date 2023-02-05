using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int whichEnemy = 0;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private int killedEnemiesToSpawn = 0;

    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private AudioClip spawnSound;

    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (EnemyCounter.MaxEnemyCount - EnemyCounter.EnemyCount >= killedEnemiesToSpawn && PlayerMovement.HasMoved)
        {
            Invoke("Spawning", spawnDelay);
        }
    }

    void Spawning()
    {
        CancelInvoke("Spawning");
        Instantiate(enemyPrefabs[whichEnemy], transform.position, transform.rotation);
        Instantiate(spawnEffect, transform.position, transform.rotation);
        SoundManager.Instance.PlaySound(spawnSound, 1f, true, transform);
        Destroy(gameObject);
    }
    
    //Draw gizmos of the enemy stats
    private void OnDrawGizmos()
    {
        EnemyBehaviour enemy = enemyPrefabs[whichEnemy].GetComponent<EnemyBehaviour>();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, enemy.triggerDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemy.attackDistance);
    }
}
