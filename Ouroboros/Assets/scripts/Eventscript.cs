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

    protected Eventscript() {
        player = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }

    public void activate() {
        isActive = true;
    }

    public bool roomIsSolved() {
        return roomSolved;
    }

    public bool roomIsActive() {
        return isActive;
    }

    public float getCenterX() {
        return centerX;
    }
    public float getCenterY() {
        return centerY;
    }
}
