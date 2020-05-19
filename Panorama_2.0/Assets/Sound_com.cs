using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersSetByName : MonoBehaviour
{
    FMOD.Studio.EventInstance Ambience;

    private void Start()
    {
        Ambience = FMODUnity.RuntimeManager.CreateInstance("event:/Ambience");
        Ambience.start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FPSController")
            Ambience.setParameterByName("Ambience Fade", 1f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "FPSController")
            Ambience.setParameterByName("Ambience Fade", 0f);
    }
}