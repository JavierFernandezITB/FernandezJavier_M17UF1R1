using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManagerService : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioClip moveSound;
    public AudioClip switchGravSound;
    public AudioSource deathAudioSource;
    public AudioSource moveAudioSource;
    public AudioSource switchGravAudioSource;

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
        switchGravAudioSource = transform.gameObject.AddComponent<AudioSource>();

        deathAudioSource.Stop();
        moveAudioSource.Stop();
        switchGravAudioSource.Stop();

        deathAudioSource.volume = 0.15f;
        moveAudioSource.volume = 0.15f;
        switchGravAudioSource.volume = 0.15f;

        deathAudioSource.clip = deathSound;
        moveAudioSource.clip = moveSound;
        switchGravAudioSource.clip = switchGravSound;
    }
}
