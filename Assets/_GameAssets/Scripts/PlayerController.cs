using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _orientationTransform;
    private Rigidbody _playerRigidbody;

    private void Awake() {
        _playerRigidbody = GetComponent<Rigidbody>();
    }
}
