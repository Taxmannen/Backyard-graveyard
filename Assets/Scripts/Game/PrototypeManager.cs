using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>Simon</author>
/// </summary>
[System.Serializable]
public class GameWave {
    [SerializeField] private ZombieWaves[] zombieWaves;
    [SerializeField] private GraveRobberWaves[] graveRobberWaves;
}

[System.Serializable]
public class EnemyWaves {
    [SerializeField] protected int nrOfEnemies = 5;
    [SerializeField] protected float timeUntilSpawn;

    public int NrOfEnemies { get => nrOfEnemies; private set => nrOfEnemies = value; }
    public float TimeUntilSpawn { get => timeUntilSpawn; private set => timeUntilSpawn = value; }

    public EnemyWaves(int nrOfEnemies, float timeUntilSpawn) {
        this.nrOfEnemies = nrOfEnemies;
        this.timeUntilSpawn = timeUntilSpawn;
    }
}

[System.Serializable]
public class ZombieWaves : EnemyWaves {
    public ZombieWaves(int nrOfEnemies, float timeUntilSpawn) : base(nrOfEnemies, timeUntilSpawn){
    }
}

[System.Serializable]
public class GraveRobberWaves : EnemyWaves {
    public GraveRobberWaves(int nrOfEnemies, float timeUntilSpawn) : base(nrOfEnemies, timeUntilSpawn){
    }
}

public class PrototypeManager : Singleton<PrototypeManager>
{
    [Header("Levels")]
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private int currentLevel;
    [SerializeField] private int currentWave;
    [SerializeField] private bool clearInteractablesOnPickup = false;

    [Header("References")]
    //[SerializeField] private EnemySpawner zombieSpawner;
    //[SerializeField] private EnemySpawner graveRobberSpawner;

    private DateTime waveStartTime;

    public int NrOfTasks { get => GetCurrentWave().nrOfTasks; private set => GetCurrentWave().nrOfTasks = value; }

    private void Awake() {
        SetInstance(this);

        if (levels[0] == null) throw new System.Exception("No levels set in PrototypeManager");

        SetLevel(levels[0]);
    }

    void Start()
    {
        TaskManager.GetInstance().ActivateTasks(GetCurrentWave().timePerTask, GetCurrentWave().nrOfTasks);

        SetEnemySpawnerProperties();
    }

    private void Update() {
        if(
            GetCurrentWave().timeLimit == true &&
            (DateTime.Now - waveStartTime).TotalSeconds > GetCurrentWave().timelimitForWave) {
            Debug.Log("Time limit over: you are Lose game?", this);
            //Lose game here
        }
    }

    public LevelSO GetCurrentLevel() { return levels[currentLevel]; }
    public EnemyWaveSO GetCurrentWave() { return levels[currentLevel].gameWaves[currentWave]; }

    public void AdvanceLevel() {
        if(levels[currentLevel + 1] == null) {
            Debug.Log("No more levels! You are win!");
        }
    }

    public void CompleteWave() { AdvanceWave(); }
    public void AdvanceWave() {
        if (levels[currentLevel].gameWaves[currentWave + 1] == null) {
            AdvanceLevel();
        }
        else {
            currentWave++;

            try { TaskManager.GetInstance().ResetTasks(); } catch(System.Exception e) { Debug.LogError(e); }
            if (clearInteractablesOnPickup) GameManager.GetInstance().ClearAllInteractables();

            waveStartTime = DateTime.Now;

            TaskManager.GetInstance().ActivateTasks(GetCurrentWave().timePerTask, GetCurrentWave().nrOfTasks);
        }
    }

    void SetLevel(LevelSO levelSO) {
        levels[currentWave] = levelSO;
        if (levels[currentWave].gameWaves[0] == null) throw new System.Exception("No levels set in currentLevel");

        currentWave = 0;
        waveStartTime = DateTime.Now;
    }

    private void SetEnemySpawnerProperties() {
        EnemySpawner.GetInstance()?.SetWavesProperties(GetCurrentWave().timeBetweenEnemySpawns, GetCurrentWave().unrestModifier, GetCurrentWave().nrOfSpawnsPerWave);
    }
}
