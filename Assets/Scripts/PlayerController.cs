using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : Car {
	[System.Serializable]
	public class Audio {
		public AudioSource source = null;
		[Range(-3f, +3f)]
		public float idle = 0.5f;
		public float rate = 500f;
	}
	
	[SerializeField] private RaceSystem system = null;
	[SerializeField] private WheelCollider[] wheelColliders = null;
	[SerializeField] new private Audio audio = null;
	[SerializeField] private float rpmDampening = 0.75f;
	
	public float Rpm { get; private set; }
	
	public bool IsPathCollided { get; private set; }
	
	// Loading objects for colour change
	public MeshRenderer vehicleBody;
	public MeshRenderer vehicleTire;
	public MeshRenderer vehicleSpoiler;
	
	private MeshRenderer vehiclePart;
	private Color partColour;
	private string[] VehicleColourPref = new string[3];	
	
	new void Start() {	
		base.Start();
		InitColor();
		InitAudio();
	}
	
	private void InitColor() {
		this.system = Extra.GetObject("System").GetOnlyComponent<RaceSystem>();
		
		this.IsPathCollided = false;
		
		VehicleColourPref[0] = "VehicleBody";
		VehicleColourPref[1] = "VehicleTire";
		VehicleColourPref[2] = "VehicleSpoiler";		
		
		// Load vehicle RGB values	
		
		for (int i = 0; i < VehicleColourPref.Length; i++) {
			if (i == 0) vehiclePart = vehicleBody;
			if (i == 1) vehiclePart = vehicleTire;
			if (i == 2) vehiclePart = vehicleSpoiler;
			SaveRGBValue(VehicleColourPref[i], vehiclePart);
		}
	}
	
	private void InitAudio() {
		this.audio.source.pitch = audio.idle;
		this.audio.source.Play();
	}
	
	public void TogglePause() {
		var phase = this.system.GetPhase();
		
		if (phase == RaceSystem.Phase.PLAYING) {
			this.audio.source.UnPause();
		} else if (phase == RaceSystem.Phase.PAUSED) {
			this.audio.source.Pause();
		}
	}
	
	private void UpdateRpm() {
		var colliders = this.wheelColliders;
		float total = 0;
		
		foreach (var collider in colliders) {
			total += collider.rpm;
		}
		
		var dampening = this.rpmDampening;
		this.Rpm = ((1 - dampening) * (Mathf.Abs(total) / colliders.Length)) + (dampening * this.Rpm);
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
		
		this.system.CheckStray();
		this.IsPathCollided = false;
		
		this.UpdateRpm();
		this.UpdateSounds();
	}
	
	void UpdateSounds() {
		this.audio.source.pitch = this.audio.idle + (this.Rpm / this.audio.rate);
	}
	
	void OnTriggerEnter(Collider other) {
		if (GameObject.ReferenceEquals(other.gameObject, this.system.GetFinish())) {
			this.system.TriggerFinish();
		}
	}
	
	void OnTriggerStay(Collider other) {
		var path = this.system.GetPath();
		if (path != null && GameObject.ReferenceEquals(other.gameObject.transform.parent.gameObject, path)) {
			this.IsPathCollided = true;
		}
	}
	
	// Load RGB values to vehicle
	void SaveRGBValue(string bodyPart, MeshRenderer vehiclePart) {
		// Get PlayerPrefs Colours for Vehicle
		ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString(bodyPart+"ColourPref"), out partColour);
		// Set Vehicle colour value
		vehiclePart.material.color = partColour;
		vehiclePart.material.SetColor("_EmmisionColor", partColour);
	}
}
