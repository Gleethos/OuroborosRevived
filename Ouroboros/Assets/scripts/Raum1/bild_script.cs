using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bild_script : MonoBehaviour {
	private bool interactionOcc = false;
	private int entry = 0;

	void Update () {

		if (entry == 1) {
			if (Input.GetKey (KeyCode.F)) {
				interactionOcc = true;
				}
			}
		}
		


	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			entry = 1;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			entry = 0;
		}
	}

	public bool interactionOccurred(){
		bool occ = interactionOcc;
		interactionOcc = false;
		return occ;	
	}
	
}
