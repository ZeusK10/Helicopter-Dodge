using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private ObstacleSpawner _obstacleSpawner;
    private bool _isMove = true;

    private void OnEnable()
    {
        _isMove = true;
        Helicoptor.OnGameOver += DisableMovement;
    }

    private void OnDisable()
    {
        Helicoptor.OnGameOver -= DisableMovement;
    }


    private void DisableMovement()
    {
        _isMove = false;
    }

    void Update()
    {
        if (!_isMove) return;
        transform.position += new Vector3(-3f*Time.deltaTime, 0, 0);
        if(transform.position.x < -15f)
        {
            _obstacleSpawner.DespawnObjectacle(gameObject);
        }
    }

    public void SetReference(ObstacleSpawner obstacleSpawner)
    {
        _obstacleSpawner = obstacleSpawner;
    }
}
