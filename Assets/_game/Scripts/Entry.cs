using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Entry : MonoBehaviour
{
    public const int PLAYER_COUNT = 4;

    private readonly List<Character> characters = new List<Character>();
    private readonly List<SeedTerrainImpact> seedTerrainImpactQueue = new List<SeedTerrainImpact>();
    private readonly List<SeedPlayerImpact> seedPlayerImpactQueue = new List<SeedPlayerImpact>();
    private readonly List<SeedVineStay> seedVineStayQueue = new List<SeedVineStay>();
    private readonly List<Seed> seeds = new List<Seed>();
    private readonly List<Vine> vines = new List<Vine>();
    private readonly List<Player> players = new List<Player>();
    private readonly List<ShootIntent> shootIntents = new List<ShootIntent>();
    private readonly List<SpawnCharacterRequest> spawnCharacterRequests = new List<SpawnCharacterRequest>();
    private readonly List<ExplosionIntent> explosionIntents = new List<ExplosionIntent>();

    public List<Transform> spawnPoints;
    public Camera cam;
    public GameObject startVirtualCam;
    public CinemachineTargetGroup targetGroup;

    [Header("Prefabs")]
    public Seed seedPrefab;
    public Character characterPrefab;
    public Vine vinePrefab;
    public Explosion explosionPrefab;

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
    private SeedMovementSystem seedMovementSystem;
    private FireSystem fireSystem;

    IEnumerator Start()
    {
        for (int i = 0; i < PLAYER_COUNT; ++i)
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
        characterMovementSystem = new CharacterMovementSystem(characters, seeds, seedPlayerImpactQueue, cam.transform);
        seedFiringSystem = new SeedFiringSystem(seeds, seedPrefab, seedSettings, shootSettings, seedTerrainImpactQueue, seedPlayerImpactQueue, seedVineStayQueue, shootIntents);
        vineSystem = new VineSystem(vinePrefab, seedTerrainImpactQueue, seeds, vines, vineSettings, characters, spawnCharacterRequests, explosionIntents);
        playerSystem = new PlayerSystem(players, shootSettings);
        playerInputSystem = new PlayerInputSystem(players, shootIntents);
        seedMovementSystem = new SeedMovementSystem(seeds, seedVineStayQueue, seedTerrainImpactQueue);
        fireSystem = new FireSystem(explosionIntents, explosionPrefab);
        yield return new WaitForSeconds(2f);
        startVirtualCam.SetActive(false);
    }

    private void FixedUpdate()
    {
        seedMovementSystem.FixedTick();
        characterMovementSystem.FixedTick();
        vineSystem.Tick();
    }

    void Update()
    {
        characterSpawnSystem.Tick();
        playerInputSystem.Tick();
        playerSystem.Tick();
        seedFiringSystem.Tick();
        fireSystem.Tick();
    }
}
