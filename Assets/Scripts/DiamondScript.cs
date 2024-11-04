using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondScript : MonoBehaviour
{
    private AudioManagerService audioManagerService;

    private void Start()
    {
        audioManagerService = GameObject.Find("/AudioManagerService").GetComponent<AudioManagerService>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioManagerService.diamondAudioSource.Play();
            Destroy(gameObject);
        }
    }
}
