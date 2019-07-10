using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_script : MonoBehaviour {
	private PlayerController player;
	private bool entry = false;
	string item;

	void Start () {
		
		player = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;

	}
	
	void Update () {
		if (entry) {
			if (Input.GetKeyDown (KeyCode.F)) {
				item = this.tag;
				player.SetItemHolder(item);
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			entry = true;

		}
	}

	void OnTriggerExit2D(Collider2D coll){
		entry = false;
	}
}
