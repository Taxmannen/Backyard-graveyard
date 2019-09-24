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
    [SerializeField, ReadOnly] private int currentLevel = -1;
    [SerializeField, ReadOnly] private int currentWave = -1;
    [SerializeField] private bool clearInteractablesOnPickup = false;

    [Header("References")]
    public TaskDoneBox taskDoneBox;
    //[SerializeField] private EnemySpawner zombieSpawner;
    //[SerializeField] private EnemySpawner graveRobberSpawner;

    private PlayButton playButton;
    private DateTime waveStartTime;

    public int NrOfTasks { get => GetCurrentWave().nrOfTasks; private set => GetCurrentWave().nrOfTasks = value; }
    public int CurrentLevel {
        get => currentLevel;
        set {
            Debug.Log($"Setting currentLevel from {currentLevel} to {value}");
            currentLevel = value;
        }
    }

    private void Awake()
    {
        SetInstance(this);

        if (levels[0] == null) throw new System.Exception("No levels set in PrototypeManager");

        //CurrentLevel = 0;
        //currentWave = 0;
        //SetLevel(0);
        playButton = PlayButton.GetInstance();
        PlayButton.PlayEvent += StartNewGame;
    }

    private void Reset()
    {
        currentLevel = -1;
        currentWave = -1;
        waveStartTime = DateTime.Now;
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
            TaskManager.GetInstance().levelCompletedText.text = "No more time! You are lose!";
            playButton.StopPlaying();
            //Lose game here
        }
    }

    public LevelSO GetCurrentLevel() { return levels[CurrentLevel]; }
    public EnemyWaveSO GetCurrentWave() { return levels[CurrentLevel].gameWaves[currentWave]; }

    public void AdvanceLevel()
    {
        if(CurrentLevel + 1 < levels.Length)
        {
            CurrentLevel++;
            currentWave = 0;
            SetWaveProperties();
        }
        else
        {
            Debug.Log("No more levels! You are win!");
            playButton.StopPlaying();
            TaskManager.GetInstance().levelCompletedText.text = "No more levels! You are win!";
        }
    }

    public void CompleteWave()
    {
        if (GetCurrentWave().PauseAfterCompletedWave)
        {
            //Pause
            playButton.StopPlaying();
        }
        else StartCoroutine(AdvanceWaveOnDelay(5)); // Just continue... on a delay
    }
    IEnumerator AdvanceWaveOnDelay(int delay)
    {
        for(int i = delay; i > 0; i--)
        {
            TaskManager.GetInstance().levelCompletedText.text = $"Completed wave {currentWave + 1}.. \nAdvancing in {i}";
            yield return new WaitForSecondsRealtime(1f);
        }

        TaskManager.GetInstance().levelCompletedText.text = "";
        AdvanceWave();
    }
    public void AdvanceWave()
    {
        if(currentWave + 1 < GetCurrentLevel().gameWaves.Length)
        {
            //taskDoneBox.Reset();
            currentWave++;
            SetWaveProperties();
            taskDoneBox.Reset();
        }
        else
        {
            // No more waves in this level, move on to the next level
            AdvanceLevel();
        }
    }

    private void SetWaveProperties()
    {
        Debug.Log("Setting properties for wave " + currentWave);

        try { TaskManager.GetInstance().ResetTasks(); } catch (System.Exception e) { Debug.LogError(e); }

        waveStartTime = DateTime.Now;
        SetEnemySpawnerProperties();

        TaskManager.GetInstance().ActivateTasks(GetCurrentWave().timePerTask, GetCurrentWave().nrOfTasks, GetCurrentWave().minNrOfOrnamnets, GetCurrentWave().maxNrOfOrnamnets, GetCurrentWave().chanceOfTreatment);
    }

    //void SetLevel(LevelSO levelSO)
    //{
    //    levels[currentWave] = levelSO;
    //    if (levels[currentWave].gameWaves[0] == null) throw new System.Exception("No levels set in currentLevel");

    //    currentWave = 0;
    //    waveStartTime = DateTime.Now;
    //}

    //void SetLevel(int nr)
    //{
    //    try { LevelSO tmp = levels[nr]; }
    //    catch (Exception e) { Debug.Log("No level"); }
    //    try { EnemyWaveSO tmp = levels[nr].gameWaves[0]; }
    //    catch (Exception e){ Debug.Log("No more waves!"); }

    //    CurrentLevel = nr;
    //    Debug.Log($"Setting level to {nr}");
    //    currentWave = 0;
    //    waveStartTime = DateTime.Now;
    //    SetWaveProperties();
    //}

    private void SetEnemySpawnerProperties()
    {
        EnemySpawner.GetInstance()?.SetWavesProperties(GetCurrentWave().timeBetweenEnemySpawns, GetCurrentWave().unrestModifier, GetCurrentWave().nrOfSpawnsPerWave);
    }

    private void StartNewGame()
    {
        Reset();
        AdvanceLevel();
        //AdvanceWave();
    }
}
