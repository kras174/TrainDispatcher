using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void MainLogicEventHendler ();

    public static event MainLogicEventHendler TrafficLightEvent;
    public static event MainLogicEventHendler WayPointEvent;
    public static event MainLogicEventHendler PlatformEvent;

    public void TrafficLightLogic()
    {
        if(TrafficLightEvent != null)
        {
            TrafficLightEvent();
        }
    }
}
