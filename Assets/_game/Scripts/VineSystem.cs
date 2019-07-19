using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class VineSystem
{

    private readonly Vine vinePrefab;
    private readonly List<SeedImpact> seedImpactQueue;
    private readonly List<Vine> vines;
    private readonly VineSettings vineSettings;

    public VineSystem(
        Vine vinePrefab,
        List<SeedImpact> seedImpactQueue,
        List<Vine> vines,
        VineSettings vineSettings
    )
    {
        this.vinePrefab = vinePrefab;
        this.seedImpactQueue = seedImpactQueue;
        this.vines = vines;
        this.vineSettings = vineSettings;
    }

    public void Tick()
    {
        // Spawn vines
        foreach (var impact in seedImpactQueue)
        {
            Vine vine = Object.Instantiate(vinePrefab, impact.position, Random.rotation);
            vine.transform.localScale = vineSettings.startScale * Vector3.one;
            vines.Add(vine);
        }

        seedImpactQueue.Clear();

        // Scale vines
        foreach (var vine in vines)
        {
            vine.transform.localScale = Vector3.Lerp(vine.transform.localScale, Vector3.one * vineSettings.endScale, Time.deltaTime);
        }
    }
}
