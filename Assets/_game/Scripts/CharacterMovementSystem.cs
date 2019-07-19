using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementSystem
{
    readonly List<Character> characters;
    readonly List<SeedPlayerImpact> seedPlayerImpactQueue;

    public CharacterMovementSystem(
        List<Character> characters,
        List<SeedPlayerImpact> seedPlayerImpactQueue
    ) {
        this.characters = characters;
        this.seedPlayerImpactQueue = seedPlayerImpactQueue;
    }

    public void FixedTick()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            Character character = characters[i];
            character.rb.AddForce(characters[i].speed * Time.deltaTime * character.movementIntent);
            if (character.movementIntent.sqrMagnitude > .01f)
            {
                character.transform.rotation = Quaternion.LookRotation(character.movementIntent);
            }
        }
        for (int i = 0; i < seedPlayerImpactQueue.Count; i++)
        {
            seedPlayerImpactQueue[i].character.rb.AddForce(Vector3.up * 100f);
        }
        seedPlayerImpactQueue.Clear();
    }
}
