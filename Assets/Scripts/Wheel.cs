using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour {
	private WheelPair pair;
	
	new private Transform transform;
	new private WheelCollider collider;
	
	void Start() {
		this.pair = this.gameObject.GetParent().GetOnlyComponent<WheelPair>();
		this.transform = this.gameObject.GetChild("Mesh").transform;
		this.collider = this.gameObject.GetOnlyComponent<WheelCollider>();
	}
	
	void Update() {
		this.collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
		this.transform.position = position;
		this.transform.rotation = rotation;
	}
	
	void FixedUpdate() {
		var motion = this.pair.motionCurrent;
		var collider = this.collider;
		collider.motorTorque = motion.power;
		collider.brakeTorque = motion.brake;
		collider.steerAngle = motion.steer;
	}
}
