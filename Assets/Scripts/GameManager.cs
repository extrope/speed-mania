using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Hosting;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    public GameObject FinishText;
    public GameObject MenuButton;
    public GameObject NextMapButton;


    void awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
            Destroy(gameObject);
    }
    public void Win()
    {
        FinishText.SetActive(true);
        NextMapButton.SetActive(true);
        MenuButton.SetActive(true);
        Time.timeScale = 0.5f;
    }

 
}

