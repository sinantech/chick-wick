using UnityEngine;

// Spatula nesnesinin oyuncuya hız kazandırmasını sağlayan sınıf
public class SpatulaBoos : MonoBehaviour, IBoostable
{
    [Header("References")]
    // Spatula animasyonlarını kontrol eden Animator bileşeni
    [SerializeField] Animator _spatulaAnimator;

    [Header("Settings")]
    // Oyuncuya uygulanacak zıplama kuvveti
    [SerializeField] private float _jumpForce;

    // Spatula'nın aktif olup olmadığını kontrol eden değişken
    private bool _isActivaded;

    // Oyuncuya hız kazandıran fonksiyon
    public void Boost(PlayerController playerController)
    {
        // Eğer spatula zaten aktifse işlemi durdur
        if (_isActivaded) { return; }

        // Boost animasyonunu oynat
        PlayBoostAnimation();

        // Oyuncunun Rigidbody bileşenini al
        Rigidbody playerRigidBody = playerController.GetPlayerRigidBody();

        // Oyuncunun dikey hızını sıfırla (önceki zıplamanın etkisini kaldır)
        playerRigidBody.linearVelocity = new Vector3(playerRigidBody.linearVelocity.x, 0f, playerRigidBody.linearVelocity.z);

        // Oyuncuya ileriye doğru bir kuvvet uygula
        playerRigidBody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);

        // Spatula'nın aktif olduğunu işaretle
        _isActivaded = true;

        // Belirli bir süre sonra tekrar aktif hale gelmesini sağla
        Invoke(nameof(ResetActivation), 0.2f);
    }

    // Spatula'nın boost animasyonunu oynatan fonksiyon
    private void PlayBoostAnimation()
    {
        _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPING);
    }

    // Spatula'nın tekrar aktif hale gelmesini sağlayan fonksiyon
    private void ResetActivation()
    {
        _isActivaded = false;
    }
}
