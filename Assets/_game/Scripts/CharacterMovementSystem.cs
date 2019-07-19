using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementSystem
{
    readonly List<Character> characters;

    public CharacterMovementSystem(List<Character> characters)
    {
        this.characters = characters;
    }

    public void FixedTick()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            Character character = characters[i];
            character.rb.AddForce(characters[i].speed * Time.deltaTime * character.movementIntent);
        }
    }

    public void Tick()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            Character character = characters[i];
            character.movementIntent = new Vector3(Input.GetAxisRaw("Horizontal" + character.playerIndex), 0f, Input.GetAxisRaw("Vertical" + character.playerIndex));
        }
    }
}
