using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarController : Car {
	public float strayLimit;
	
	private RaceSystem system;
	private GameObject strayUI;
	private Text strayText;
	private int pathCollided;
	private float straySince;
	
	new void Start() {
		base.Start();
		this.system = Extensions.GetObject("System").GetOnlyComponent<RaceSystem>();
		this.strayUI = Extensions.GetObject("UI", "Stray", "Base");
		this.strayText = this.strayUI.GetChild("Countdown").GetOnlyComponent<Text>();
		this.pathCollided = 0;
		this.straySince = Time.time;
	}
	
	void FixedUpdate() {
		var velocity = this.gameObject.transform.InverseTransformDirection(rigidbody.velocity).z;
		var motion = new Motion();
		var inputVertical = Input.GetAxis("Vertical");
		motion.steer = Input.GetAxis("Horizontal");
		
		if (velocity * inputVertical >= 0) {
			motion.power = inputVertical >= 0 ? inputVertical : inputVertical / 2;
		} else {
			motion.brake = Mathf.Abs(inputVertical);
		}
		
		this.motionCurrent = motion * this.motionMaximum;
		
		if (this.pathCollided == 0) {
			this.strayUI.SetActive(true);
			var stray = this.strayLimit - (Time.time - this.straySince);
			if (stray <= 0) {
				// TODO: Game Over or whatever
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				return;
			} else {
				this.strayText.text = Mathf.FloorToInt(stray).ToString();
			}
		} else {
			this.pathCollided = 0;
			this.straySince = Time.time;
			this.strayUI.SetActive(false);
		}
	}
	
	void OnTriggerStay(Collider other) {
		if (GameObject.ReferenceEquals(other.gameObject.TryGetParent(), this.system.pathsVolume)) {
			this.pathCollided += 1;
		}
	}
}
