using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float waitTime=3f;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private List<Transform> spawnPositions;
    private List<GameObject> _activeList=new List<GameObject>();
    private List<GameObject> _inactiveList=new List<GameObject>();
    private int _defaultSpawnCount=10;
    private int _currentSpawnCount=10;
    private int _maximumSpawnCount=100;
    private bool _shouldSpawn = true;

    void Start()
    {
        Helicoptor.OnGameOver += DisableSpawn;
        GameManager.OnGamestart += GameStart;
        for (int i = 0; i < _defaultSpawnCount; i++)
        {
            var gb=Instantiate(obstaclePrefab);
            var obs = gb.GetComponent<Obstacles>();
            obs.SetReference(this);
            gb.SetActive(false);
            _inactiveList.Add(gb);
        }
    }


    private void OnDestroy()
    {
        Helicoptor.OnGameOver -= DisableSpawn;
        GameManager.OnGamestart -= GameStart;
    }

    private void GameStart()
    {
        _shouldSpawn = true;
        ResetSpawn();
        StartCoroutine(SpawnRoutine());
    }

    private void DisableSpawn()
    {
        _shouldSpawn = false;
        StopAllCoroutines();
    }

    private void ResetSpawn()
    {
        int count = _activeList.Count;
        for (int i=0;i<count;i++)
        {
            var gb = _activeList[0];
            gb.SetActive(false);
            _activeList.RemoveAt(0);
            _inactiveList.Add(gb);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (_shouldSpawn)
        {
            SpawnObject();
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnObject()
    {
        int index = Random.Range(0, spawnPositions.Count);
        if (_currentSpawnCount >= _maximumSpawnCount) return;

        if(_inactiveList.Count<=0)
        {
            var gb0=Instantiate(obstaclePrefab);
            var obs = gb0.GetComponent<Obstacles>();
            obs.SetReference(this);
            gb0.SetActive(false);
            _inactiveList.Add(gb0);
            _currentSpawnCount++;
        }

        var gb = _inactiveList[0];
        gb.transform.position=spawnPositions[index].position;
        gb.SetActive(true);
        _activeList.Add(gb);
        _inactiveList.RemoveAt(0);
    }

    public void DespawnObjectacle(GameObject obj)
    {
        var index = _activeList.IndexOf(obj);
        var gb = _activeList[index];
        gb.SetActive(false);
        _activeList.RemoveAt(index);
        _inactiveList.Add(gb);
    }
}
