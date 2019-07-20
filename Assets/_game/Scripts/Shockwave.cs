using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

    public AnimationCurve scaleCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    public float scaleMultiplier = 150f;
    public float duration = 0.5f;
    private float timer = 0f;


    void Update() {
        timer += Time.deltaTime / duration;
        var scaleValue = scaleCurve.Evaluate(timer) * scaleMultiplier;
        transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
        if(timer >= 1f) {
            Destroy(gameObject);
        }
    }
}
