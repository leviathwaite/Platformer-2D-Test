using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement2D : MonoBehaviour {

	[SerializeField]float maxSpeed = 10f;
	[SerializeField]float jumpForce = 10f;
	[SerializeField]float dragFactor = 0.99f;
	[SerializeField]float walkingAcceleration = 5f;
	[SerializeField]float runningAcceleration = 10f;
	[SerializeField]float currentAcceleration = 0f;
	[SerializeField]float extraGravity = -50f;


	private Rigidbody2D rb;
	private Vector2 input;
	private Vector2 movement;
	private bool isRunning = false;
	private bool isJumping = false;
	private bool isGrounded = false;
	private float doubleTapTimer;
	private float doubleTapTime = 5f;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		CheckIfGrounded();
		GetInput();
		Move();
	}

	void CheckIfGrounded(){

		float rayLength = 1f;

		Debug.DrawRay(transform.position, -Vector2.up * rayLength, Color.red, 1f,false);
		 /* 
		 RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if (hit.collider != null) {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);
            float heightError = floatHeight - distance;
            float force = liftForce * heightError - rb2D.velocity.y * damping;
            rb2D.AddForce(Vector3.up * force);
        }
		*/
	}

	void GetInput(){
		input.x = Input.GetAxis("Horizontal");
		if(Input.GetButton("Jump")){
			isJumping = true;
		}
	}

	void Move(){
		// TODO change acceleration to a curve, use sin wave
		currentAcceleration = runningAcceleration;

		movement.x = input.x * currentAcceleration * Time.deltaTime;
		movement.y = 0;

		rb.velocity = movement;
	}
}
