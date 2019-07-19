using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem
{
    private readonly List<Player> players;
    private readonly List<ShootIntent> shootIntents;

    public PlayerInputSystem(
        List<Player> players,
        List<ShootIntent> shootIntents
    )
    {
        this.players = players;
        this.shootIntents = shootIntents;
    }

    public void Tick()
    {
        foreach (var player in players)
        {
            if (player.possesedCharacter.shootIntent)
            {
                Transform shootTransform = player.possesedCharacter.shootTransform;
                shootIntents.Add(new ShootIntent
                {
                    position = shootTransform.position + shootTransform.forward,
                    direction = shootTransform.forward
                });
            }
        }
    }
}
