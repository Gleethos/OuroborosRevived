using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class event_script_R3 : Eventscript {


	private new readonly float centerX = 0f;
	private new readonly float centerY = 11.69f;

	int eventCounter=0;

	door_script exitDoor;
	door_script entryDoor;
    
	event_script_R4 nextEventScript;
	ScriptReader dialogManager;
	bildchange_script beautifulPainting;
	bild_script scaryPainting;
	ColliderListener event1;
	private bool firstTime = true;
	private bool endDialog = false;
	private GameObject planken;
	private GameObject spieler;
	private float tempPlanken = 0f;
	private float timeCounter = 0f;
	private bool delete = false;
	Collider2D collider;
	private SpriteRenderer r4Dark;
	private SpriteRenderer r3Dark;

	GameObject audio;



	// Use this for initialization
	void Start () {
		//Debug.Log("Event script for room 3 initialized!");
		exitDoor = GameObject.FindWithTag ("Exit_R3").GetComponent<door_script>();
		entryDoor = GameObject.FindWithTag ("Entry_R3").GetComponent<door_script>();
        
		nextEventScript = GameObject.FindObjectOfType (typeof(event_script_R4)) as event_script_R4;
		dialogManager = GameObject.FindObjectOfType (typeof(ScriptReader)) as ScriptReader;
		event1 = GameObject.FindWithTag ("Bridge_Start").GetComponent<ColliderListener> ();
		planken = GameObject.FindWithTag ("Panken");
		spieler = GameObject.FindWithTag ("Player");
		collider = GameObject.FindGameObjectWithTag ("Bridge").GetComponent<Collider2D>();

		//audio = GameObject.Find ("EventManager_Anfang");
	}

	// Update is called once per frame
	void Update () {

		if(isActive)
		{
			dialogManager.setRoom ("room_3");
			player.SetRoom ("room_3");

			switch (eventCounter) {
			case 0:
				if (event1.getColliderStateEnter ()) {
					player.Freeze ();

					if (player.GetFirstItem () != null) {
						//Debug.Log ("FirstIsntHere");
						dialogManager.setDialog ("someitems.txt");
						firstTime = false;
					} else if (firstTime) {
						dialogManager.setDialog ("noitems.txt");

					} else {
						dialogManager.setDialog ("lostitems.txt");
						eventCounter++;
					
					}

				}
				if (dialogManager.getDialogOutput () == "falling") {
					collider.enabled = false;
					if (timeCounter < 2f) {
						tempPlanken = planken.transform.position.y + Time.deltaTime * 2f;

						planken.transform.position = new Vector3 (planken.transform.position.x, tempPlanken, planken.transform.position.z);
					}

					if (timeCounter > .5f && (spieler.transform.position.y > 14)) {
						tempPlanken = spieler.transform.position.y - Time.deltaTime * 1f;

						spieler.transform.position = new Vector3 (spieler.transform.position.x, tempPlanken, spieler.transform.position.z);

						tempPlanken = spieler.transform.localScale.y - 0.05f * Time.deltaTime;

						spieler.transform.localScale = new Vector3 (tempPlanken, tempPlanken, 1f);
					} 

					if (timeCounter > 1.5f && timeCounter < 3.5f) {
						tempPlanken = planken.transform.position.y - Time.deltaTime * 2f;

						planken.transform.position = new Vector3 (planken.transform.position.x, tempPlanken, planken.transform.position.z);
					} else if (timeCounter > 3.5f) {
						spieler.transform.localScale = new Vector3 (0.025f, 0.025f, 1f);
						timeCounter = 0f;
						player.Movementspeed = (1f);
						collider.enabled = true;
						//player.unfreeze ();
						dialogManager.setDialogOutputTo ("");
					}

					timeCounter += Time.deltaTime;
				}
				if (dialogManager.getDialogOutput () == "delete") {
					player.DeleteIventory ();
				}
				if (dialogManager.getDialogOutput () == "endDialog") {
					//Debug.Log ("endDialog = true");
					endDialog = true;
				}

				if (dialogManager.getDialogOutput () == "unfreeze") {
					player.Unfreeze ();
					dialogManager.setDialogOutputTo("");
				}
				if (dialogManager.isActive () == false && endDialog ) {
					eventCounter++;
				}



				break;

			case 1:
				player.Unfreeze ();
				eventCounter++;

				break;


			case 2:
				eventCounter++;

				break;

			case 3:

				exitDoor.setIsLocked ();//exitDoor locked=false!! (unlock)
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
					File.Delete("dialog/room_3/ItemHolder.txt");
					r4Dark = GameObject.Find ("raum4D").GetComponent<SpriteRenderer>();
					r4Dark.enabled = false;
					r3Dark = GameObject.Find ("raum3D").GetComponent<SpriteRenderer>();
					r3Dark.enabled = true;
					//audio.GetComponent<AudioSource> ().Stop ();
					endAndProceed();
				}
			}



		}



	}



	void endAndProceed()
	{
        //Debug.Log ("Event script for room 3 is now ending it's procedure.\n");
		isActive = false;
		nextEventScript.Activate ();
	}

	
}
