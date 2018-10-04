using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
	// Hazard spawn controll
	public GameObject [] hazards;
	private GameObject hazard;
	public Vector3 spawnPosit;
	public int hazardCount;
	
	// Wave controll
	public float spawnTime;
	public float startTime;
	public float waveTime;
	
	// Score
	public Text scoreText;
	private int score;

	// Flow
	public Text gameOverText;	
	public Text restartText;
	private bool restart;
	private bool gameOver;

	void Start() {
		gameOver = false;
		gameOverText.enabled = false;

		restartText.enabled = false;

		DefSpawnPosit();
		StartCoroutine(SpawnWaves());
	}

	void Update() {
		gameOverText.enabled = gameOver;
		restartText.enabled = gameOver;
		if(gameOver){
			if(Input.GetButtonDown("Restart")) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void RandomizeHazards() {
		int factor = Random.Range(0, hazards.Length);
		hazard = hazards[factor];
	}

	IEnumerator SpawnWaves(){
		while(!gameOver){
			yield return new WaitForSeconds(startTime);
			for(int i = 0; i < hazardCount; i++) {
				RandomizeHazards();
				Instantiate(hazard, spawnPosit, Quaternion.identity);
				DefSpawnPosit();
				yield return new WaitForSeconds(spawnTime);
			}
			yield return new WaitForSeconds(waveTime);
		}
	}

	void DefSpawnPosit(){
		float x = Random.Range(-7.0f, 7.0f);
		spawnPosit = new Vector3(x, 0.0f, 17);
	}

	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore();
	}

	void UpdateScore() {
		scoreText.text = "score: " + score.ToString();
	}

	public void GameOver() {
		gameOver = true;
		gameOverText.enabled = true;
	}
}
