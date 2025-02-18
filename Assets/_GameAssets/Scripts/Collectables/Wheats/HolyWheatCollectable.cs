using UnityEngine;

public class HolyWheatCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] WheatDesignSO _wheatDesignSo;
    [SerializeField] private PlayerController _playerController;

    public void Collect()
    {
        _playerController.SetJumpForce(_wheatDesignSo.IncreaseDecreaseMultiplier, _wheatDesignSo.ResetBoostDuration);
        Destroy(this.gameObject);
    }
}
