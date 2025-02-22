using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton Design Pattern

    [Header("References")]
    [SerializeField] private EggCounterUI _eggCounterUI;

    [Header("Settings")]
    [SerializeField] private int _maxEggconut = 5;

    private int _currentEggCount;

    private void Awake()
    {
        Instance = this;
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
        }
    }
}
