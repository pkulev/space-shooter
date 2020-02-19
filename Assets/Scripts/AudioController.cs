using System;
using UnityEngine;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour
{
    public Sound[] sounds;

    [Header("REFACTOR THIS OUT")]
    public AudioMixer musicMixer;
    public AudioMixer effectsMixer;

    public static AudioController Instance { get; private set; } = null;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        UpdateMixerSettings();
        Play("music");
    }

    private void UpdateMixerSettings() {
        musicMixer.SetFloat("volume", Options.MusicVolume);
        effectsMixer.SetFloat("volume", Options.EffectsVolume);
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning($"Sound '{name}' was not found.");
            return;
        }

        s.source.Play();
    }
}