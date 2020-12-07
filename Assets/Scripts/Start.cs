using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour {
	void OnTriggerEnter() {
		Extensions.GetObject("TimeController").GetOnlyComponent<TimerController>().BeginTimer();
	}
}
