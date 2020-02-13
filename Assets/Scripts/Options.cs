using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Options : MonoBehaviour
{
    public AudioMixer musicAudioMixer;
    public AudioMixer effectsAudioMixer;
    public Toggle autofireToggle;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    private static readonly string _music = "music volume";
    private static readonly string _effects = "effects volume";
    private static readonly string _autofire = "autofire";

    private void Awake() {
        autofireToggle.isOn = GetAutofire();

        musicVolumeSlider.value = GetMusicVolume();
        effectsVolumeSlider.value = GetEffectsVolume();
    }

    public void SetAutofire(bool autofire) {
        PlayerPrefs.SetInt(_autofire, autofire ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetAutofire() {
        return PlayerPrefs.GetInt(_autofire, 1) == 1;
    }

    public void SetMusicVolume(float volume) {
        SetVolume(_music, musicAudioMixer, volume);
    }

    public void SetEffectsVolume(float volume) {
        SetVolume(_effects, effectsAudioMixer, volume);
    }

    public float GetMusicVolume() {
        return PlayerPrefs.GetFloat(_music, 0.0f);
    }

    public float GetEffectsVolume() {
        return PlayerPrefs.GetFloat(_effects, 0.0f);
    }

    /// <summary>
    /// Common method for setting any volume parametrized by setting name and mixer.
    /// </summary>
    /// <param name="settingName">Name of volume setting in PlayerPrefs storage.</param>
    /// <param name="mixer">Mixer to control.</param>
    /// <param name="volume">Volume amount (from -80 to 0).</param>
    private void SetVolume(string settingName, AudioMixer mixer, float volume) {
        mixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat(settingName, volume);
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}