using UnityEngine;
using UnityEngine.UI;

// Kutsal buğdayın toplanabilir olmasını sağlayan sınıf
public class HolyWheatCollectable : MonoBehaviour, ICollectable
{
    // Buğdayın güçlendirme değerlerini içeren ScriptableObject referansı
    [SerializeField] WheatDesignSO _wheatDesignSo;
    // Oyuncunun zıplamasını kontrol eden PlayerController referansı
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerStateUI _playerStateUI;


    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterJumpTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    // Buğday toplandığında çağrılan fonksiyon
    public void Collect()
    {
        // Oyuncunun zıplama kuvvetini artır ve belirlenen süre sonunda eski haline döndür
        _playerController.SetJumpForce(_wheatDesignSo.IncreaseDecreaseMultiplier, _wheatDesignSo.ResetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetHolyBoosterImage,
            _wheatDesignSo.ActiveSprite, _wheatDesignSo.PassiveSprite, _wheatDesignSo.ActiveWheatSprite,
            _wheatDesignSo.PassiveWheatSprite, _wheatDesignSo.ResetBoostDuration);

        // Buğday nesnesini sahneden kaldır
        Destroy(this.gameObject);
    }
}
