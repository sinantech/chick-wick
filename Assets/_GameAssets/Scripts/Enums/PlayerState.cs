using UnityEngine;

// Oyuncunun sahip olabileceği durumları tanımlayan enum
public enum PlayerState
{
    // Oyuncu hareketsiz durumda
    Idle,

    // Oyuncu hareket halinde
    Move,

    // Oyuncu zıplıyor
    Jump,

    // Oyuncu kayma pozisyonunda ama hareket etmiyor
    SlideIdle,

    // Oyuncu kayma halinde
    Slide
}
