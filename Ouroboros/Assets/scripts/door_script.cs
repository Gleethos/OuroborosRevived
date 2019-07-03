using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_script : MonoBehaviour {

	[SerializeField]
	bool beLocked;
	private bool interactionOcc = false;
	[SerializeField]
	private float startx;
	[SerializeField]
	private float starty;
	[SerializeField]
	private float openx;
	[SerializeField]
	private float openy;
	[SerializeField]
	private float closex;
	[SerializeField]
	private float closey;
	private int door = 0;
	private int doorHolder = 0;

	void Update () {

		if (Input.GetKey (KeyCode.F) && beLocked == true) {
			interactionOcc = true;
		}

		if (door == 1 && beLocked == false) {
			if (Input.GetKey (KeyCode.F) || (doorHolder == 1)) {
				doorHolder = 1;

				if (closex > openx && closey > openy) {
					
					if (startx >= openx) {
						startx = startx - 0.5f * Time.deltaTime;
					}
					if (starty >= openy) {
						starty = starty - 0.5f * Time.deltaTime;
					}

					transform.position = new Vector3 (startx, starty, 0.0f);
					if (startx < openx && starty < openy) {
						door = 0;
						doorHolder = 0;
					}

				} else if (closex < openx && closey < openy) {
					
					if (startx <= openx) {
						startx = startx + 0.5f * Time.deltaTime;
					}
					if (starty <= openy) {
						starty = starty + 0.5f * Time.deltaTime;
					}

					transform.position = new Vector3 (startx, starty, 0.0f);
					if (startx > openx && starty > openy) {
						door = 0;
						doorHolder = 0;
					}

				} else if (closex < openx && closey > openy) {

					if (startx <= openx) {
						startx = startx + 0.5f * Time.deltaTime;
					}
					if (starty >= openy) {
						starty = starty - 0.5f * Time.deltaTime;
					}

					transform.position = new Vector3 (startx, starty, 0.0f);
					if (startx > openx && starty < openy) {
						door = 0;
						doorHolder = 0;

					}

				} else if (closex > openx && closey < openy) {
					
					if (startx >= openx) {
						startx = startx - 0.5f * Time.deltaTime;
					}
					if (starty <= openy) {
						starty = starty + 0.5f * Time.deltaTime;
					}

					transform.position = new Vector3 (startx, starty, 0.0f);
					if (startx < openx && starty > openy) {
						door = 0;
						doorHolder = 0;
					}

				}

			}
		}

		if (door == -1) {
			doorHolder = 1;
			if (doorHolder == 1) {

				if (closex > openx && closey > openy) {
					
					if (startx <= closex) {
						startx = startx + 0.5f * Time.deltaTime;
					}
					if (starty <= closey) {
						starty = starty + 0.5f * Time.deltaTime;
					}
					transform.position = new Vector3 (startx, starty, 0.0f);
					if (startx > closex && starty > closey) {
						door = 0;
						doorHolder = 0;
					}

					} else if (closex < openx && closey < openy) {

						if (startx >= closex) {
							startx = startx - 0.5f * Time.deltaTime;
						}
						if (starty >= closey) {
							starty = starty - 0.5f * Time.deltaTime;
						}
						transform.position = new Vector3 (startx, starty, 0.0f);
						if (startx < closex && starty < closey) {
							door = 0;
							doorHolder = 0;
						}

					} else if (closex < openx && closey > openy) {

					if (startx >= closex) {
						startx = startx - 0.5f * Time.deltaTime;
					}
					if (starty <= closey) {
						starty = starty + 0.5f * Time.deltaTime;
					}
					transform.position = new Vector3 (startx, starty, 0.0f);
					if (startx < closex && starty > closey) {
						door = 0;
						doorHolder = 0;
					}
						
					} else if (closex > openx && closey < openy) {

					if (startx <= closex) {
						startx = startx + 0.5f * Time.deltaTime;
					}
					if (starty >= closey) {
						starty = starty - 0.5f * Time.deltaTime;
					}
					transform.position = new Vector3 (startx, starty, 0.0f);
					if (startx > closex && starty < closey) {
						door = 0;
						doorHolder = 0;
					}

					}

				}

			}
		}

	// UPDATE ENDING

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
				door = 1;
		}
	}
	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			door = -1;
		}
	}

	/*---------------------------------------------
				Door Status
	--------------------------------------------*/

	public bool isLocked(){
		return beLocked;
	}

	public void setIsLocked(){
		beLocked = false;
	}

	public void isLockedTrue(){
		beLocked = true;
	}

	/*---------------------------------------------
				Door Interaction
	--------------------------------------------*/

	public bool interactionOccurred(){
		bool occ = interactionOcc;
		interactionOcc = false;
		return occ;	
	}

}
