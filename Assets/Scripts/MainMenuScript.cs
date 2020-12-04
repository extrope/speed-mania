using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    /*
    public void GoToGarage()
    {
        SceneManager.LoadScene("Garage");
    }

    public void RacetrackSelection()
    {
        SceneManager.LoadScene("Racetracks");
    }
    */
    public void ExitTheGame()
    {
        Debug.Log("QUIT THE GAME");
        Application.Quit();
    }
}
