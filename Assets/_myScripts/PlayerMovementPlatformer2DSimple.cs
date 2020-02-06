using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]


public class PlayerMovementPlatformer2DSimple : MonoBehaviour {

	public bool cameraFollow = false;
	CameraLerpSmoothFollow cameraFollowPlatformer;

	[SerializeField]float maxSpeed = 10f;
	[SerializeField]float acceleration = 2;
	[SerializeField]float inAirAcceleration = 1;
	[SerializeField]float deceleration = 2;
	[SerializeField]float extraGravity = -10f;

	// jumping variables
	[SerializeField]float jumpStrength = 500;
	[SerializeField]float minJumpMaxVelocity = 300;

	Rigidbody2D rb;
	SpriteRenderer sr;
	float curSpeed = 0f;
	bool jumpPressed = false;
	bool isJumping = false;

	CapsuleCollider2D playerCollider;

	// Debug, TODO change to local variable
	public Transform groundCheck;
	// if facing left use groundCheckRight to create a wiley coyote effect
	// and use groundCheckLeft or combo of ground checks to play on edge animation	
	public Transform groundCheckLeft;
	public Transform groundCheckRight;
	public LayerMask groundLayerMask;

	public bool isGrounded = false;
	// TODO do I need this?
	bool groundedLastFrame = false;

	// Use this for initialization
	void Start () {
		if(cameraFollow){
			// cameraFollowPlatformer2DScript = Camera.main.GetComponent<CameraFollowPlatformer2D>();
			cameraFollowPlatformer = Camera.main.GetComponent<CameraLerpSmoothFollow>();
		}
		// TODO should it be in awake instead?
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();

		playerCollider = GetComponent<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
		checkGrounded();

		jumpPressed = Input.GetButton("Jump");

		// Cap top speed by adjusting acceration
		/*
		var horizontal = Input.GetAxis("Horizontal");
 		var acceleration = MaxInitialAcceleration * horizontal;
 		acceleration *= Mathf.Clamp(1.0f - (currentVelocity / MaxSpeed), 0.0f, 1.0f);
 		currentVelocity += acceleration * Time.deltaTime;
		 */
		

		// check if added input
		float horizontal = Input.GetAxis("Horizontal");

		// TODO change drag to not stop the player so soon while in the air

		// reset current speed
		// TODO i think this is wrong
		if(horizontal < 0.1f && horizontal > -0.1f){
			if(curSpeed > deceleration){
				curSpeed -= deceleration;
			}else{
				curSpeed = 0;
			}
			
		}else{
			// add to current speed up to maxSpeed
			if(!isJumping){
				curSpeed += horizontal * acceleration;
			}else{
				curSpeed += horizontal * inAirAcceleration;
			}
			
			if(curSpeed > maxSpeed){
				curSpeed = maxSpeed;
			}else if(curSpeed < -maxSpeed){
				curSpeed = - maxSpeed;
			}
		}

		ChangeDirection();
	}

	void checkGrounded(){
		// isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		// check if velocity == 0 and collision with ground
		
		// check which way character is facing and use opposite ground check
		// Transform groundCheck;
		// facing left
		if(sr.flipX == true){
			groundCheck = groundCheckRight;
		}else{
			groundCheck = groundCheckLeft;
		}

		if(rb.velocity.y == 0 | Physics2D.Linecast(transform.position, groundCheck.position, groundLayerMask)){
			isGrounded = true;
		}else { 
			isGrounded = false;
		}
		 
	}

	void FixedUpdate(){
		
		Jump();

		// need to check if grounded for jumping
		if(isGrounded){
			// grounded = false;
			
			Move();
		}else{
			// fall faster after jump
			rb.AddForce(-Vector2.up * extraGravity);

			MoveInAir();
		}

		
	}

	void Move(){
		rb.velocity = new Vector2(curSpeed, rb.velocity.y);
	}

	void MoveInAir(){
		// TODO need to slow movement in air, maybe in input...
		// rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
		rb.velocity = new Vector2(curSpeed, rb.velocity.y);
	}

	void ChangeDirection(){
		// facing right
		if(curSpeed > 0 && sr.flipX){
			sr.flipX = false;

			flipPlayerColliderOffsetX();

			if(cameraFollow){
				// cameraFollowPlatformer.flipCameraOffsetX();
			}
		// facing left
		}else if(curSpeed < 0 && !sr.flipX){
			sr.flipX = true;

			flipPlayerColliderOffsetX();

			if(cameraFollow){
				// cameraFollowPlatformer.flipCameraOffsetX();
			}
			
		}
	}

	private void flipPlayerColliderOffsetX(){
		// move player capusle collider
			playerCollider.offset = new Vector2(-playerCollider.offset.x, playerCollider.offset.y);
			
	}

	void Jump(){
	if (jumpPressed && !isJumping)
	{
		jumpPressed = false;
			Debug.Log("trying to jump");
			if(isGrounded){
				// Instantly push the avatar upward.
				rb.AddForce(Vector2.up * jumpStrength * Time.deltaTime, ForceMode2D.Impulse);
				isJumping = true;
			}
			
	
	}
	else if (isJumping)
	{
		if (!Input.GetButton("Jump"))
		{
		// Cancel the jump early.
		var v = rb.velocity;
		v.y = Mathf.Min(v.y, minJumpMaxVelocity * Time.deltaTime);
		rb.velocity = v;
		isJumping = false;
		}
		else if (rb.velocity.y <= minJumpMaxVelocity * Time.deltaTime)
		{
		// The jump has now progressed beyond the point where it can be canceled early.
		isJumping = false;
		}
	}
	}

}
