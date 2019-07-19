using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Entry : MonoBehaviour
{
    private readonly List<Character> characters = new List<Character>();
    private readonly List<SeedTerrainImpact> seedTerrainImpactQueue = new List<SeedTerrainImpact>();
    private readonly List<SeedPlayerImpact> seedPlayerImpactQueue = new List<SeedPlayerImpact>();
    private readonly List<Vine> vines = new List<Vine>();
    private readonly List<Player> players = new List<Player>();
    private readonly List<ShootIntent> shootIntents = new List<ShootIntent>();
    private readonly List<SpawnCharacterRequest> spawnCharacterRequests = new List<SpawnCharacterRequest>();

    public List<Transform> spawnPoints;
    public Camera cam;
    public CinemachineTargetGroup targetGroup;

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
    private CharacterSpawnSystem characterSpawnSystem;
    
    void Start()
    {
        for (int i = 0; i < 2; ++i)
        {
            players.Add(new Player
            {
                playerIndex = i
            });
            spawnCharacterRequests.Add(new SpawnCharacterRequest
            {
                spawnTimer = 0f,
                playerIndex = i
            });
        }

        characterSpawnSystem = new CharacterSpawnSystem(characters, spawnPoints, spawnCharacterRequests, characterPrefab, players, targetGroup);
        characterMovementSystem = new CharacterMovementSystem(characters, seedPlayerImpactQueue, cam.transform);
        seedFiringSystem = new SeedFiringSystem(seedPrefab, seedSettings, seedTerrainImpactQueue, seedPlayerImpactQueue, shootIntents);
        vineSystem = new VineSystem(vinePrefab, seedTerrainImpactQueue, vines, vineSettings, characters, spawnCharacterRequests);
        playerSystem = new PlayerSystem(players, shootSettings);
        playerInputSystem = new PlayerInputSystem(players, shootIntents);
    }

    private void FixedUpdate()
    {
        characterMovementSystem.FixedTick();
    }

    void Update()
    {
        characterSpawnSystem.Tick();
        playerInputSystem.Tick();
        playerSystem.Tick();
        seedFiringSystem.Tick();
        vineSystem.Tick();
    }
}
