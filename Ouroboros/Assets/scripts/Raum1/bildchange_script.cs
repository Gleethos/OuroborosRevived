using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bildchange_script : MonoBehaviour {

	private bool interactionOcc = false;
	private int entry = 0;
	private bool solution = false;

	[SerializeField]
	private Sprite bild_saved;
	[SerializeField]
	private Sprite bild_destroyed;
	
	void Update () {
		if (solution == false) {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = bild_saved;
		} else if (solution == true) {
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = bild_destroyed;
				
		}
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

	public void ripPainting(){
		solution = true;
	}

	public bool getSolution(){
		return solution;
	}

}
