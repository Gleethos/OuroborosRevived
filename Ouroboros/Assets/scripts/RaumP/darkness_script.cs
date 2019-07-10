using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkness_script : MonoBehaviour {
	private bool activScript= false;

	void OnTriggerEnter2D(Collider2D coll){
		activScript = true;
	}
	public bool checkDark(){
		return activScript;
	}
}
