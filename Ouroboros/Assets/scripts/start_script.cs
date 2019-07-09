using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start_script : MonoBehaviour {

	public void load(string name){
		SceneManager.LoadScene (name);
	}
}
