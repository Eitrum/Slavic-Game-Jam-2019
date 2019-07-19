using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem
{
    readonly List<Player> players;

    public PlayerSystem(List<Player> players)
    {
        this.players = players;
    }

    public void Tick()
    {
        for (int i = 0; i < players.Count; i++)
        {
            Player player = players[i];
            if (player.possesedCharacter == null)
            {
                continue;
            }
            player.possesedCharacter.movementIntent = new Vector3(Input.GetAxisRaw("Horizontal" + player.playerIndex), 0f, Input.GetAxisRaw("Vertical" + player.playerIndex));
            player.possesedCharacter.shootIntent = Input.GetKeyDown(KeyCode.Space);
        }
    }
}
