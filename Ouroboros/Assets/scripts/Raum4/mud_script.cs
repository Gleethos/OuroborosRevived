using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mud_script : MonoBehaviour {

	GameObject Layer1;
	GameObject Layer2;
	GameObject Layer3;
	GameObject Layer4;

	int counter=-10;

	float currentFloodLevel =0;
	float maxFloodLevel= 100;

	Vector3 defaultScale1;
	Vector3 defaultScale2;
	Vector3 defaultScale3;
	Vector3 defaultScale4;

	bool upcount = true;

	bool filling = false;
	bool draining = true;
	GameObject audio_mud;

	void Start () {
		Layer1 = GameObject.Find ("Layer1");
		Layer2 = GameObject.Find ("Layer2");
		Layer3 = GameObject.Find ("Layer3");
		Layer4 = GameObject.Find ("Layer4");

		defaultScale1 = Layer1.transform.localScale;

		defaultScale2 = Layer2.transform.localScale;

		defaultScale3 = Layer3.transform.localScale;

		defaultScale4 = Layer4.transform.localScale;


		//Debug.Log ("default scale 1: "+defaultScale1);
		//Debug.Log ("default scale 2: "+defaultScale2);
		//Debug.Log ("default scale 3: "+defaultScale3);
		//Debug.Log ("default scale 4: "+defaultScale4);
		audio_mud = GameObject.Find ("Mud");

	}

	// Update is called once per frame
	void Update ()
    {
		if (counter < 2) {
			counter++;
		} else {
			counter = 0;

			if (upcount) {
				currentFloodLevel += 0.5f;
				if (currentFloodLevel > 100) {
					currentFloodLevel =100f;
					//upcount = false;//hh
					if (draining == true) {
						upcount = false;
					}
				}
			} else {
				currentFloodLevel -= 0.5f;
				if (currentFloodLevel < 0) {
					currentFloodLevel =0f;
					if(filling==true){upcount = true;}
					//upcount = true;
				}
			}


			float level1 = getL1Level (currentFloodLevel);

			float level2 = getL2Level (currentFloodLevel);

			float level3 = getL3Level (currentFloodLevel);

			float level4 = getL4Level (currentFloodLevel);


			setTransparencyTo (level1, Layer1);

			setTransparencyTo (level2, Layer2);

			setTransparencyTo (level3, Layer3);

			setTransparencyTo (level4, Layer4);

			setScale (defaultScale1, Layer1, level1/40);
			setScale (defaultScale2, Layer2, level2/40);
			setScale (defaultScale3, Layer3, level3/40);
			setScale (defaultScale4, Layer4, level4/40);
		}
	}

	public bool isFilling(){
		return draining;
    }
	
	public bool isDraining(){
		return filling;
	}

	public void fill(){
        //Debug.Log ("Fill activated!");
		filling = false; draining = true;
		upcount = false;
			//audio_mud.GetComponent<AudioSource> ().Play ();

	}
	public void drain(){
		draining = false; filling = true;
		upcount = true;

//			audio_mud.GetComponent<AudioSource> ().Stop ();

	}

		
	private void setTransparencyTo(float transparency, GameObject layer){
		if(transparency<0 || transparency>1){return;}
		SpriteRenderer renderer = layer.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		renderer.color = new Color (1f, 1f, 1f, transparency);
	}
		
	private void setScale(Vector3 scale, GameObject layer, float mod){
//		Debug.Log ("Scaling");
		layer.transform.localScale = new Vector3 (scale.x+mod, scale.y+mod, scale.z+mod);
	}
		
	private float getL1Level(float input)
	{return getSigmoidOf (input, -5f, (3f/20f), 1f);
	}
	private float getL2Level(float input)
	{return getSigmoidOf (input, -8f, (3f/20f), 1f);
	}
	private float getL3Level(float input)
	{return getSigmoidOf (input, -11f, (3f/20f), 1f);
	}
	private float getL4Level(float input)
	{return getSigmoidOf (input, -14f, (3f/20f), 1f);
	}

	private float getSigmoidOf(float input, float shift, float inputMod, float multiplier)
	{return multiplier * 1 / (1 + Mathf.Pow (2.7182818284f, input * inputMod + shift));}


}


