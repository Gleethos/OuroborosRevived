using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_script : MonoBehaviour {
	private player_script spieler;
	private bool entry = false;
	string item;

	void Start () {
		
		spieler = GameObject.FindObjectOfType(typeof(player_script)) as player_script;

	}
	
	void Update () {
		if (entry) {
			if (Input.GetKeyDown (KeyCode.F)) {
				item = this.tag;
				spieler.setItemHolder (item);
				spieler.printItemHolder ();
				spieler.saveItemHolder ();
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log ("Player detected");
			entry = true;

		}
	}

	void OnTriggerExit2D(Collider2D coll){
		entry = false;
	}
}
