using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class event_script_A : Eventscript {


	private new readonly float centerX = -0.29f;
	private new readonly float centerY = -11.79f;
	private float speed;
	int eventCounter=0;

	private door_script exitDoor;
	private event_script_R1 nextEventScript;
	private ScriptReader dialogManager;
	[SerializeField]
	private GameObject victim;
	private GameObject playerCameraObject;
	private GameObject victimCameraObject;
	public Camera playerCamera;
	public Camera victimCamera;
	private bool fallingState = false;
	private SpriteRenderer victimSprite;
	private ColliderListener teddy;
	private ColliderListener wife;
	private ColliderListener basketball;
	private ColliderListener armchair;
	private bool tState = false;
	private bool aState = false;
	private bool bState = false;
	private bool wState = false;
	private SpriteRenderer aDark;
	private SpriteRenderer r1Dark;

	private GameObject audio;

	
	void Start () {
        isActive = true;
        exitDoor = GameObject.FindWithTag ("Exit_A").GetComponent<door_script>();
        
		nextEventScript = GameObject.FindObjectOfType (typeof(event_script_R1)) as event_script_R1;
		dialogManager = GameObject.FindObjectOfType (typeof(ScriptReader)) as ScriptReader;
		victim = GameObject.FindWithTag ("Victim");
		playerCameraObject = GameObject.FindWithTag ("MainCamera");
		victimCameraObject = GameObject.FindWithTag ("Camera2");
		playerCamera = playerCameraObject.GetComponent<Camera> ();
		victimCamera = victimCameraObject.GetComponent<Camera> ();
		victimSprite = victim.GetComponentInChildren (typeof(SpriteRenderer)) as SpriteRenderer;

		//... Items Collider
		teddy = GameObject.FindWithTag ("my teddy").GetComponent<ColliderListener> ();
		wife = GameObject.FindWithTag ("my wife").GetComponent<ColliderListener> ();
		armchair = GameObject.FindWithTag ("my armchair").GetComponent<ColliderListener> ();
		basketball = GameObject.FindWithTag ("my basketball").GetComponent<ColliderListener> ();

		audio = GameObject.Find ("EventManager_Anfang");
	
	}

	
	void Update () {

		//... Items 
		if (teddy.getColliderStateEnter() == true){
			tState = true;
		}
		if(tState && Input.GetKeyDown(KeyCode.F)){
			dialogManager.setDialog ("teddy.txt");
		}
		if (teddy.getColliderStateExit () == true) {
			tState = false;
		}

		if(wife.getColliderStateEnter() == true){
			wState = true;

		}

		if(wState && Input.GetKeyDown(KeyCode.F)){
			dialogManager.setDialog ("wife.txt");
		}
		if (wife.getColliderStateExit () == true) {
			wState = false;
		}

		if(basketball.getColliderStateEnter() == true){
			bState = true;
		}
		if(bState && Input.GetKeyDown(KeyCode.F)){
			dialogManager.setDialog ("basketball.txt");
		}
		if (basketball.getColliderStateExit () == true) {
			bState = false;
		}

		if(armchair.getColliderStateEnter() == true){
			aState = true;
		}
		if(aState && Input.GetKeyDown(KeyCode.F)){
			dialogManager.setDialog ("armchair.txt");
		}
		if (armchair.getColliderStateExit () == true) {
			aState = false;
		}

		if(isActive)
		{	
			dialogManager.setRoom ("room_A");
			player.SetRoom ("room_A");


			switch (eventCounter) {
			case 0:

				player.Freeze ();
				dialogManager.setDialog ("thebeginning.txt");
				eventCounter++;
				break;

			case 1:
				if (dialogManager.getDialogOutput () == "falling") {
					playerCamera.enabled = false;
                    if (victimCamera!=null) { victimCamera.enabled = true; }
					float tempLocX = victim.transform.position.x;
					float tempLocY = victim.transform.position.y;
					float tempSizeX = victim.transform.localScale.x;
					float tempSizeY = victim.transform.localScale.y;

					fallingState = true;
				
					if (fallingState) {
						
						if (victim.transform.position.y > -10.8f) {
							speed += 9.82f*Time.deltaTime;
							tempLocY = victim.transform.position.y - 0.1f * Time.deltaTime*speed;
							tempSizeX = victim.transform.localScale.x - 0.2f * Time.deltaTime;
							tempSizeY = victim.transform.localScale.y - 0.24f * Time.deltaTime;
						}
                        
						victim.transform.position = new Vector3 (tempLocX, tempLocY, 0f);
						victim.transform.localScale = new Vector3 (tempSizeX,tempSizeY,1f);
						if (victim.transform.position.y <= -10.8f) {
							victimSprite.enabled = false;
						}
					}


				}
				if (dialogManager.getDialogOutput () == "fallingEnd") {
                    if (victimCamera != null) { victimCamera.enabled = false; }
					playerCamera.enabled = true;
					fallingState = false;
					Destroy (victim);
				}
				if (dialogManager.getDialogOutput () == "endingDialogue") {
					audio.GetComponent<AudioSource> ().Play ();
					player.Unfreeze ();
					roomSolved = true;
					eventCounter++;
				}
				break;

			case 2:
				break;

			}

			if(roomSolved)
			{
				if(exitDoor.isLocked())
				{

					aDark = GameObject.Find ("anfangsraumD").GetComponent<SpriteRenderer>();
					aDark.enabled = true;
					r1Dark = GameObject.Find ("raum1D").GetComponent<SpriteRenderer>();
					r1Dark.enabled = false;
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
