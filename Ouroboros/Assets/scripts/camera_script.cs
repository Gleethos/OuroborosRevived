using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour {


	event_script_A roomA;
	event_script_R1 room1;
	event_script_R2 room2;
	event_script_R3 room3;
	event_script_R4 room4;
	event_script_R5 room5;
	PlayerController player;
	private GameObject playerCameraObject;
	public Camera playerCamera;

	void Start () {
		
		roomA = GameObject.FindObjectOfType (typeof(event_script_A)) as event_script_A;
		room1 = GameObject.FindObjectOfType (typeof(event_script_R1)) as event_script_R1;
		room2 = GameObject.FindObjectOfType (typeof(event_script_R2)) as event_script_R2;
		room3 = GameObject.FindObjectOfType (typeof(event_script_R3)) as event_script_R3;
		room4 = GameObject.FindObjectOfType (typeof(event_script_R4)) as event_script_R4;
		room5 = GameObject.FindObjectOfType (typeof(event_script_R5)) as event_script_R5;

		player =GameObject.FindObjectOfType (typeof(PlayerController)) as PlayerController;
		playerCameraObject = GameObject.FindWithTag ("MainCamera");
		playerCamera = playerCameraObject.GetComponent<Camera> ();

        
	}
	
	void Update () {

		//size calculation utility variables:
		float a, b, distance;
		a = 0f;
		b = 0f;
		float sizeModifier=0;

		if
		(roomA.RoomIsActive())
		{
            a = roomA.GetCenterX()-player.GetX;
			b = roomA.GetCenterY()-player.GetY;
			distance = Mathf.Pow ((float)(Mathf.Pow ((float)a, 2.0f) + Mathf.Pow ((float)b, 2.0f)), 0.5f);
			sizeModifier = getGaussianOf (distance, 2.5f, 0.5f);
		}
		else if 
		(room1.RoomIsActive())
		{
            a = room1.GetCenterX()-player.GetX;
			b = room1.GetCenterY()-player.GetY;
			distance = Mathf.Pow ((float)(Mathf.Pow ((float)a, 2.0f) + Mathf.Pow ((float)b, 2.0f)), 0.5f);
			sizeModifier = getGaussianOf (distance, 1f, 1f);
		}
		else if 
		(room2.RoomIsActive())
		{
            a = room2.GetCenterX()-player.GetX;
			b = room2.GetCenterY()-player.GetY;
			distance = Mathf.Pow ((float)(Mathf.Pow ((float)a, 2.0f) + Mathf.Pow ((float)b, 2.0f)), 0.5f);
			sizeModifier = getGaussianOf (distance, 1f, 1f);
		}
		else if 
		(room3.RoomIsActive())
		{
            a = room3.GetCenterX()-player.GetX;
			b = room3.GetCenterY()-player.GetY;
			distance = Mathf.Pow ((float)(Mathf.Pow ((float)a, 2.0f) + Mathf.Pow ((float)b, 2.0f)), 0.5f);
			sizeModifier = getGaussianOf (distance, 2f, 0.7f);
		}
		else if 
		(room4.RoomIsActive())
		{
            a = room4.GetCenterX()-player.GetX;
			b = room4.GetCenterY()-player.GetY;
			distance = Mathf.Pow ((float)(Mathf.Pow ((float)a, 2.0f) + Mathf.Pow ((float)b, 2.0f)), 0.5f);
			sizeModifier = getGaussianOf (distance, 2f, 0.6f);
		}
		else if (room5.RoomIsActive())
		{
            a = room5.GetCenterX()-player.GetX;
			b = room5.GetCenterY()-player.GetY;
			distance = Mathf.Pow ((float)(Mathf.Pow ((float)a, 2.0f) + Mathf.Pow ((float)b, 2.0f)), 0.5f);
			sizeModifier = getGaussianOf (distance, 2f, 1.1f);
		}
		//Setting new camera size according to current sizeModifier:
		if (playerCamera.enabled)
        {
			Camera.main.orthographicSize = 2f + sizeModifier;
		}


		//Vector3 temp = gameObject.transform.eulerAngles;
		//temp.z = 1;
		//gameObject.transform.eulerAngles += temp;

	}


	//GAUSSIAN FUNCTION
	//=========================================================================
	private float getGaussianOf(float distance, float multiplier, float distributor)
	{
		return multiplier*Mathf.Pow (2.7182818f, -(Mathf.Pow ((distance), 2f) / 2f*Mathf.Pow(distributor,2f))); //Larger distributor -> more "spikey"
	}
}
