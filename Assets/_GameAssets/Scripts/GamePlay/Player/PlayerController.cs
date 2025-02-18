using System;
using UnityEngine; // UnityEngine kütüphanesini içe aktarıyoruz. Rigidbody, Transform ve Input gibi sınıfları kullanmamızı sağlar.

public class PlayerController : MonoBehaviour // PlayerController sınıfını tanımlıyoruz, MonoBehaviour sınıfından miras alıyor.
{
    // Oyuncunun zıplama olayını temsil eden event (olay)
    public event Action OnPlayerJumped;


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
    [SerializeField] private float _airMultiplier; // Zıplama miktarının çarpanını taşıyacak olan değişken.
    [SerializeField] private float _airDrag; // Hava sürtünmesinin değerini tutacak olan değişken.
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
    private StateController _stateController; // State kontrol durumunu değişkene atayıp fonksiyonda kullanacağız.
    private Rigidbody _playerRigidbody; // Oyuncunun fizik motorunu kontrol etmek için Rigidbody bileşeni.
    private Vector3 _movementDirection; // Hareket yönünü tutan vektör.
    private float _horizontalInput, _verticalInput; // Kullanıcının yön tuşlarından gelen girişleri saklayan değişkenler.
    private float _startingMovementSpeed, _startingJumpForce; // Karakterin başlagıçtaki hızı ve zıplama kuvvti değeri değişkenleri.
    private bool _isSliding; // Kaydırma olup olmadığını kontrol eden değişken default olarak false başlar.

    private void Awake()
    {
        _stateController = GetComponent<StateController>(); // Oyuncunun state controller bileşenini alıyoruz.
        _playerRigidbody = GetComponent<Rigidbody>(); // Oyuncunun Rigidbody bileşenini alıyoruz.
        _playerRigidbody.freezeRotation = true; // Rigidbody'nin dönmesini engelliyoruz, böylece oyuncu devrilmez.
        _startingMovementSpeed = _movementSpeed; // Başlagıçtaki hızı normal hıza eşitliyoruz.
        _startingJumpForce = _jumpForce; // Başlangıçtaki zıplama kuvveti normal zıplama kuvvetine eşitleniyor.
    }

    private void Update()
    {
        SetInputs(); // Oyuncunun girişlerini (klavye tuşlarını) her karede kontrol ediyoruz.
        SetStates(); // Oyun karakterinin state girişlerini çağırıyoruz.
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

    private void SetStates()
    {
        // Hareket yönünü al
        var movementDirection = GetMovementDirection();

        // Karakterin yerde olup olmadığını kontrol et
        var isGrounded = IsGrounded();

        // Karakterin kayma durumunda olup olmadığını kontrol et
        var isSliding = IsSliding();

        // Mevcut durumu al
        var currentState = _stateController.GetCurrentState();

        // Yeni durumu belirle
        var newState = currentState switch
        {
            // Eğer hareket yönü yoksa, karakter yerdeyse ve kayma durumu aktif değilse "Idle" durumuna geç
            _ when movementDirection == Vector3.zero && isGrounded && !_isSliding => PlayerState.Idle,

            // Eğer hareket yönü varsa, karakter yerdeyse ve kayma durumu aktif değilse "Move" durumuna geç
            _ when movementDirection != Vector3.zero && isGrounded && !_isSliding => PlayerState.Move,

            // Eğer hareket yönü varsa, karakter yerdeyse ve kayma durumu aktifse "Slide" durumuna geç
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,

            // Eğer hareket yönü yoksa, karakter yerdeyse ve kayma durumu aktifse "SlideIdle" durumuna geç
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,

            // Eğer zıplama yapamıyorsa ve karakter havadaysa "Jump" durumuna geç
            _ when !_canJump && !isGrounded => PlayerState.Jump,

            // Hiçbir koşul sağlanmazsa mevcut durumu koru
            _ => currentState
        };

        // Eğer yeni belirlenen durum, mevcut durumdan farklıysa durumu değiştir
        if (newState != currentState)
        {
            // Durum değişimini gerçekleştir
            _stateController.ChangeState(newState);
        }


    }

    private void SetPlayerMovement()
    {
        // Hareket yönünü belirliyoruz. 
        // Kamera yönüne bağlı olarak ileri ve sağ yönlerine göre hareket ediyoruz.
        _movementDirection = _orientationTransform.forward * _verticalInput + _orientationTransform.right * _horizontalInput;

        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };

        // Rigidbody'ye kuvvet uygulayarak ve force multipler ile çarparak oyuncuyu daha hızlı hareket ettiriyoruz.
        _playerRigidbody.AddForce(_movementSpeed * forceMultiplier * _movementDirection.normalized, ForceMode.Force);
    }

    private void SetPlayerDrag()
    {
        // Karakterin mevcut durumuna göre hava, zemin veya kayma sürtünmesini ayarlayan kod bloğu
        _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch
        {
            // Eğer karakter hareket ediyorsa, zemin sürtünmesini uygula
            PlayerState.Move => _groundDrag,

            // Eğer karakter kayıyorsa, kayma sürtünmesini uygula
            PlayerState.Slide => _slideDrag,

            // Eğer karakter havadaysa, hava sürtünmesini uygula
            PlayerState.Jump => _airDrag,

            // Eğer hiçbir durum eşleşmiyorsa mevcut sürtünmeyi koru
            _ => _playerRigidbody.linearDamping
        };

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
        // Jump eventi invoke ile tetikleniyor null gelmesine karşı.
        OnPlayerJumped?.Invoke();
        // Y eksenindeki hızını sıfırlıyoruz ki üst üste zıplamalar birikmesin.
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);

        // Oyuncuyu yukarıya doğru zıplatıyoruz.
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJumping()
    {
        _canJump = true; // Belirlenen süre sonunda oyuncunun tekrar zıplayabilmesini sağlıyoruz.
    }

    #region Helper Functions

    private bool IsGrounded()
    {
        // Oyuncunun yerde olup olmadığını kontrol ediyoruz.
        // Oyuncunun altına doğru bir ışın (ray) gönderiyoruz ve belirlenen layer'a çarptığında yerde olduğunu anlıyoruz.
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }

    // Karakterin hareket yönünü döndüren fonksiyon
    private Vector3 GetMovementDirection()
    {
        // Hareket yönünü normalize ederek döndür (büyüklüğünü 1 birime indirir)
        return _movementDirection.normalized;
    }

    // Karakterin kayma durumunda olup olmadığını kontrol eden fonksiyon
    private bool IsSliding()
    {
        // Kayma durumunu belirten değişkeni döndür
        return _isSliding;
    }

    public void SetMovementSpeed(float speed, float duration)
    {
        _movementSpeed += speed;
        Invoke(nameof(ResetMovementSpeed), duration);
    }

    private void ResetMovementSpeed()
    {
        _movementSpeed = _startingMovementSpeed;
    }

    public void SetJumpForce(float force, float duration)
    {
        _jumpForce += force;
        Invoke(nameof(ResetJumpForce), duration);

    }

    private void ResetJumpForce()
    {
        _jumpForce = _startingJumpForce;
    }

    #endregion

}
