using UnityEngine;
using System.Collections.Generic;

public sealed class Seed : MonoBehaviour
{
    public Rigidbody rb;
    public List<SeedImpact> seedImpactQueue;

    public void OnCollisionEnter(Collision collision)
    {
        seedImpactQueue.Add(new SeedImpact
        {
            position = transform.position
        });
        Destroy(this.gameObject);
    }
}

public struct SeedImpact
{
    public Vector3 position;
}
