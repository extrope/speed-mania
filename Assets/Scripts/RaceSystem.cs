using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Assertions;
using TMPro;

public class RaceSystem : MonoBehaviour {
	public static readonly string SCENE_NAME = "Race";
	
	private static string mapName = "0";
	
	[System.Serializable]
	private class Overlay {
		public GameObject finish = null;
		public GameObject pause = null;
		public GameObject stray = null;
		public GameObject strayCount = null;
		public GameObject shadow = null;
		public GameObject timer = null;
		
		[HideInInspector] public Text strayCountText = null;
		[HideInInspector] public TextMeshProUGUI timerText = null;
	}
	
	[System.Serializable]
	private class Map {
		public GameObject root = null;
		public GameObject path = null;
		public GameObject finish = null;
	}
	
	public enum Phase {
		PLAYING,
		PAUSED,
		FINISHED
	}
	
	public static void LoadMap(string mapName) {
		RaceSystem.mapName = mapName;
		SceneManager.LoadScene(SCENE_NAME, LoadSceneMode.Single);
	}
	
	[SerializeField] private Overlay overlay = null;
	[SerializeField] new private GameObject camera = null;
	[SerializeField] private GameObject player = null;
	
	public float strayLimit;
	
	private PlayerController playerController;
	private Map map;
	private float straySince;
	private Phase phase;
	private float timeStart;
	private float timeElapsed;
	
	private void Start() {
		Time.timeScale = 1f;
		LoadMap();
		InitPlayer();
		InitOverlay();
		this.straySince = Time.time;
		this.timeStart = Time.time;
	}
	
	public GameObject GetPlayer() {
		return this.player;
	}
	
	public GameObject GetMapRoot() {
		return this.map.root;
	}
	
	public GameObject GetPath() {
		return this.map.path;
	}
	
	public GameObject GetFinish() {
		return this.map.finish;
	}
	
	public Phase GetPhase() {
		return this.phase;
	}
	
	public bool IsPathCollider(Collider collider) {
		return collider.gameObject.HasParentEqualTo(this.map.path);
	}
	
	private void LoadMap() {
		var gameObject = Instantiate<GameObject>(Resources.Load<GameObject>("Maps/" + RaceSystem.mapName));
		gameObject.name = "Map";
		
		var map = new Map();
		map.root = gameObject;
		map.path = gameObject.TryGetChild("Path");
		map.finish = gameObject.GetChild("Finish");
		this.map = map;
	}
	
	private void InitPlayer() {
		var gameObject = this.player;
		this.playerController = gameObject.GetOnlyComponent<PlayerController>();
		
		var transform = gameObject.transform;
		var spawn = this.map.root.GetChild("Spawn").transform;
		
		transform.position = spawn.position;
		transform.rotation = spawn.rotation;
		
		gameObject.SetActive(true);
		this.camera.SetActive(true);
	}
	
	private void InitOverlay() {
		var overlay = this.overlay;
		overlay.strayCountText = overlay.strayCount.GetOnlyComponent<Text>();
		overlay.timerText = overlay.timer.GetOnlyComponent<TextMeshProUGUI>();
	}
	
	public void CheckStray() {
		if (this.map.path == null) {
			return;
		}
		
		var overlay = this.overlay;
		
		if (this.playerController.IsPathCollided) {
			overlay.stray.SetActive(false);
			overlay.shadow.SetActive(false);
			
			this.straySince = Time.time;
		} else {
			var count = this.strayLimit - (Time.time - this.straySince);
			
			if (count > 0) {
				overlay.strayCountText.text = Mathf.FloorToInt(count).ToString();
			} else {
				Extra.ReloadScene();
				return;
			}
			
			overlay.stray.SetActive(true);
			overlay.shadow.SetActive(true);
		}
	}
	
	private void UpdateTimer() {
		if (this.phase == Phase.PLAYING) {
			this.timeElapsed = Time.time - this.timeStart;
		}
		
		var elapsed = this.timeElapsed;
		
		var min = Mathf.Floor(elapsed / 60f);
		elapsed -= min * 60f;
		var sec = Mathf.Floor(elapsed);
		elapsed -= sec;
		var cs = Mathf.Floor(elapsed * 100);
		
		var strMin = ((int) min).ToString("D2");
		var strSec = ((int) sec).ToString("D2");
		var strCs = ((int) cs).ToString("D2");
		
		this.overlay.timerText.text = $"{strMin}:{strSec}<#DDD>.</color><#BBB>{strCs}</color>";
	}
	
	public void TriggerFinish() {
		this.phase = Phase.FINISHED;
	}
	
	public void Pause() {
		Time.timeScale = 0f;
		this.phase = Phase.PAUSED;
		this.playerController.TogglePause();
		overlay.pause.SetActive(true);
		overlay.shadow.SetActive(true);
	}
	
	public void Resume() {
		overlay.pause.SetActive(false);
		overlay.shadow.SetActive(false);
		this.phase = Phase.PLAYING;
		this.playerController.TogglePause();
		Time.timeScale = 1f;
	}
	
	public void Restart() {
		Extra.ReloadScene();
	}
	
	public void ExitToMenu() {
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
	
	public void ExitGame() {
		Application.Quit();
	}
	
	private void Update() {
		UpdateTimer();
		
		if (this.phase == Phase.PLAYING) {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				Pause();
			}
		} else if (this.phase == Phase.PAUSED) {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				Resume();
			}
		} else if (this.phase == Phase.FINISHED) {
			
		}
	}
	
	private void FixedUpdate() {
		if (this.phase == Phase.FINISHED) {
			Time.timeScale *= 0.99f;
		}
	}
}