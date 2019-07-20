using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FireSystem
{
    private readonly List<ExplosionIntent> explosionIntents;
    private readonly List<Vine> vines;
    private readonly Explosion explosionPrefab;
    private readonly List<Explosion> explosions = new List<Explosion>();
    private readonly List<Vine> vinesToExplode;

    public FireSystem(
        List<ExplosionIntent> explosionIntents,
        Explosion explosionPrefab,
        List<Vine> vines,
        List<Vine> vinesToExplode
    )
    {
        this.explosionIntents = explosionIntents;
        this.explosionPrefab = explosionPrefab;
        this.vines = vines;
        this.vinesToExplode = vinesToExplode;
    }

    internal void Tick()
    {
        foreach (var explosion in explosions)
        {
            if (explosion != null)
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
                vines.Remove(explosionIntent.vine);    
                explosionIntent.vine.isDestroying = true;
                vinesToExplode.Remove(explosionIntent.vine);
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
