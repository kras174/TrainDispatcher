using System.Collections;
using UnityEngine;

public class WaypointsSystem : MonoBehaviour
{
    public float accel = 0.8f;
    public float inertia = 0.9f;
    public float speedLimit = 10.0f;
    public float minSpeed = 1.0f;
    public float stopTime = 1.0f;

    private float currentSpeed = 0.0f;
    public float functionState = 0;
    private bool accelState;
    private bool slowState;
    [SerializeField]
    private Transform[] wayPoints;
    private int wayCount = 0;

    public float rotationDamping = 6.0f;
    public bool smoothRotation = true;

    private GameObject _currTrafficLight;
    private int _currTraficLightSignal;

    private GameObject _currWaypoint;
    private int _currWaypointSignal;

    public float turnState = 0;


    void Start()
    {
        functionState = 0;
    }

    void Update()
    {
        if (_currTrafficLight)
            functionState = _currTrafficLight.GetComponent<TrafficLight>().LightHandler;

        if (_currWaypoint)
            turnState = _currWaypoint.GetComponent<WayPoint>().Waypoint;

        if (functionState == 0)
        {
            Accell();
        }

        if (functionState == 1)
        {
            Slow();
        }
    }

    void Accell()
    {
        if (accelState == false)
        {
            accelState = true;
            slowState = false;
        }

        //if (waypoint)
        //{
        //    if (smoothRotation)
        //    {
        //        var rotation = Quaternion.LookRotation(waypoint.position - transform.position);
        //        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
        //    }
        //}

        currentSpeed = currentSpeed + accel * accel;
        transform.Translate(0, 0, Time.deltaTime * currentSpeed);

        if (currentSpeed >= speedLimit)
        {
            currentSpeed = speedLimit;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "TrafficLight")
        {
            _currTrafficLight = collider.gameObject;
            _currTraficLightSignal = _currTrafficLight.GetComponent<TrafficLight>().LightHandler;
            if (_currTraficLightSignal == functionState)
            {
                functionState = 1;
            }
        }
        if (collider.tag == "WayPoint")
        {
            _currWaypoint = collider.gameObject;
            _currWaypointSignal = _currWaypoint.GetComponent<WayPoint>().Waypoint;

            if (_currWaypointSignal == 0) return;
            if (_currWaypointSignal == 1)
            {
                wayCount++;
                SetRotation(wayPoints[wayCount].position);
                //float toAngle = 45f;
                //var rotation = Quaternion.LookRotation(new Vector3(0f, 45f, 0f) - transform.position);
                //Debug.Log(transform.rotation);
                //Debug.Log(rotation.eulerAngles);
                //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            }
            else if (_currWaypointSignal == -1)
            {
                var rotation = Quaternion.LookRotation(new Vector3(0f, -45f, 0f) - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime/* * rotationDamping*/);
            }
        }
    }

    void Slow()
    {
        if (slowState == false) 
        {
            accelState = false;
            slowState = true;
        }

        currentSpeed = currentSpeed * inertia;
        transform.Translate(0, 0, Time.deltaTime * currentSpeed);

        if (currentSpeed <= minSpeed)
        {
            currentSpeed = 0.0f;
        }
    }


    void SetRotation(Vector3 lookAt)
    {
        Vector3 lookPos = lookAt - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationDamping * Time.deltaTime);
    }
}
