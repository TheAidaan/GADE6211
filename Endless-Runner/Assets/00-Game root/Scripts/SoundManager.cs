using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip[] SoundEffects = new AudioClip[5];
    public static AudioClip[] Music = new AudioClip[2];

    static SoundManager sound;
    static AudioSource _audio;
    // Start is called before the first frame update
    void Awake()
    {
        sound = this;
        SoundEffects = Resources.LoadAll<AudioClip>("Audio/SoundEffects");

        _audio = GetComponent<AudioSource>();
    }

    public static void PlaySoundEffect(int id)
    {
        _audio.PlayOneShot(SoundEffects[id]);
    }

}
