using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlatformer2D : MonoBehaviour {

	[SerializeField]Transform target;
	// TODO does it need to store orginal
	[SerializeField]Vector3 targetOffset;

	[SerializeField]Vector3 cameraSpeed;

	Vector3 movement;
	float orgZ;


	// amount target has to move before camera follows
	[SerializeField]Vector2 deadZone;


	// Use this for initialization
	void Start () {
		orgZ = transform.position.z;
		movement = new Vector3(target.position.x, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {

		// check x
		if(checkDeadZone(transform.position.x, target.position.x + targetOffset.x)){
			// TODO change to Lerp or give camera a speed
			// movement.x = target.position.x + targetOffset.x;
			movement.x = Mathf.Lerp(transform.position.x, target.position.x + targetOffset.x, cameraSpeed.x * Time.deltaTime);
		}
/*
		if(target.position.y > transform.position.y + 2){
			if(checkDeadZone(transform.position.y, target.position.y + targetOffset.y)){
			// TODO change to Lerp or give camera a speed
			movement.y = target.position.y + targetOffset.y;
		}
		}


		// check y
		if(checkDeadZone(transform.position.y, target.position.y + targetOffset.y)){
			// TODO change to Lerp or give camera a speed
			movement.y = target.position.y + targetOffset.y;
		}
		*/
		

		// adjust camera z position
		movement.z = orgZ;

		// apply movement
		transform.position = movement;
	}

	bool checkDeadZone(float currentLocation, float targetLocation){
		if(Mathf.Abs(currentLocation- targetLocation) > deadZone.x){
			return true;
		}
		return false;
	}

	public void flipCameraOffsetX(){
		targetOffset.x = -targetOffset.x;
	}
}
