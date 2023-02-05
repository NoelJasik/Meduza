using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI enemyCounter;

    [SerializeField] private GameObject[] showWhenAllDead;
    public static int EnemyCount;
    public static int MaxEnemyCount;
    
    // Start is called before the first frame update
    void Start()
    {
        MaxEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        EnemyCount = MaxEnemyCount;
        foreach (GameObject thing in showWhenAllDead)
        {
            thing.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCounter == null)
            return;
        if (EnemyCount == 0)
        {
            Win();
            return;
        }
        enemyCounter.gameObject.SetActive(EnemyCount < 5 && MaxEnemyCount != 1);
        enemyCounter.text = EnemyCount.ToString() + " enemies left";
    }

    void Win()
    {
        enemyCounter.text = "Head to the exit";
        enemyCounter.gameObject.SetActive(true);
        foreach (GameObject thing in showWhenAllDead)
        {
            thing.SetActive(true);
        }
    }
}
