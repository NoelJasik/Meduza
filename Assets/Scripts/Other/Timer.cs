using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Timer : MonoBehaviour
{
    public static Timer Instance;
    
    public float time;
    public bool isCounting;
    
    [SerializeField] TextMeshProUGUI timerText;
    
    void Awake()
    {
        Instance = this;
        time = 0;
        isCounting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerText != null)
            timerText.text = FormatTime(time);
        if(isCounting)
            time += Time.deltaTime;
    }

    public void SaveTime()
    {
        if(PlayerPrefs.GetFloat("bestTime" + SceneManager.GetActiveScene().buildIndex) >= time)
        PlayerPrefs.SetFloat("bestTime" + SceneManager.GetActiveScene().buildIndex, time);
    }

    public static String FormatTime(float timeToConvert)
    {
        String temp = "";
        float minutes = Mathf.FloorToInt(timeToConvert / 60);
        float seconds = Mathf.FloorToInt(timeToConvert % 60);
        float milliSeconds = (timeToConvert % 1) * 1000;
        temp =  string.Format("{0:00}.{1:00}.{2:000}", minutes, seconds, milliSeconds);
        return temp;
    }
}
