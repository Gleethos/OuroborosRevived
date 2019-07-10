using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class event_script_R4 : Eventscript {
	private new readonly float centerX = 11.85f;
	private new readonly float centerY = 4.98f;

	private float escapeX = 16.94f;
	private float escapeY = 6.85f;

	private float defaultPlayerSpeed;

	float distanceToEscape = 100;

	int eventCounter=0;

	door_script exitDoor;
	door_script entryDoor;
    
	event_script_R5 nextEventScript;
	ScriptReader dialogManager;
	bildchange_script beautifulPainting;
	bild_script scaryPainting;
	private SpriteRenderer r4Dark;
	private SpriteRenderer r5Dark;
	mud_script mud;
	switch_script mudSwitch;
	PolygonCollider2D colliderE;
	GameObject audio;



	// Use this for initialization
	void Start () {
		//Debug.Log("Event script for room 4 initialized!");
		exitDoor = GameObject.FindWithTag ("Exit_R4").GetComponent<door_script>();
		entryDoor = GameObject.FindWithTag ("Entry_R4").GetComponent<door_script>();
        
		nextEventScript = GameObject.FindObjectOfType (typeof(event_script_R5)) as event_script_R5;
		dialogManager = GameObject.FindObjectOfType (typeof(ScriptReader)) as ScriptReader;

		mud = GameObject.FindObjectOfType (typeof(mud_script)) as mud_script;
		mudSwitch = GameObject.FindObjectOfType (typeof(switch_script)) as switch_script;
		colliderE = GameObject.Find ("Collider [R4] Lightning").GetComponent<PolygonCollider2D> ();
		defaultPlayerSpeed = player.Movementspeed;
		mud.drain();

		audio = GameObject.Find ("EventManager_R4");

	}

	// Update is called once per frame
	void Update () {

		if(mudSwitch.isSwitchedOn()){mud.fill (); colliderE.enabled = false;}
		if(!mudSwitch.isSwitchedOn()){mud.drain (); colliderE.enabled = true;

				//audio.GetComponent<AudioSource> ().Stop ();

			}

		if(mud.isFilling()){
			player.Movementspeed = (drainedPlayerSpeed(player.GetX, player.GetY, defaultPlayerSpeed));

				audio.GetComponent<AudioSource> ().Play ();

		}

		if(isActive)
		{
			dialogManager.setRoom ("room_4");
			player.SetRoom ("room_4");

			switch (eventCounter) {

			case 0:
		
			
				if(mudSwitch.isSwitchedOn()){eventCounter++;}


				break;

			case 1:

				exitDoor.setIsLocked ();//exitDoor locked=false!! (unlock)
				roomSolved = true;
				eventCounter++;

				break;


			case 2:
				
				if(distanceToEscape >10 && distanceToEscape < 15){
					dialogManager.setDialog ("dialog1.txt");
					eventCounter++;
				}					
				break;

			case 3:
				if(distanceToEscape >7 && distanceToEscape < 10){
					dialogManager.setDialog ("dialog2.txt");
					eventCounter++;
				}
				break;


			case 4:
				if(distanceToEscape >5 && distanceToEscape < 7){
					dialogManager.setDialog ("dialog3.txt");
					eventCounter++;
				}
				break;

			case 5:
				if(distanceToEscape >4 && distanceToEscape < 5){
					dialogManager.setDialog ("dialog4.txt");
					eventCounter++;
				}
				break;


			case 6:
				if(distanceToEscape >3 && distanceToEscape < 4){
					dialogManager.setDialog ("dialog5.txt");
					eventCounter++;
				}
				break;
			case 7:
				
				break;



			}



			if(roomSolved)
			{
				if(exitDoor.isLocked())
				{
					//GetComponent<AudioSource> ().enabled = false;
					player.Movementspeed = (defaultPlayerSpeed);
					File.Delete("dialog/room_4/ItemHolder.txt");
					r4Dark = GameObject.Find ("raum4D").GetComponent<SpriteRenderer>();
					r4Dark.enabled = true;
					r5Dark = GameObject.Find ("raum5D").GetComponent<SpriteRenderer>();
					r5Dark.enabled = false;
					endAndProceed();
				}
			}



		}



	}



	void endAndProceed()
	{
        //Debug.Log ("Event script for room 4 is now ending it's procedure.\n");
		isActive = false;
		nextEventScript.Activate ();
	}

	private float drainedPlayerSpeed(float pX, float pY, float speed)
	{
		distanceToEscape = Mathf.Pow ((Mathf.Pow((pX-escapeX), 2f)+Mathf.Pow((pY-escapeY),2f)), 0.5f);
		return speed * getSigmoidOf ((1/distanceToEscape), -2f, 20f, 1f);
	}
		

	private float getSigmoidOf(float input, float shift, float inputMod, float multiplier)
	{return multiplier * 1 / (1 + Mathf.Pow (2.7182818284f, input * inputMod + shift));}


}
