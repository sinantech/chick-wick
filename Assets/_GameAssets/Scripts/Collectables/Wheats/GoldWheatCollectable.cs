using UnityEngine;

// Altın buğdayın toplanabilir olmasını sağlayan sınıf
public class GoldWheatCollectable : MonoBehaviour, ICollectable
{
    // Buğdayın güçlendirme değerlerini içeren ScriptableObject referansı
    [SerializeField] WheatDesignSO _wheatDesignSo;

    // Oyuncunun hareketini kontrol eden PlayerController referansı
    [SerializeField] private PlayerController _playerController;

    // Buğday toplandığında çağrılan fonksiyon
    public void Collect()
    {
        // Oyuncunun hareket hızını artır ve belirlenen süre sonunda eski haline döndür
        _playerController.SetMovementSpeed(_wheatDesignSo.IncreaseDecreaseMultiplier, _wheatDesignSo.ResetBoostDuration);

        // Buğday nesnesini sahneden kaldır
        Destroy(this.gameObject);
    }
}
