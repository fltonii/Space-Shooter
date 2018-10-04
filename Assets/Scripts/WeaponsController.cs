using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour {
	public GameObject shot;

	public Transform shotSpawn;
	private AudioSource audioSource;
	public float fireRate;
	public float delay;

	void Start () {
		audioSource = GetComponent<AudioSource>();
		StartCoroutine(Shoot());
	}

	IEnumerator Shoot() {
		yield return new WaitForSeconds(delay);
		while(true) {
			fireWeapon();
			yield return new WaitForSeconds(fireRate);
		}
	}

	void fireWeapon(){
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play();
	}
}
