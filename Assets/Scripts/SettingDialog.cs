using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class SettingDialog : MonoBehaviour {

    [SerializeField] private Image soundIcon;
    [SerializeField] private Image musicIcon;
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite musicOn;
    [SerializeField] private Sprite musicOff;

    public void Show() {
        gameObject.SetActive(true);
        UpdateTogglesState();
    }
    
    public void Hide() {
        gameObject.SetActive(false);
    }

    private void UpdateTogglesState() {
        var sm = SoundManager.shared;
        soundIcon.sprite = sm.IsSoundOn ? soundOn : soundOff;
        musicIcon.sprite = sm.IsMusicOn ? musicOn : musicOff;
    }

    public void OnSoundToggleClicked() {
        SoundManager.shared.ChangeSoundOnOffState();
        UpdateTogglesState();
    }
    
    public void OnMusicToggleClicked() {
        SoundManager.shared.ChangeMusicOnOffState();
        UpdateTogglesState();
    }
}