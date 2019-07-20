using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FireSystem
{
    public List<ExplosionIntent> explosionIntents;
    public Explosion explosionPrefab;
    private List<Explosion> explosions = new List<Explosion>();

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
        foreach (var explosion in explosions)
        {
            explosion.col.enabled = false;
        }
        explosions.Clear();

        float dt = Time.deltaTime;
        for (int i = explosionIntents.Count - 1, n = 0; i >= n; --i)
        {
            ExplosionIntent explosionIntent = explosionIntents[i];
            explosionIntent.timeToExplosion -= dt;
            if (explosionIntent.timeToExplosion <= 0f)
            {
                // Explode
                Object.DestroyImmediate(explosionIntent.vine.gameObject);
                Explosion explosion = Object.Instantiate(explosionPrefab, explosionIntent.position, Random.rotation);
                explosions.Add(explosion);
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
    public Vine vine;
    public float timeToExplosion;
    public Vector3 position;
}
