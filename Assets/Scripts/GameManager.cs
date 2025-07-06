using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnGamestart;

    [SerializeField] private GameObject gameOverMenu;

    private void Start()
    {
        Helicoptor.OnGameOver += GameOver;
    }

    private void OnDestroy()
    {
        Helicoptor.OnGameOver -= GameOver;
    }

    private void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        gameOverMenu.SetActive(false);
        OnGamestart?.Invoke();
        Time.timeScale = 1f;
    }
}
