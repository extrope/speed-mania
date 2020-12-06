using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : Car {
	public float strayLimit;
	
	private GameObject path;
	private GameObject strayUI;
	private Text strayText;
	private int pathCollided;
	private float straySince;
	
	private GameObject debugWheel;
	private Quaternion lastRotation;
	
	new void Start() {
		base.Start();
		this.path = Extensions.GetObject("Map", "Paths", "Player");
		this.strayUI = Extensions.GetObject("UI", "Stray", "Base");
		this.strayText = this.strayUI.GetChild("Countdown").GetOnlyComponent<Text>();
		
		//this.debugWheel = this.gameObject.GetDescendant("Wheels", "Rear", "Left");
		//this.lastRotation = this.debugWheel.transform.localRotation;
	}
	
	void FixedUpdate() {
		var motion = new Motion();
		motion.power = Input.GetAxis("Vertical");
		motion.steer = Input.GetAxis("Horizontal");
		this.motionCurrent = motion * this.motionMaximum;
		
		if (this.pathCollided == 0) {
			this.strayUI.SetActive(true);
			var stray = this.strayLimit - (Time.time - this.straySince);
			if (stray <= 0) {
				// TODO: Game Over or whatever
				UnityEditor.EditorApplication.isPlaying = false;
			} else {
				this.strayText.text = Mathf.FloorToInt(stray).ToString();
			}
		} else {
			this.pathCollided = 0;
			this.straySince = Time.time;
			this.strayUI.SetActive(false);
		}
		
		//Debug.Log(this.debugWheel.rpm);
	}
	
	void OnTriggerStay(Collider other) {
		if (GameObject.ReferenceEquals(other.gameObject.TryGetParent(), this.path)) {
			this.pathCollided += 1;
		}
	}
}
