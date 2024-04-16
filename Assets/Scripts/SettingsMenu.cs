using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private static readonly string MusicVolume = "MusicVolume";
    private static readonly string EffectVolume = "EffectVolume";

    public AudioMixer audioMixer;
    public AudioMixer effectMixer;

    public Slider volumeSlider;
    public Slider effectSlider;
    public Slider volumeGameSlider;
    public Slider effectGameSlider;
   
    public void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(MusicVolume);
        effectSlider.value = PlayerPrefs.GetFloat(EffectVolume);
        volumeGameSlider.value = volumeSlider.value;
        effectGameSlider.value = effectSlider.value;

    }

    public void SetVolume(float volume)
    {
        
        audioMixer.SetFloat("volume", volume);
        volumeGameSlider.value = volume;
    }

    public void SetEffectVolume(float volumeEffect)
    {
        effectMixer.SetFloat("effectVolume", volumeEffect);
        effectGameSlider.value = volumeEffect;
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(MusicVolume, volumeSlider.value);
        PlayerPrefs.SetFloat(EffectVolume, effectSlider.value);
    }

   

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSettings();
        }
    }
}
