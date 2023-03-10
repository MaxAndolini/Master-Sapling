using System;
using UnityEngine;

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0, 1)] public float volume = 1;
    [Range(0, 3)] public float pitch = 1;
    public bool loop;
}