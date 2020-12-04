using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown qualityDropdown;
    
    public Dropdown resolutionDropdown;
    
    // Start is called before the first frame update
    Resolution[] resolutions;
    

    void Start()
    {
        // Setting Quality dropdown value to current quality setting
        int index = QualitySettings.GetQualityLevel();//qualityDropdown.value;
        qualityDropdown.value =  index;
        
        // Setting Resolution dropdown value to current screen setting
        // Collect array of possible resolutions
        resolutions = Screen.resolutions;
        // clear list of resolutions within dropdown
        resolutionDropdown.ClearOptions();
        // create list of strings with our options
        List<string> options = new List<string>();
        Debug.Log(resolutions);
        int currentResolutionIndex = 0;
        // loop through each resolution to create our option list
        for ( int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);            

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        // add options list to resolution dropdown
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    // function to determine the sound Volume set by user
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    // function to determine the Quality set by user
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    // function to determine the Fullscreen set by user
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
