using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip background;
    public AudioSource audio;


    private void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
        
        audio.volume = 0.1f;
        audio.PlayOneShot(background);
    }

    private void Update()
    {
        if (!audio.isPlaying)
            audio.PlayOneShot(background);
    }
}
