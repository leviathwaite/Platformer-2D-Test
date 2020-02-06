using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ChestPickup2D : MonoBehaviour {

	Animator anim;
	bool open = false;

	public ParticleSystem particleSystem;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D other){

		if(!open){
		//Checks if other gameobject has a Tag of Player
			if(other.gameObject.tag == "Player"){
				open = true;
				anim.SetBool("open", open);
				particleSystem.Play();
			}
		}
	}
}
