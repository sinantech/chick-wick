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
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _passiveSprite;
    [SerializeField] private Sprite _activeWheatSprite;
    [SerializeField] private Sprite _passiveWheatSprite;

    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;

    // Güçlendirme süresine dışarıdan erişim sağlayan özellik
    public float ResetBoostDuration => _resetBoostDuration;
    public Sprite ActiveSprite => _activeSprite;
    public Sprite PassiveSprite => _passiveSprite;
    public Sprite ActiveWheatSprite => _activeWheatSprite;
    public Sprite PassiveWheatSprite => _passiveWheatSprite;
}
