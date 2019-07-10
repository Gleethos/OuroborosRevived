using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System.Globalization;

public class ScriptReader : MonoBehaviour {

	private string subdirectory;
	private string dialogtxt="";
	private string command; 
	private string room = "";

	private string rawDialog = "";
	private string currentLine = "";
	private int    dialogIndex = 0;
	private int    read = 0;
	private int    scriptDepth=0;
	private int globalChoiceCount = 0;
	private StreamReader reader;

	private bool paused = false;
	private bool waitingForEnter= false;
	private bool waitingForAnswer= false;
	private int  AnswerID=0;
	private string jumpToString = "";

	private string dialogOutputString = "";

	private bool end = false;
	public bool stopped = false;

	private PlayerController player;
	private door_script currentExitDoor;
	public Text dialogAusgabe;
	AudioSource audio;
	AudioClip clip;
	public Image display; 

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType (typeof(PlayerController)) as PlayerController;
		dialogAusgabe = GetComponentInChildren (typeof(Text)) as Text;
		display = GetComponentInChildren (typeof(Image)) as Image;
		display.enabled = false;
		//setRoom ("");
		//setDialog ("");
		loadAndPlaySoundFile("MyCave.wav", "room_A");
	}
	
	// Update is called once per frame
	void Update () {
			
		//Checking if there is no dialog set... if there is:
		//checking if a new dialog is set to be loaded!
		if (rawDialog == "" && stopped == false) {
			if (dialogtxt != "") {
				string path = "dialog/" + subdirectory + "/" + dialogtxt;

				StreamReader reader = new StreamReader (path);// CREATING STREAM READER
				rawDialog = reader.ReadToEnd ();//========================================>>> READING TXT FILE!
				reader.Close ();//Closing stream reader...
				//Debug.Log (rawDialog);
				scriptDepth = 0;
				//display.enabled = true;
			}
		} 

		if(waitingForEnter==true){
			if(Input.GetKeyDown (KeyCode.Return))
		  	   {waitingForEnter = false;
				if(rawDialog.Length<dialogIndex){if(rawDialog[dialogIndex]=='\n'){dialogIndex++;}}
				if(rawDialog == ""){
                    //Debug.Log ("raw dialog empty!");
                } else{
                    //Debug.Log ("raw dialog NOT empty");
                }
				if(paused){
                    //Debug.Log ("paused true");
                } else{
                    //Debug.Log ("paused false");
                }
			}
		}
		//Debug.Log ("update");
		if(waitingForAnswer==true){
			//Debug.Log ("waiting! ...for answer");
			if(Input.GetKeyDown (KeyCode.Keypad1) || Input.GetKeyDown (KeyCode.Keypad2) || Input.GetKeyDown (KeyCode.Keypad3) || Input.GetKeyDown (KeyCode.Keypad4) || Input.GetKeyDown (KeyCode.Keypad5) 
				|| Input.GetKeyDown (KeyCode.Alpha1)  || Input.GetKeyDown (KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.Alpha3) || Input.GetKeyDown (KeyCode.Alpha4) || Input.GetKeyDown (KeyCode.Alpha5))
			{
                //Debug.Log ("Player has given an answer!");
		 		waitingForAnswer = false;
				if (Input.GetKeyDown (KeyCode.Keypad1)) {AnswerID = 1; /*Debug.Log ("Answer sent... 1");*/}
				if (Input.GetKeyDown (KeyCode.Keypad2)) {AnswerID = 2; /*Debug.Log ("Answer sent... 2");*/}
				if (Input.GetKeyDown (KeyCode.Keypad3)) {AnswerID = 3; /*Debug.Log ("Answer sent... 3");*/}
				if (Input.GetKeyDown (KeyCode.Keypad4)) {AnswerID = 4; /*Debug.Log ("Answer sent... 4");*/}
				if (Input.GetKeyDown (KeyCode.Keypad5)) {AnswerID = 5; /*Debug.Log ("Answer sent... 5");*/}
				if (Input.GetKeyDown (KeyCode.Keypad6)) {AnswerID = 6; /*Debug.Log ("Answer sent... 6");*/}
				if (Input.GetKeyDown (KeyCode.Keypad7)) {AnswerID = 7; /*Debug.Log ("Answer sent... 7");*/}
				if (Input.GetKeyDown (KeyCode.Keypad8)) {AnswerID = 8; /*Debug.Log ("Answer sent... 8");*/}
				if (Input.GetKeyDown (KeyCode.Keypad9)) {AnswerID = 9; /*Debug.Log ("Answer sent... 9");*/}
				if (Input.GetKeyDown (KeyCode.Alpha1)) {AnswerID = 1; /*Debug.Log ("Answer sent... 1");*/}
				if (Input.GetKeyDown (KeyCode.Alpha2)) {AnswerID = 2; /*Debug.Log ("Answer sent... 2");*/}
				if (Input.GetKeyDown (KeyCode.Alpha3)) {AnswerID = 3; /*Debug.Log ("Answer sent... 3");*/}
				if (Input.GetKeyDown (KeyCode.Alpha4)) {AnswerID = 4; /*Debug.Log ("Answer sent... 4");*/}
				if (Input.GetKeyDown (KeyCode.Alpha5)) {AnswerID = 5; /*Debug.Log ("Answer sent... 5");*/}
				if (Input.GetKeyDown (KeyCode.Alpha6)) {AnswerID = 6; /*Debug.Log ("Answer sent... 6");*/}
				if (Input.GetKeyDown (KeyCode.Alpha7)) {AnswerID = 7; /*Debug.Log ("Answer sent... 7");*/}
				if (Input.GetKeyDown (KeyCode.Alpha8)) {AnswerID = 8; /*Debug.Log ("Answer sent... 8");*/}
				if (Input.GetKeyDown (KeyCode.Alpha9)) {AnswerID = 9; /*Debug.Log ("Answer sent... 9");*/}
				if (AnswerID > globalChoiceCount) {
					waitingForAnswer = true;
				} else {
					globalChoiceCount = 0;}
			}
		}

		//If a dialog is set and it is not paused (because of timeout for example)
		//->new interpretation starts!
		if (rawDialog != "" && paused == false && waitingForEnter == false && waitingForAnswer==false) {
			//display.enabled = true;
			string currentDialogPayload = "";
			string currentMetaCommand = "";
			dialogOutputString = "";
			bool isCommand = false;

			for (int i = dialogIndex; i < rawDialog.Length; i++)//looping through the raw dialog content!
			{
				//IS NOT COMMAND
				//========================================================
				if (isCommand == false) {
						if (rawDialog [i] == '[') {
							isCommand = true; scriptDepth++;
							//Debug.Log ("Dialog payload:" + currentDialogPayload);
							//currentLine += currentDialogPayload;
							currentDialogPayload = "";
						} else {
							if (jumpToString == "") 
								{
							
								if (i > 1) {
									if (rawDialog [i - 1] == ']'||rawDialog [i - 2] == ']') {
										if (rawDialog [i] != '\n') {
											currentLine += rawDialog [i];
										} else {
										//Debug.Log ("Enter sign ignored at index: "+i);
                                        }
									} else {
										currentLine += rawDialog [i];
									}
								} else {
									currentLine += rawDialog [i];}
							
								currentDialogPayload += rawDialog [i];
								}
							}					   
				//IS COMMAND
				//========================================================
				} else if (isCommand == true) {
					if (rawDialog [i] == ']') {
						isCommand = false; scriptDepth--;
						//Debug.Log ("Meta command payload:" + currentMetaCommand);
						if (jumpToString == "") {
							executeMetaCommand (currentMetaCommand);
						} else {
							if(currentMetaCommand==jumpToString){executeMetaCommand (currentMetaCommand);}
						}//executing
						currentMetaCommand = "";
						if (paused == true)       {dialogIndex = i + 1; return;} else {dialogIndex = 0;}
						if(waitingForEnter==true) {dialogIndex = i + 1; return;} else {dialogIndex = 0;}
						if(waitingForAnswer==true){dialogIndex = i + 1; return;} else {dialogIndex = 0;}

					} 
					else {currentMetaCommand += rawDialog [i];}
				}//========================================================

				//rekursive jumpToString search! (Warning! Can cause endless loop!)
				if(jumpToString!="")
				   {if(i==rawDialog.Length-1){i=0;
                        //Debug.Log("Interpreter is restarting loop now! (jumpTo message not found!)");
					}}
			}
			//Debug.Log ("End of dialog char loop reached! dialog index: "+dialogIndex);
			rawDialog = "";//Interpretation is done! rawDialog empty now!
			display.enabled = false;
			dialogAusgabe.text = "";
			currentLine = "";
			dialogIndex = 0;
		} 
			dialogtxt = "";
		if(paused==false && rawDialog=="" && waitingForEnter==false && waitingForAnswer==false){end = true;
            //Debug.Log ("Dialog exited!");
        }
	}

	//Executing commands found in txt files:
	//===================================================
	private void executeMetaCommand(string metaCommand)
	{
        string[] parsed = metaCommand.Split(':');
		string command = parsed[0];
        string value = (parsed.Length > 1) ? parsed[1] :"";
        bool isValue = (parsed.Length > 1) ? true : false ;
        
        if (value.Contains(",")) {
            parsed = value.Split(',');
            value = "";
            for(int i=0; i<parsed.Length; i++) {
                value += parsed[i];
                if (i < (parsed.Length - 1)) {
                    value += ",";
                }
            }
        }
		//Debug.Log ("Command: " + command);
		//Debug.Log ("Value: " + value);

		//if - if else routing: Executing according to command! (If recognizable)
		//===================================================
		if (command == "timeout") {
			float time = 0;
			if (float.TryParse (value, NumberStyles.Any, CultureInfo.InvariantCulture, out time)) {
				//dialogAusgabe.text = currentLine;
				//Debug.Log ("Timout initiated by dialog script!");
				StartCoroutine (timeoutFor (time));
			} else {
				//Debug.Log ("Dialog command 'timeout' failed!");
			}
		 dialogAusgabe.text = currentLine;
		}
		//===================================================
		else if (command == "playsound") {
			//Debug.Log ("Playsound command executing! ->filename: "+value);
			loadAndPlaySoundFile (value, subdirectory);

		}
		//===================================================
		else if (command == "unlockExit") {
			if (currentExitDoor != null) {
				currentExitDoor.setIsLocked ();
				//Debug.Log ("Exit unlocked through dialog script!");
			}
		}
		//===================================================
		else if (command == "choice") {
			//Debug.Log ("...executing choice command now...");
			string content = "Choose: \n";
			//dialogAusgabe.text = content;
			string currentChoice = "";
			bool isChoice = false;
			int choiceCounter = 0;

			for (int i = 0; i < value.Length; i++) {
				if (isChoice == true) {
					if (value [i] == '"') {
						isChoice = false;
						globalChoiceCount++;// How many choices are there?
						choiceCounter++;
						//Debug.Log ("Choice: " + currentChoice);
						content += choiceCounter + ": " + currentChoice + "\n";
						currentChoice = "";
					} else {
						currentChoice += value [i];
					}

				} else {
					if (value [i] == '"') {
						isChoice = true;
					}
				}
			}
			currentLine += content;
		}//===================================================
		else if (command == "waitForAnswer") {
			dialogAusgabe.text = currentLine;
			currentLine = "";
			waitingForAnswer = true;
		}//=================================================== 
		else if (command == "jumpTo") {
			//Debug.Log ("JumpTo command active!");
			for (int i = 0; i < value.Length; i++) {
				if (value [i] == '#') {					
					jumpToString += AnswerID;
				} else {
					jumpToString += value [i];
				}
			}
			//Debug.Log ("JumptToString has been set to: "+jumpToString);
		}//===================================================
		else if (command == "freezeFor") {
			int time = 0;
			if (int.TryParse (value, NumberStyles.Any, CultureInfo.InvariantCulture, out time)) {
				player.SetFreezeFor (time);
				//Debug.Log ("Player frozen through dialog script!");
			} else {
				//Debug.Log ("Dialog command 'freezeFor' failed!");
			}
		}//===================================================
		else if (command == "freeze") {
			player.Freeze();
		}//===================================================
		else if (command == "unfreeze") {
			player.Unfreeze();
		}//===================================================
		else if (command == "waitForEnter") {
			dialogAusgabe.text = currentLine;
			currentLine = "";
			waitingForEnter = true;
		}//===================================================
		else if (command == "movePlayerTo") {
			bool check = false;
			float x = 0;
			float y = 0;
			string currentValuePart = "";

			for (int i = 0; i < value.Length; i++) {				
				if (check == false) {
					if (value [i] != ',') {
						currentValuePart += value [i];
					} else {
						//Debug.Log ("Current valuePart x: " + currentValuePart);
						if (float.TryParse (currentValuePart, NumberStyles.Any, CultureInfo.InvariantCulture, out x)) {
							//dialogAusgabe.text = currentLine;
							//Debug.Log ("x initiated by dialog script!");

						} else {
							//Debug.Log ("Dialog command 'movePlayerTo' failed!");
						}
						currentValuePart = "";
						check = true;
					}
				} else {
					if (value [i] <= '9') {
						currentValuePart += value [i];
					} 
				}

			}

			//Debug.Log ("Current valuePart y: " + currentValuePart);
			if (float.TryParse (currentValuePart, NumberStyles.Any, CultureInfo.InvariantCulture, out y)) {
				//dialogAusgabe.text = currentLine;
				//Debug.Log ("y initiated by dialog script!");

			} else {
				//Debug.Log ("Dialog command 'movePlayerTo' failed!");
			}

			player.MoveTo (x, y);
			currentValuePart = "";
			check = true;
		}//===================================================
		else if (command == "movePlayerRelative") {
			bool check = false;
			float x = 0;
			float y = 0;
			string currentValuePart = "";

			for (int i = 0; i < value.Length; i++) {

				if (check == false) {
					if (value [i] != ',') {
						currentValuePart += value [i];
					} else {
						//Debug.Log ("Current valuePart x: " + currentValuePart);
						if (float.TryParse (currentValuePart, NumberStyles.Any, CultureInfo.InvariantCulture, out x)) {
							//dialogAusgabe.text = currentLine;
							//Debug.Log ("x initiated by dialog script!");

						} else {
							//Debug.Log ("Dialog command 'movePlayerTo' failed!");
						}
						currentValuePart = "";
						check = true;
					}
				} else {
					if (value [i] <= '9') {
						currentValuePart += value [i];
					} 
				}

			}

			//Debug.Log ("Current valuePart y: " + currentValuePart);
			if (float.TryParse (currentValuePart, NumberStyles.Any, CultureInfo.InvariantCulture, out y)) {
				//dialogAusgabe.text = currentLine;
				//Debug.Log ("y initiated by dialog script!");

			} else {
				//Debug.Log ("Dialog command 'movePlayerRelative' failed!");
			}
			//Debug.Log ("Player is being moved by vector: " + x + ", " + y + ";");
			player.MoveRelative (x, y);

		}//===================================================
		else if (command == "returnMessage") {
			dialogOutputString = value;

		}//===================================================
		else if (command == "enableDisplay") {
			display.enabled = true;

		}//===================================================
		else if (command == "disableDisplay") {
			display.enabled = false;

		} else if (command == "exitDialog") {
			//Debug.Log ("Dialog has been ended by meta command! dialog index: " + dialogIndex);
			rawDialog = "";//Interpretation is done! rawDialog empty now!
			display.enabled = false;
			dialogAusgabe.text = "";
			currentLine = "";
			dialogIndex = 0;
			dialogtxt = "";
		} else if(command == jumpToString){
			jumpToString = "";
		}
	}
	//Meta commands:
	// timeout 
	//==========================================================================
	//Timeout function: 
	IEnumerator timeoutFor(float time){
        paused = true;
        yield return new WaitForSeconds(time);
        paused = false;
    }
	//Pauses interpretation process...
	//==========================================================================

	public void setCurrentExitDoor(door_script exit){
		currentExitDoor = exit;}

	public void setRoom(string room){
		subdirectory = room;
	}

	public void setDialog(string dialog){
		dialogtxt = dialog;
	}

	public bool readEnd() {
		bool done = end;
		end = false;
		return done;
	}

	public int getAnswerId(){
		return AnswerID;
    }

	public void resetAnswerID(){
		AnswerID = 0;
    }

	public bool isActive(){
		if(rawDialog==""){
            return false;
        } else{
            return true;
        }
    }
		
	public string getDialogOutput (){
		return dialogOutputString;
    }

	public void setDialogOutputTo(string newOutput){
		dialogOutputString = newOutput;
    }

	private void loadAndPlaySoundFile(string filename, string subdirectoryName)
	{
		audio = GetComponent<AudioSource>();
		//AUDIO
		string path = "file://" + Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + "/Assets/dialog/"+subdirectoryName+"/"+filename;
		//Debug.Log ("Loading soundfile due to dialog script request. Loading file at path: "+path);

		WWW www = new WWW(path);
		//Getting the clip from local files....
		clip = www.GetAudioClip();

		if(clip==null){
            Debug.Log ("Audio file cold not be loaded. Audio clip empty... :/");
        }
		else{
            Debug.Log("Loading successful.");
        }

		//Clip name:
		clip.name = filename;

		//clip.LoadAudioData ();
		audio.playOnAwake = false;
		audio.loop = false;
		audio.clip = clip;

		//audio.PlayOneShot (clip);

		if (!audio.isPlaying)
		{
            //Debug.Log ("Audiofile is being played now.");
			audio.Play();
		}
		//audio.PlayOneShot (clip);

	}

}
