using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour {
 // Use this to reset scene

 	void Update(){
		 if(Input.GetKey(KeyCode.R)){
			ResetLevel();
		 }
	 }

 	void ResetLevel(){
		 SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	 }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
            // Application.LoadLevel(Application.loadedLevel);
    }
}
