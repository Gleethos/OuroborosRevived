using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablePictureCollider_script : MonoBehaviour {

	private bildchange_script destroy; 
	private bool solution;

	void Update () {
		destroy = GameObject.FindObjectOfType(typeof(bildchange_script)) as bildchange_script;
		solution = destroy.getSolution ();

		if (solution == true) {
			BoxCollider2D temp = GetComponent<BoxCollider2D> ();
			temp.isTrigger = true;
		}
		
	}
}
