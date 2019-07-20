using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    public float duration = 2f;

    void Update() {
        duration -= Time.deltaTime;
        if(duration <= 0f) {
            Destroy(gameObject);
        }
    }
}
