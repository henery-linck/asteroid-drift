using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameOver { get; private set; }
    public float SurvivalTime { get; private set; }

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        Instance = this;
        _inputActions = new PlayerInputActions();
    }

    private void Update()
    {
        if (IsGameOver)
            return;

        SurvivalTime += Time.deltaTime;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Restart.performed += OnRestart;
    }

    private void OnDisable()
    {
        _inputActions.Player.Restart.performed -= OnRestart;
        _inputActions.Player.Disable();
    }

    private void OnRestart(InputAction.CallbackContext context)
    {
        if (!IsGameOver)
            return;

        Restart();
    }

    public void GameOver()
    {
        if (IsGameOver)
            return;

        IsGameOver = true;
        Time.timeScale = 0f;
        Debug.Log("GAME OVER");
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public string GetFormattedSurvivalTime()
    {
        int minutes = Mathf.FloorToInt(SurvivalTime / 60f);
        int seconds = Mathf.FloorToInt(SurvivalTime % 60f);

        return $"{minutes:00}:{seconds:00}";
    }
}
