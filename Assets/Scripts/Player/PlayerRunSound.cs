using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunSound : MonoBehaviour
{
    public AudioSource audioSource;
    private void Awake()
    {

    }
    public void PlayRunPlayer(bool playrun)
    {
        if(playrun)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
