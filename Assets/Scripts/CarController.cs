﻿using System.Collections;
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
	// Loading objects for colour change
    public MeshRenderer vehicleBody;
    public MeshRenderer vehicleTire;
    public MeshRenderer vehicleSpoiler;
	private MeshRenderer vehiclePart;
	private Color partColour;
	private string[] VehicleColourPref = new string[3];	
	
	new void Start() {	
	
 		VehicleColourPref[0] = "VehicleBody";
		VehicleColourPref[1] = "VehicleTire";
		VehicleColourPref[2] = "VehicleSpoiler";		

		// Load vehicle RGB values			
        for (int i = 0; i < VehicleColourPref.Length; i++)
		{
			if(i == 0){vehiclePart = vehicleBody;}
			if(i == 1){vehiclePart = vehicleTire;}
			if(i == 2){vehiclePart = vehicleSpoiler;}
			SaveRGBValue(VehicleColourPref[i], vehiclePart);
		}
		

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
	// Load RGB values to vehicle
	void SaveRGBValue(string bodyPart, MeshRenderer vehiclePart)
	{
		// Get PlayerPrefs Colours for Vehicle
		ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString(bodyPart+"ColourPref"), out partColour);
		// Set Vehicle colour value
		vehiclePart.material.color = partColour;        
		vehiclePart.material.SetColor("_EmmisionColor", partColour);
	}
}
