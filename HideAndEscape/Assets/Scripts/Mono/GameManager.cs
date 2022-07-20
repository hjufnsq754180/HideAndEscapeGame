using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _startGamePanel;
    [SerializeField]
    private GameObject _gameOverPanel;
    [SerializeField]
    private GameObject _finishGamePanel;

    public bool isGameOver;
    public bool isGameFinished;
    private bool _isGameStarted;
    public bool IsGameStarted => _isGameStarted;

    [SerializeField]
    private float _unitSpeed;
    public float UnitSpeed => _unitSpeed;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        _startGamePanel.SetActive(true);
        PauseGame();
    }

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
        isGameOver = true;
        PauseGame();
        _isGameStarted = false;
    }

    public void GameFinished()
    {
        _finishGamePanel.SetActive(true);
        PauseGame();
        _isGameStarted = false;
    }

    public void StartGameButton()
    {
        UnpauseGame();
        _startGamePanel.SetActive(false);
        _isGameStarted = true;
    }

    public void RestartGameButton()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}
