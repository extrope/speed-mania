using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
	[System.Serializable]
	public struct Motion {
		public static Motion operator *(Motion x, Motion y) {
			var result = new Motion();
			result.power = x.power * y.power;
			result.brake = x.brake * y.brake;
			result.steer = x.steer * y.steer;
			return result;
		}
		
		public float power;
		public float brake;
		public float steer;
	}
	
	new private Rigidbody rigidbody;
	
	public Motion motionMaximum;
	public Motion motionCurrent;
	
	void Start() {
		this.gameObject.GetOnlyComponent<Rigidbody>().centerOfMass =
			this.gameObject.GetChild("Center of Mass").transform.localPosition;
	}
}
