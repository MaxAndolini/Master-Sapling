using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Sound mainMenuMusic;
    [SerializeField] private Sound inGameMusic;

    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private Sound currentMusic;

    public static AudioManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
        
        foreach (Sound sound in sounds)
        {
            GameObject soundGO = new GameObject("Sound_" + sound.name);
            soundGO.transform.parent = transform;

            AudioSource audioSource = soundGO.AddComponent<AudioSource>();
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
    }
    
    public void StopInGameMusic()
    {
        audioSources["InGameMusic"].Stop();
    }

    private void PlayMusic(Sound music, string audioSourceName)
    {
        if (music == currentMusic) return;

        AudioSource audioSource = audioSources[audioSourceName];
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
        if (!audioSources.TryGetValue(name, out AudioSource audioSource))
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        audioSource.Play();
    }
    
    public void Mute(bool isMute){
        AudioListener.volume =  isMute ? 0 : 1;
    }
}
