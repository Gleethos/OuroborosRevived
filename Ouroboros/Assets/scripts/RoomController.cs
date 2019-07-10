using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    private GameObject player;
    private Eventscript currentRoom;
    private Eventscript nextRoom;

    public string room;

    private void Start() {
        player = GameObject.FindWithTag ("Player");
    }
    private void Update() {

        switch (room) {
            case "A":
                currentRoom = GameObject.FindObjectOfType (typeof (event_script_A)) as event_script_A;
                nextRoom = GameObject.FindObjectOfType (typeof (event_script_R1)) as event_script_R1;
                //player.transform.position = new Vector3()
                currentRoom.activate ();
                break;
            case "1":
                break;
            case "2":
                break;
            case "3":
                break;
            case "4":
                break;
            case "5":
                break;
            case "P":
                break;
            default:
                break;
        }
    }
}
