using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform shootTransform;
    public Rigidbody rb;
    public float speed;

    internal Vector3 movementIntent;
    internal bool shootIntent;
}
