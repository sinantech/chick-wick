using Unity.VisualScripting;
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
            other.gameObject?.GetComponent<GoldWheatCollectable>().Collect();
        }

        // Eğer çarpışılan nesne "Holy Wheat" etiketiyle eşleşiyorsa
        if (other.CompareTag(Consts.WheatTypes.HOLY_WHEAT))
        {
            other.gameObject?.GetComponent<HolyWheatCollectable>().Collect();
        }

        // Eğer çarpışılan nesne "Rotten Wheat" etiketiyle eşleşiyorsa
        if (other.CompareTag(Consts.WheatTypes.ROTTEN_WHEAT))
        {
            other.gameObject?.GetComponent<RottenWheatCollectable>().Collect();
        }
    }
}
