using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementSystem
{
    readonly List<Character> characters;
    readonly List<Seed> seeds;
    readonly List<SeedPlayerImpact> seedPlayerImpactQueue;
    readonly Transform camera;

    public CharacterMovementSystem(
        List<Character> characters,
        List<Seed> seeds,
        List<SeedPlayerImpact> seedPlayerImpactQueue,
        Transform camera
    ) {
        this.characters = characters;
        this.seeds = seeds;
        this.seedPlayerImpactQueue = seedPlayerImpactQueue;
        this.camera = camera;
    }

    public void FixedTick()
    {
        for (int i = 0; i < seedPlayerImpactQueue.Count; i++)
        {
            seedPlayerImpactQueue[i].character.pushbackTimeStamp = Time.time;
        }
        seedPlayerImpactQueue.Clear();

        for (int i = 0; i < characters.Count; i++)
        {
            Character character = characters[i];
            if (Time.time < character.pushbackTimeStamp + .5f)
            {
                continue;
            }
            Vector3 movementRotatedByCamera = Quaternion.Euler(0f, camera.eulerAngles.y, 0f) * character.movementIntent;
            character.rb.AddForce(characters[i].speed * Time.deltaTime * movementRotatedByCamera);
            if (character.movementIntent.sqrMagnitude > .01f)
            {
                character.transform.rotation = Quaternion.LookRotation(character.movementIntent);
            }

            Vector3 planeVelocity = new Vector3(character.rb.velocity.x, 0f, character.rb.velocity.z);
            if (planeVelocity.magnitude > 0.5f)
            {
                character.transform.localScale = Mathf.Lerp(character.transform.localScale.x, 0.05f * Mathf.Sin(Time.time * 20f) + 1f, Time.fixedDeltaTime * 100f ) * Vector3.one;
            }
            else
            {
                character.transform.localScale = Vector3.one;
            }
        }
    }
}
