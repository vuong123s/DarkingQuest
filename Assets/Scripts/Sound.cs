using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public AudioMixer myMixer;
    public Slider musicSlider;
    public Slider SFXSlider;

    private const float MinVolume = 0.0001f;

    private void Start()
    {
        if (musicSlider != null)
        {
            SetMusicVolume(musicSlider.value);
        }

        if (SFXSlider != null)
        {
            SetSFXVolume(SFXSlider.value);
        }
    }

    public void SetMusicVolume(float value)
    {
        SetMixerVolume("music", value, nameof(SetMusicVolume));
    }

    public void SetSFXVolume(float value)
    {
        SetMixerVolume("SFX", value, nameof(SetSFXVolume));
    }

    private void SetMixerVolume(string parameter, float value, string caller)
    {
        if (myMixer == null)
        {
            Debug.LogWarning($"Sound.{caller}: AudioMixer is not assigned.");
            return;
        }

        var clampedValue = Mathf.Max(value, MinVolume);
        myMixer.SetFloat(parameter, Mathf.Log10(clampedValue) * 20f);
    }
}
