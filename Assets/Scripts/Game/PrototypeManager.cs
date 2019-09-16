using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Header("References")]
    [SerializeField] private EnemySpawner zombieSpawner;
    [SerializeField] private EnemySpawner graveRobberSpawner;

    [Header("Tasks")]
    [SerializeField] private int initialNrOfTasks = 5; // Is this needed?
    [SerializeField] private int totalNrOfTasks = 5;
    [SerializeField] private int nrOfOrnaments = int.MaxValue; //Unused
    [SerializeField] private float timePerTask = 10 * 60; // 10 minutes * 60 seconds per minute

    [Header("Zombies")]
    //[SerializeField] private int totalNrOfZombies = 3;
    [SerializeField] private ZombieWaves[] zombieWaves;
    [SerializeField] private float timeBetweenZombies = 0.5f;

    [Header("GraveRobbers")]
    //[SerializeField] private int totalNrOfGraveRobbers = 2;
    [SerializeField] private GraveRobberWaves[] graveRobbersWaves;
    [SerializeField] private float timeBetweenGraveRobbers = 0.5f;

    public int TotalNrOfTasks { get => totalNrOfTasks; private set => totalNrOfTasks = value; }

    private void Awake() {
        SetInstance(this);
    }

    void Start()
    {
        if (initialNrOfTasks > TotalNrOfTasks) throw new System.Exception("PrototypeManager Exception: totalNrOfTasks cannot be higer than initialNrOfTasks");

        TaskManager.GetInstance().ActivateTasks(initialNrOfTasks, timePerTask, TotalNrOfTasks);

        SetZombieWaves();
        SetGraveRobberWaves();
    }

    private void SetZombieWaves() {
        Queue<EnemyWaves> zombieWavesQueue = new Queue<EnemyWaves>();
        for (int i = 0; i < zombieWaves.Length; i++) {
            zombieWavesQueue.Enqueue(zombieWaves[i]);
        }
        zombieSpawner.SetWaves(zombieWavesQueue, timeBetweenZombies);
    }

    private void SetGraveRobberWaves() {
        Queue<EnemyWaves> graveRobberWavesQueue = new Queue<EnemyWaves>();
        for (int i = 0; i < zombieWaves.Length; i++) {
            graveRobberWavesQueue.Enqueue(zombieWaves[i]);
        }
        graveRobberSpawner.SetWaves(graveRobberWavesQueue, timeBetweenGraveRobbers);
    }
}
