using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    List<Character> characters;

    private List<SeedImpact> seedImpactQueue = new List<SeedImpact>();

    [Header("Prefabs")]
    public Seed seedPrefab;
    public Character characterPrefab;

    [Header("Settings")]
    public SeedSettings seedSettings;

    // Systems
    private CharacterMovementSystem characterMovementSystem;
    private SeedFiringSystem seedFiringSystem;
    
    void Start()
    {
        characters = new List<Character>(32);
        for (int i = 0; i < 2; i++)
        {
            Character character = Instantiate(characterPrefab);
            character.playerIndex = i;
            characters.Add(character);
        }
        characterMovementSystem = new CharacterMovementSystem(characters);
        seedFiringSystem = new SeedFiringSystem(seedPrefab, seedSettings, seedImpactQueue);
    }

    private void FixedUpdate()
    {
        characterMovementSystem.FixedTick();
    }

    void Update()
    {
        characterMovementSystem.Tick();
        seedFiringSystem.Tick();
    }
}
