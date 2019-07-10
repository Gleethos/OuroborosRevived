using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventscript : MonoBehaviour
{
    protected bool isActive = false;
    protected bool roomSolved = false;
    protected float centerX = 0;
    protected float centerY = 0;
    protected PlayerController player;
    protected int eventCounter = 0;

    
    public void Setup() {
        player = GameObject.FindObjectOfType (typeof (PlayerController)) as PlayerController;
    }
    
    public void ResetEventCounter() {
        eventCounter = 0;
    }

    public void Unsolve() {
        roomSolved = false;
    }

    public void Solve() {
        roomSolved = true;
    }

    public void Deactivate() {
        isActive = false;
    }

    public void Activate() {
        isActive = true;
    }

    public bool RoomIsSolved() {
        return roomSolved;
    }

    public bool RoomIsActive() {
        return isActive;
    }

    public float GetCenterX() {
        return centerX;
    }
    public float GetCenterY() {
        return centerY;
    }
}
