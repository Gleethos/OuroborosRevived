using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_depth_script : MonoBehaviour {
	[SerializeField]
	private float size;
	private int status = 0;
	private int status2 = 0;
	private float scale = 1f;
	private int scaleSet = 0;

	void Update () {

		if (status == 1) { 
			if (scaleSet == 0) {
				scale = transform.localScale.x;
				scaleSet++;
			}

			if (status2 == 1 && transform.localScale.x < (scale * size)) {
				transform.localScale = new Vector3 (transform.localScale.x * (1 + (size * (Time.deltaTime/2))), transform.localScale.y * (1 + (size * (Time.deltaTime/2))), 1f);
			} else if (status2 == 2 && transform.localScale.x > (scale * size)) {
				transform.localScale = new Vector3 (transform.localScale.x * (1 - (size * (Time.deltaTime))), transform.localScale.y * (1 - (size * (Time.deltaTime))), 1f);
			} else {
				status = 0;
				status2 = 0;
				scaleSet = 0;
			}

		}
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "deep") {
			size = 6/10f;
			status = 1;
			status2 = 2;
		}
		if (coll.gameObject.tag == "height") {
			size = 10/6f;
			status = 1;
			status2 = 1;
		}
	}
	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "deep") {
			size = 10/6f;
			status = 1;
			status2 = 1;
		}
		if (coll.gameObject.tag == "height") {
			size = 6/10f;
			status = 1;
			status2 = 2;
		}
	}

}
