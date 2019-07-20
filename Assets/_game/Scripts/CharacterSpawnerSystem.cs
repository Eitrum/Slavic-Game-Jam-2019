using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public sealed class CharacterSpawnSystem
{
    private readonly List<Character> characters;
    private readonly List<Transform> spawnPositions;
    private readonly List<SpawnCharacterRequest> spawnRequests;
    private readonly Character[] characterPrefabs;
    private readonly List<Player> players;
    private readonly CinemachineTargetGroup targetGroup;

    public CharacterSpawnSystem(
        List<Character> characters,
        List<Transform> spawnPositions,
        List<SpawnCharacterRequest> spawnRequests,
        Character[] characterPrefabs,
        List<Player> players,
        CinemachineTargetGroup targetGroup
    )
    {
        this.characters = characters;
        this.spawnPositions = spawnPositions;
        this.spawnRequests = spawnRequests;
        this.characterPrefabs = characterPrefabs;
        this.players = players;
        this.targetGroup = targetGroup;
    }

    public void Tick()
    {
        for (int i = spawnRequests.Count - 1; i >= 0; --i)
        {
            SpawnCharacterRequest request = spawnRequests[i];
            if (request.spawnTimer <= 0f)
            {
                SpawnCharacter(request.playerIndex);
                spawnRequests.Remove(request);
            }
            else
            {
                request.spawnTimer -= Time.deltaTime;
                spawnRequests[i] = request;
            }
        }
    }

    private void SpawnCharacter(int playerIndex)
    {
        Vector3 spawnPosition = spawnPositions[playerIndex].position;
        Character character = Object.Instantiate(characterPrefabs[playerIndex], spawnPosition, Quaternion.identity);
        foreach (var player in players)
        {
            if (player.playerIndex == playerIndex)
            {
                player.possesedCharacter = character;
                player.inputIndex = -1;
            }
        }
        targetGroup.AddMember(character.transform, 1f, 1f);
        characters.Add(character);
    }
}

public struct SpawnCharacterRequest
{
    public float spawnTimer;
    public int playerIndex;
}
