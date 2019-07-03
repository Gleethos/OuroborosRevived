using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorlocked_script : MonoBehaviour {

	private door_script locked; 

	void Start () {
		locked = GetComponentInParent(typeof(door_script)) as door_script;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		locked.isLockedTrue();
	}
}
