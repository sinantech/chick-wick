using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton Design Pattern

    [SerializeField] private int _maxEggconut = 5;

    private int _currentEggCount;

    private void Awake()
    {
        Instance = this;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        Debug.Log("Egg Count : " + _currentEggCount);

        if (_currentEggCount == _maxEggconut)
        {
            //WİN
            Debug.Log("Game Win");
        }
    }
}
