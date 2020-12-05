using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    // Quality prefs to hold user's settings
    private static readonly string QualityPref = "QualityPref";
    private static readonly string ResolutionPref = "ResolutionPref";

    // Varaiables to hold int location from dropdown
    private int qLevel, resolutionLevel;

    public AudioMixer audioMixer;

    public TMP_Dropdown qualityDropdown;
    
    public Dropdown resolutionDropdown;
    
    // Start is called before the first frame update
    Resolution[] resolutions;
    

    void Start()
    {
        // Setting Quality dropdown value to current quality setting
        // Check if any preferences have been saved
        if(PlayerPrefs.HasKey("QualityPref"))
        {
            qLevel = PlayerPrefs.GetInt("QualityPref");
            QualitySettings.SetQualityLevel(qLevel);
            qualityDropdown.value = qLevel;
        }
        else 
        {
            qLevel = 0;
            QualitySettings.SetQualityLevel(qLevel);
            qualityDropdown.value = qLevel;                      
        }
        
        // Collect array of possible resolutions
        resolutions = Screen.resolutions;
        // Clear list of resolutions within dropdown
        resolutionDropdown.ClearOptions();
        // Create list of strings with our options
        List<string> options = new List<string>();
        //Debug.Log(resolutions);
        //int currentResolutionIndex = 0;
        // loop through each resolution to create our option list
        for ( int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);            

         //   if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
          //  {
          //      currentResolutionIndex = i;
          //  }
        }
        // add options list to resolution dropdown
        resolutionDropdown.AddOptions(options);         

        if(PlayerPrefs.HasKey("ResolutionPref"))
        {
            resolutionLevel = PlayerPrefs.GetInt("ResolutionPref");
            Screen.SetResolution(resolutions[resolutionLevel].width, resolutions[resolutionLevel].height, true);
            resolutionDropdown.value = resolutionLevel;
            resolutionDropdown.RefreshShownValue();   
        }
        else
        {
            resolutionLevel = 0;
            resolutionDropdown.value = resolutionLevel;
            resolutionDropdown.RefreshShownValue();             
        }    
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
        PlayerPrefs.SetInt(QualityPref, qualityIndex);
    }
    // function to determine the Fullscreen set by user
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
     // Function to determine the resolution set by user
    public void SetResolution()
    {
        resolutionLevel = resolutionDropdown.value;
        Screen.SetResolution(resolutions[resolutionLevel].width, resolutions[resolutionLevel].height, true);
        PlayerPrefs.SetInt(ResolutionPref, resolutionDropdown.value);
    }
}
