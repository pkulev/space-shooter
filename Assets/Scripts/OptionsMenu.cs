using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    public AudioMixer musicAudioMixer;
    public AudioMixer effectsAudioMixer;
    public Toggle autofireToggle;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    public InputField playerNameInputField;

    private void Awake() {
        playerNameInputField.text = Options.PlayerName;
        autofireToggle.isOn = Options.Autofire;

        UpdateMusic(Options.MusicVolume);
        UpdateEffects(Options.EffectsVolume);
    }

    private void UpdateMusic(float volume) {
        musicVolumeSlider.value = volume;
        musicAudioMixer.SetFloat("volume", volume);
    }

    private void UpdateEffects(float volume) {
        effectsVolumeSlider.value = volume;
        effectsAudioMixer.SetFloat("volume", volume);
    }

    public void SetAutofire(bool autofire) {
        Options.Autofire = autofire;
    }

    public void SetMusicVolume(float volume) {
        Options.MusicVolume = volume;
        musicAudioMixer.SetFloat("volume", volume);
    }

    public void SetEffectsVolume(float volume) {
        Options.EffectsVolume = volume;
        effectsAudioMixer.SetFloat("volume", volume);
    }

    public static void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetPlayerName(string name) {
        Options.PlayerName = name;
    }
}