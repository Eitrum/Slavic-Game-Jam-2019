using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementSystem
{
    readonly List<Character> characters;
    readonly List<SeedPlayerImpact> seedPlayerImpactQueue;
    readonly Transform camera;

    public CharacterMovementSystem(
        List<Character> characters,
        List<SeedPlayerImpact> seedPlayerImpactQueue,
        Transform camera
    ) {
        this.characters = characters;
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
        }
    }
}
