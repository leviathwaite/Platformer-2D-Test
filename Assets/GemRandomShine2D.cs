using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRandomShine2D : MonoBehaviour {

	float maxRandomTime = 10f;
	float timer = 0;
	Animator anim;

	int shineHash;

	void Start () {
		anim = GetComponent<Animator>();
		shineHash = Animator.StringToHash("shine");
    	// int runStateHash = Animator.StringToHash("Base Layer.Run");
	}

	void ResetTimer(){
		timer = Random.Range(0, maxRandomTime) + Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		CheckTimer();
	}

	void CheckTimer(){
		if(Time.time > timer){
			ResetTimer();
			// using hash instead of String for optimization
			// anim.SetTrigger("shine");
			anim.SetTrigger(shineHash);
		}
	}
}
