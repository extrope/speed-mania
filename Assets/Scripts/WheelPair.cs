using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPair : MonoBehaviour {
	private Car car;
	public Car.Motion motionMultipliers;
	public Car.Motion motionCurrent;
	
	void Start() {
		this.car = this.gameObject.GetAncestor(2).GetOnlyComponent<Car>();
	}
	
	void FixedUpdate() {
		this.motionCurrent = this.car.motionCurrent * this.motionMultipliers;
	}
}