using UnityEngine;

public class EggCollectable : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        GameManager.Instance.OnEggCollected();
        AudioManager.Instance.Play(SoundType.PickupGoodSound);
        Destroy(gameObject);
    }
}
