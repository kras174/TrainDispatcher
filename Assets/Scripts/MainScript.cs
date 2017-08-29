using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

    private Transform Spawn;
    public GameObject Train;
    private GameObject WayPoint;
    private GameObject _curSpawnPoint;
    private int _randomSpawnPoint;

    public void CreateTrain()
    {
        _randomSpawnPoint = Random.Range(1, 5);
        Spawn = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        _curSpawnPoint = Spawn.Find("SpawnPoint" + _randomSpawnPoint).gameObject;
        Instantiate(Train);
        Train.transform.position = _curSpawnPoint.transform.position;
    }

}
