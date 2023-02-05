using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private GameObject audioSourcePrefab;
    
    private GameObject _musicSource;
    
    public static String CurrentTrack;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    
    public void PlayMusic(AudioClip clip)
    {
        CurrentTrack = clip.name;
        if(_musicSource != null)
            Destroy(_musicSource);
        AudioSource newSource = Instantiate(audioSourcePrefab, transform).GetComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = true;
        newSource.Play();
        _musicSource = newSource.gameObject;
    }

    public void PlaySound(AudioClip clip, float volume = 1f, bool is3DSound = false, Transform whereToPlay = null)
    {
        if (whereToPlay == null)
        {
            whereToPlay = transform;
        }
        AudioSource newSource = Instantiate(audioSourcePrefab, whereToPlay.position, whereToPlay.rotation).GetComponent<AudioSource>();
        if(is3DSound)
            newSource.spatialBlend = 1f;
        else
            newSource.spatialBlend = 0f;
        newSource.volume = volume;
        newSource.clip = clip;
        newSource.Play();
        Destroy(newSource.gameObject, clip.length);
    }

    public void PlayRandom(AudioClip[] clips, float volume = 1f, bool is3DSound = false, Transform whereToPlay = null)
    {
        int rand = Random.Range(0, clips.Length);
        PlaySound(clips[rand], volume, is3DSound, whereToPlay);
    }
}
