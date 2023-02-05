using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayBestTIme : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    private TextMeshProUGUI bestTimeText;

    // Start is called before the first frame update
    void Start()
    {
        if (levelIndex == 0)
        {
            levelIndex = SceneManager.GetActiveScene().buildIndex;
        }
  bestTimeText = GetComponent<TextMeshProUGUI>();
  bestTimeText.text = Timer.FormatTime(PlayerPrefs.GetFloat("bestTime" + levelIndex));
    }

 
}
