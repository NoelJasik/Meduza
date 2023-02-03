using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    PlayerHealth playerHealth;
    Slider healthSlider;
    
    void Start()
    {
        healthSlider = GetComponent<Slider>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        healthSlider.maxValue = playerHealth.GetMaxHealth();
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetCurrentHealth();
    }
}
