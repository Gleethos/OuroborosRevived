using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc_script : MonoBehaviour {

	//ms regulieren
	[SerializeField]
	private float speed;
	[SerializeField]
	private Sprite idle_bild;

	private Vector2 direction;
	private int time;
	private bool checkF = false;
	private bool isMoving = false;
	private float movingX;
	private float movingY;
	private float tempLocX;
	private float tempLocY;
	private string[] itemHolder = new string[30];
	private int itemIndex = 0;
	private Animator walking;
	private float rotate = 0f;
	private SpriteRenderer idle;
	[SerializeField]
	private bool lockX = false;
	[SerializeField]
	private bool lockY = false;

	void Start () {
		walking = GetComponentInChildren(typeof(Animator)) as Animator;
		idle = GetComponentInChildren (typeof(SpriteRenderer)) as SpriteRenderer;
		walking.enabled = false;
		idle.sprite = idle_bild; 
		GetComponent<AudioSource> ().Pause ();
	}

	void Update () {

		if (isMoving == true) {

			if ((transform.position.x < movingX + 0.2f && transform.position.x > movingX - 0.2f) && (transform.position.y < movingY + 0.2f && transform.position.y > movingY - 0.2f)) {
				isMoving = false;
				lockY = false;
				lockX = false;
				walking.enabled = false;
				idle.sprite = idle_bild;
				GetComponent<AudioSource> ().Pause ();

			} else {

				walking.enabled = true;
				//===
				//GetComponent<AudioSource> ().UnPause ();

				tempLocX = transform.position.x;
				tempLocY = transform.position.y;

				if (!lockX) {
					if (transform.position.x - 0.05f  > movingX) {
						tempLocX = transform.position.x - 0.35f * speed * Time.deltaTime;
						rotate = 90f;
					} else if (transform.position.x + 0.05f < movingX) {
						tempLocX = transform.position.x + 0.35f * speed * Time.deltaTime;
						rotate = 270f;
					} else {
						tempLocX = transform.position.x;
						lockX = true;
					}
				}

				if (!lockY) {
					if (transform.position.y + 0.05f  < movingY) {
						tempLocY = transform.position.y + 0.35f * speed * Time.deltaTime;
						rotate = 0f;
					} else if (transform.position.y - 0.05f > movingY) {
						tempLocY = transform.position.y - 0.35f * speed * Time.deltaTime;
						rotate = 180f;
					} else {
						tempLocY = transform.position.y;
						lockY = true;
					}
				}

				if (movingX > transform.position.x + 0.1f && movingY > transform.position.y +0.1f) {
					rotate = 315f;
				} else if (movingX > transform.position.x + 0.1f && movingY < transform.position.y - 0.1f) {
					rotate = 225f;
				}else if (movingX < transform.position.x -0.1f && movingY > transform.position.y + 0.1f){
					rotate = 45f;
				}else if(movingX < transform.position.x -0.1f && movingY < transform.position.y -0.1f){
					rotate = 135f;
				}

				walking.transform.eulerAngles = new Vector3 (0f,0f,rotate);
				transform.position = new Vector3 (tempLocX, tempLocY, 0f);
			}
		}

	}
	// UPDATE ENDING

	/*---------------------------------------------
				Player Manipulation
	--------------------------------------------*/

	public void moveTo(float x, float y){
		transform.position = new Vector3 (x, y, 0f);
		transform.eulerAngles = new Vector3 (0f,0f,0f);
	}

	public void moveRelative(float x, float y){
		isMoving = true;
		movingX = transform.position.x + x;
		movingY =  transform.position.y + y;

	}
		
	/*---------------------------------------------
				Set Private Values
	--------------------------------------------*/
	public void setSpeed(float speedy){
		speed = speedy;
	}

	/*---------------------------------------------
				Get Private Values
	--------------------------------------------*/
	public float getSpeed(){
		return speed;
	}


}
