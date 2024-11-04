using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManagerService : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioClip moveSound;
    public AudioClip musicSound;
    public AudioClip diamondSound;
    public AudioClip switchGravSound;
    public AudioSource deathAudioSource;
    public AudioSource moveAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource diamondAudioSource;
    public AudioSource switchGravSource;

    // Start is called before the first frame update
    void Start()
    {
        if (!(FindObjectsOfType<AudioManagerService>().Count() > 1))
        {
            DontDestroyOnLoad(transform);
        }
        else
        {
            Destroy(gameObject);
        }

        deathAudioSource = transform.gameObject.AddComponent<AudioSource>();
        moveAudioSource = transform.gameObject.AddComponent<AudioSource>();
        musicAudioSource = transform.gameObject.AddComponent<AudioSource>();
        diamondAudioSource = transform.gameObject.AddComponent<AudioSource>();
        switchGravSource = transform.gameObject.AddComponent<AudioSource>();

        deathAudioSource.Stop();
        moveAudioSource.Stop();
        diamondAudioSource.Stop();
        switchGravSource.Stop();

        deathAudioSource.volume = 0.15f;
        moveAudioSource.volume = 0.15f;
        musicAudioSource.volume = 0.10f;
        diamondAudioSource.volume = 0.15f;
        switchGravSource.volume = 1;

        deathAudioSource.clip = deathSound;
        moveAudioSource.clip = moveSound;
        musicAudioSource.clip = musicSound;
        diamondAudioSource.clip = diamondSound;
        switchGravSource.clip = switchGravSound;

        musicAudioSource.Play();
    }
}
