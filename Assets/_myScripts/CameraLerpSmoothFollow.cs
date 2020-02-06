using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpSmoothFollow : MonoBehaviour {

	// stores original distance between camera and target
	Vector3 offset;
	// used to move the Camera
	Vector3 newCameraPos;

	// used to smooth lerp
	public float smoothing;
	// used to stop following player if he falls out of screen
	public float lowY;

	[SerializeField]Transform target;

	// Use this for initialization
	void Start () {
		// get Original offset
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
		// update cameras goal
		newCameraPos = target.position + offset;

		// lerp camera toward new position
		transform.position = Vector3.Lerp(transform.position, newCameraPos, smoothing * Time.deltaTime);
	
		// check if player fell off screen
		if(transform.position.y < lowY){
			transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
		}
	}

	// flip X offset when player is facing other direction
	public void flipCameraOffsetX(){
		offset.x = -offset.x;
	}
}
