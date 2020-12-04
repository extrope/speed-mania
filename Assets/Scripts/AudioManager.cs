using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string BackgroundPref = "BackgroundPref";
    private static readonly string SFXPref = "SFXPref";
    private int firstPlayInt;
    public Slider backgroundSlider, sfxSlider;
    private float backgroundFloat, sfxFloat;

    public AudioSource backgroundAudio;
    public AudioSource[] sfxAudio;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        // First time setup for sound
        // Set sound to default settings
        if(firstPlayInt == 0)
        {
            // Set the values
            backgroundFloat = .25f;
            sfxFloat = .75f;
            // Update the values in the game
            backgroundSlider.value = backgroundFloat;
            sfxSlider.value = sfxFloat;
            // Save background and SFX sound settings for future run throughs
            PlayerPrefs.SetFloat(BackgroundPref, backgroundFloat);
            PlayerPrefs.SetFloat(SFXPref, sfxFloat);
            // Change FirstPlay value, as player has played before
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            // Storing previous sound values and applying them to the current game
            backgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            backgroundSlider.value = backgroundFloat;
            
            sfxFloat = PlayerPrefs.GetFloat(SFXPref);
            sfxSlider.value = sfxFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(BackgroundPref, backgroundSlider.value);
        PlayerPrefs.SetFloat(SFXPref, sfxSlider.value);
    }

    // save game whenver focus is lost (i.e. minimising app, closing app, etc)
    void OnApplicationFocus(bool inFocus)
    {
        if(!inFocus)
        {
            SaveSoundSettings();
        }
    }

    // Update volume of audio with new volume settings
    public void UpdateSound()
    {
        
        backgroundAudio.volume = backgroundSlider.value;
        // loop through list of audio samples and apply the new volume 
        for (int i = 0; i < sfxAudio.Length; i++)
        {
            sfxAudio[i].volume = sfxSlider.value;
        }
    }
}
