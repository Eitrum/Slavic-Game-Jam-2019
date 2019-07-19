using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem
{
    private readonly List<Character> characters;
    private readonly List<ShootIntent> fireSeedIntents;

    public PlayerInputSystem(
        List<Character> characters
    )
    {
        this.characters = characters;
    }

    public void Tick()
    {
        foreach (var character in characters)
        {
            if (character.shootIntent)
            {
                Transform charTransform = character.transform;
                fireSeedIntents.Add(new ShootIntent
                {
                    position = charTransform.position + charTransform.forward,
                    direction = charTransform.forward
                });
            }
        }
    }
}
