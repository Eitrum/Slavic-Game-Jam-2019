using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMovementSystem
{
    private readonly List<Seed> seeds;
    private readonly List<SeedVineStay> seedVineStayQueue;
    private readonly List<SeedTerrainImpact> seedImpactQueue;


    public SeedMovementSystem(
        List<Seed> seeds,
        List<SeedVineStay> seedVineStayQueue,
        List<SeedTerrainImpact> seedImpactQueue
    ) {
        this.seeds = seeds;
        this.seedVineStayQueue = seedVineStayQueue;
        this.seedImpactQueue = seedImpactQueue;
    }

    public void FixedTick()
    {
        // Reset value
        for (int i = 0; i < seeds.Count; i++)
        {
            seeds[i].wasOverlappingVines = seeds[i].overlappedVines.Count > 0;
            seeds[i].overlappedVines.Clear();
        }

        // Apply value
        for (int i = 0; i < seedVineStayQueue.Count; i++)
        {
            SeedVineStay seedVineStay = seedVineStayQueue[i];
            Seed seed = seedVineStay.seed;
            seed.overlappedVines.Add(seedVineStay.vine);
            if (!seed.wasOverlappingVines && seed.overlappedVines.Count == 1)
            {
                seed.yVelocityWhenEnterVine = seed.rb.velocity.y;
                seed.rb.velocity = new Vector3(seed.rb.velocity.x, 0f, seed.rb.velocity.z);
                seed.rb.useGravity = false;
            }
        }
        seedVineStayQueue.Clear();

        // Reapply 
        for (int i = 0; i < seeds.Count; i++)
        {
            Seed seed = seeds[i];
            if (seed.wasOverlappingVines && seed.overlappedVines.Count == 0)
            {
                seedImpactQueue.Add(new SeedTerrainImpact
                {
                    position = seed.transform.position,
                    seedFlightDuration = Time.time - seed.spawnTimeStamp
                });
                seeds.Remove(seed);
                Object.Destroy(seed.gameObject);
            }
        }
    }
}
