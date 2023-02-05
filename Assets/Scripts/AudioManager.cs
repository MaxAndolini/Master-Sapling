using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Sound mainMenuMusic;
    [SerializeField] private Sound inGameMusic;

    private readonly Dictionary<string, AudioSource> audioSources = new();
    private Sound currentMusic;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        foreach (var sound in sounds)
        {
            var soundGO = new GameObject("Sound_" + sound.name);
            soundGO.transform.parent = transform;

            var audioSource = soundGO.AddComponent<AudioSource>();
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume;
            audioSource.pitch = sound.pitch;
            audioSource.loop = sound.loop;

            audioSources.Add(sound.name, audioSource);
        }

        audioSources.Add("MainMenuMusic", gameObject.AddComponent<AudioSource>());
        audioSources.Add("InGameMusic", gameObject.AddComponent<AudioSource>());
    }

    private void Start()
    {
        PlayMainMenuMusic();
    }

    public void PlayMainMenuMusic()
    {
        PlayMusic(mainMenuMusic, "MainMenuMusic");
    }

    public void PlayInGameMusic()
    {
        PlayMusic(inGameMusic, "InGameMusic");
    }

    public void StopMainMenuMusic()
    {
        audioSources["MainMenuMusic"].Stop();
        currentMusic = null;
    }

    public void StopInGameMusic()
    {
        audioSources["InGameMusic"].Stop();
        currentMusic = null;
    }

    private void PlayMusic(Sound music, string audioSourceName)
    {
        if (music == currentMusic) return;

        var audioSource = audioSources[audioSourceName];
        audioSource.Stop();
        audioSource.clip = music.clip;
        audioSource.volume = music.volume;
        audioSource.pitch = music.pitch;
        audioSource.loop = music.loop;
        audioSource.Play();
        currentMusic = music;
    }

    public void PlaySound(string name)
    {
        if (!audioSources.TryGetValue(name, out var audioSource))
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        audioSource.Play();
    }

    public void Mute(bool isMute)
    {
        AudioListener.volume = isMute ? 0 : 1;
    }
}