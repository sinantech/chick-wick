using System;
using UnityEngine;

// Oyuncunun animasyonlarını yöneten kontrolcü sınıfı
public class PlayerAnimationController : MonoBehaviour
{
    // Oyuncu animasyonlarını kontrol eden Animator bileşeni
    [SerializeField] private Animator _playerAnimator;

    // Oyuncunun hareketlerini yöneten kontrolcü
    private PlayerController _playerController;

    // Oyuncunun durumlarını yöneten kontrolcü
    private StateController _stateController;

    // Bileşenlerin başlangıçta alınması için Awake fonksiyonu
    void Awake()
    {
        // PlayerController bileşenini al
        _playerController = GetComponent<PlayerController>();

        // StateController bileşenini al
        _stateController = GetComponent<StateController>();
    }

    // Oyun başladığında çalıştırılan fonksiyon
    void Start()
    {
        // Oyuncunun zıplama olayına dinleyici ekle
        _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
    }

    // Her karede çalıştırılan fonksiyon
    void Update()
    {
        // Oyuncunun durumuna göre animasyonları ayarla
        SetPlayerAnimations();
    }

    // Oyuncu zıpladığında çağrılan fonksiyon
    private void PlayerController_OnPlayerJumped()
    {
        // Zıplama animasyonunu etkinleştir
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);

        // Belirli bir süre sonra zıplama animasyonunu sıfırla
        Invoke(nameof(ResetJumping), 0.5f);
    }

    // Zıplama animasyonunu sıfırlayan fonksiyon
    private void ResetJumping()
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }

    // Oyuncunun mevcut durumuna göre animasyonları güncelleyen fonksiyon
    private void SetPlayerAnimations()
    {
        // Oyuncunun mevcut durumunu al
        var currentState = _stateController.GetCurrentState();

        // Mevcut duruma göre animasyonları ayarla
        switch (currentState)
        {
            // Oyuncu hareketsiz durumda ise
            case PlayerState.Idle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                break;

            // Oyuncu hareket halinde ise
            case PlayerState.Move:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                break;

            // Oyuncu kayma pozisyonunda ama hareket etmiyorsa
            case PlayerState.SlideIdle:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;

            // Oyuncu kayma halinde ise
            case PlayerState.Slide:
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;
        }
    }
}
