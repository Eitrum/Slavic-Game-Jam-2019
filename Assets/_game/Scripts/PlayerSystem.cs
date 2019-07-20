using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem {
    readonly List<Player> players;
    readonly ShootSettings shootSettings;

    public PlayerSystem(
        List<Player> players,
        ShootSettings shootSettings
    ) {
        this.players = players;
        this.shootSettings = shootSettings;
    }

    public void Tick() {
        for(int i = 0; i < players.Count; i++) {
            Player player = players[i];
            Character character = player.possesedCharacter;
            if(character == null) {
                continue;
            }
            character.movementIntent = new Vector3(Input.GetAxis("Horizontal" + player.playerIndex), 0f, Input.GetAxis("Vertical" + player.playerIndex));
            character.shootIntent = Input.GetAxisRaw("Shoot" + (player.playerIndex + 1)) > 0.5f;
            if(character.shootTimer > 0f) {
                character.shootIntent = false;
                character.shootTimer -= Time.deltaTime;
            }
            else if(character.shootIntent) {
                character.shootTimer = shootSettings.shootInterval;
            }


            character.aimIntent = new Vector3(Input.GetAxis("AimHorizontal" + player.playerIndex), 0f, Input.GetAxis("AimVertical" + player.playerIndex));

        }
    }
}
