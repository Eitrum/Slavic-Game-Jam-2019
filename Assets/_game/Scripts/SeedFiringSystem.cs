using UnityEngine;

public class SeedFiringSystem
{
    public Seed seedPrefab;
    public SeedSettings seedSettings;

    public SeedFiringSystem(
        Seed seedPrefab,
        SeedSettings seedSettings
    )
    {
        this.seedPrefab = seedPrefab;
        this.seedSettings = seedSettings;
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Seed seed = Object.Instantiate(seedPrefab);
            Rigidbody rb = seed.rb;
            // TODO : Spawn later at firing player.
            rb.position = Vector3.zero;
            rb.velocity = seedSettings.velocityMultiplier * Vector3.forward;
        }
    }
}
