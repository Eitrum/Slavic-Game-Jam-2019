using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public Collider col;
    public int fireEffectRange = 3;
    public GameObject firePrefab;

    void Awake() {
        Instantiate(firePrefab, transform);
        Destroy(gameObject, 5f);
    }
}
