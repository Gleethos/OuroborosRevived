using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    
	private player_script player;
	private GameObject playerCameraObject;
	private Camera playerCamera;

    private List<GameObject> listeners = new List<GameObject>();
    
	void Start ()
    {
        // get root objects in scene
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        // iterate root objects and do something
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject gameObject = rootObjects[i];
            AddListener(gameObject);
        }//Adding all game objects!

        Debug.developerConsoleVisible = true;

		player = GameObject.FindObjectOfType (typeof(player_script)) as player_script;
		playerCameraObject = GameObject.FindWithTag ("MainCamera");
		playerCamera = playerCameraObject.GetComponent<Camera> ();
        
        AddListener(gameObject);
        AddListener(GameObject.Find("player"));
        AddListener(GameObject.Find("EventManager_Anfang"));
        AddListener(GameObject.Find("EventManager_R1"));
        AddListener(GameObject.Find("EventManager_R2"));
        AddListener(GameObject.Find("EventManager_R3"));
        AddListener(GameObject.Find("EventManager_R4"));
        AddListener(GameObject.Find("EventManager_R5"));
        AddListener(GameObject.Find("EventManager_Plateau"));

    }
    public void AddListener(GameObject listener) {
        if (!listeners.Contains(listener)) {
            listeners.Add(listener);
        }
    }
    
    public bool executeStateCommand(string input)
    {
        if (input.Equals("help")) {
            
        }

        string[] parts = input.Split(new char[] {'.', '(', ')' }, 4);
        if (parts == null) { return false; }
        if (listeners.Where(obj => obj.name == parts[0]) == null) { return false; }
        GameObject go = listeners.Where(obj => obj.name == parts[0]).SingleOrDefault();
        if (go != null)
        {
            Debug.Log("Receiver object found: " + go.name);
            go.SendMessage(parts[1], parts[2]);
            return true;
        }
        else
        {
            return false;
        }

    }

    public string AvailableObjects() {
        string result = "Available objects:\n";
        foreach (GameObject go in listeners) {
            result += go.name + "\n";
        }
        return result;
    }


    void Update () {

		//size calculation utility variables:
		
        

	}


}
