using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum GameState {
    Init,
    Start,
    Play,
    End
}

public class Entry : MonoBehaviour
{
    public const int PLAYER_COUNT = 2;

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
    public GameObject winText;
    public CinemachineImpulseSource impulseSource;

    [Header("Prefabs")]
    public Seed seedPrefab;
    public Character[] characterPrefabs;
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

    private GameState state;
    private GameState prevState;

    private float endGameTimer;
    private const float END_GAME_TIME = 3f;

    IEnumerator Start()
    {
        for (int i = 0; i < PLAYER_COUNT; i++)
        {
        players.Add(new Player
        {
            playerIndex = i
        });
        }


        characterSpawnSystem = new CharacterSpawnSystem(characters, spawnPoints, spawnCharacterRequests, characterPrefabs, players, targetGroup);
        characterMovementSystem = new CharacterMovementSystem(characters, seeds, seedPlayerImpactQueue, cam.transform);
        seedFiringSystem = new SeedFiringSystem(seeds, seedPrefab, seedSettings, shootSettings, seedTerrainImpactQueue, seedPlayerImpactQueue, seedVineStayQueue, shootIntents, impulseSource);
        vineSystem = new VineSystem(vinePrefab, seedTerrainImpactQueue, seeds, vines, vineSettings, characters, spawnCharacterRequests, explosionIntents, targetGroup);
        playerSystem = new PlayerSystem(players, shootSettings);
        playerInputSystem = new PlayerInputSystem(players, shootIntents);
        seedMovementSystem = new SeedMovementSystem(seeds, seedVineStayQueue, seedTerrainImpactQueue);
        fireSystem = new FireSystem(explosionIntents, explosionPrefab);
        yield return new WaitForSeconds(2f);
        
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case GameState.Init:
                break;
            case GameState.Start:
                
                break;
            case GameState.Play:
                seedMovementSystem.FixedTick();
                characterMovementSystem.FixedTick();
                vineSystem.FixedTick();
                break;
            case GameState.End:
                characterMovementSystem.FixedTick();
                break;
            default:
                break;
        }
    }

    void Update()
    {
        switch (state)
        {
            case GameState.Init:
                if (Input.anyKeyDown)
                {
                    startVirtualCam.SetActive(false);
                    state = GameState.Start;
                }
                break;
            case GameState.Start:
                for (int i = characters.Count - 1; i >= 0; --i)
                {
                    targetGroup.RemoveMember(characters[i].transform);
                    Object.Destroy(characters[i].gameObject);
                }
                characters.Clear();

                for (int i = vines.Count - 1; i >= 0; --i)
                {
                    Object.Destroy(vines[i].gameObject);
                }
                vines.Clear();

                for (int i = 0; i < PLAYER_COUNT; ++i)
                {
                    spawnCharacterRequests.Add(new SpawnCharacterRequest
                    {
                        spawnTimer = 0f,
                        playerIndex = i
                    });
                }

                state = GameState.Play;
                break;
            case GameState.Play:
                characterSpawnSystem.Tick();
                playerInputSystem.Tick();
                playerSystem.Tick();
                seedFiringSystem.Tick();
                fireSystem.Tick();

                if (characters.Count <= 1)
                {
                    endGameTimer = END_GAME_TIME;
                    winText.SetActive(true);
                    state = GameState.End;
                }
                break;
            case GameState.End:
                playerSystem.Tick();

                endGameTimer -= Time.deltaTime;
                if (endGameTimer < 0f && Input.anyKeyDown) {
                    winText.SetActive(false);
                    state = GameState.Start;
                }
                break;
            default:
                break;
        }
        prevState = state;
    }
}
