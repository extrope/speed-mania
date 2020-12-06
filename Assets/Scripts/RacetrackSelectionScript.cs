using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RacetrackSelectionScript : MonoBehaviour
{
    private static readonly string MapSelectionPref = "MapSelectionPref";
    private int mapSelectionIndex = 0;

    // Functions to select desired racetrack
    public void RacetrackSelection_1()
    {
        // Assign and save racetrack number
        mapSelectionIndex = 1;
        PlayerPrefs.SetInt(MapSelectionPref, mapSelectionIndex);
    }

    public void RacetrackSelection_2()
    {
        mapSelectionIndex = 2;
        PlayerPrefs.SetInt(MapSelectionPref, mapSelectionIndex);
    }
    /*
    public void RacetrackSelection_3()
    {
        
    }

    public void RacetrackSelection_3()
    {
        
    }
    */
    public void GoToMap()
    {
        mapSelectionIndex = PlayerPrefs.GetInt(MapSelectionPref);
        if(mapSelectionIndex > 0)
        {
			RaceSystem.mapIndex = mapSelectionIndex - 1;
            SceneManager.LoadScene("Race");
        }
        else
        {
            Debug.Log("SELECT A MAP");
        }
    }
}
