using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishEvent : MonoBehaviour
{
    public static FinishEvent instance;
    GameManager manager;
  
   
    void start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void OnTriggerEnter()
    {
       manager.Win();
       TimerController.instance.EndTimer();
    }
}
