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

    [Header("References")]
    [SerializeField] private EnemySpawner zombieSpawner;
    [SerializeField] private EnemySpawner graveRobberSpawner;

    //[Header("Tasks")]
    //[SerializeField] private int nrOfTasks = 5;
    //[SerializeField] private int nrOfOrnaments = int.MaxValue; //Unused
    //[SerializeField] private float timePerTask = 10 * 60; // 10 minutes * 60 seconds per minute

    //[Header("Zombies")]
    ////[SerializeField] private int totalNrOfZombies = 3;
    //[SerializeField] private ZombieWaves[] zombieWaves;
    //[SerializeField] private float timeBetweenZombies = 0.5f;

    //[Header("GraveRobbers")]
    ////[SerializeField] private int totalNrOfGraveRobbers = 2;
    //[SerializeField] private GraveRobberWaves[] graveRobbersWaves;
    //[SerializeField] private float timeBetweenGraveRobbers = 0.5f;

    public int NrOfTasks { get => levels[currentLevel].gameWaves[currentWave].nrOfTasks; private set => levels[currentLevel].gameWaves[currentWave].nrOfTasks = value; }

    private void Awake() {
        SetInstance(this);

        if (levels[0] == null) throw new System.Exception("No levels set in PrototypeManager");

        SetLevel(levels[0]);
    }

    void Start()
    {
        TaskManager.GetInstance().ActivateTasks(levels[currentLevel].gameWaves[currentWave].timePerTask, levels[currentLevel].gameWaves[currentWave].nrOfTasks);

        SetZombieWaves();
        SetGraveRobberWaves();
    }

    public void AdvanceLevel() {
        if(levels[currentLevel + 1] == null) {
            Debug.Log("No more levels! You are win!");
        }
    }

    public void AdvanceWave() {
        if (levels[currentLevel].gameWaves[currentWave + 1] == null) {
            AdvanceLevel();
        }
        else {
            currentWave++;
        }
    }

    void SetLevel(LevelSO levelSO) {
        levels[currentWave] = levelSO;
        if (levels[currentWave].gameWaves[0] == null) throw new System.Exception("No levels set in currentLevel");

        currentWave = 0;
    }

    private void SetZombieWaves() {
        Queue<EnemyWaves> zombieWavesQueue = new Queue<EnemyWaves>();
        for (int i = 0; i < levels[currentLevel].gameWaves[currentWave].zombieWaves.Length; i++) {
            zombieWavesQueue.Enqueue(levels[currentLevel].gameWaves[currentWave].zombieWaves[i]);
        }
        zombieSpawner?.SetWaves(zombieWavesQueue, levels[currentLevel].gameWaves[currentWave].timeBetweenZombies);
    }

    private void SetGraveRobberWaves() {
        Queue<EnemyWaves> graveRobberWavesQueue = new Queue<EnemyWaves>();
        for (int i = 0; i < levels[currentLevel].gameWaves[currentWave].graveRobberWaves.Length; i++) {
            graveRobberWavesQueue.Enqueue(levels[currentLevel].gameWaves[currentWave].graveRobberWaves[i]);
        }
        graveRobberSpawner?.SetWaves(graveRobberWavesQueue, levels[currentLevel].gameWaves[currentWave].timeBetweenZombies);
    }
}
