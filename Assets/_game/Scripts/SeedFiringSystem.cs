using UnityEngine;
using System.Collections.Generic;

public class SeedFiringSystem
{
    private readonly Seed seedPrefab;
    private readonly SeedSettings seedSettings;
    private readonly List<SeedTerrainImpact> seedTerrainImpactQueue;
    private readonly List<SeedPlayerImpact> seedPlayerImpactQueue;
    private readonly List<ShootIntent> shootIntents;

    public SeedFiringSystem(
        Seed seedPrefab,
        SeedSettings seedSettings,
        List<SeedTerrainImpact> seedTerrainImpactQueue,
        List<SeedPlayerImpact> seedPlayerImpactQueue,
        List<ShootIntent> shootIntents
    )
    {
        this.seedPrefab = seedPrefab;
        this.seedSettings = seedSettings;
        this.seedTerrainImpactQueue = seedTerrainImpactQueue;
        this.seedPlayerImpactQueue = seedPlayerImpactQueue;
        this.shootIntents = shootIntents;
    }

    public void Tick()
    {
        foreach (var shootIntent in shootIntents)
        {
            Seed seed = Object.Instantiate(seedPrefab);
            Physics.IgnoreCollision(seed.GetComponentInChildren<Collider>(), shootIntent.character.GetComponentInChildren<Collider>());
            seed.seedTerrainImpactQueue = seedTerrainImpactQueue;
            seed.seedPlayerImpactQueue = seedPlayerImpactQueue;
            Rigidbody rb = seed.rb;
            rb.position = shootIntent.position;
            rb.velocity = seedSettings.velocityMultiplier * shootIntent.direction;
        }
        shootIntents.Clear();
    }
}

public struct ShootIntent
{
    public Character character;
    public Vector3 position;
    public Vector3 direction;
}
