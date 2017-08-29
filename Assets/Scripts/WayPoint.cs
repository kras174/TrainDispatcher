using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour {
    [SerializeField]
    private int _waypoint;

    public int Waypoint
    {
        get { return _waypoint; }
        set { _waypoint = value; }
    }
}
