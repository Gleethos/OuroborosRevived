using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class event_script_R5 : Eventscript {

	private new readonly float centerX = 11.8f;
	private new readonly float centerY = -4.68f;

	int eventCounter=0;

	darkness_script exitDoor;
	door_script entryDoor;
    
	event_script_P nextEventScript;
	ScriptReader dialogManager;
	bildchange_script beautifulPainting;
	bild_script scaryPainting;
	ColliderListener event1;
	ColliderListener event2;
	GameObject audio;



	// Use this for initialization
	void Start () {

        Setup();

        //Debug.Log("Event script for room 5 initialized!");
        exitDoor = GameObject.FindWithTag ("Exit_R5").GetComponent<darkness_script>();
		entryDoor = GameObject.FindWithTag ("Entry_R5").GetComponent<door_script>();
        
		nextEventScript = GameObject.FindObjectOfType (typeof(event_script_P)) as event_script_P;
		dialogManager = GameObject.FindObjectOfType (typeof(ScriptReader)) as ScriptReader;
		event1 = GameObject.FindWithTag ("Event1").GetComponent<ColliderListener> ();
		event2 = GameObject.FindWithTag ("Event2").GetComponent<ColliderListener> ();
		audio = GameObject.Find ("EventManager_R5");

	}

	// Update is called once per frame
	void Update () {

		if(isActive)
		{
			dialogManager.setRoom ("room_5");
			player.SetRoom ("room_5");

			switch (eventCounter) {
			case 0:
				if (event1.getColliderStateEnter () == true) {
					
					audio.GetComponent<AudioSource> ().Play ();

					player.Freeze ();
					dialogManager.setDialog ("beforeTheEnd.txt");
				}
				if (dialogManager.getDialogOutput () == "endingDialogue") {
					player.Unfreeze ();
					eventCounter++;
				}

				break;

			case 1:
				if (event2.getColliderStateEnter () == true) {
					player.Freeze ();
					dialogManager.setDialog ("scaryScene.txt");
				} 
				if (dialogManager.getDialogOutput () == "endingDialogue2") {
					player.Unfreeze ();
					eventCounter++;
				}

				break;


			case 2:
				eventCounter++;

				break;

			case 3:
				roomSolved = true;
				eventCounter++;
				break;
			case 4:
				break;


			}



			if(roomSolved)
			{
				if(exitDoor.checkDark())
				{
					File.Delete("dialog/room_5/ItemHolder.txt");
					endAndProceed();
				}
			}



		}



	}



	void endAndProceed()
	{
        //Debug.Log ("Event script for room 5 is now ending it's procedure.\n");
		isActive = false;
		nextEventScript.Activate ();
	}

}
