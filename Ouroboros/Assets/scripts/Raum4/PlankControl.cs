using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankControl : MonoBehaviour
{
    [SerializeField]
    private GameObject plank;
    [SerializeField]
    private switch_script switchScript;
    private bool isMoving = false;

    /*
     * if collision wall -> stop moving
     * if switch on -> moving & Rigidbody on
     * if switch off -> moving stop & Rigidbody off
     * if switch on && player collision -> moving correctly
     */

    private void Start() {
        plank.GetComponent<Rigidbody2D> ().isKinematic = true;
    }

    private void Update() {
        if (switchScript.isSwitchedOn () && !isMoving) {
            int number = 3;
            int randomNumber = Random.Range (0, 4);

            plank.GetComponent<Rigidbody2D> ().isKinematic = false;
            
            if(randomNumber == 0)
                plank.GetComponent<Rigidbody2D> ().AddForce (transform.up*number);
            if(randomNumber == 1)
                plank.GetComponent<Rigidbody2D> ().AddForce (transform.right * number);
            if(randomNumber == 2)
                plank.GetComponent<Rigidbody2D> ().AddForce (-transform.up * number);
            if(randomNumber == 3)
                plank.GetComponent<Rigidbody2D> ().AddForce (-transform.right * number);
            isMoving = true;

        }

        if (!switchScript.isSwitchedOn ()) {
            isMoving = false;
            plank.GetComponent<Rigidbody2D> ().isKinematic = true;
        }
        
    }
}
