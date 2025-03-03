using UnityEngine;
using UnityEngine.UI;

// Çürük buğdayın toplanabilir olmasını sağlayan sınıf
public class RottenWheatCollectable : MonoBehaviour, ICollectable
{
    // Buğdayın güçlendirme değerlerini içeren ScriptableObject referansı
    [SerializeField] WheatDesignSO _wheatDesignSo;
    // Oyuncunun hareketini kontrol eden PlayerController referansı
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerStateUI _playerStateUI;


    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSlowTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }


    // Buğday toplandığında çağrılan fonksiyon
    public void Collect()
    {
        // Oyuncunun hareket hızını artır ve belirlenen süre sonunda eski haline döndür
        _playerController.SetMovementSpeed(_wheatDesignSo.IncreaseDecreaseMultiplier, _wheatDesignSo.ResetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetRottenBoosterImage,
            _wheatDesignSo.ActiveSprite, _wheatDesignSo.PassiveSprite, _wheatDesignSo.ActiveWheatSprite,
            _wheatDesignSo.PassiveWheatSprite, _wheatDesignSo.ResetBoostDuration);

        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        // Buğday nesnesini sahneden kaldır
        Destroy(this.gameObject);
    }
}
