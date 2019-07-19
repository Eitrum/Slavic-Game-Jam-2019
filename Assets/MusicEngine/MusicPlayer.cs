using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    public Channel[] channels = { };
    public int[] activeClips = { };

    void Awake() {
        for(int i = 0; i < channels.Length; i++) {
            channels[i].Initialize(gameObject);
        }
    }

    public float drums = 0f;
    public float smack = 0f;

    public int instrumentMask = 0;
    public float instument = 0f;


    public float melody = 0;

    void Update() {
        var audioTime = (float)AudioSettings.dspTime;
        drums = Mathf.PerlinNoise(audioTime / 4f, Mathf.Cos(audioTime / 8f));
        activeClips[0] = (int)Mathf.Lerp(-1, channels[0].clips.Length, drums * 1.1f);

        smack = Mathf.PerlinNoise(audioTime, Mathf.Cos(audioTime + 0.37f));
        activeClips[1] = (int)Mathf.Lerp(-1, channels[1].clips.Length, smack * 1.1f);

        instument = Mathf.PerlinNoise(audioTime / 4f, Mathf.Sin(audioTime / 3f - 0.37f));
        activeClips[2] = (int)Mathf.Lerp(-1, channels[2].clips.Length, instument * 1.2f);
        activeClips[3] = (int)Mathf.Lerp(-1, channels[3].clips.Length, instument * 1.2f);

        UpdateMusic();
    }

    [ContextMenu("Update All")]
    public void UpdateMusic() {
        for(int i = 0; i < channels.Length; i++) {
            channels[i].Play(activeClips[i]);
        }
    }
}