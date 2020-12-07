using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
  
    public GameObject Finish;

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

        Finish.SetActive(true);
        Time.timeScale = 0.5f;
    }

 
}

