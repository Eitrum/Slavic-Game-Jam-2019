using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    List<Character> characters;

    private List<SeedImpact> seedImpactQueue = new List<SeedImpact>();
    private List<Vine> vines = new List<Vine>();

    [Header("Prefabs")]
    public Seed seedPrefab;
    public Character characterPrefab;
    public Vine vinePrefab;

    [Header("Settings")]
    public SeedSettings seedSettings;
    public VineSettings vineSettings;

    // Systems
    private CharacterMovementSystem characterMovementSystem;
    private SeedFiringSystem seedFiringSystem;
    private VineSystem vineSystem;
    
    void Start()
    {
        characters = new List<Character>(32);
        for (int i = 0; i < 2; i++)
        {
            characters.Add(Instantiate(characterPrefab));
        }
        characterMovementSystem = new CharacterMovementSystem(characters);
        seedFiringSystem = new SeedFiringSystem(seedPrefab, seedSettings, seedImpactQueue);
        vineSystem = new VineSystem(vinePrefab, seedImpactQueue, vines, vineSettings);
    }

    private void FixedUpdate()
    {
        characterMovementSystem.FixedTick();
    }

    void Update()
    {
        seedFiringSystem.Tick();
        vineSystem.Tick();
    }
}
