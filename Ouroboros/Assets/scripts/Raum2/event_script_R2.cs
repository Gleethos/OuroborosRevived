using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class event_script_R2 : Eventscript {

	private new readonly float centerX = -11.03f;
	private new readonly float centerY = -4.78f;

	private int eventCounter=0;

	private door_script exitDoor;
	private door_script entryDoor;
    
	private event_script_R3 nextEventScript;
	private ScriptReader dialogManager;
	private bildchange_script beautifulPainting;
	private bild_script scaryPainting;
	private SpriteRenderer r2Dark;
	private SpriteRenderer r1Dark;

    [SerializeField]
    ColliderListener event1;

	void Start () {
		exitDoor = GameObject.FindWithTag ("Exit_R2").GetComponent<door_script>();
		entryDoor = GameObject.FindWithTag ("Entry_R2").GetComponent<door_script>();
        
		nextEventScript = GameObject.FindObjectOfType (typeof(event_script_R3)) as event_script_R3;
		dialogManager = GameObject.FindObjectOfType (typeof(ScriptReader)) as ScriptReader;

	}

	
	void Update () {

		if(isActive)
		{
			dialogManager.setRoom ("room_2");
			player.SetRoom ("room_2");

			switch (eventCounter) {
			case 0:
                    if (event1.getColliderStateEnter () || event1.getColliderStateExit ()) {
                        dialogManager.setDialog ("conversationWithFire.txt");
                        eventCounter++;
                    }
				    break;

			case 1:
				    if(!dialogManager.isActive()){
					    eventCounter++;
				    }

				    break;


			case 2:
				    dialogManager.setDialog ("areYouAngry.txt");
				    if(dialogManager.getDialogOutput()=="selfIsCalm"){
					    eventCounter++;
					    player.Unfreeze ();
				    }
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
