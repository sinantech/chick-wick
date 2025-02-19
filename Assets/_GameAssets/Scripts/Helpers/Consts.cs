// Oyunda kullanılan sabit değerleri içeren sınıf
public class Consts
{
    // Oyuncu animasyonlarına ait sabit değerleri içeren yapı
    public struct PlayerAnimations
    {
        // Oyuncunun hareket halinde olup olmadığını belirten animasyon parametresi
        public const string IS_MOVING = "IsMoving";

        // Oyuncunun zıplama animasyonunu kontrol eden parametre
        public const string IS_JUMPING = "IsJumping";

        // Oyuncunun kayma animasyonunu kontrol eden parametre
        public const string IS_SLIDING = "IsSliding";

        // Oyuncunun kayma durumunun aktif olup olmadığını belirten parametre
        public const string IS_SLIDING_ACTIVE = "IsSlidingActive";
    }

    // Diğer animasyonlara ait sabit değerleri içeren yapı
    public struct OtherAnimations
    {
        // Spatula'nın zıplama animasyonunu tetikleyen parametre
        public const string IS_SPATULA_JUMPING = "IsSpatulaJumping";
    }

    // Oyundaki buğday türlerini temsil eden sabit değerleri içeren yapı
    public struct WheatTypes
    {
        // Altın buğday etiketi
        public const string GOLD_WHEAT = "GoldWheat";

        // Kutsal buğday etiketi
        public const string HOLY_WHEAT = "HolyWheat";

        // Çürük buğday etiketi
        public const string ROTTEN_WHEAT = "RottenWheat";
    }
}
