// Oyuncuya hız veya ekstra yetenek kazandıran nesneler için arayüz
public interface IBoostable
{
    // Oyuncunun belirli bir güçlendirme almasını sağlayan fonksiyon
    void Boost(PlayerController playerController);
}
