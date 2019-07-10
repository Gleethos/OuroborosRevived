using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class player_script : MonoBehaviour {

	//ms regulieren
	[SerializeField]
	private float speed;
	[SerializeField]
	private Sprite idle_bild;

	private Vector2 direction;
	private bool occ = false;
	private bool freezy = false;
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
	private string roomDir = "room_A";
	[SerializeField]
	private bool controlDelete = false;
	private StreamReader reader;
	private ScriptReader dialog;
	public Text dialogAusgabe;
	public Image display; 
	[SerializeField]
	private bool lockX = false;
	[SerializeField]
	private bool lockY = false;

    private bool invActive = false;


	void Start () {
		walking = GetComponentInChildren(typeof(Animator)) as Animator;
		idle = GetComponentInChildren (typeof(SpriteRenderer)) as SpriteRenderer;
		dialog = GameObject.FindObjectOfType(typeof(ScriptReader)) as ScriptReader;

	}
	

	void Update () {


		if (freezy == false) {
			GetDirection ();
			Move ();
			if (Input.GetKeyDown(KeyCode.I) && !invActive){

				StartCoroutine (displayItemHolder ());
                invActive = true;
			} else if (Input.GetKeyDown (KeyCode.I) && invActive) {

                dialog.dialogAusgabe.text = "";
                dialog.display.enabled = false;
                invActive = false;

            }

            if (Input.GetKey (KeyCode.H)) {
				occInteraction ();
			}
		}

		if (checkF == true) {
			StartCoroutine (freezeFor (time));
		}

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
				GetComponent<AudioSource> ().UnPause ();

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
				Player Movement
	--------------------------------------------*/

	//controll the movement of the player
	public void Move(){
	
		transform.Translate (direction*speed*Time.deltaTime);
	}

	//Keyboard input for movement
	private void GetDirection(){

		//Start-Input für direction
		direction = Vector2.zero;

		if ((Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) || (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.RightArrow))) {
			walking.enabled = true;
			rotate = 315f + transform.eulerAngles.z;
			walking.transform.eulerAngles = new Vector3 (0f,0f,rotate);
			//===
			GetComponent<AudioSource> ().UnPause ();
			//GetComponent<AudioSource> ().loop = true;
			//==

			direction += Vector2.up;
			direction += Vector2.right;

		} else if ((Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A)) || (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.LeftArrow))) {
			walking.enabled = true;
			rotate = 45f + transform.eulerAngles.z;
			walking.transform.eulerAngles = new Vector3 (0f,0f,rotate);
			//===
			GetComponent<AudioSource> ().UnPause ();
			//GetComponent<AudioSource> ().loop = true;
			//==

			direction += Vector2.up;
			direction += Vector2.left;

		} else if ((Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) || (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.RightArrow))) {
			walking.enabled = true;
			rotate = 225f + transform.eulerAngles.z;
			walking.transform.eulerAngles = new Vector3 (0f,0f,rotate);
			//===
			GetComponent<AudioSource> ().UnPause ();
			//GetComponent<AudioSource> ().loop = true;
			//==

			direction += Vector2.down;
			direction += Vector2.right;

		} else if ((Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) || (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.LeftArrow))) {
			walking.enabled = true;
			rotate = 135f + transform.eulerAngles.z;
			walking.transform.eulerAngles = new Vector3 (0f,0f,rotate);
			//===
			GetComponent<AudioSource> ().UnPause ();
			//GetComponent<AudioSource> ().loop = true;
			//==

			direction += Vector2.down;
			direction += Vector2.left;

		} else if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			walking.enabled = true;
			rotate = 0f + transform.eulerAngles.z;
			walking.transform.eulerAngles = new Vector3 (0f,0f,rotate);
			
			//===
			GetComponent<AudioSource> ().UnPause ();
			//GetComponent<AudioSource> ().loop = true;
			//==
			direction += Vector2.up;
			
		} else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			walking.enabled = true;
			rotate = 180f + transform.eulerAngles.z;
			walking.transform.eulerAngles = new Vector3 (0f,0f,rotate);

			//===
			GetComponent<AudioSource> ().UnPause ();
			//GetComponent<AudioSource> ().loop = true;
			//==
			direction += Vector2.down;
			
		} else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			walking.enabled = true;
			rotate = 90f + transform.eulerAngles.z;
			walking.transform.eulerAngles = new Vector3 (0f,0f,rotate);
				//===
			GetComponent<AudioSource> ().UnPause ();
			//GetComponent<AudioSource> ().loop = true;
			//==
			direction += Vector2.left;

		} else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			walking.enabled = true;
			rotate = 270f + transform.eulerAngles.z;
			walking.transform.eulerAngles = new Vector3 (0f,0f,rotate);

			//===
			GetComponent<AudioSource> ().UnPause ();
			//GetComponent<AudioSource> ().loop = true;
			//==
			direction += Vector2.right;

		} else {
			
			//===
			//GetComponent<AudioSource> ().loop=false;
			GetComponent<AudioSource> ().Pause ();
			walking.enabled = false;
			idle.sprite = idle_bild;
			//==
		}
			

		if (Input.GetKey (KeyCode.Q)) {
			Vector3 temp = gameObject.transform.eulerAngles;
			temp.z = 1;
			gameObject.transform.eulerAngles -= temp;
		} else if (Input.GetKey (KeyCode.E)) {
			Vector3 temp = gameObject.transform.eulerAngles;
			temp.z = 1;
			gameObject.transform.eulerAngles += temp;
		}

	}

	/*---------------------------------------------
				Player Manipulation
	--------------------------------------------*/

	public void freeze(){
		freezy = true;
		GetComponent<AudioSource> ().Pause ();
		walking.enabled = false;
		idle.sprite = idle_bild;
	}
	public void unfreeze(){
		freezy = false;
	}


	IEnumerator freezeFor(int time){
		freeze ();
		yield return new WaitForSeconds(time); 
		unfreeze ();
		checkF = false;
	}

	public void setFreezeFor(int time){
		this.time = time;
		checkF = true;
	}

	public void moveTo(float x, float y){
		transform.position = new Vector3 (x, y, 0f);
	}

	public void moveRelative(float x, float y){
		isMoving = true;
		movingX = transform.position.x + x;
		movingY =  transform.position.y + y;

	}

	/*---------------------------------------------
				Player Inventory
	--------------------------------------------*/

	//Not ready today
	public void printItemHolder(){
		for (int i = 0; i < itemIndex; i++) {
			Debug.Log (itemHolder[i]);
		}
	}
	public void setItemHolder(string item){
		itemHolder [itemIndex] = item;
		itemIndex++;
	}

	// Delete and Save rethink
	public void saveItemHolder(){
		System.IO.StreamWriter file = new System.IO.StreamWriter("dialog/"+roomDir+"/ItemHolder.txt");

		for (int i = 0; i < itemIndex; i++) {
			if (itemHolder [i] != "") {
				file.WriteLine (itemHolder [i]);
			}

		}

		file.Close();
	}

	public void deleteIventory(){
		//Debug.Log ("Deleting success");
		for (int i = 0; i < itemIndex; i++) {
				itemHolder [i] = null;
		}
	}

IEnumerator displayItemHolder(){

        string dialogtxt = "";

        dialog.display.enabled = true;

        for (int i = 0; i < itemIndex && itemHolder [i] != null; i++) {
            dialogtxt += itemHolder [i] + " | ";
        }

        dialog.dialogAusgabe.text = "Memories\n";
        if (itemHolder[0] != null)
            dialog.dialogAusgabe.text += "| " + dialogtxt;

        yield return null;

    }
	public void setRoom(string room){
		roomDir = room;
	}

	public string getFirstItem(){
		string firstItem = itemHolder [0];
		return firstItem;
	}

	/*---------------------------------------------
				Player Interaction
	--------------------------------------------*/
	private void occInteraction(){
		occ = true;
	}
		
	public bool needlessInteraction(){
		bool occured = occ;
		occ = false;
		return occured;
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
		
	public float getX(){
		return transform.position.x;
	}

	public float getY(){
		return transform.position.y;
	}

}
