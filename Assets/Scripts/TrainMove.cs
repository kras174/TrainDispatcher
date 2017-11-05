using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrainMove : MonoBehaviour
{
    private NavMeshAgent agent;

    public float functionState = 0;

    private Transform wayPoints;
    private GameObject _currTrafficLight;
    private GameObject _currentWayPoint;
    private int _currTraficLightSignal;
    private int _randomWayPoint;

    public int CurWayPoint;

    private Renderer render;

    void Awake()
    {
        wayPoints = GameObject.FindGameObjectWithTag("WayPoints").transform;
        _randomWayPoint = UnityEngine.Random.Range(1, 4);
        CurWayPoint = _randomWayPoint;

        functionState = 0;
        render = gameObject.GetComponent<Renderer>();

        agent = GetComponent<NavMeshAgent>();

        _currentWayPoint = wayPoints.Find("WayPoint" + _randomWayPoint).gameObject;
        agent.destination = _currentWayPoint.transform.position;
        
    }

    private void Update()
    {
        if (_currTrafficLight)
            functionState = _currTrafficLight.GetComponent<TrafficLight>().LightHandler;

        if (functionState == 0)
        {
            agent.isStopped = false;
            //agent.destination = wayPoint.transform.position;
        }

        if (functionState == 1)
        {
            agent.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "TrafficLight")
        {
            _currTrafficLight = collider.gameObject;
            _currTraficLightSignal = _currTrafficLight.GetComponent<TrafficLight>().LightHandler;
            functionState = _currTraficLightSignal;
        }
        if (collider.tag == "Platform")
        {
            
        }

        if (collider.tag == "Train")
        {
            render.material.color = Color.red;
        }

        if (collider.gameObject == _currentWayPoint)
        {
            render.material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        try
        {
            if (collider.tag == "TrafficLight")
            {
                _currTrafficLight.GetComponent<TrafficLight>().LightHandler = 1;
                _currTrafficLight = null;
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Эта грёбанная ошибка!"); // нужно её исправить
            Debug.Log(ex);
        }
    }
}
