using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    void OnTriggerEnter()
    {
        TimerController.instance.BeginTimer();
    }
}
