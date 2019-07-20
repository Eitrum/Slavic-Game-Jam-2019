using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FireSystem
{
    public List<ExplosionIntent> explosionIntents;
    public Explosion explosionPrefab;

    public FireSystem(
        List<ExplosionIntent> explosionIntents,
        Explosion explosionPrefab
    )
    {
        this.explosionIntents = explosionIntents;
        this.explosionPrefab = explosionPrefab;
    }

    internal void Tick()
    {
        float dt = Time.deltaTime;
        for (int i = explosionIntents.Count - 1, n = 0; i >= n; --i)
        {
            ExplosionIntent explosionIntent = explosionIntents[i];
            explosionIntent.timeToExplosion -= dt;
            if (explosionIntent.timeToExplosion <= 0f)
            {
                // Explode
                Object.Instantiate(explosionPrefab, explosionIntent.position, Random.rotation);
                explosionIntents.Remove(explosionIntents[i]);
            }
            else
            {
                explosionIntents[i] = explosionIntent;
            }
        }
    }
}

public struct ExplosionIntent
{
    public float timeToExplosion;
    public Vector3 position;
}
