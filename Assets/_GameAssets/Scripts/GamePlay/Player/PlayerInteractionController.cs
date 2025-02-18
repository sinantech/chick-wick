using Unity.VisualScripting;
using UnityEngine;

// Oyuncunun etkileşime girdiği nesneleri yöneten kontrolcü sınıfı
public class PlayerInteractionController : MonoBehaviour
{
    // Oyuncu bir nesne ile çarpıştığında tetiklenen fonksiyon
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectable>(out var collectable))
        {
            collectable.Collect();
        }
    }
}
