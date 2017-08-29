using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightTougle : MonoBehaviour {

    void OnEnable()
    {
        EventManager.TrafficLightEvent += TrafficLight;
    }

    void OnDisable()
    {
        EventManager.TrafficLightEvent -= TrafficLight;
    }

    void TrafficLight()
    {
        Debug.Log("EventCall");
    }
}
