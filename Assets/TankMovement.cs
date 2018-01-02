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
	public static float driveSpeed = 3.0f;
	int driveDirection = 0;
	public static float rotationSpeed = 200.0f;
	int rotationDirection = 0;

	public Rigidbody2D rigb;
	public int score = 1;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public int currentShots = 0;
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

		if (Input.GetKeyDown (shotKey) && Time.time > nextFire) {
			if (currentShots < 5) {
				nextFire = Time.time + fireRate;
				GameObject placed = Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
				placed.GetComponents<shot> () [0].owner = this;
				currentShots = currentShots + 1;
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

	void MoveTank(float amount) {
		//if (amount < 0.0001)
		//	return;
		
		Vector3 tmpPos = transform.position;
		Quaternion tmpRot = transform.rotation;

		transform.position +=  transform.rotation * new Vector3(0, driveSpeed * driveDirection, 0) * amount;
		transform.RotateAround (transform.position, new Vector3 (0, 0, 1), rotationSpeed * rotationDirection * amount);
		//Collider2D[] list = new Collider2D[1];
		//if (rigb.OverlapCollider(new ContactFilter2D(), list) > 0) {
		//	transform.position = tmpPos;
		//	transform.rotation = tmpRot;
		//	MoveTank (amount / 2f);
		//}
	}

	void FixedUpdate () {
		MoveTank (Time.deltaTime);
	}

}
