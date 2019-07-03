using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class event_script_R2 : Eventscript {

	private new readonly float centerX = -11.03f;
	private new readonly float centerY = -4.78f;

	int eventCounter=0;

	door_script exitDoor;
	door_script entryDoor;

	player_script player;
	event_script_R3 nextEventScript;
	dialog_script dialogManager;
	bildchange_script beautifulPainting;
	bild_script scaryPainting;
	private SpriteRenderer r2Dark;
	private SpriteRenderer r1Dark;

	void Start () {
		//Debug.Log("Event script for room 2 initialized!");
		exitDoor = GameObject.FindWithTag ("Exit_R2").GetComponent<door_script>();
		entryDoor = GameObject.FindWithTag ("Entry_R2").GetComponent<door_script>();

		player = GameObject.FindObjectOfType (typeof(player_script)) as player_script;
		nextEventScript = GameObject.FindObjectOfType (typeof(event_script_R3)) as event_script_R3;
		dialogManager = GameObject.FindObjectOfType (typeof(dialog_script)) as dialog_script;

	}

	// Update is called once per frame
	void Update () {

		if(isActive)
		{
			dialogManager.setRoom ("room_2");
			player.setRoom ("room_2");

			switch (eventCounter) {
			case 0:
				dialogManager.setDialog ("conversationWithFire.txt");
				eventCounter++;
				break;

			case 1:
				//Debug.Log ("Dialog still running: "+dialogManager.isActive());
				if(!dialogManager.isActive()){
					eventCounter++;
				}

				break;


			case 2:
				Debug.Log ("I'm here :>");
				/*dialogManager.setDialog ("areYouAngry.txt");
				if(dialogManager.getDialogOutput()=="selfIsCalm"){
					eventCounter++;
					player.unfreeze ();
				}*/
				eventCounter++;
				break;

			case 3:
				exitDoor.setIsLocked ();
				roomSolved = true;
				eventCounter++;
				break;
			case 4:
				break;


			}



			if(roomSolved)
			{
				if(exitDoor.isLocked())
				{	
					//File.Delete("dialog/room_2/ItemHolder.txt");
					r2Dark = GameObject.Find ("raum2D").GetComponent<SpriteRenderer>();
					r2Dark.enabled = true;
					r1Dark = GameObject.Find ("raum3D").GetComponent<SpriteRenderer>();
					r1Dark.enabled = false;
					endAndProceed();
				}
			}



		}



	}



	void endAndProceed()
	{
        //Debug.Log ("Event script for room 2 is now ending it's procedure.\n");
		isActive = false;
		nextEventScript.activate ();
	}

}
