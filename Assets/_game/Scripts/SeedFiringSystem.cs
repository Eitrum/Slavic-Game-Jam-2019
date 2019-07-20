using UnityEngine;
using System.Collections.Generic;

public class SeedFiringSystem
{
    private readonly List<Seed> seeds;
    private readonly Seed seedPrefab;
    private readonly SeedSettings seedSettings;
    private readonly List<SeedTerrainImpact> seedTerrainImpactQueue;
    private readonly List<SeedPlayerImpact> seedPlayerImpactQueue;
    private readonly List<SeedVineStay> seedVineStayQueue;
    private readonly List<ShootIntent> shootIntents;

    public SeedFiringSystem(
        List<Seed> seeds,
        Seed seedPrefab,
        SeedSettings seedSettings,
        List<SeedTerrainImpact> seedTerrainImpactQueue,
        List<SeedPlayerImpact> seedPlayerImpactQueue,
        List<SeedVineStay> seedVineStayQueue,
        List<ShootIntent> shootIntents
    )
    {
        this.seeds = seeds;
        this.seedPrefab = seedPrefab;
        this.seedSettings = seedSettings;
        this.seedTerrainImpactQueue = seedTerrainImpactQueue;
        this.seedPlayerImpactQueue = seedPlayerImpactQueue;
        this.seedVineStayQueue = seedVineStayQueue;
        this.shootIntents = shootIntents;
    }

    public void Tick()
    {
        foreach (var shootIntent in shootIntents)
        {
            Seed seed = Object.Instantiate(seedPrefab);
            seeds.Add(seed);
            Physics.IgnoreCollision(seed.collider, shootIntent.character.GetComponentInChildren<Collider>());
            seed.seeds = seeds;
            seed.seedTerrainImpactQueue = seedTerrainImpactQueue;
            seed.seedPlayerImpactQueue = seedPlayerImpactQueue;
            seed.seedVineStayQueue = seedVineStayQueue;
            Rigidbody rb = seed.rb;
            rb.position = shootIntent.position;
            seed.spawnPosition = shootIntent.position;
            rb.velocity = seedSettings.velocityMultiplier * shootIntent.direction;
            seed.direction = shootIntent.direction.normalized;
            float directionAngle = Vector3.SignedAngle(Vector3.forward, seed.direction, Vector3.up);
            directionAngle = Mathf.Round(directionAngle / 45f) * 45f;
            seed.direction = new Vector3(Mathf.Cos(directionAngle), 0f, Mathf.Sin(directionAngle));
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
