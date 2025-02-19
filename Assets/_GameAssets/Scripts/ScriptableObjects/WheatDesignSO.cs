using UnityEngine;

// Bu ScriptableObject, buğday tasarımına ait değişkenleri saklamak için kullanılır
[CreateAssetMenu(fileName = "WheatDesignSOAsset", menuName = "ScriptableObjects/WheatDesignSOAsset")]
public class WheatDesignSO : ScriptableObject
{
    // Buğdayın sağladığı artış veya azalış katsayısını belirten değişken
    [SerializeField] private float _increaseDecreaseMultiplier;

    // Güçlendirme etkisinin ne kadar süreceğini belirten değişken
    [SerializeField] private float _resetBoostDuration;

    // Artış/Azalış katsayısına dışarıdan erişim sağlayan özellik
    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;

    // Güçlendirme süresine dışarıdan erişim sağlayan özellik
    public float ResetBoostDuration => _resetBoostDuration;
}
