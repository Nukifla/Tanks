using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour {
		
	public static float bulletSpeed = 3.0f;
	public Rigidbody2D rigb;
	public TankMovement owner;

	void Start () {
		rigb = GetComponent<Rigidbody2D> ();
		rigb.velocity = transform.rotation * new Vector3(0, bulletSpeed,0);
		Destroy (gameObject, 4.0f);
	}
	void OnDestroy() {
		owner.currentShots -= 1;
	}
}	