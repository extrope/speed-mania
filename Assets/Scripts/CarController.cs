using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : Car {
	void FixedUpdate() {
		var motion = new Motion();
		motion.power = Input.GetAxis("Vertical");
		motion.steer = Input.GetAxis("Horizontal");
		this.motionCurrent = motion * this.motionMaximum;
	}
}
