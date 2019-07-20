using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour {
    internal float spawnTimeStamp;
    internal float endScale;
    public Collider col;
    public float destroyDuration = 2f;
    private float destroyTimer = 0f;
    public bool isDestroying = false;

    public ParticleSystem[] particleSystemsToPause;
    private Material material;
    private float timer = 0f;

    void Awake() {
        material = particleSystemsToPause[0].GetComponent<ParticleSystemRenderer>().material;
        for(int i = 0; i < 7; i++) {
            particleSystemsToPause[i].GetComponent<ParticleSystemRenderer>().material = material;
        }
    }

    private void Update() {
        if(timer < 1f) {
            timer += Time.deltaTime;

        }
        else {
            foreach(var ps in particleSystemsToPause) {
                ps.Pause();
            }
        }
        if(isDestroying) {
            destroyTimer += Time.deltaTime / destroyDuration;
            material.SetFloat("_ErosionDriver", 1f - destroyTimer);
            if(destroyTimer >= 1f) {
                Destroy(gameObject);
            }
        }
    }
}
