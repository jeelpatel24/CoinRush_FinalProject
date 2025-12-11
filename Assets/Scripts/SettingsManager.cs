using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Toggle musicMuteToggle;
    [SerializeField] private Toggle sfxMuteToggle;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    void Start()
    {
        if (musicMuteToggle != null)
        {
            musicMuteToggle.isOn = AudioManager.Instance.isMusicMuted;
            musicMuteToggle.onValueChanged.AddListener(OnMusicMuteToggled);
        }

        if (sfxMuteToggle != null)
        {
            sfxMuteToggle.isOn = AudioManager.Instance.isSfxMuted;
            sfxMuteToggle.onValueChanged.AddListener(OnSfxMuteToggled);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
        }
    }

    /// <summary>
    /// Called when music mute toggle is changed.
    /// </summary>
    /// <param name="isOn">True if music should be muted, false to unmute.</param>
    public void OnMusicMuteToggled(bool isOn)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicMuted(isOn);
        }
    }

    /// <summary>
    /// Called when SFX mute toggle is changed.
    /// </summary>
    /// <param name="isOn">True if SFX should be muted, false to unmute.</param>
    public void OnSfxMuteToggled(bool isOn)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetSfxMuted(isOn);
        }
    }

    /// <summary>
    /// Called when music volume slider is adjusted.
    /// </summary>
    /// <param name="volume">Volume value (0-1).</param>
    public void OnMusicVolumeChanged(float volume)
    {
        if (AudioManager.Instance != null && AudioManager.Instance.musicSource != null)
        {
            AudioManager.Instance.musicSource.volume = volume;
        }
    }

    /// <summary>
    /// Called when SFX volume slider is adjusted.
    /// (Can be extended to affect all SFX sources in the game)
    /// </summary>
    /// <param name="volume">Volume value (0-1).</param>
    public void OnSfxVolumeChanged(float volume)
    {
        // This can be extended to control all SFX AudioSources
        // For now, it just updates the setting
    }
}
