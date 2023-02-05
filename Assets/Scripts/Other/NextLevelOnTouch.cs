using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelOnTouch : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Timer.Instance.isCounting = false;
            Timer.Instance.SaveTime();
            SceneSwitcher.Instance.LoadNextScene();
        }
    }
}
