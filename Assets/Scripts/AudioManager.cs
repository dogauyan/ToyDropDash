using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Sound Effects")]
    public AudioClip catchNormal;
    public AudioClip catchBonus;
    public AudioClip catchTrap;
    public AudioClip miss;
    public AudioClip gameOver;
    public AudioClip buttonClick;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;

    [Header("Volume")]
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.7f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", sfxVolume);

            ApplyVolumes();

            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ---------- SFX ----------
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // ---------- MUSIC ----------
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
            PlayMusic(menuMusic);
        else
            PlayMusic(gameMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null) return;

        if (musicSource.clip == clip)
            return;

        musicSource.Stop();
        musicSource.clip = clip;

        if (clip != null)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource == null) return;
        musicSource.Stop();
        musicSource.clip = null;
    }
    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        ApplyVolumes();
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        ApplyVolumes();
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    void ApplyVolumes()
    {
        if (musicSource != null)
            musicSource.volume = musicVolume;

        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }
}
