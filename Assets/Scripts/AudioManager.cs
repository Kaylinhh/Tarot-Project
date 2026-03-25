using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // ===== AUDIO SOURCES =====
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    // ===== MUSIC PLAYLIST =====
    [Header("Music Playlist")]
    public AudioClip[] musicPlaylist;
    public bool loopPlaylist = true;
    public bool shufflePlaylist = true;

    // ===== SOUND EFFECTS =====
    [Header("Sound Effects")]
    public AudioClip shakeSound;
    public AudioClip buttonClickSound;
    public AudioClip sparkleSound;

    // ===== STATE =====
    private int currentTrackIndex = 0;

    // ===== SINGLETON SETUP =====
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (musicPlaylist.Length > 0)
        {
            PlayPlaylist();
        }
    }

    public void PlayPlaylist()
    {
        if (musicPlaylist.Length == 0) return;

        if (shufflePlaylist)
        {
            currentTrackIndex = Random.Range(0, musicPlaylist.Length);
        }
        else
        {
            currentTrackIndex = 0;
        }

        PlayCurrentTrack();
    }

    void PlayCurrentTrack()
    {
        if (musicPlaylist.Length == 0) return;

        musicSource.clip = musicPlaylist[currentTrackIndex];
        musicSource.Play();

        StartCoroutine(WaitForTrackEnd());
    }

    IEnumerator WaitForTrackEnd()
    {
        yield return new WaitForSeconds(musicSource.clip.length);

        NextTrack();
    }

    void NextTrack()
    {
        if (shufflePlaylist)
        {
            currentTrackIndex = Random.Range(0, musicPlaylist.Length);
        }
        else
        {
            currentTrackIndex++;

            if (currentTrackIndex >= musicPlaylist.Length)
            {
                if (loopPlaylist)
                {
                    currentTrackIndex = 0; 
                }
                else
                {
                    return; 
                }
            }
        }

        PlayCurrentTrack();
    }

    public void PlayMusic(AudioClip clip)
    {
        StopAllCoroutines();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        StopAllCoroutines();
        musicSource.Stop();
    }

    // ========== SOUND EFFECTS ==========

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // Helpers for specific sounds
    public void PlayShakeSound() => PlaySFX(shakeSound);
    public void PlaySparkleSound() => PlaySFX(sparkleSound);
    public void PlayButtonClickSound() => PlaySFX(buttonClickSound);
}