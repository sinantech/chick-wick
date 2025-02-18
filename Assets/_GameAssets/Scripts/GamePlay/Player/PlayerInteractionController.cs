using UnityEngine;

// Oyuncunun etkileşime girdiği nesneleri yöneten kontrolcü sınıfı
public class PlayerInteractionController : MonoBehaviour
{
    // Oyuncu bir nesne ile çarpıştığında tetiklenen fonksiyon
    void OnTriggerEnter(Collider other)
    {
        // Eğer çarpışılan nesne "Gold Wheat" etiketiyle eşleşiyorsa
        if (other.CompareTag(Consts.WheatTypes.GOLD_WHEAT))
        {
            // Konsola "Gold Wheat Collected" mesajını yazdır
            Debug.Log("Gold Wheat Collected");
        }

        // Eğer çarpışılan nesne "Holy Wheat" etiketiyle eşleşiyorsa
        if (other.CompareTag(Consts.WheatTypes.HOLY_WHEAT))
        {
            // Konsola "Holy Wheat Collected" mesajını yazdır
            Debug.Log("Holy Wheat Collected");
        }

        // Eğer çarpışılan nesne "Rotten Wheat" etiketiyle eşleşiyorsa
        if (other.CompareTag(Consts.WheatTypes.ROTTEN_WHEAT))
        {
            // Konsola "Rotten Wheat Collected" mesajını yazdır
            Debug.Log("Rotten Wheat Collected");
        }
    }
}
