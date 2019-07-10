using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * You can put it on a collider
 * and than you can check the state of the collider
 * with getColliderStateEnter or getColliderStateExit
 */
 
public class ColliderListener : MonoBehaviour {
	private bool checkEY = false;
	private bool checkEX = false;

	void OnTriggerEnter2D(Collider2D coll) { 
        if(coll.CompareTag("Player"))
		    checkEY = true;
	}
	void OnTriggerExit2D(Collider2D coll) {
        if (coll.CompareTag ("Player"))
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
