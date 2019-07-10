using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //... Show in Inspectors
    [SerializeField]
    private float ms;
    [SerializeField]
    private Sprite idle;

    //... Animator Control
    private Animator walking;
    private SpriteRenderer idleRender;

    //... Dialogue Control
    private ScriptReader dialog;
    private string roomDir = "room_A";

    //... Movement
    private bool isFreeze = false;
    private bool isResponsiv = true;
    private Vector2 direction;
    private float rotate = 0f;
    private bool isMoving = false;
    private float movingX;
    private float movingY;
    private bool lockX = false;
    private bool lockY = false;
    private float time = 0f;
    private bool checkF = false;

    //... Inventory
    private string [] itemHolder = new string [30];
    private int itemIndex = 0;
    private bool invActive = false;

    void Start() {
        walking = GetComponentInChildren (typeof (Animator)) as Animator;
        idleRender = GetComponentInChildren (typeof (SpriteRenderer)) as SpriteRenderer;
        dialog = GameObject.FindObjectOfType (typeof (ScriptReader)) as ScriptReader;

        walking.enabled = false;

    }

    /*
		Program Behavior:
		---------------------------
		* Open/Close Inventory -> Update
		* Rotate Camera -> Update
		* Interaction -> Update

		* Move Player to Point X/Y -> fixed Update
		* Move Player by pressing key -> fixed Update
		* Start/End Walk Animation -> fixed Update
			Switch to Idle
			
		* Freeze/Unfreeze Player -> both
		* Asking if Console -> both
	*/


    void FixedUpdate() {

        if (!isFreeze && isResponsiv) {

            GetDirection ();
            Move ();

        } else {

            if (isMoving) {

                float xDistance =  Mathf.Abs (Mathf.Abs (movingX) - Mathf.Abs (transform.position.x));
                float yDistance =  Mathf.Abs (Mathf.Abs (movingY) - Mathf.Abs (transform.position.y));

                if(xDistance < 0.009 && yDistance < 0.009) {
                    isMoving = false;
                    lockY = false;
                    lockX = false;
                    walking.enabled = false;
                    idleRender.sprite = idle;
                    GetComponent<AudioSource> ().Stop ();
                } else {

                    float tempLocX = transform.position.x;
                    float tempLocY = transform.position.y;

                    if(xDistance < 0.009) {
                        lockX = true;
                    } else if (transform.position.x > movingX) {
                        tempLocX = transform.position.x - 0.35f * ms * Time.deltaTime;
                        rotate = 90f;
                    } else if (transform.position.x < movingX) {
                        tempLocX = transform.position.x + 0.35f * ms * Time.deltaTime;
                        rotate = 270f;
                    }

                    if (!lockY) {

                        if (yDistance < 0.009) {
                            lockY = true;
                        } else if (transform.position.y < movingY) {
                            tempLocY = transform.position.y + 0.35f * ms * Time.deltaTime;
                            rotate = 0f;
                        } else if (transform.position.y > movingY) {
                            tempLocY = transform.position.y - 0.35f * ms * Time.deltaTime;
                            rotate = 180f;
                        } 
                    }

                    if (movingX > transform.position.x + 0.009f && movingY > transform.position.y + 0.009f) {
                        rotate = 315f;
                    } else if (movingX > transform.position.x + 0.009f && movingY < transform.position.y - 0.009f) {
                        rotate = 225f;
                    } else if (movingX < transform.position.x - 0.009f && movingY > transform.position.y + 0.009f) {
                        rotate = 45f;
                    } else if (movingX < transform.position.x - 0.009f && movingY < transform.position.y - 0.009f) {
                        rotate = 135f;
                    }

                    walking.transform.eulerAngles = new Vector3 (0f, 0f, rotate);
                    transform.position = new Vector3 (tempLocX, tempLocY, 0f);

                    walking.enabled = true;
                    GetComponent<AudioSource> ().Play ();

                }
            }

        }
    }

    void Update() {

        if (!isFreeze && isResponsiv) {
            
            if (Input.GetKeyDown (KeyCode.M) && !invActive) { 
                StartCoroutine (DisplayItemHolder ());
            }else if (Input.GetKeyDown (KeyCode.M) && invActive) {

                dialog.dialogAusgabe.text = "";
                dialog.display.enabled = false;
                invActive = false;

            }

        }

        if (checkF == true)
        {
            StartCoroutine(FreezeFor(time));
        }

    }

    //... Player Movement
    public void Move() {
        transform.Translate (direction * Time.deltaTime);
    }

    private void GetDirection() {

        //Start-Input für direction
        direction = Vector2.zero;

        if ((Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D)) || (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.RightArrow))) {
            walking.enabled = true;
            GetComponent<AudioSource> ().Play ();

            rotate = 315f + transform.eulerAngles.z;
            walking.transform.eulerAngles = new Vector3 (0f, 0f, rotate);
            
            direction += Vector2.up * (Mathf.Sqrt (Mathf.Pow (ms, 2) / 2));
            direction += Vector2.right * (Mathf.Sqrt (Mathf.Pow (ms, 2) / 2));

        } else if ((Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A)) || (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.LeftArrow))) {
            walking.enabled = true;
            GetComponent<AudioSource> ().Play ();

            rotate = 45f + transform.eulerAngles.z;
            walking.transform.eulerAngles = new Vector3 (0f, 0f, rotate);
            

            direction += Vector2.up * (Mathf.Sqrt (Mathf.Pow (ms, 2) / 2));
            direction += Vector2.left * (Mathf.Sqrt (Mathf.Pow (ms, 2) / 2));

        } else if ((Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.D)) || (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.RightArrow))) {
            walking.enabled = true;
            GetComponent<AudioSource> ().Play ();

            rotate = 225f + transform.eulerAngles.z;
            walking.transform.eulerAngles = new Vector3 (0f, 0f, rotate);

            direction += Vector2.down * (Mathf.Sqrt (Mathf.Pow (ms, 2) / 2));
            direction += Vector2.right * (Mathf.Sqrt (Mathf.Pow (ms, 2) / 2));

        } else if ((Input.GetKey (KeyCode.S) && Input.GetKey (KeyCode.A)) || (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.LeftArrow))) {
            walking.enabled = true;
            GetComponent<AudioSource> ().Play ();

            rotate = 135f + transform.eulerAngles.z;
            walking.transform.eulerAngles = new Vector3 (0f, 0f, rotate);  
            
            direction += Vector2.down * (Mathf.Sqrt(Mathf.Pow(ms,2)/2));
            direction += Vector2.left * (Mathf.Sqrt (Mathf.Pow (ms, 2) / 2));

        } else if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
            walking.enabled = true;
            GetComponent<AudioSource> ().Play ();

            rotate = 0f + transform.eulerAngles.z;
            walking.transform.eulerAngles = new Vector3 (0f, 0f, rotate);
            
            direction += Vector2.up * ms;

        } else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
            walking.enabled = true;
            GetComponent<AudioSource> ().Play ();

            rotate = 180f + transform.eulerAngles.z;
            walking.transform.eulerAngles = new Vector3 (0f, 0f, rotate);            
            
            direction += Vector2.down * ms;

        } else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
            walking.enabled = true;
            GetComponent<AudioSource> ().Play ();

            rotate = 90f + transform.eulerAngles.z;
            walking.transform.eulerAngles = new Vector3 (0f, 0f, rotate);            
            
            direction += Vector2.left * ms;

        } else if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
            walking.enabled = true;
            GetComponent<AudioSource> ().Play ();

            rotate = 270f + transform.eulerAngles.z;
            walking.transform.eulerAngles = new Vector3 (0f, 0f, rotate);
            
            direction += Vector2.right * ms;

        } else {

            GetComponent<AudioSource> ().Stop ();
            walking.enabled = false;
            idleRender.sprite = idle;
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

    //... Player Manipulation
    public void Freeze() {
        isFreeze = true;
        GetComponent<AudioSource> ().Stop ();
        walking.enabled = false;
        idleRender.sprite = idle;
    }

    IEnumerator FreezeFor(float time)
    {
        Freeze();
        yield return new WaitForSeconds(time);
        Unfreeze();
        checkF = false;
    }

    public void SetFreezeFor(float time)
    {
        this.time = time;
        checkF = true;
    }

    public void Unfreeze() {
        isFreeze = false;
    }

    public void TeleportTo(float x, float y) {
        transform.position = new Vector3 (x, y, 0f);
    }

    public void MoveRelative(float x, float y) {
        isMoving = true;
        movingX = transform.position.x + x;
        movingY = transform.position.y + y;
    }

    public void MoveTo(float x, float y) {
        isMoving = true;
        movingX = x;
        movingY = y;
    }

    public void Rotate(float degree) {
        walking.transform.eulerAngles = new Vector3 (0f, 0f, degree);
    }

    //... Inventory
    IEnumerator DisplayItemHolder() {

        string dialogtxt = "";

        dialog.display.enabled = true;

        for (int i = 0; i < itemIndex && itemHolder [i] != null; i++) {
            dialogtxt += itemHolder [i] + " | ";
        }

        dialog.dialogAusgabe.text = "Memories\n";
        if (itemHolder.Length > 0)
            dialog.dialogAusgabe.text += "| " + dialogtxt;

        yield return null;

    }

    public string GetFirstItem() {
        return itemHolder [0];
    }

    public void SetItemHolder(string item) {
        itemHolder [itemIndex] = item;
        itemIndex++;
    }

    public void DeleteIventory() {
        for (int i = 0; i < itemIndex; i++) {
            itemHolder [i] = null;
        }
    }

    //... GETTER / SETTER
    public void SetRoom(string room) {
        roomDir = room;
    }

    public bool IsResponsiv {
        get { return isResponsiv; }
        set { isResponsiv = value; }
    }

    public float Movementspeed {
        get { return ms; }
        set { ms = value; }
    }

    public float GetX {
        get { return transform.position.x; }
    }

    public float GetY {
        get { return transform.position.y; }
    }
}
