using UnityEngine; // UnityEngine kütüphanesini içe aktarıyoruz. Rigidbody, Transform ve Input gibi sınıfları kullanmamızı sağlar.

public class PlayerController : MonoBehaviour // PlayerController sınıfını tanımlıyoruz, MonoBehaviour sınıfından miras alıyor.
{
    // --- REFERANSLAR (REFERENCES) ---
    [Header("References")] // Unity Inspector'da düzeni sağlamak için başlık ekliyoruz.
    [SerializeField] private Transform _orientationTransform; // Oyuncunun hareket yönünü belirlemek için bir referans.

    // --- HAREKET AYARLARI (MOVEMENT SETTINGS) ---
    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey; // Normal movementa dönmek için atanan tuş.
    [SerializeField] private float _movementSpeed; // Oyuncunun hareket hızı.

    // --- ZIPLAMA AYARLARI (JUMP SETTINGS) ---
    [Header("Jump settings")]
    [SerializeField] private KeyCode _jumpKey; // Zıplamak için kullanılacak tuş.
    [SerializeField] private float _jumpForce; // Zıplama kuvveti.
    [SerializeField] private float _jumpCooldown; // Zıpladıktan sonra tekrar zıplayabilmek için gereken süre.
    [SerializeField] private bool _canJump; // Oyuncunun zıplayıp zıplayamayacağını belirleyen değişken.

    // --- KAYDIRMA AYARLARI (SLIDING SETTINGS) ---
    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey; // Kaymak için kullanılacak tuş.
    [SerializeField] private float _slideMultiplier; // Kaydırma miktarını belirleyen değişen bunu unityde kontrol edicez.
    [SerializeField] private float _slideDrag; // Kaydırmadaki sürtünmeyi kontrol edecek olan değişken.

    // --- YERDE OLUP OLMADIĞINI KONTROL ETMEK İÇİN (GROUND CHECK SETTINGS) ---
    [Header("Ground Check Settings")]
    [SerializeField] private float _playerHeight; // Oyuncunun yüksekliği (yer kontrolü için kullanılır).
    [SerializeField] private LayerMask _groundLayer; // Oyuncunun hangi katmana (layer) temas ettiğini kontrol etmek için.
    [SerializeField] private float _groundDrag; // Oyuncunun lineer düzlemdeki sürtünmesini kontrol edecek değişken.

    // --- ÖZEL DEĞİŞKENLER (PRIVATE VARIABLES) ---
    private Rigidbody _playerRigidbody; // Oyuncunun fizik motorunu kontrol etmek için Rigidbody bileşeni.
    private float _horizontalInput, _verticalInput; // Kullanıcının yön tuşlarından gelen girişleri saklayan değişkenler.
    private Vector3 _movementDirection; // Hareket yönünü tutan vektör.
    private bool _isSliding; // Kaydırma olup olmadığını kontrol eden değişken default olarak false başlar.

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>(); // Oyuncunun Rigidbody bileşenini alıyoruz.
        _playerRigidbody.freezeRotation = true; // Rigidbody'nin dönmesini engelliyoruz, böylece oyuncu devrilmez.
    }

    private void Update()
    {
        SetInputs(); // Oyuncunun girişlerini (klavye tuşlarını) her karede kontrol ediyoruz.
        SetPlayerDrag(); // Oyuncunun giriş yaptığında draglerini de devreye almasını sağlıyoruz.
        LimitPlayerSpeed(); // Oyuncunun hız sınırını aşmaması için fonksiyonu devreye alıyoruz.
    }

    private void FixedUpdate()
    {
        SetPlayerMovement(); // Oyuncunun fizik tabanlı hareketini işleyen fonksiyon.
    }

    private void SetInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal"); // Kullanıcının "A-D" veya "Sol-Sağ" tuşlarıyla yatay girişini alıyoruz.
        _verticalInput = Input.GetAxisRaw("Vertical"); // Kullanıcının "W-S" veya "Yukarı-Aşağı" tuşlarıyla dikey girişini alıyoruz.

        // Slide key girişinde false olanı true ya çevirerek kaydırmaya başlıyor.
        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
        }

        // Movement key tuşuna basılırsa eğer normal hareketine geri dönecek.
        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
        }

        // Eğer atama yapılan zıplama tuşuna basılırsa ve zıplama izni varsa ve oyuncu yerdeyse:
        else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        {
            _canJump = false; // Hemen tekrar zıplanamaması için zıplama yeteneğini kapatıyoruz.
            SetPlayerJumping(); // Zıplama fonksiyonunu çağırıyoruz.
            Invoke(nameof(ResetJumping), _jumpCooldown); // Belirlenen süre sonra tekrar zıplayabilmesi için fonksiyon çağırıyoruz.
        }
    }

    private void SetPlayerMovement()
    {
        // Hareket yönünü belirliyoruz. 
        // Kamera yönüne bağlı olarak ileri ve sağ yönlerine göre hareket ediyoruz.
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;

        // Sliding key'e basılırsa hızlı hareket için.
        if (_isSliding)
        {
            // Rigidbody'ye kuvvet uygulayarak ve slider multipler ile çarparak oyuncuyu daha hızlı hareket ettiriyoruz.
            _playerRigidbody.AddForce(_movementSpeed * _slideMultiplier * _movementDirection.normalized, ForceMode.Force);
        }
        // Normal hareket için.
        else
        {
            // Rigidbody'ye kuvvet uygulayarak oyuncuyu hareket ettiriyoruz.
            _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed, ForceMode.Force);

        }
    }

    private void SetPlayerDrag()
    {
        // Sliding açıksa bunu slide drag e eşitleyecek.
        if (_isSliding)
        {
            _playerRigidbody.linearDamping = _slideDrag;
        }
        // Normal harekette ground drag devreye giriyor.
        else
        {
            _playerRigidbody.linearDamping = _groundDrag;
        }
    }

    private void LimitPlayerSpeed()
    {
        // Oyuncunun yatay düzlemdeki (X-Z eksenindeki) hızını alıyoruz. 
        // Y eksenindeki hızı sıfırlıyoruz çünkü sadece yatay hız üzerinde işlem yapacağız.
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        // Eğer yatay hızın büyüklüğü (_movementSpeed değerinden) daha büyükse:
        if (flatVelocity.magnitude > _movementSpeed)
        {
            // Hızı, _movementSpeed ile sınırlamak için vektörü normalize edip çarptık.
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;

            // Yeni sınırlanmış hızı, oyuncunun mevcut Y ekseni hızını koruyarak tekrar Rigidbody'ye atıyoruz.
            _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
        }
    }


    private void SetPlayerJumping()
    {
        // Y eksenindeki hızını sıfırlıyoruz ki üst üste zıplamalar birikmesin.
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        // Oyuncuyu yukarıya doğru zıplatıyoruz.
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        _canJump = true; // Belirlenen süre sonunda oyuncunun tekrar zıplayabilmesini sağlıyoruz.
    }

    private bool IsGrounded()
    {
        // Oyuncunun yerde olup olmadığını kontrol ediyoruz.
        // Oyuncunun altına doğru bir ışın (ray) gönderiyoruz ve belirlenen layer'a çarptığında yerde olduğunu anlıyoruz.
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }
}
