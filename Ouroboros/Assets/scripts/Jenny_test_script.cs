using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jenny_test_script : MonoBehaviour {

	private player_script objekt; 
	event_script_P nextEventScript;
	event_script_R2 R3EventScript;

	// Use this for initialization
	void Start () {
		objekt = GameObject.FindObjectOfType(typeof(player_script)) as player_script;
		nextEventScript = GameObject.FindObjectOfType (typeof(event_script_P)) as event_script_P;
		R3EventScript = GameObject.FindObjectOfType (typeof(event_script_R2)) as event_script_R2;
		//nextEventScript.activate ();
		R3EventScript.Activate();
		// Test No. 1
		//objekt.moveRelative(0,0);

		// Test No. 2
		//objekt.moveRelative(1,0);

		// Test No. 3
		//objekt.moveRelative(0,1);

		// Test No. 4
		//objekt.moveRelative(1,1);

		// Test No. 5
		//objekt.moveRelative(-1,0);

		// Test No. 6
		//objekt.moveRelative(0,-1);

		// Test No. 7
		//objekt.moveRelative(-1,-2);

		//Test No. 8
		//objekt.moveRelative(4,-2);

		//Test No. 9
		//objekt.moveRelative(-1,2);
	
	}


	
	// Update is called once per frame
	void Update () {
		// Test No. 2
		//objekt.moveRelative(1,0);

		// Test No. 3
		//objekt.moveRelative(0,1);
	}
}
