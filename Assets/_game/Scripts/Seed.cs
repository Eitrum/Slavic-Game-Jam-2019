using UnityEngine;
using System.Collections.Generic;

public sealed class Seed : MonoBehaviour
{
    public Rigidbody rb;
    public Collider collider;
    public List<SeedTerrainImpact> seedTerrainImpactQueue;
    public List<SeedPlayerImpact> seedPlayerImpactQueue;
    public List<SeedVineStay> seedVineStayQueue;
    public Vector3 direction;

    internal List<Seed> seeds;
    internal List<Vine> overlappedVines = new List<Vine>();
    internal bool wasOverlappingVines;
    internal float yVelocityWhenEnterVine;
    internal Vector3 spawnPosition;

    public void OnCollisionEnter(Collision collision)
    {
        int layerMask = LayerMask.GetMask("Vines");
        if (Physics.CheckSphere(transform.position, 2f, layerMask))
        {
            return;
        }
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
                direction = direction,
                seedFlightLength = Vector3.Magnitude(transform.position - spawnPosition)
            });
        }
        seeds.Remove(this);
        Destroy(this.gameObject);
    }

    public void OnTriggerStay(Collider other)
    {
        Vine vine = other.GetComponent<Vine>();
        if (vine != null)
        {
            seedVineStayQueue.Add(new SeedVineStay
            {
                seed = this,
                vine = vine
            });
        }
    }
}

public struct SeedTerrainImpact
{
    public Vector3 position;
    public Vector3 direction;
    public float seedFlightLength;
}

public struct SeedPlayerImpact
{
    public Vector3 position;
    public Character character;
}

public struct SeedVineStay
{
    public Seed seed;
    public Vine vine;
}