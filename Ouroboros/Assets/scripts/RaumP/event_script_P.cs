using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class event_script_P : Eventscript {

	private new readonly float centerX = -11.03f;
	private new readonly float centerY = -4.78f;

	
	int eventCounter=0;
	ColliderListener event1;
	private SpriteRenderer NPC2;
	float delay = 5f;

	npc_script npc;
	private GameObject npc_object;
	private Animator walking;
	private SpriteRenderer idle;
	private GameObject playerCameraObject;
	private GameObject npcCameraObject;
	public Camera playerCamera;
	public Camera npcCamera;
	ScriptReader dialogManager;
	bool npcMove = false;
	private SpriteRenderer npcSprite;
	private Animator npcAnimation;
	private bool fallingState = false;
	private float speed;
	private bool wasRead = false;
	private SpriteRenderer aDark;



	// Use this for initialization
	void Start () {
		//Debug.Log("Event script for room P initialized!");
	
		npc = GameObject.FindObjectOfType (typeof(npc_script)) as npc_script;
		npc_object = GameObject.FindWithTag ("NPC");
		NPC2 = GameObject.FindWithTag ("NPC2").GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;
		walking = npc_object.GetComponentInChildren(typeof(Animator)) as Animator;
		idle = npc_object.GetComponentInChildren (typeof(SpriteRenderer)) as SpriteRenderer;
		dialogManager = GameObject.FindObjectOfType (typeof(ScriptReader)) as ScriptReader;
		event1 = GameObject.FindWithTag ("Entry_P").GetComponent<ColliderListener> ();
		playerCameraObject = GameObject.FindWithTag ("MainCamera");
		npcCameraObject = GameObject.FindWithTag ("Camera3");
		playerCamera = playerCameraObject.GetComponent<Camera> ();
		npcCamera = npcCameraObject.GetComponent<Camera> ();
		npcSprite = npc_object.GetComponentInChildren (typeof(SpriteRenderer)) as SpriteRenderer;
		npcAnimation = npc_object.GetComponentInChildren (typeof(Animator)) as Animator;

	}

	// Update is called once per frame
	void Update () {
        //Debug.Log ("THis is gameobject: " + NPC2.transform.name);
        //Debug.Log (dialogManager.getDialogOutput ());

        if (isActive) {
            dialogManager.setRoom ("room_P");
            player.SetRoom ("room_P");

            switch (eventCounter) {
                case 0:
                    //NPC2.SetActive (false);
                    npc.moveTo (0f, -0.33f);
                    eventCounter++;
                    break;

                case 1:

                    if (event1.getColliderStateEnter () == true) {
                        player.Freeze ();
                        dialogManager.setDialog ("First_Meeting.txt");

                    }

                    if (dialogManager.getDialogOutput () == "npcmove") {
                        npc.moveRelative (0f, -3.5f);
                        eventCounter++;

                    }


                    break;


                case 2:
                    dialogManager.setDialog ("Next_Step.txt");
                    if (dialogManager.isActive () == false) {
                        //Debug.Log ("IM HERE");
                        eventCounter++;
                    }
                    break;

                case 3:
                    NPC2.enabled = true;
                    dialogManager.setDialog ("Final.txt");
                    if (dialogManager.getDialogOutput () == "falling") {
                        aDark = GameObject.Find ("anfangsraumD").GetComponent<SpriteRenderer> ();
                        aDark.enabled = false;
                        wasRead = true;
                        playerCamera.enabled = false;
                        npcCamera.enabled = true;
                        //falling (victim.transform.position.x,-10.5f);
                        float tempLocX = npc_object.transform.position.x;
                        float tempLocY = npc_object.transform.position.y;
                        float tempSizeX = npc_object.transform.localScale.x;
                        float tempSizeY = npc_object.transform.localScale.y;

                        fallingState = true;

                        if (fallingState) {

                            if (npc_object.transform.position.y > -10.8f) {
                                speed += 9.82f * Time.deltaTime;
                                tempLocY = npc_object.transform.position.y - 0.1f * Time.deltaTime * speed;
                                tempSizeX = npc_object.transform.localScale.x - 0.015f * Time.deltaTime;
                                tempSizeY = npc_object.transform.localScale.y - 0.015f * Time.deltaTime;
                            }


                            npc_object.transform.position = new Vector3 (tempLocX, tempLocY, 0f);
                            npc_object.transform.localScale = new Vector3 (tempSizeX, tempSizeY, 1f);
                            if (npc_object.transform.position.y <= -10.8f) {
                                npcSprite.enabled = false;
                                npcAnimation.enabled = false;
                                eventCounter++;
                            }
                        }
                    }


                    break;
                case 4:
                    float tempY = npcCamera.transform.position.y - Time.deltaTime;
                    npcCamera.transform.position = new Vector3 (npcCamera.transform.position.x, tempY, npcCamera.transform.position.z);
                    if (tempY <= -12.5f) {
                        eventCounter++;
                    }
                    break;
                case 5:

                    delay -= Time.deltaTime;
                    if (delay <= 0) {
                        SceneManager.LoadScene ("the_end");
                        roomSolved = true;
                    }
                    break;


            }



            if (roomSolved) {

                File.Delete ("dialog/room_P/ItemHolder.txt");
                endAndProceed ();

            }



        }

    }
    void endAndProceed() {
        //Debug.Log ("Event script for room 5 is now ending it's procedure.\n");
        isActive = false;
    }


}
