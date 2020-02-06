using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllCollected : MonoBehaviour {

	public int numberOfChildren = 0;

	// Use this for initialization
	void Start () {
		checkNumberOfChildren();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void checkNumberOfChildren(){
		numberOfChildren = transform.childCount;
		Debug.Log("numberOfChildren: " + numberOfChildren);
		Debug.Log(transform.childCount);

		if(numberOfChildren <= 1){
			if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount){

				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
			}else{
				Debug.Log("No more scenes");
			}
		}
	}
}
