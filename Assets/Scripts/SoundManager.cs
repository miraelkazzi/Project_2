using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip backgroundMusic;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public Sound buttonClick;

    public Sound shootSound;
    public Sound hurtSound;
    public Sound healSound;
    public Sound shieldSound;

    public Sound coinPickup;
    public Sound ammoRefill;
    public Sound buyItem;

    public Sound explosionSound;

    public Sound keypadClick;
    public Sound accessGranted;
    public Sound accessDenied;

    public Sound winSound;
    public Sound loseSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (musicSource.isPlaying) return;

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(Sound sound)
    {
        if (sound.clip == null) return;

        sfxSource.PlayOneShot(sound.clip, sound.volume);
    }
}