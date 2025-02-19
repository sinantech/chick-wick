using UnityEngine;

// Kutsal buğdayın toplanabilir olmasını sağlayan sınıf
public class HolyWheatCollectable : MonoBehaviour, ICollectable
{
    // Buğdayın güçlendirme değerlerini içeren ScriptableObject referansı
    [SerializeField] WheatDesignSO _wheatDesignSo;

    // Oyuncunun zıplamasını kontrol eden PlayerController referansı
    [SerializeField] private PlayerController _playerController;

    // Buğday toplandığında çağrılan fonksiyon
    public void Collect()
    {
        // Oyuncunun zıplama kuvvetini artır ve belirlenen süre sonunda eski haline döndür
        _playerController.SetJumpForce(_wheatDesignSo.IncreaseDecreaseMultiplier, _wheatDesignSo.ResetBoostDuration);

        // Buğday nesnesini sahneden kaldır
        Destroy(this.gameObject);
    }
}
