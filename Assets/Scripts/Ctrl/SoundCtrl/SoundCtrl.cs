using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType{
    DropWaveBoss,
    SpawnWaveBoss,
    DeathWaveBoss,
    JumpWaveBoss,
    ReadyWaveBoss,
    RendingWaveBoss,
    DeathBoss,
    Wind,
    WitchBoom,
    Song,
    Boom,
    Bless,
    Bullet,
}

public class SoundCtrl : MonoBehaviour
{
    public AudioClip[] Sounds;

    public static AudioClip[] Sound;
    static GameObject player;
    public static SoundCtrl sc;

    private void Awake()
    {
        Sound = new AudioClip[Sounds.Length];
       for(int i = 0; i < Sounds.Length; i++)
        {
            Sound[i] = Sounds[i];
        }

        player = gameObject;
        sc = this;
    }

    IEnumerator _PlaySound(SoundType soundIndex)
    {
        AudioSource ac=  player.AddComponent<AudioSource>();
        ac.volume = 0.3f;
        ac.PlayOneShot(Sound[(int)soundIndex]);

        while (true)
        {
            if (!ac.isPlaying)
                break;

            yield return new WaitForSeconds(0);
        }

        Destroy(ac);
    }


    public void PlaySound(SoundType soundIndex)
    {
        StartCoroutine(_PlaySound(soundIndex));
    }
}
