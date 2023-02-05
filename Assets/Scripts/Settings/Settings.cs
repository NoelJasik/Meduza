using System;
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
    [SerializeField]
    private GameObject pauseScreen;
    bool isThereAPauseScreen = false;
    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        if(dpiSlider != null)
            dpiSlider.value = Dpi;
        if(graphicsDropdown != null)
            graphicsDropdown.value = QualitySettings.GetQualityLevel();
        if (pauseScreen != null)
        {
            isThereAPauseScreen = true;
            pauseScreen.SetActive(false);
            paused = false;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public static void SetDpi(float dpi)
    {
        Dpi = dpi;
    }
    
    public void SetGraphics()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }
    
    public void Pause()
    {
        if (isThereAPauseScreen)
        {
            paused = !paused;
            pauseScreen.SetActive(paused);
            Time.timeScale = paused ? 0 : 1;
        }
    }
    
    public void SetActiveWithDelay(GameObject thingToActivate)
    {
        StartCoroutine(ActivateWithDelay(thingToActivate, 1f, !thingToActivate.activeSelf));
    }
    
    IEnumerator ActivateWithDelay(GameObject thingToActivate, float delay, bool active)
    {
        yield return new WaitForSecondsRealtime(delay);
        thingToActivate.SetActive(active);
    }




}
