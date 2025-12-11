using UnityEngine;

/// <summary>
/// Manages all audio settings in the game including music and SFX muting.
/// Implements the Singleton pattern to ensure only one instance exists globally.
/// Persists across scene changes.
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Static reference to the single AudioManager instance (Singleton).
    /// </summary>
    public static AudioManager Instance;

    /// <summary>
    /// Reference to the AudioSource component that plays background music.
    /// </summary>
    [Header("Music")]
    public AudioSource musicSource;

    /// <summary>
    /// Whether background music is currently muted.
    /// </summary>
    [HideInInspector] public bool isMusicMuted = false;

    /// <summary>
    /// Whether sound effects (SFX) are currently muted.
    /// </summary>
    [HideInInspector] public bool isSfxMuted = false;

    /// <summary>
    /// Called when the script instance is being loaded (before Start).
    /// Sets up the Singleton pattern and ensures this object persists across scenes.
    /// </summary>
    void Awake()
    {
        // Initialize singleton: only one AudioManager should exist
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // Destroy duplicate AudioManagers

        // Keep this AudioManager alive when loading new scenes
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Mutes or unmutes the background music.
    /// </summary>
    /// <param name="mute">True to mute music, false to unmute.</param>
    public void SetMusicMuted(bool mute)
    {
        isMusicMuted = mute;
        if (musicSource != null)
            musicSource.mute = mute; // Apply mute state to the music AudioSource
    }

    /// <summary>
    /// Mutes or unmutes all sound effects.
    /// </summary>
    /// <param name="mute">True to mute SFX, false to unmute.</param>
    public void SetSfxMuted(bool mute)
    {
        isSfxMuted = mute; // Other scripts check this flag to determine if they should play sounds
    }
}
