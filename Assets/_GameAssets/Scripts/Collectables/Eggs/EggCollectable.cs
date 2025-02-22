using UnityEngine;

public class EggCollectable : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        GameManager.Instance.OnEggCollected();
        Destroy(gameObject);
    }
}
