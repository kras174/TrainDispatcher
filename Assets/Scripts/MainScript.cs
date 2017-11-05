using UnityEngine;

public class MainScript : MonoBehaviour {

    private Transform spawn;
    public GameObject train;
    private GameObject _wayPoint;
    private GameObject _curSpawnPoint;
    private int _randomSpawnPoint;

    public void CreateTrain()
    {
        _randomSpawnPoint = Random.Range(1, 5);
        spawn = GameObject.FindGameObjectWithTag("SpawnPoints").transform;
        _curSpawnPoint = spawn.Find("SpawnPoint" + _randomSpawnPoint).gameObject;
        Instantiate(train);
        train.transform.rotation = _curSpawnPoint.transform.rotation;
        train.transform.position = _curSpawnPoint.transform.position;
    }

}
