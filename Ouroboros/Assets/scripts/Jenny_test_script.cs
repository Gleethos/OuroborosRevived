using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jenny_test_script : MonoBehaviour {

	private player_script objekt; 
	event_script_P nextEventScript;
	event_script_R2 R3EventScript;

	
	void Start () {
		objekt = GameObject.FindObjectOfType(typeof(player_script)) as player_script;
		nextEventScript = GameObject.FindObjectOfType (typeof(event_script_P)) as event_script_P;
		R3EventScript = GameObject.FindObjectOfType (typeof(event_script_R2)) as event_script_R2;
		//nextEventScript.activate ();
		R3EventScript.activate();

	}


	
	void Update () {
		
	}
}
