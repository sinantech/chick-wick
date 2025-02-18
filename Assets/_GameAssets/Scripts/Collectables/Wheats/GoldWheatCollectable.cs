using UnityEngine;

public class GoldWheatCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] WheatDesignSO _wheatDesignSo;
    [SerializeField] private PlayerController _playerController;

    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSo.IncreaseDecreaseMultiplier, _wheatDesignSo.ResetBoostDuration);
        Destroy(this.gameObject);
    }
}
