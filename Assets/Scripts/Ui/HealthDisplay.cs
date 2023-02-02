using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    PlayerHealth playerHealth;
    Slider healthSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        healthSlider.maxValue = playerHealth.GetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = playerHealth.GetCurrentHealth();
    }
}
