using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public static SceneSwitcher Instance;
    [SerializeField]
    private Animator transition;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneCo(sceneIndex));
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneCo(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    public void LoadPreviousScene()
    {
        StartCoroutine(LoadSceneCo(SceneManager.GetActiveScene().buildIndex - 1));
    }
    
    public void ReloadScene()
    {
        StartCoroutine(LoadSceneCo(SceneManager.GetActiveScene().buildIndex));
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    
    
    
    IEnumerator LoadSceneCo(int sceneNum)
    {
        if(sceneNum < 0)
            sceneNum = 0;
        if(sceneNum >= SceneManager.sceneCountInBuildSettings)
            sceneNum = SceneManager.sceneCountInBuildSettings - 1;
        
        transition.Play("SceneEnd");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneNum);
    }
}