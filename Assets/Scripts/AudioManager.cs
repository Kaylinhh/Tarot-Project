using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Playlist")]
    public AudioClip[] musicPlaylist;
    public bool loopPlaylist = true;
    public bool shufflePlaylist = true;

    [Header("Sound Effects")]
    public AudioClip shakeSound;
    public AudioClip buttonClickSound;
    public AudioClip sparkleSound;

    private int currentTrackIndex = 0;

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
        // Démarre la playlist au lancement
        if (musicPlaylist.Length > 0)
        {
            PlayPlaylist();
        }
    }

    // ========== MUSIC ==========

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

        // Lance la coroutine pour détecter la fin du morceau
        StartCoroutine(WaitForTrackEnd());
    }

    IEnumerator WaitForTrackEnd()
    {
        // Attend que le morceau se termine
        yield return new WaitForSeconds(musicSource.clip.length);

        // Passe au suivant
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
                    currentTrackIndex = 0; // Recommence la playlist
                }
                else
                {
                    return; // Arrête la musique
                }
            }
        }

        PlayCurrentTrack();
    }

    public void PlayMusic(AudioClip clip)
    {
        StopAllCoroutines(); // Arrête la playlist
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

    // Helpers pour les sons spécifiques
    public void PlayShakeSound()
    {
        PlaySFX(shakeSound);
    }

    public void PlaySparkleSound()
    {
        PlaySFX(sparkleSound);
    }

    public void PlayButtonClickSound()
    {
        PlaySFX(buttonClickSound);
    }
}