using UnityEngine;
using System.Collections.Generic;

public class SeedFiringSystem
{
    private readonly Seed seedPrefab;
    private readonly SeedSettings seedSettings;
    private readonly List<SeedImpact> seedImpactQueue;


    public SeedFiringSystem(
        Seed seedPrefab,
        SeedSettings seedSettings,
        List<SeedImpact> seedImpactQueue
    )
    {
        this.seedPrefab = seedPrefab;
        this.seedSettings = seedSettings;
        this.seedImpactQueue = seedImpactQueue;
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Seed seed = Object.Instantiate(seedPrefab);
            seed.seedImpactQueue = seedImpactQueue;
            Rigidbody rb = seed.rb;
            // TODO : Spawn later at firing player.
            rb.position = Vector3.zero + 5f * Vector3.up;
            rb.velocity = seedSettings.velocityMultiplier * Vector3.forward;
        }
    }
}
