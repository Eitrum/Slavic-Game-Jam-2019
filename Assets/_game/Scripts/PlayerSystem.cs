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
            Rewired.Player rPlayer = Rewired.ReInput.players.GetPlayer(player.playerIndex);
            character.movementIntent = new Vector3( rPlayer.GetAxis(RewiredConsts.Action.MoveHorizontal), 0f, rPlayer.GetAxis(RewiredConsts.Action.MoveVertical));
            character.shootIntent = rPlayer.GetButtonDown(RewiredConsts.Action.Shoot);
            if(character.shootTimer > 0f) {
                character.shootIntent = false;
                character.shootTimer -= Time.deltaTime;
            }
            else if(character.shootIntent) {
                character.shootTimer = shootSettings.shootInterval;
            }

            character.aimIntent = new Vector3(rPlayer.GetAxis(RewiredConsts.Action.AimHorizontal), 0f, rPlayer.GetAxis(RewiredConsts.Action.AimVertical));
        }
    }

    private int FindController(Player targetPlayer, IReadOnlyList<Player> otherPlayers) {
        int taken = 0;
        for(int i = 0; i < otherPlayers.Count; i++) {
            taken |= (otherPlayers[i].inputIndex >= 0 ? 1 << (otherPlayers[i].inputIndex) : 0);
        }

        for(int i = 0; i < 4; i++) {
            if((taken & (1 << i)) == (1 << i)) {
                continue;
            }

            var val = Input.GetAxis("AimHorizontal" + i) +
                Input.GetAxis("AimVertical" + i) +
                Input.GetAxis("Horizontal" + i) +
                Input.GetAxis("Vertical" + i);
            if(Mathf.Abs(val) >= 0.4f) {
                Debug.Log($"Player {targetPlayer.playerIndex} Input Index Assigned: {i}");
                return i;
            }
        }

        return -1;
    }
}
