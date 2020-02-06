using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePickup : MonoBehaviour {

	int pointsForPickup = 50;

	public ParticleSystem particleSystem;
	public AllCollected allCollected;

	void Start(){
		allCollected = transform.parent.GetComponent<AllCollected>();
	}
	

	void OnTriggerEnter2D(Collider2D other){

		//Checks if other gameobject has a Tag of Player
			if(other.gameObject.tag == "Player"){
				if(allCollected != null){	
					allCollected.checkNumberOfChildren();
				}
				Destroy(gameObject);
			}
		
	}
}