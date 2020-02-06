using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementVariableJump2D : MonoBehaviour {

	// movement variables
	[SerializeField]float maxMovementSpeed = 10;
	[SerializeField]float currentMovementSpeed = 0;
	[SerializeField]float acceleration = 1f;


	// jumping variables
	[SerializeField]float jumpStrength;
	[SerializeField]float minJumpMaxVelocity;
	
	private Vector2 movement;

	bool isJumping = false;

	Rigidbody2D rb;


	void Start(){
		rb = GetComponent<Rigidbody2D>();
	}

	void Update(){
		CheckIfGrounded();
		Move();
		Jumping();
	}

	void CheckIfGrounded(){

	}

	void Move(){
	}

	void Jumping()
	{
	if (Input.GetButtonDown("Jump") && !isJumping)
	{
		Debug.Log("trying to jump");
		// Instantly push the avatar upward.
		rb.AddForce(Vector2.up * jumpStrength * Time.deltaTime, ForceMode2D.Impulse);
		isJumping = true;
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
