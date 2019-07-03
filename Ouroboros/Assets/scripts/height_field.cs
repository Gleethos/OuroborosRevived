using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class height_field : MonoBehaviour {
	private player_script spieler; 
	private float distance;
	private BoxCollider2D box;
	private float colliderSize;
	private float[] playerPoint = new float[2];

	Vector3 colliderPointL;
	Vector3 colliderPointR;

	float gausValue = 0;
	ColliderListener boxListener;
	private bool colliderActiv = false;
	GameObject realPlayer;
	private float tempDistance; //garbage variabel
	private string parentName = "";

	void Start () {
		spieler = GameObject.FindObjectOfType(typeof(player_script)) as player_script;
		box = GetComponentInChildren(typeof(BoxCollider2D)) as BoxCollider2D;
		boxListener = GetComponentInChildren<ColliderListener> ();
		realPlayer = GameObject.FindGameObjectWithTag ("Player");

		//-------- BOX POINT CALCULATION -----------------------------

		Transform bcTransform = box.transform;
		Vector2 size = new Vector2(box.size.x * bcTransform.localScale.x * 0.5f, box.size.y * bcTransform.localScale.y * 0.5f);

		colliderPointL = new Vector2(-size.x, -size.y);
		colliderPointR = new Vector2 (size.x, -size.y);
		colliderPointL = RotatePointAroundPivot(colliderPointL, Vector3.zero, bcTransform.eulerAngles);
		colliderPointR = RotatePointAroundPivot(colliderPointR, Vector3.zero, bcTransform.eulerAngles);
		colliderPointL [0] += box.transform.position.x;
		colliderPointL [1] += box.transform.position.y;
		colliderPointR [0] += box.transform.position.x;
		colliderPointR [1] += box.transform.position.y;
		//Debug.Log ("Corner1X: " + colliderPointL [0] + " Corner1Y: " + colliderPointL [1] + " || Corner2X: " + colliderPointR [0] + " Corner2Y: " + colliderPointR [1]);

	}

	//-------------- UPDATE ------------------

	void Update () {

		//---------- DEBUG NACHRICHTEN ---------------------------------------------------
//		Debug.Log("Speed: " + spieler.getSpeed());

		//---------- Variabeln, die jedes Update überprüft werden müssen -----------------
		playerPoint [0] = spieler.transform.position.x;
		playerPoint [1] = spieler.transform.position.y;
		parentName = box.transform.parent.name;

		//---------- Hören die Collider ab -----------------------------------------------
		if (boxListener.getColliderStateEnter()) {
			colliderActiv = true;
		} else if (boxListener.getColliderStateExit()) {
			colliderActiv = false;

			//RESET
			switch (parentName) {
			case "R1_Treppe":
				break;
			case "AP_runterfallend":
				break;
			case "R3_untere_Treppe":
				break;
			case "R3_obere_Treppe":
				break;
			case "R4_Treppe":
				break;
			case "R5_Totenkopf_Gang":
				break;
			case "P_Totenkopf_Gang":
				break;
			case "P_Treppe":
				break;
			default:
				spieler.setSpeed (2f);
				realPlayer.transform.localScale = new Vector3 (.05f,.05f,1f);
				break;

			}

		}

		//---------- Distanz zum Collider im rechten Winkel -----------------------------
		distance = getDistance (playerPoint, colliderPointL, colliderPointR);

		switch (parentName){

		//-------------------- A RUNTERFALLEND ------------------------------------------------
		case "AP_runterfallend":
			if (colliderActiv == true) {
				
					if (distance > 0) {
						//spieler.setSpeed (2f - distance);
						realPlayer.transform.localScale = new Vector3 (.05f - 0.005f * distance, .05f - 0.005f * distance, 1f);
					}

			}
			break;

		//-------------------- R1 TREPPE ------------------------------------------------
		case "R1_Treppe":
			if (colliderActiv == true) {
				if (distance > 0) {
					spieler.setSpeed (2f - (distance*0.40f));
					realPlayer.transform.localScale = new Vector3 (.05f - 0.01f * distance, .05f - 0.01f * distance, 1f);
				} 
			}
			break;

		//-------------------- R1R2 NORMALE GRÖßE ------------------------------------------
		case "R1R2_normale_Größe":
			break;
		//-------------------- R2 BRÜCKE ---------------------------------------------------
		case "R2_Brücke":
			
			if (colliderActiv == true) {
				if (distance > 0 && distance <= box.size.y/2) {
					//spielergröße bis zur mitte ansteigen lassen je nach abstand
					spieler.setSpeed (1f + distance / 2);
					realPlayer.transform.localScale = new Vector3 (.05f + 0.01f * distance, .05f + 0.01f * distance, 1f);
				} else if (distance > 0) {
					//differenz zur mitte (höchster punkt) lässt spieler bis zum verlassen der brücke wieder kleiner werden
					tempDistance = (box.size.y/2 - (distance - box.size.y/2));
					spieler.setSpeed (1f + tempDistance / 2);

					realPlayer.transform.localScale = new Vector3 (.05f + 0.01f * tempDistance, .05f + 0.01f * tempDistance, 1f);

				}
			}
			break;
		//-------------------- R3 UNTERE TREPPE -----------------------------------------------
		case "R3_untere_Treppe":
			if (colliderActiv == true) {
				if (distance > 0) {
					spieler.setSpeed (1.67f - (distance*0.20f));
					realPlayer.transform.localScale = new Vector3 (.04f - 0.0045f * distance, .04f - 0.0045f * distance, 1f);
				} 
			}
			break;
		//-------------------- R3 OBERE TREPPE -----------------------------------------------
		case "R3_obere_Treppe":
			if (colliderActiv == true) {
				if (distance > 0) {
					spieler.setSpeed (2f - (distance*0.14f));
					realPlayer.transform.localScale = new Vector3 (.05f - 0.004251f * distance, .05f - 0.004251f * distance, 1f);
				} 
			}
			break;
		//-------------------- R3 BRÜCKE -----------------------------------------------------
		case "R3_Brücke":

			if (colliderActiv == true) {
				if (distance > 0 && distance <= box.size.y/2) {
					//spielergröße bis zur mitte ansteigen lassen je nach abstand
					spieler.setSpeed (1.5f + distance / 4);
					realPlayer.transform.localScale = new Vector3 (.05f + 0.0033f * distance, .05f + 0.0033f * distance, 1f);
				} else if (distance > 0) {
					//differenz zur mitte (höchster punkt) lässt spieler bis zum verlassen der brücke wieder kleiner werden
					tempDistance = (box.size.y/2 - (distance - box.size.y/2));
					spieler.setSpeed (1.5f + tempDistance / 4);

					realPlayer.transform.localScale = new Vector3 (.05f + 0.0033f * tempDistance, .05f + 0.0033f * tempDistance, 1f);

				}
			}
			break;
		//-------------------- R4 TREPPE -----------------------------------------------------
		case "R4_Treppe":
			if (colliderActiv == true) {
				if (distance > 0) {
					spieler.setSpeed (2f + (distance*0.33f));
					realPlayer.transform.localScale = new Vector3 (.05f + 0.01f * distance, .05f + 0.01f * distance, 1f);
				} 
			}
			break;
		//-------------------- R5 TOTENKOPF ---------------------------------------------------
		case "R5_Totenkopf_Gang":
			if (colliderActiv == true) {
				if (distance > 0) {
					spieler.setSpeed (2f - distance*.33f);
					realPlayer.transform.localScale = new Vector3 (.05f - 0.00825f * distance, .05f - 0.00825f * distance, 1f);
				} 
			}
			break;
		//-------------------- P TOTENKOPF ---------------------------------------------------
		case "P_Totenkopf_Gang":
			if (colliderActiv == true) {
				if (distance > 0) {
					spieler.setSpeed (.8f + distance*.8975f);
					realPlayer.transform.localScale = new Vector3 (.02f + 0.02244f * distance, .02f + 0.02244f * distance, 1f);
				} 
			}
			break;
		//-------------------- P TREPPE ------------------------------------------------------
		case "P_Treppe":
			if (colliderActiv == true) {
				if (distance > 0) {
					spieler.setSpeed (3f - distance*.8857f);
					realPlayer.transform.localScale = new Vector3 (.075f - 0.02214f * distance, .075f - 0.02214f * distance, 1f);
				} 
			}
			break;

		//-------------------- DEFAULT -------------------------------------------------------
		default: break;
		}
	}

	private float getDistance (float[] spieler, Vector3 pointL, Vector3 pointR){
		float[] richtungsvektor = { pointR [0] - pointL [0], pointR [1] - pointL [1] };
		if (richtungsvektor [0] != 0 || richtungsvektor [1] != 0) {
			float[] normalvektor = { -1 * richtungsvektor [1], richtungsvektor [0] };
			float[] pointLplayerPoint = { playerPoint [0] - pointL [0], playerPoint [1] - pointL [1] };
			distance = 1 / (Mathf.Sqrt (Mathf.Pow (normalvektor [0], 2) + Mathf.Pow (normalvektor [1], 2))) * (pointLplayerPoint [0] * normalvektor [0] + pointLplayerPoint [1] * normalvektor [1]);
		} else {
			distance = 0;
		}

		return distance;
	}

	// Helper method courtesy of @aldonaletto
	// http://answers.unity3d.com/questions/532297/rotate-a-vector-around-a-certain-point.html
	Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles) {
		Vector3 dir = point - pivot; // get point direction relative to pivot
		dir = Quaternion.Euler(angles) * dir; // rotate it
		point = dir + pivot; // calculate rotated point
		return point; // return it
	}

	private float getGaussianOf(float distance, float multiplier, float distributor)
	{
		return multiplier*Mathf.Pow (2.7182818f, -(Mathf.Pow ((distance), 2f) / 2f*Mathf.Pow(distributor,2f)));
	}

}
