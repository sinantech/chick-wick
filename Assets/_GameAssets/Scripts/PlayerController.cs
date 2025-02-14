using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    [Header("References")]
    [SerializeField] private Transform _orientationTransform;

    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed;
    private Rigidbody _playerRigidbody;
    private float _horizontalInput, _verticalInput;
    private Vector3 _movementDirection;

    private void Awake() {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }

    private void Update() {
        setInputs();
    }

    private void FixedUpdate() {
        setPlayerMovement();
    }

    private void setInputs() {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void setPlayerMovement() {
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;
        _playerRigidbody.AddForce(_movementDirection * _movementSpeed, ForceMode.Force);
    }
}
