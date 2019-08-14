using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self : MonoBehaviour
{

    Dictionary<string, float> traits;

    // Start is called before the first frame update
    void Start()
    {
        traits = new Dictionary<string, float>
        {
            {"Honest", 0},
            {"Apathetic", 0},
            {"Dogmatic", 0},
            {"Agnostic", 0},
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Gain(string t)
    {
        Mod(t, 1);
        SideEffectsOf(t, true);
    }

    public void Lose(string t)
    {
        Mod(t, -1);
        SideEffectsOf(t, false);

    }

    private void SideEffectsOf(string t, bool sign)
    {
        switch(t)
        {
            case "Honest":
                Mod("Empathetic", 0.35f * ((sign) ? 1 : -1));
                break;
            case "Apathetic":
                Mod("Empathetic", -0.5f*((sign)?1:-1));
                Mod("Determent", -0.8f * ((sign) ? 1 : -1));
                break;
            case "Dogmatic":
                Mod("Empathetic", -0.5f * ((sign) ? 1 : -1));
                Mod("Agnostic", -1f * ((sign) ? 1 : -1));
                Mod("Blunt", 0.09f * ((sign) ? 1 : -1));
                break;
            case "Nihilistic":
                Mod("Determent", -0.8f * ((sign) ? 1 : -1));
                break;
            case "Empathetic":
                Mod("Greedy", -0.2f * ((sign) ? 1 : -1));
                Mod("Apathetic", -0.5f * ((sign) ? 1 : -1));
                Mod("Bhodi", -0.2f * ((sign) ? 1 : -1));
                break;
            case "Blunt":
                Mod("Bhodi", 0.1f * ((sign) ? 1 : -1));
                break;
            case "Determent":
                Mod("Bhodi", -0.09f * ((sign) ? 1 : -1));
                break;
            case "Optimistic":
                Mod("Bhodi", -0.09f * ((sign) ? 1 : -1));
                break;
            case "Humble":
                Mod("Bhodi", 0.4f * ((sign) ? 1 : -1));
                break;
            case "Greedy":
                Mod("Bhodi", -0.6f * ((sign) ? 1 : -1));
                break;
            case "Bodhi":
                Mod("Greedy", -0.1f * ((sign) ? 1 : -1));
                Mod("Apathetic", -0.4f * ((sign) ? 1 : -1));
                Mod("Honest", 0.3f * ((sign) ? 1 : -1));
                Mod("Patient", 0.1f * ((sign) ? 1 : -1));
                break;
            case "Patient":
                Mod("Bhodi", 0.1f * ((sign) ? 1 : -1));
                break;
            default:
                break;
        }
      
    }

    private void Mod(string t, float v) {
        traits[t] = traits[t]+v;
    }




}
