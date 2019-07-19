using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    List<Character> characters = new List<Character>();
    private List<SeedImpact> seedImpactQueue = new List<SeedImpact>();
    private List<Vine> vines = new List<Vine>();
    private List<Player> players = new List<Player>();

    public List<Transform> spawnPoints;

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
    private PlayerSystem playerSystem;
    
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            Transform spawnPoint = spawnPoints[i];
            Character character = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
            characters.Add(character);
            Player player = new Player();
            player.possesedCharacter = character;
            player.playerIndex = i;
            players.Add(player);
        }
        characterMovementSystem = new CharacterMovementSystem(characters);
        seedFiringSystem = new SeedFiringSystem(seedPrefab, seedSettings, seedImpactQueue);
        vineSystem = new VineSystem(vinePrefab, seedImpactQueue, vines, vineSettings);
        playerSystem = new PlayerSystem(players);
    }

    private void FixedUpdate()
    {
        characterMovementSystem.FixedTick();
    }

    void Update()
    {
        playerSystem.Tick();
        seedFiringSystem.Tick();
        vineSystem.Tick();
    }
}
