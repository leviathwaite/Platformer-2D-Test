using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script works with animator to swap scripts for player states

public class PlayerStateController : MonoBehaviour {

	private Animator anim;

	private PlatformerMovement2D playerMovement;

	enum PlayerMovement{
		IDLE, WALKING, RUNNING, JUMPING
	}

	enum PlayerCondition{
		NORMAL, DEAD
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		playerMovement = GetComponent<PlatformerMovement2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GetStateFromAnimator(){

		// get current base state
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

		// find current state
		/*
			// get current state
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		// int stateInfo = anim.GetCurrentAnimatorStateInfo(0).
		// tagHash 0
		// nameHash changes for different states
		// idlehash = 2081823275
		// shortNameHash = 2081823275

		// debug
		//Debug.Log(stateInfo.shortNameHash);

		// handle chase state, wish I could use a switch statement
		if(stateInfo.shortNameHash == idleHash){
			Debug.Log("Idle");
			state = State.Idle;
		}else if(stateInfo.shortNameHash == chaseHash){
			Debug.Log("ChasePlayer");
			state = State.Chase;
		}else if(stateInfo.shortNameHash == attackHash){
			Debug.Log("Attack");
			state = State.Attack;
		}else if(stateInfo.shortNameHash == attackHash){
			Debug.Log("Flee");
			state = State.Flee;
		}else if(stateInfo.shortNameHash == attackHash){
			Debug.Log("Dead");
			state = State.Dead;
		}		
	}
	*/

	}
}
