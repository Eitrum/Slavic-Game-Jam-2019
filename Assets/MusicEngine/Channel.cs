using System;
using UnityEngine;

[System.Serializable]
public class Channel {
    public string name = "Default";
    private AudioSource audioSource;
    public AudioClip[] clips;

    private int currentClip = 0;

    public void Play(int clip) {
        if(clip == currentClip || clip >= clips.Length) {
            return;
        }
        currentClip = clip;
        audioSource.volume = clip >= 0 ? 1 : 0;
        var sample = audioSource.timeSamples;
        if(clip >= 0) {
            audioSource.clip = clips[clip];
            audioSource.Play();
            audioSource.timeSamples = sample % clips[clip].samples;
        }
    }

    public void Initialize(GameObject gameObject) {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.timeSamples = 0;
        audioSource.clip = clips[0];
        audioSource.volume = 0;
        audioSource.Play();
    }
}