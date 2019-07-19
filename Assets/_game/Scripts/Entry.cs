using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    private readonly List<Character> characters = new List<Character>();
    private readonly List<SeedImpact> seedImpactQueue = new List<SeedImpact>();
    private readonly List<Vine> vines = new List<Vine>();
    private readonly List<Player> players = new List<Player>();
    private readonly List<ShootIntent> shootIntents = new List<ShootIntent>();

    public List<Transform> spawnPoints;

    [Header("Prefabs")]
    public Seed seedPrefab;
    public Character characterPrefab;
    public Vine vinePrefab;

    [Header("Settings")]
    public SeedSettings seedSettings;
    public VineSettings vineSettings;
    public ShootSettings shootSettings;

    // Systems
    private CharacterMovementSystem characterMovementSystem;
    private SeedFiringSystem seedFiringSystem;
    private VineSystem vineSystem;
    private PlayerSystem playerSystem;
    private PlayerInputSystem playerInputSystem;
    
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
        seedFiringSystem = new SeedFiringSystem(seedPrefab, seedSettings, seedImpactQueue, shootIntents);
        vineSystem = new VineSystem(vinePrefab, seedImpactQueue, vines, vineSettings);
        playerSystem = new PlayerSystem(players, shootSettings);
        playerInputSystem = new PlayerInputSystem(players, shootIntents);
    }

    private void FixedUpdate()
    {
        characterMovementSystem.FixedTick();
    }

    void Update()
    {
        playerInputSystem.Tick();
        playerSystem.Tick();
        seedFiringSystem.Tick();
        vineSystem.Tick();
    }
}
