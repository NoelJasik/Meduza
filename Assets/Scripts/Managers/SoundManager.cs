using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private GameObject audioSourcePrefab;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource newSource = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
        newSource.clip = clip;
        newSource.Play();
        Destroy(newSource.gameObject, clip.length);
    }

    public void PlayRandom(AudioClip[] clips)
    {
        int rand = Random.Range(0, clips.Length);
        PlaySound(clips[rand]);
    }
}
