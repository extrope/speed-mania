using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishEvent : MonoBehaviour {
	void OnTriggerEnter() {
		Extensions.GetObject("TimeController").GetOnlyComponent<TimerController>().EndTimer();
		Extensions.GetObject("GameManager").GetComponent<GameManager>().Win();
	}
}
