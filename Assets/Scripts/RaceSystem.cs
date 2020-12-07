using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSystem : MonoBehaviour {
	public static int mapIndex = 0;
	
	public GameObject map;
	public GameObject pathsVolume;
	public GameObject player;
	
	void Start() {
		StartMap();
		StartPlayer();
	}
	
	void StartMap() {
		var gameObject = Instantiate<GameObject>(Resources.Load<GameObject>("Maps/" + mapIndex.ToString()));
		gameObject.name = "Map";
		this.pathsVolume = gameObject.GetDescendant("Paths", "Volume");
		this.map = gameObject;
	}
	
	void StartPlayer() {
		var gameObject = Extensions.GetObject("Car");
		Debug.Log(gameObject == null);
		var transform = gameObject.transform;
		var start = this.map.GetChild("Start Point").transform;
		transform.position = start.position;
		transform.rotation = start.rotation;
		gameObject.SetActive(true);
		this.player = gameObject;
		Extensions.GetObject("Main Camera").SetActive(true);
	}
}