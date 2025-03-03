using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private Button _tryAgainbutton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;

    private void OnEnable()
    {
        BackgroundMusic.Instance.PlayBackgroundMusic(false);
        AudioManager.Instance.Play(SoundType.LoseSound);
        _timerText.text = _timerUI.GetFinalTime();
        _tryAgainbutton.onClick.AddListener(TryAgainButtonClicked);

        _mainMenuButton.onClick.AddListener(() =>
      {
          AudioManager.Instance.Play(SoundType.TransitionSound);
          TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);
      });
    }

    private void TryAgainButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound);
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
    }
}
