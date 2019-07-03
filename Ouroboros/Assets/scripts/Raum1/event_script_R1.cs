using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class event_script_R1 : Eventscript {

	private new readonly float centerX = -11.03f;
	private new readonly float centerY = -4.78f;

	int eventCounter=0;

	door_script exitDoor;
	door_script entryDoor;

	player_script player;
	event_script_R2 nextEventScript;
	dialog_script dialogManager;
	bildchange_script beautifulPainting;
	bild_script scaryPainting;
	private SpriteRenderer r2Dark;
	private SpriteRenderer r1Dark;



	// Use this for initialization
	void Start () {
		//Debug.Log("Event script for room 1 initialized!");
		exitDoor = GameObject.FindWithTag ("Exit_R1").GetComponent<door_script>();
		entryDoor = GameObject.FindWithTag ("Entry_R1").GetComponent<door_script>();

		player = GameObject.FindObjectOfType (typeof(player_script)) as player_script;
		nextEventScript = GameObject.FindObjectOfType (typeof(event_script_R2)) as event_script_R2;
		dialogManager = GameObject.FindObjectOfType (typeof(dialog_script)) as dialog_script;
		beautifulPainting = GameObject.FindObjectOfType (typeof(bildchange_script)) as bildchange_script;
		scaryPainting = GameObject.FindObjectOfType (typeof(bild_script)) as bild_script;
	}

	// Update is called once per frame
	void Update () {

		if(isActive)
		{	
			dialogManager.setRoom ("room_1");
			player.setRoom ("room_1");


			switch (eventCounter) {
			case 0:
				//dialogManager.setDialog ("Lala lulu!");
				//player.freeze();

				dialogManager.setDialog ("contrastEncounter.txt");
				eventCounter++;
				break;

			case 1:

				if (scaryPainting.interactionOccurred()) {
					dialogManager.setDialog ("scaryPainting_firstLook.txt");
					eventCounter++; 	
				}

				break;


			case 2:
				if (beautifulPainting.interactionOccurred()) {
					//Debug.Log ("Dialog running: "+dialogManager.isActive());
					dialogManager.setDialog ("beautifulPainting_firstLook.txt");
					//Debug.Log ("...and running: "+dialogManager.isActive());
					eventCounter++;

				}
					
				break;

			case 3:
				//Debug.Log ("Dialog still running: "+dialogManager.isActive());
				if(!dialogManager.isActive()){eventCounter++;}
				break;

			case 4:
				if (beautifulPainting.interactionOccurred()) {
					//Debug.Log ("Dialog running: "+dialogManager.isActive());
					dialogManager.setDialog ("beautifulPainting_secondLook.txt");
					//Debug.Log ("...and running: "+dialogManager.isActive());
					eventCounter++;

				}
				break;

			case 5:
				//Debug.Log ("Dialog still running: "+dialogManager.isActive());
				if(!dialogManager.isActive()){eventCounter++;}
				break;

			case 6:
				if (beautifulPainting.interactionOccurred()) {
					//Debug.Log ("Dialog running: "+dialogManager.isActive());
					dialogManager.setDialog ("beautifulPainting_thirdLook.txt");
					//Debug.Log ("...and running: "+dialogManager.isActive());
					eventCounter++;
				}
				break;


			case 7:
				//Debug.Log ("Dialog still running: "+dialogManager.isActive());
				if(!dialogManager.isActive()){eventCounter++;}
				break;


			case 8:
				if (beautifulPainting.interactionOccurred()) {
					//Debug.Log ("Dialog running: "+dialogManager.isActive());
					dialogManager.setDialog ("beautifulPainting_fourthLook.txt");
					//Debug.Log ("...and running: "+dialogManager.isActive());
					eventCounter++;
				}
				break;

			case 9:
				//Debug.Log ("Dialog still running: "+dialogManager.isActive());
				if(!dialogManager.isActive()){eventCounter++;}
				break;

			case 10:
				if (beautifulPainting.interactionOccurred()) {
					//Debug.Log ("Dialog running: "+dialogManager.isActive());
					dialogManager.setDialog ("inDenial.txt");
					//Debug.Log ("...and running: "+dialogManager.isActive());
					eventCounter++;
				}
				break;

			case 11:
				//Debug.Log ("Dialog still running: "+dialogManager.isActive());
				if(!dialogManager.isActive()){eventCounter++;}
				break;

			case 12:
				if (beautifulPainting.interactionOccurred()) {
					//Debug.Log ("Dialog running: "+dialogManager.isActive());
					dialogManager.setDialog ("finalChoice.txt");
					//Debug.Log ("...and running: "+dialogManager.isActive());

				}
				if(dialogManager.getDialogOutput()=="paintingBeingRipped"){eventCounter++;}
				break;


			case 13:
				eventCounter++;
				break;

			case 14:
				//dialogManager.setDialog ("talkingToSomeone.txt");
				//if(!dialogManager.isActive()){eventCounter++;}
				eventCounter++;
				break;




			case 15: 
				beautifulPainting.ripPainting ();
				exitDoor.setIsLocked ();//exitDoor locked=false!! (unlock)
				roomSolved = true;
				eventCounter++; 

				break;


			}



			if(roomSolved)
			{
				if(exitDoor.isLocked())
				{
					File.Delete("dialog/room_1/ItemHolder.txt");
					r2Dark = GameObject.Find ("raum2D").GetComponent<SpriteRenderer>();
					r2Dark.enabled = false;
					r1Dark = GameObject.Find ("raum1D").GetComponent<SpriteRenderer>();
					r1Dark.enabled = true;
					endAndProceed();
				}
			}


		}



	}
    void endAndProceed() {
        //Debug.Log ("Event script for room 5 is now ending it's procedure.\n");
        isActive = false;
        nextEventScript.activate ();
    }

}
