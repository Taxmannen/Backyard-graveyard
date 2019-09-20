using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>Simon</author>
/// </summary>
[System.Serializable]
public class GameWave
{
    [SerializeField] private ZombieWaves[] zombieWaves;
    [SerializeField] private GraveRobberWaves[] graveRobberWaves;
}

[System.Serializable]
public class EnemyWaves
{
    [SerializeField] protected int nrOfEnemies = 5;
    [SerializeField] protected float timeUntilSpawn;

    public int NrOfEnemies { get => nrOfEnemies; private set => nrOfEnemies = value; }
    public float TimeUntilSpawn { get => timeUntilSpawn; private set => timeUntilSpawn = value; }

    public EnemyWaves(int nrOfEnemies, float timeUntilSpawn)
    {
        this.nrOfEnemies = nrOfEnemies;
        this.timeUntilSpawn = timeUntilSpawn;
    }
}

[System.Serializable]
public class ZombieWaves : EnemyWaves
{
    public ZombieWaves(int nrOfEnemies, float timeUntilSpawn) : base(nrOfEnemies, timeUntilSpawn)
    {
    }
}

[System.Serializable]
public class GraveRobberWaves : EnemyWaves
{
    public GraveRobberWaves(int nrOfEnemies, float timeUntilSpawn) : base(nrOfEnemies, timeUntilSpawn)
    {
    }
}

public class PrototypeManager : Singleton<PrototypeManager>
{
    [Header("Levels")]
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private int currentLevel = -1;
    [SerializeField] private int currentWave = -1;
    [SerializeField] private bool clearInteractablesOnPickup = false;

    [Header("References")]
    //[SerializeField] private EnemySpawner zombieSpawner;
    //[SerializeField] private EnemySpawner graveRobberSpawner;
    [SerializeField] private PlayButton playButton;

    private DateTime waveStartTime;

    public int NrOfTasks { get => GetCurrentWave().nrOfTasks; private set => GetCurrentWave().nrOfTasks = value; }

    private void Awake()
    {
        SetInstance(this);

        if (levels[0] == null) throw new System.Exception("No levels set in PrototypeManager");

        currentLevel = 0;
        currentWave = 0;
        SetLevel(levels[0]);
        PlayButton.PlayEvent += StartNewGame;
    }

    void Start()
    {
        //This swhould not happen here pls
        //Make start functrion or somth smh
        //AdvanceWave();
        //TaskManager.GetInstance().ActivateTasks(GetCurrentWave().timePerTask, GetCurrentWave().nrOfTasks, GetCurrentWave().minNrOfOrnamnets, GetCurrentWave().maxNrOfOrnamnets);
    }

    private void Update()
    {
        if (!PlayButton.isPlaying) return;

        if (
            GetCurrentWave().timeLimit == true &&
            (DateTime.Now - waveStartTime).TotalSeconds > GetCurrentWave().timelimitForWave)
        {
            Debug.Log("Time limit over: you are Lose game?", this);
            playButton.StopPlaying();
            //Lose game here
        }
    }

    public LevelSO GetCurrentLevel() { return levels[currentLevel]; }
    public EnemyWaveSO GetCurrentWave() { return levels[currentLevel].gameWaves[currentWave]; }

    public void AdvanceLevel()
    {
        if (levels[currentLevel + 1] == null)
        {
            Debug.Log("No more levels! You are win!");
            playButton.StopPlaying();
        }
    }

    public void CompleteWave()
    {
        if (GetCurrentWave().PauseAfterCompletedWave)
        {
            //Pause
        }
        else AdvanceWave(); // Just continue
    }
    public void AdvanceWave()
    {
        //Initiate the first wave
        if (currentLevel < 0 || currentWave < 0)
        {
            SetLevel(levels[0]);
            SetWaveProperties();
            return;
        }

        // No more waves in this level, move on to the next level
        try
        {
            bool b = levels[currentLevel].gameWaves[currentWave + 1] == null;
        }
        catch (Exception e)
        {
            AdvanceLevel();
            return;
        }

        currentWave++;
        SetWaveProperties();
    }

    private void SetWaveProperties()
    {
        Debug.Log("Setting properties for wave " + currentWave);

        try { TaskManager.GetInstance().ResetTasks(); } catch (System.Exception e) { Debug.LogError(e); }

        waveStartTime = DateTime.Now;
        SetEnemySpawnerProperties();

        TaskManager.GetInstance().ActivateTasks(GetCurrentWave().timePerTask, GetCurrentWave().nrOfTasks, GetCurrentWave().minNrOfOrnamnets, GetCurrentWave().maxNrOfOrnamnets, GetCurrentWave().chanceOfTreatment);
    }

    void SetLevel(LevelSO levelSO)
    {
        levels[currentWave] = levelSO;
        if (levels[currentWave].gameWaves[0] == null) throw new System.Exception("No levels set in currentLevel");

        currentWave = 0;
        waveStartTime = DateTime.Now;
    }

    private void SetEnemySpawnerProperties()
    {
        EnemySpawner.GetInstance()?.SetWavesProperties(GetCurrentWave().timeBetweenEnemySpawns, GetCurrentWave().unrestModifier, GetCurrentWave().nrOfSpawnsPerWave);
    }

    private void StartNewGame()
    {
        SetLevel(levels[0]);
        AdvanceWave();
    }

    public void Test()
    {
        float t = 12;
    }

    public void NewTest()
    {

    }
}
