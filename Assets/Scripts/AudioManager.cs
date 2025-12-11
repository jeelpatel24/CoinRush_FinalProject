using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource musicSource;

    [HideInInspector] public bool isMusicMuted = false;
    [HideInInspector] public bool isSfxMuted = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetMusicMuted(bool mute)
    {
        isMusicMuted = mute;
        if (musicSource != null)
            musicSource.mute = mute;
    }

    public void SetSfxMuted(bool mute)
    {
        isSfxMuted = mute;
    }
}
