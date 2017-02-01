using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioSource;
    public AudioClip clip;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play()
    {
        audioSource.PlayOneShot(clip);
    }
}
