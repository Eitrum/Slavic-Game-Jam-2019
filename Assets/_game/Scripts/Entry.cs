using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    List<Character> characters;

    private List<SeedImpact> seedImpactQueue = new List<SeedImpact>();

    [Header("Prefabs")]
    public Seed seedPrefab;

    [Header("Settings")]
    public SeedSettings seedSettings;

    // Systems
    private CharacterMovementSystem characterMovementSystem;
    private SeedFiringSystem seedFiringSystem;
    
    void Start()
    {
        characters = new List<Character>(32);
        characterMovementSystem = new CharacterMovementSystem(characters);
        seedFiringSystem = new SeedFiringSystem(seedPrefab, seedSettings, seedImpactQueue);
    }

    private void FixedUpdate()
    {
        characterMovementSystem.FixedTick();
    }

    void Update()
    {
        seedFiringSystem.Tick();
    }
}
