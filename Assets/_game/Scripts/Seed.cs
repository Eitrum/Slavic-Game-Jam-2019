using UnityEngine;
using System.Collections.Generic;

public sealed class Seed : MonoBehaviour
{
    public Rigidbody rb;
    public Collider collider;
    public List<SeedTerrainImpact> seedTerrainImpactQueue;
    public List<SeedPlayerImpact> seedPlayerImpactQueue;
    public Vector3 direction;

    public void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRb = collision.collider.attachedRigidbody;
        Character character = null;
        if (otherRb != null)
        {
            character = otherRb.GetComponent<Character>();
        }
        if (otherRb != null && character != null)
        {
            seedPlayerImpactQueue.Add(new SeedPlayerImpact
            {
                position = transform.position,
                character = character
            });
        }
        else
        {
            seedTerrainImpactQueue.Add(new SeedTerrainImpact
            {
                position = rb.position,
                direction = direction
            });
        }
        Destroy(this.gameObject);
    }
}

public struct SeedTerrainImpact
{
    public Vector3 position;
    public Vector3 direction;
}

public struct SeedPlayerImpact
{
    public Vector3 position;
    public Character character;
}