using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>Simon</author>
/// </summary>
[CreateAssetMenu(fileName = "Enemy wave", menuName = "Level manager/Waves/Enemy wave")]
public class EnemyWaveSO : ScriptableObject
{
    [Header("Zombies")]
    public ZombieWaves[] zombieWaves;
    public float timeBetweenZombies = 0.5f;

    [Header("GraveRobbers")]
    public GraveRobberWaves[] graveRobberWaves;
    public float timeBetweenGraveRobbers = 0.5f;

    [Header("Properties")]
    public bool timeLimit;
    public int timelimitForWave;

    [Header("Tasks")]
    public int nrOfTasks = 5;
    public float timePerTask = 10 * 60; // 10 minutes * 60 seconds per minute

    [Header("Unrest")]
    public float unrestModifier;
}
