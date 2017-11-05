using System;
using UnityEngine;
using UnityEngine.AI;

public class TrainMove : MonoBehaviour
{
    private NavMeshAgent agent;

    public float functionState = 0;

    private Transform _wayPoints;
    private GameObject _currTrafficLight;
    private Transform _currentWayPoint;
    private int _currTraficLightSignal;
    private int _randomWayPoint;

    public int curWayPoint;
    public int curSpawnPoint;

    private Renderer render;
    private Transform _spawnPoints;

    void Awake()
    {
        functionState = 0;
        render = gameObject.GetComponent<Renderer>();

        agent = GetComponent<NavMeshAgent>();

        agent.destination = SetDestinationWayPoint().position;

    }

    void Update()
    {
        if (_currTrafficLight)
            functionState = _currTrafficLight.GetComponent<TrafficLight>().LightHandler;

        if (functionState == 0)
        {
            agent.isStopped = false;
        }

        if (functionState == 1)
        {
            agent.isStopped = true;
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "TrafficLight")
        {
            _currTrafficLight = collider.gameObject;
            _currTraficLightSignal = _currTrafficLight.GetComponent<TrafficLight>().LightHandler;
            functionState = _currTraficLightSignal;
        }
        if (collider.tag == "SpawnPoint")
        {
            if (GetComponent<TrainClick>().trainState == 1)
            {
                Destroy(gameObject);
            }
        }

        if (collider.tag == "Train")
        {
            functionState = 1;
            render.material.color = Color.red;
            GetComponent<TrainClick>().trainState = 3;
        }

        if (collider.tag == "WayPoint")
        {
            render.material.color = Color.green;
            GetComponent<TrainClick>().trainState = 2;
        }
    }

    void OnTriggerExit(Collider collider)
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
            //Debug.Log("Эта грёбанная ошибка!"); // нужно её исправить
            //Debug.Log(ex);
        }
    }

    Transform SetDestinationWayPoint()
    {
        _wayPoints = GameObject.FindGameObjectWithTag("WayPoints").transform;
        _randomWayPoint = UnityEngine.Random.Range(1, 4);
        curWayPoint = _randomWayPoint;

        return _wayPoints.Find("WayPoint" + _randomWayPoint).gameObject.transform;

    }

    Transform SetDestinationSpawnPoint()
    {
        _spawnPoints = GameObject.FindGameObjectWithTag("SpawnPoints").transform;
        curSpawnPoint = UnityEngine.Random.Range(1, 5);
        Debug.Log(curSpawnPoint);
        return _spawnPoints.Find("SpawnPoint" + curSpawnPoint).gameObject.transform;

    }

    public void DepartTrain()
    {
        render.material.color = Color.grey;
        functionState = 0;
        agent.destination = SetDestinationSpawnPoint().position;
    }
}
