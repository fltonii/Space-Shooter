using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
	// Object
	private Rigidbody hazard;

	// Animations
	public GameObject hazardExplosion;
	public GameObject playerExplosion;
	
	// Score incrementation
	private GameController gameController;
	public int scoreValue;


	void Start() {
		hazard = GetComponent<Rigidbody>();
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if(gameController == null){
			Debug.Log("cannot find 'GameController' reference");
		}
	}
	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Boundary") || other.CompareTag("Enemy")) {
			return;	
		}
		if(other.CompareTag("Ship")) {
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);		   
			Destroy(other.gameObject);
			gameController.GameOver();
		} else if(hazardExplosion != null){
			Destroy(hazard.gameObject);
			Instantiate(hazardExplosion, transform.position, transform.rotation);
			Destroy(other.gameObject);
		   	gameController.AddScore(scoreValue);
		}
    }
}
