using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Vehicle Selection
    //public GameObject camera1;
    //public GameObject camera2;
    public GameObject GarageScene;
    public GameObject MainMenuScene;
    
    public void GoToGarage()
    {

        //camera2.SetActive(true);
        //camera1.SetActive(false);
        GarageScene.SetActive(true);        
        MainMenuScene.SetActive(false);

    }

    public void ExitTheGame()
    {
        Debug.Log("QUIT THE GAME");
        Application.Quit();
    }

 
}
