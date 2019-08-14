using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switch_script : MonoBehaviour {
	
	private bool entry = false;
	private bool isOn = false;
	GameObject audio_mud;

	
	void Start () {

		audio_mud = GameObject.Find ("MudSwitch");
	}

	
	void Update ()   
	{
		if (entry) 
		{
			if (Input.GetKeyDown (KeyCode.F)) {
				if (!audio_mud.GetComponent<AudioSource> ().isPlaying) {
					audio_mud.GetComponent<AudioSource> ().Play ();
				}
                if (isOn == false)
                    isOn = true;
                else
                    isOn = false; 

			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			entry = true;

		}
	}

	void OnTriggerExit2D(Collider2D coll){
		entry = false;
	}


	public bool isSwitchedOn()
	{
		return isOn;
	}

}
