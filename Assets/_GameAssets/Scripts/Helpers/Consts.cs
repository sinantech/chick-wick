// Oyunda kullanılan sabit değerleri içeren sınıf
public class Consts
{
    public struct SceneNames
    {
        public const string GAME_SCENE = "GameScene";
    }

    public struct Layers
    {
        public const string GROUND_LAYER = "Ground";
        public const string FLOOR_LAYER = "Floor";
    }

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

    public struct CatAnimations
    {
        public const string IS_IDLING = "IsIdling";
        public const string IS_WALKING = "IsWalking";
        public const string IS_RUNNING = "IsRunning";
        public const string IS_ATTACKING = "IsAttacking";
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
