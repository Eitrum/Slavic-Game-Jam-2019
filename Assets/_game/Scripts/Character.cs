using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform shootTransform;
    public Rigidbody rb;
    public ParticleSystem shellParticle;
    public float speed;
    public GameObject deathEffectPrefab = null;

    internal Vector3 movementIntent;
    internal Vector3 aimIntent;
    internal bool shootIntent;

    internal float pushbackTimeStamp;

    internal float shootTimer;
}
