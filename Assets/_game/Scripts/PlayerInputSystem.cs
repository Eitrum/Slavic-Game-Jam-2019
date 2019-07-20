using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputSystem {
    private readonly List<Player> players;
    private readonly List<ShootIntent> shootIntents;

    public PlayerInputSystem(
        List<Player> players,
        List<ShootIntent> shootIntents
    ) {
        this.players = players;
        this.shootIntents = shootIntents;
    }

    public void Tick() {
        foreach(var player in players) {
            if(player.possesedCharacter != null && player.possesedCharacter.shootIntent) {
                Transform shootTransform = player.possesedCharacter.shootTransform;
                Vector3 direction;
                if (player.possesedCharacter.aimIntent.magnitude >= .4f)
                {
                    direction = player.possesedCharacter.aimIntent;
                }
                else if(player.possesedCharacter.aimIntent.magnitude <= Mathf.Epsilon)
                {
                    direction = shootTransform.forward;
                }
                else
                {
                    continue;
                }

                shootIntents.Add(new ShootIntent {
                    character = player.possesedCharacter,
                    position = shootTransform.position,
                    direction = direction
                });
            }
        }
    }
}
