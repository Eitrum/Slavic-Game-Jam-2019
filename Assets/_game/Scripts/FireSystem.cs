using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FireSystem : MonoBehaviour
{
    public List<ExplosionIntent> explosionIntents;

    public FireSystem(
        List<ExplosionIntent> explosionIntents
    )
    {
        this.explosionIntents = explosionIntents;
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
                explosionIntents.Remove(explosionIntents[i]);
            }
        }
    }
}

public struct ExplosionIntent
{
    public float timeToExplosion;
    public Vector3 position;
}
