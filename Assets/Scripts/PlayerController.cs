using System.Collections;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	private Rigidbody ship;
	public Boundary boundary;
	
	public float speed;
	public float tilt;

	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;
	private float nextFire = 0.0f;

	void Start() {
		ship = GetComponent<Rigidbody>();
	}

	void Update() {
		if((Input.GetButton("Fire1") || Input.GetAxis("JoystickFire") == 1) && Time.time > nextFire) {
			nextFire = Time.time + fireRate/1000;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			ship.GetComponent<AudioSource>().Play();
		}

	}
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");	
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		ship.velocity = movement * speed;
		ship.position = new Vector3(
			Mathf.Clamp(ship.position.x, boundary.xMin, boundary.xMax) ,
			0.0f,
			Mathf.Clamp(ship.position.z, boundary.zMin, boundary.zMax)
		);
		ship.rotation = Quaternion.Euler(0.0f, 0.0f, ship.velocity.x * -tilt);
	}
}
