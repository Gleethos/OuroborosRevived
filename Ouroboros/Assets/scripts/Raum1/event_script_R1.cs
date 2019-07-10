using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class event_script_R1 : Eventscript {

	private new readonly float centerX = -11.03f;
	private new readonly float centerY = -4.78f;

	private int eventCounter=0;

	private door_script exitDoor;
	private door_script entryDoor;

	private player_script player;
	private event_script_R2 nextEventScript;
	private dialog_script dialogManager;
	private bildchange_script beautifulPainting;
	private bild_script scaryPainting;
	private SpriteRenderer r2Dark;
	private SpriteRenderer r1Dark;

    [SerializeField]
    private ColliderListener event1;



	
	void Start () {
		
		exitDoor = GameObject.FindWithTag ("Exit_R1").GetComponent<door_script>();
		entryDoor = GameObject.FindWithTag ("Entry_R1").GetComponent<door_script>();

		player = GameObject.FindObjectOfType (typeof(player_script)) as player_script;
		nextEventScript = GameObject.FindObjectOfType (typeof(event_script_R2)) as event_script_R2;
		dialogManager = GameObject.FindObjectOfType (typeof(dialog_script)) as dialog_script;
		beautifulPainting = GameObject.FindObjectOfType (typeof(bildchange_script)) as bildchange_script;
		scaryPainting = GameObject.FindObjectOfType (typeof(bild_script)) as bild_script;
	}

	
	void Update () {

        //... Scarry Painting
        if (scaryPainting.interactionOccurred ()) {
            dialogManager.setDialog ("scaryPainting_firstLook.txt");
        }

        if (isActive)
		{	
			dialogManager.setRoom ("room_1");
			player.setRoom ("room_1");

            //Debug.Log ("ExitState: " + event1.getColliderStateExit () + "EnterState: " + event1.getColliderStateEnter ());

			switch (eventCounter) {
			case 0:
                    if(event1.getColliderStateExit() || event1.getColliderStateEnter ()) {
                        dialogManager.setDialog ("contrastEncounter.txt");
                        eventCounter++;
                    }
                    
                    break;

			case 1:

                    if (!dialogManager.isActive ()) { eventCounter++; }
                    break;

			case 2:
                    if (beautifulPainting.interactionOccurred ()) {
                        dialogManager.setDialog ("beautifulPainting_firstLook.txt");
                        eventCounter++;
                    }

                    break;

			case 3:

                    if (!dialogManager.isActive ()) { eventCounter++; }
                    break;

			case 4:
				    if (beautifulPainting.interactionOccurred()) {
					    dialogManager.setDialog ("beautifulPainting_secondLook.txt");
					    eventCounter++;

				    }
				    break;

			case 5:
				    if(!dialogManager.isActive()){eventCounter++;}
				    break;

			case 6:
				    if (beautifulPainting.interactionOccurred()) {
					    dialogManager.setDialog ("beautifulPainting_thirdLook.txt");
					    eventCounter++;
				    }
				    break;


			case 7:
				    if(!dialogManager.isActive()){eventCounter++;}
				    break;


			case 8:
				    if (beautifulPainting.interactionOccurred()) {
					    dialogManager.setDialog ("beautifulPainting_fourthLook.txt");
					    eventCounter++;
				    }
				    break;

			case 9:
				    if(!dialogManager.isActive()){eventCounter++;}
				    break;

			case 10:
				    if (beautifulPainting.interactionOccurred()) {
					    dialogManager.setDialog ("inDenial.txt");
					    eventCounter++;
				    }
				    break;

			case 11:
				    if(!dialogManager.isActive()){eventCounter++;}
				    break;

			case 12:
				    if (beautifulPainting.interactionOccurred()) {
					    dialogManager.setDialog ("finalChoice.txt");

				    }
				    if(dialogManager.getDialogOutput()=="paintingBeingRipped"){eventCounter++;}
				    break;

			case 13: 
				    beautifulPainting.ripPainting ();
				    exitDoor.setIsLocked ();
				    roomSolved = true;
				    eventCounter++; 

				    break;

            case 14:
                    break;

			}



			if(roomSolved)
			{
				if(exitDoor.isLocked())
				{
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
        isActive = false;
        nextEventScript.activate ();
    }

}
