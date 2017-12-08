using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	} 
	public int scoreValue = 1;
	public KeyCode shotKey = KeyCode.C;
	public KeyCode downKey = KeyCode.S;
	public KeyCode upKey = KeyCode.W;
	public KeyCode rightKey = KeyCode.D;
	public KeyCode leftKey = KeyCode.A;
	public float driveSpeed = 5.0f;
	int driveDirection = 0;
	public float rotationSpeed = 100.0f;
	int rotationDirection = 0;

	public int score = 1;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag.Equals ("shot")) {
			if (score == 1) {
				tank2score.score += scoreValue;
			}
			else {
				tank1score.score += scoreValue;
			}
			Destroy (col.gameObject);
		}
	}
	// Update is called once per frame
	void Update () {

		{
			if (Input.GetKeyDown (shotKey) && Time.time > nextFire)
			{
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			}
		}

		if (Input.GetKeyDown (upKey)) {
			driveDirection = 1;
		}
		if (Input.GetKeyUp (upKey) && driveDirection == 1) {
			driveDirection = 0;
		}


		if (Input.GetKeyDown (downKey)) {
			driveDirection = -1;
		}
		if (Input.GetKeyUp (downKey) && driveDirection == -1) {
			driveDirection = 0;
		}

		//rotation
		if (Input.GetKeyDown (rightKey)) {
			rotationDirection = -1;
		}
		if (Input.GetKeyUp (rightKey) && rotationDirection == -1) {
			rotationDirection = 0;
		}


		if (Input.GetKeyDown (leftKey)) {
			rotationDirection = 1;
		}
		if (Input.GetKeyUp (leftKey) && rotationDirection == 1) {
			rotationDirection = 0;
		}
	}
	void FixedUpdate () {
		transform.position +=  transform.rotation * new Vector3(0, driveSpeed * driveDirection, 0) * Time.deltaTime;
		transform.RotateAround (transform.position, new Vector3 (0, 0, 1), rotationSpeed * rotationDirection * Time.deltaTime);
	}

}
