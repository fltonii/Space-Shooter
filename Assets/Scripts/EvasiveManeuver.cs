using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {
	public float dodge;

	public Vector2 startDelay;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;

	public float smoothing;

	private float targetManeuver;
	private Rigidbody enemy;
	public Boundary boundary;
	public float tilt;

	void Start () {
		enemy = GetComponent <Rigidbody> ();
		StartCoroutine (Evade ());
	}

	IEnumerator Evade (){
		yield return new WaitForSeconds (Random.Range (startDelay.x, startDelay.y));
		while (true) {
			targetManeuver = Random.Range (2, dodge) * -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds (Random.Range (maneuverTime.x, maneuverTime.y));
			targetManeuver = 0;
			yield return new WaitForSeconds (Random.Range (maneuverWait.x, maneuverWait.y));
		}
	}
	void FixedUpdate () {
		float newManeuver = Mathf.MoveTowards (enemy.velocity.x, targetManeuver, Time.deltaTime * smoothing);
		enemy.velocity = new Vector3 (newManeuver, 0.0f, enemy.velocity.z);
		
		enemy.position = new Vector3(
			Mathf.Clamp(enemy.position.x, boundary.xMin, boundary.xMax) ,
			0.0f,
			Mathf.Clamp(enemy.position.z, boundary.zMin, boundary.zMax)
		);

		enemy.rotation = Quaternion.Euler(0.0f, 0.0f, enemy.velocity.x * -tilt);
	}
}
