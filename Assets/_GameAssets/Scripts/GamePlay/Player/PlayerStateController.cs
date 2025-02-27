using UnityEngine;

// Oyuncunun durumlarını yöneten kontrolcü sınıfı
public class StateController : MonoBehaviour
{
    // Oyuncunun mevcut durumunu saklayan değişken (varsayılan olarak Idle - Boşta)
    private PlayerState _currentPlayerState = PlayerState.Idle;

    // Oyun başladığında çağrılan fonksiyon
    private void Start()
    {
        // Oyuncunun başlangıç durumunu "Idle" olarak ayarla
        ChangeState(PlayerState.Idle);
    }

    // Oyuncunun durumunu değiştiren fonksiyon
    public void ChangeState(PlayerState newPlayerState)
    {
        // Eğer yeni durum mevcut durum ile aynıysa, değişiklik yapma
        if (_currentPlayerState == newPlayerState) { return; }

        // Yeni durumu mevcut durum olarak ata
        _currentPlayerState = newPlayerState;
    }

    // Oyuncunun mevcut durumunu döndüren fonksiyon
    public PlayerState GetCurrentState()
    {
        return _currentPlayerState;
    }
}
