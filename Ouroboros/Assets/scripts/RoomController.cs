using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private GameObject player;
    private Eventscript currentRoom = null;
    private Eventscript nextRoom = null;

    private SpriteRenderer darkRoomA;
    private SpriteRenderer darkRoom1;
    private SpriteRenderer darkRoom2;
    private SpriteRenderer darkRoom3;
    private SpriteRenderer darkRoom4;
    private SpriteRenderer darkRoom5;
    private SpriteRenderer darkRoomP;

    private Eventscript [] eventscripts = new Eventscript [7];

    [SerializeField]
    private string room = null;

    public string Room {
        get { return room; }
        set { room = value; }
    }

    private void Start() {
        player = GameObject.FindWithTag ("Player");

        darkRoomA = GameObject.Find ("anfangsraumD").GetComponent<SpriteRenderer> ();
        darkRoom1 = GameObject.Find ("raum1D").GetComponent<SpriteRenderer> ();
        darkRoom2 = GameObject.Find ("raum2D").GetComponent<SpriteRenderer> ();
        darkRoom3 = GameObject.Find ("raum3D").GetComponent<SpriteRenderer> ();
        darkRoom4 = GameObject.Find ("raum4D").GetComponent<SpriteRenderer> ();
        darkRoom5 = GameObject.Find ("raum5D").GetComponent<SpriteRenderer> ();

        eventscripts [0] = GameObject.FindObjectOfType (typeof (event_script_A)) as event_script_A;
        eventscripts [1] = GameObject.FindObjectOfType (typeof (event_script_R1)) as event_script_R1;
        eventscripts [2] = GameObject.FindObjectOfType (typeof (event_script_R2)) as event_script_R2;
        eventscripts [3] = GameObject.FindObjectOfType (typeof (event_script_R3)) as event_script_R3;
        eventscripts [4] = GameObject.FindObjectOfType (typeof (event_script_R4)) as event_script_R4;
        eventscripts [5] = GameObject.FindObjectOfType (typeof (event_script_R5)) as event_script_R5;
        eventscripts [6] = GameObject.FindObjectOfType (typeof (event_script_P)) as event_script_P;




    }

    private void ResetRoom() {
        currentRoom.ResetEventCounter ();
        currentRoom.Unsolve ();
        currentRoom.Deactivate ();
    }

    private void Update() {
        if (Input.GetKeyDown (KeyCode.R)) {
            currentRoom.ResetEventCounter ();
            currentRoom.Unsolve ();
            currentRoom.Deactivate ();
            currentRoom = null;
        }
        

        
        if (!string.IsNullOrEmpty(room) && (currentRoom == null || !currentRoom.name.Contains (room))) {

            darkRoomA.enabled = false;
            darkRoom1.enabled = false;
            darkRoom2.enabled = false;
            darkRoom3.enabled = false;
            darkRoom4.enabled = false;
            darkRoom5.enabled = false;

            if (currentRoom != null)
                ResetRoom ();
            else
                for (int i = 0; i < eventscripts.Length; i++) {
                    eventscripts [i].ResetEventCounter ();
                    eventscripts [i].Unsolve ();
                    eventscripts [i].Deactivate ();
                }
                    

            switch (room) {

                case "A":
                    currentRoom = eventscripts[0];
                    nextRoom = eventscripts [1];
                    player.transform.position = new Vector3 (-0.3f, -19f, 0f);
                    currentRoom.Activate ();
                    break;
                case "1":
                    currentRoom = eventscripts [1];
                    nextRoom = eventscripts [2];
                    player.transform.position = new Vector3 (-9.04f, -6.6f, 0f);
                    currentRoom.Activate ();
                    break;
                case "2":
                        currentRoom = eventscripts [2];
                        nextRoom = eventscripts [3];
                    player.transform.position = new Vector3 (-11.85f, 2.4f, 0f);
                    currentRoom.Activate ();
                    break;
                case "3":
                    currentRoom = eventscripts [3];
                    nextRoom = eventscripts [4];
                    player.transform.position = new Vector3 (-6.4f, 11.4f, 0f);
                    currentRoom.Activate ();
                    break;
                case "4":
                    currentRoom = eventscripts [4];
                    nextRoom = eventscripts [5];
                    player.transform.position = new Vector3 (10.41f, 7.47f, 0f);
                    currentRoom.Activate ();
                    break;
                case "5":
                    currentRoom = eventscripts [5];
                    nextRoom = eventscripts [6];
                    player.transform.position = new Vector3 (12.43f, -1.83f, 0f);
                    currentRoom.Activate ();
                    break;
                case "P":
                    currentRoom = eventscripts [6];
                    nextRoom = null;
                    player.transform.position = new Vector3 (3.4f, -1.7f, 0f);
                    currentRoom.Activate ();
                    break;
                default:
                    break;
            }
        }
    }
}
