using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self : MonoBehaviour
{

    Dictionary<string, float> traits = new Dictionary<string, float>
    {
        {"Apathetic", 0},
        {"Dogmatic", 0},
        {"Nihilistic", 0},
        {"Empathetic", 0},
        {"Blunt", 0},
        {"Determent", 0},
        {"Optimistic", 0},
        {"Humble", 0},
        {"Greedy", 0},
        {"Bodhi", 0},
        {"Patient", 0}

    };



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Gain(string t) {

    }

    public void Lose(string t) {

    }


    private void Mod(string t, float v) {
        traits[t] = traits[t]+v;
    }




}
