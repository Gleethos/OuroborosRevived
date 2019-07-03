using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderListener : MonoBehaviour {
	private bool checkEY = false;
	private bool checkEX = false;

	void OnTriggerEnter2D(Collider2D coll) {
		checkEY = true;
	}
	void OnTriggerExit2D(Collider2D coll) {
		checkEX = true;
	}

	public bool getColliderStateEnter(){
		bool state = checkEY;
		checkEY = false;
		return state;
	}

	public bool getColliderStateExit(){
		bool state = checkEX;
		checkEX = false;
		return state;
	}

}
