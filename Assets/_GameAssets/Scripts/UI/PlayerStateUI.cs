using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

// Oyuncu durumlarını kullanıcı arayüzünde gösteren sınıf
public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _playerController; // Oyuncu kontrolcüsü referansı
    [SerializeField] private RectTransform _playerWalkingTransform; // Yürüyüş durumu UI elementi
    [SerializeField] private RectTransform _playerSlidingTransform; // Kayma durumu UI elementi
    [SerializeField] private RectTransform _boosterSpeedTransform; // Hız güçlendirme UI elementi
    [SerializeField] private RectTransform _boosterJumpTransform; // Zıplama güçlendirme UI elementi
    [SerializeField] private RectTransform _boosterSlowTransform; // Yavaşlatma güçlendirme UI elementi
    [SerializeField] private PlayableDirector _playableDirector;

    [Header("Images")]
    [SerializeField] private Image _goldBoosterWheatImage; // Altın buğday güçlendirme ikonu
    [SerializeField] private Image _holyBoosterWheatImage; // Kutsal buğday güçlendirme ikonu
    [SerializeField] private Image _rottenBoosterWheatImage; // Çürük buğday güçlendirme ikonu

    [Header("Sprites")]
    [SerializeField] private Sprite _playerWalkingActiveSprite; // Aktif yürüyüş durumu ikonu
    [SerializeField] private Sprite _playerWalkingPassiveSprite; // Pasif yürüyüş durumu ikonu
    [SerializeField] private Sprite _playerSlidingActiveSprite; // Aktif kayma durumu ikonu
    [SerializeField] private Sprite _playerSlidingPassiveSprite; // Pasif kayma durumu ikonu

    [Header("Seetings")]
    [SerializeField] private float _moveDuration; // UI animasyon süresi
    [SerializeField] private Ease _moveEase; // UI animasyon eğrisi

    private Image _playerWalkingImage; // Yürüyüş durumunu temsil eden UI bileşeni
    private Image _playerSlidingImage; // Kayma durumunu temsil eden UI bileşeni

    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;

    public Image GetGoldBoosterImage => _goldBoosterWheatImage;
    public Image GetHolyBoosterImage => _holyBoosterWheatImage;
    public Image GetRottenBoosterImage => _rottenBoosterWheatImage;

    private void Awake()
    {
        // UI elemanlarının görüntü bileşenlerini al
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }

    private void Start()
    {
        // Oyuncunun durum değişikliği olayına abone ol
        _playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;

        _playableDirector.stopped += OnTimeLineFinished;
    }

    private void OnTimeLineFinished(PlayableDirector director)
    {
        // Varsayılan UI durumlarını ayarla (Başlangıçta yürüyüş aktif, kayma pasif)
        SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
    }

    // Oyuncu durumu değiştiğinde çağrılan fonksiyon
    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                // Yürüyüş durumunu aktif yap, kaymayı pasif hale getir
                SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                break;

            case PlayerState.Slide:
            case PlayerState.SlideIdle:
                // Kayma durumunu aktif yap, yürüyüşü pasif hale getir
                SetStateUserInterfaces(_playerWalkingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                break;
        }
    }

    // UI elemanlarını güncelleyen fonksiyon
    private void SetStateUserInterfaces(Sprite playerWalkingSprite, Sprite playerSlidingSprite, RectTransform activeTransform, RectTransform passiveTransform)
    {
        // Yürüyüş ve kayma durum ikonlarını güncelle
        _playerWalkingImage.sprite = playerWalkingSprite;
        _playerSlidingImage.sprite = playerSlidingSprite;

        // Aktif olan UI elemanını ön plana çıkar, pasif olanı geri plana at
        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform,
        Image boosterImage, Image wheatImage, Sprite activeSprite, Sprite passiveSprite,
        Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, _moveDuration).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);

        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);

    }

    public void PlayBoosterUIAnimations(RectTransform activeTransform,
        Image boosterImage, Image wheatImage, Sprite activeSprite, Sprite passiveSprite,
        Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImage, wheatImage, activeSprite, passiveSprite, activeWheatSprite, passiveWheatSprite, duration));
    }
}
