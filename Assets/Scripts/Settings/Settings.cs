using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    [Header("Player Settings")] 
    public static float Dpi = 2;
    [SerializeField]
    private Slider dpiSlider;
    [SerializeField]
    private TMP_Dropdown graphicsDropdown;

    // Start is called before the first frame update
    void Start()
    {
        if(dpiSlider != null)
            dpiSlider.value = Dpi;
        if(graphicsDropdown != null)
            graphicsDropdown.value = QualitySettings.GetQualityLevel();
    }

    public static void SetDpi(float dpi)
    {
        Dpi = dpi;
    }
    
    public void SetGraphics()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }
    
    
}
