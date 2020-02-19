using System;
using UnityEngine;
using UnityEngine.Audio;


[Serializable]
public class Sound{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup mixerGroup;

    [Range(0f, 1f)]
    public float volume = 1;

    [Range(.1f, 3f)]
    public float pitch = 1;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
