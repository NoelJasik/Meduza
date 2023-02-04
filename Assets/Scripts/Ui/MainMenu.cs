using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip mainMenuMusic;
    
    private void Start()
    {
        SoundManager.Instance.PlayMusic(mainMenuMusic);
    }
}
