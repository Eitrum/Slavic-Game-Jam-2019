using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtAnyKey : MonoBehaviour {

    bool scaleDown = false;
    private float timer = 0f;
    public float duration = 1f;
    public float sinkStrength = 1f;
    public AnimationCurve sinkCurve = AnimationCurve.Linear(0, 0, 1, 1);

    void Update() {
        if(Input.anyKeyDown) {
            scaleDown = true;
        }
        if(scaleDown) {
            transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(0, -sinkStrength), sinkCurve.Evaluate(timer));
            timer += Time.deltaTime / duration;
            if(timer >= 1f) {
                Destroy(gameObject);
            }
        }
    }
}
