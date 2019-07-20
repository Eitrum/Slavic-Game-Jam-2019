using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class VineSystem
{

    private readonly Vine vinePrefab;
    private readonly List<SeedTerrainImpact> seedImpactQueue;
    private readonly List<Seed> seeds;
    private readonly List<Vine> vines;
    private readonly VineSettings vineSettings;
    private readonly List<Character> characters;
    private readonly List<SpawnCharacterRequest> spawnCharacterRequests;
    private readonly List<ExplosionIntent> explosionIntents;

    private const float RAYCAST_DISTANCE = 1.5f;

    public VineSystem(
        Vine vinePrefab,
        List<SeedTerrainImpact> seedImpactQueue,
        List<Seed> seeds,
        List<Vine> vines,
        VineSettings vineSettings,
        List<Character> characters,
        List<SpawnCharacterRequest> spawnCharacterRequests,
        List<ExplosionIntent> explosionIntents
    )
    {
        this.vinePrefab = vinePrefab;
        this.seedImpactQueue = seedImpactQueue;
        this.seeds = seeds;
        this.vines = vines;
        this.vineSettings = vineSettings;
        this.characters = characters;
        this.spawnCharacterRequests = spawnCharacterRequests;
        this.explosionIntents = explosionIntents;
    }

    public void Tick()
    {
        // Spawn vines
        {
            foreach (var impact in seedImpactQueue)
            {
                SpawnVine(impact.position, impact.seedFlightLength);
            }
            seedImpactQueue.Clear();
        }

        {
            int layerMask = LayerMask.GetMask("Players");
            var vinesToSpawn = new List<Vector3>();
            foreach (var vine in vines)
            {
                var collisions = Physics.OverlapSphere(vine.transform.position, vine.transform.localScale.x / 2, layerMask);
                foreach (var collision in collisions)
                {
                    Character character = collision.GetComponentInParent<Character>();
                    characters.Remove(character);
                    Vector3 position = character.transform.position;

                    spawnCharacterRequests.Add(new SpawnCharacterRequest
                    {
                        spawnTimer = 1.5f,
                        playerIndex = character.playerIndex
                    });

                    // Spawn vine at death.
                    MonoBehaviour.Instantiate(character.deathEffectPrefab, position, Quaternion.identity);
                    vinesToSpawn.Add(position);

                    Object.DestroyImmediate(character.gameObject);
                }
            }

            foreach (var position in vinesToSpawn)
            {
                SpawnVine(position, 2f);

            }
        }

        // Scale vines
        foreach (var vine in vines)
        {
            vine.transform.localScale = Vector3.Lerp(vine.transform.localScale, vine.endScale * Vector3.one, (Time.time - vine.spawnTimeStamp) / vineSettings.growDuration);
        }

        // Explode if in fire
        {
            int layerMask = LayerMask.GetMask("Fire");
            List<Vine> vinesToDestroy = new List<Vine>();
            foreach (var vine in vines) {
                var colliders = Physics.OverlapSphere(vine.transform.position, vine.transform.localScale.x / 2f, layerMask, QueryTriggerInteraction.Collide);
                if (colliders.Length > 0)
                {
                    explosionIntents.Add(new ExplosionIntent
                    {
                        position = vine.transform.position,
                        timeToExplosion = 0.1f,
                        vine = vine
                    });
                    vinesToDestroy.Add(vine);
                }
            }

            foreach (var vine in vinesToDestroy)
            {
                vines.Remove(vine);
            }
        }
    }

    private void SpawnVine(Vector3 position, float seedFlightDistance)
    {
        Vine vine = Object.Instantiate(vinePrefab, position, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
        vine.transform.localScale = vineSettings.startScale * Vector3.one;
        vine.spawnTimeStamp = Time.time;
        vine.endScale = vineSettings.endScaleMultiplier * seedFlightDistance + vineSettings.minEndScale;
        vines.Add(vine);
    }
}
