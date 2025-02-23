using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")] // Unity Inspector'da bu değişkenler için bir başlık oluşturur.
    [SerializeField] private Transform _playerTransform; // Oyuncunun transform bileşeni.
    [SerializeField] private Transform _orientationTransform; // Oyuncunun yönlendirme transform bileşeni.
    [SerializeField] private Transform _playervisualTransform; // Oyuncunun görsel modelinin transform bileşeni.

    [Header("Settings")] // Unity Inspector'da bu değişkenler için bir başlık oluşturur.
    [SerializeField] private float _rotationSpeed; // Oyuncunun dönüş hızını belirten değişken.

    void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play && GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }
        // Kameranın bulunduğu konuma göre oyuncunun bakış yönünü hesaplar.
        Vector3 viewDirection = _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
        _orientationTransform.forward = viewDirection.normalized; // Oyuncunun yönlendirme bileşenini bu yöne çevirir.

        // Kullanıcının klavyeden aldığı yatay ve dikey girişleri alır.
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Kullanıcının girişlerine göre hareket yönünü belirler.
        Vector3 inputDirection = _orientationTransform.forward * verticalInput + _orientationTransform.right * horizontalInput;

        // Eğer bir giriş varsa
        if (inputDirection != Vector3.zero)
        {
            // Oyuncunun görsel modelini giriş yönüne doğru yumuşakça döndürür.
            _playervisualTransform.forward = Vector3.Slerp(
                _playervisualTransform.forward, // Mevcut yön
                inputDirection.normalized, // Yeni hedef yön
                Time.deltaTime * _rotationSpeed // Dönüş hızını uygular
            );
        }
    }
}
