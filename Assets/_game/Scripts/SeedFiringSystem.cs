using UnityEngine;
using System.Collections.Generic;
using Cinemachine;

public class SeedFiringSystem
{
    private readonly List<Seed> seeds;
    private readonly Seed seedPrefab;
    private readonly SeedSettings seedSettings;
    private readonly ShootSettings shootSettings;
    private readonly List<SeedTerrainImpact> seedTerrainImpactQueue;
    private readonly List<SeedPlayerImpact> seedPlayerImpactQueue;
    private readonly List<SeedVineStay> seedVineStayQueue;
    private readonly List<ShootIntent> shootIntents;
    private readonly CinemachineImpulseSource impulseSource;

    public SeedFiringSystem(
        List<Seed> seeds,
        Seed seedPrefab,
        SeedSettings seedSettings,
        ShootSettings shootSettings,
        List<SeedTerrainImpact> seedTerrainImpactQueue,
        List<SeedPlayerImpact> seedPlayerImpactQueue,
        List<SeedVineStay> seedVineStayQueue,
        List<ShootIntent> shootIntents,
        CinemachineImpulseSource impulseSource
    )
    {
        this.seeds = seeds;
        this.seedPrefab = seedPrefab;
        this.seedSettings = seedSettings;
        this.shootSettings = shootSettings;
        this.seedTerrainImpactQueue = seedTerrainImpactQueue;
        this.seedPlayerImpactQueue = seedPlayerImpactQueue;
        this.seedVineStayQueue = seedVineStayQueue;
        this.shootIntents = shootIntents;
        this.impulseSource = impulseSource;
    }

    public void Tick()
    {
        foreach (var shootIntent in shootIntents)
        {
            impulseSource.GenerateImpulse();
            Seed seed = Object.Instantiate(seedPrefab);
            seeds.Add(seed);
            Physics.IgnoreCollision(seed.collider, shootIntent.player.possesedCharacter.GetComponentInChildren<Collider>());
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

            shootIntent.player.possesedCharacter.GetComponent<AudioSource>().PlayOneShot(shootSettings.playerShootSFX[shootIntent.player.playerIndex]);
        }
        shootIntents.Clear();
    }
}

public struct ShootIntent
{
    public Player player;
    public Vector3 position;
    public Vector3 direction;
}
