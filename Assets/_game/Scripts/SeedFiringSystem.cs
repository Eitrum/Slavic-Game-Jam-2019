using UnityEngine;
using System.Collections.Generic;

public class SeedFiringSystem
{
    private readonly Seed seedPrefab;
    private readonly SeedSettings seedSettings;
    private readonly List<SeedImpact> seedImpactQueue;
    private readonly List<ShootIntent> shootIntents;

    public SeedFiringSystem(
        Seed seedPrefab,
        SeedSettings seedSettings,
        List<SeedImpact> seedImpactQueue,
        List<ShootIntent> shootIntents
    )
    {
        this.seedPrefab = seedPrefab;
        this.seedSettings = seedSettings;
        this.seedImpactQueue = seedImpactQueue;
        this.shootIntents = shootIntents;
    }

    public void Tick()
    {
        foreach (var shootIntent in shootIntents)
        {
            Seed seed = Object.Instantiate(seedPrefab);
            seed.seedImpactQueue = seedImpactQueue;
            Rigidbody rb = seed.rb;
            rb.position = shootIntent.position;
            rb.velocity = seedSettings.velocityMultiplier * shootIntent.direction;
        }
    }
}

public struct ShootIntent
{
    public Vector3 position;
    public Vector3 direction;
}
