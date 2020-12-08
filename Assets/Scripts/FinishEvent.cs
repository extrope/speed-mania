using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishEvent : MonoBehaviour {
	void OnTriggerEnter() {
		Extra.GetRootObject("TimeController").GetOnlyComponent<TimerController>().EndTimer();
		Extra.GetRootObject("GameManager").GetComponent<GameManager>().Win();
	}
}
