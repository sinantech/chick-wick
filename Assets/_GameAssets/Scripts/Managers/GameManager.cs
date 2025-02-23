using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton Design Pattern

    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private EggCounterUI _eggCounterUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggconut = 5;

    private GameState _currentGameState;
    private int _currentEggCount;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log("Game State: " + gameState);
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggconut);

        if (_currentEggCount == _maxEggconut)
        {
            //WİN
            Debug.Log("Game Win");
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
        }
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
