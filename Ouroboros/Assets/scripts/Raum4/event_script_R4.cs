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



	
	void Start () {
        Setup();
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

	
	void Update () {

        Debug.Log ("Exit Door open?: " + roomSolved);

		if(mudSwitch.isSwitchedOn()){
            mud.fill ();
            colliderE.enabled = false;
        }

        if (!mudSwitch.isSwitchedOn()){
            mud.drain ();
            colliderE.enabled = true;
		}

		if(mud.isFilling()){
			player.Movementspeed = (drainedPlayerSpeed(player.GetX, player.GetY, defaultPlayerSpeed));

				audio.GetComponent<AudioSource> ().Play ();

		}

        //... Distance Dialogues

        if (distanceToEscape > 10 && distanceToEscape < 15) {
            dialogManager.setDialog ("dialog1.txt");
        }
        if (distanceToEscape > 7 && distanceToEscape < 10) {
            dialogManager.setDialog ("dialog2.txt");
        }
        if (distanceToEscape > 5 && distanceToEscape < 7) {
            dialogManager.setDialog ("dialog3.txt");
        }
        if (distanceToEscape > 4 && distanceToEscape < 5) {
            dialogManager.setDialog ("dialog4.txt");
        }
        if (distanceToEscape > 3 && distanceToEscape < 4) {
            dialogManager.setDialog ("dialog5.txt");
        }

        //... Event Flow
        if (isActive)
		{
			dialogManager.setRoom ("room_4");
			player.SetRoom ("room_4");

			switch (eventCounter) {

			case 0:
		
			
				if(mudSwitch.isSwitchedOn()){
                        roomSolved = true;
                        eventCounter++;
                    }


				break;

			case 1:
                    

				break;


			case 2:				
				break;

			case 3:
				
				break;


			case 4:
				
				break;

			case 5:
				
				break;


			case 6:
				
				break;
			case 7:
				
				break;



			}



			if(roomSolved)
			{
                Debug.Log ("Room finish");
				if(exitDoor.isLocked())
				{
                    Debug.Log ("Door finished");
				    player.Movementspeed = (defaultPlayerSpeed);
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
