using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>Simon</author>
/// </summary>
[CreateAssetMenu(fileName = "Enemy wave", menuName = "Level manager/Waves/Enemy wave")]
public class EnemyWaveSO : ScriptableObject
{
    [Header("Properties")]
    public bool timeLimit;
    public int timelimitForWave;
    public float timeBetweenEnemySpawns;

    [Header("Tasks")]
    public int nrOfTasks = 5;
    public float timePerTask = 10 * 60; // 10 minutes * 60 seconds per minute
    [Range(0, 100)] public int chanceOfCorrectBodyCombination;
    public bool PauseAfterCompletedWave;
    public bool clearAllObjectPoolsOnPause;
    [Range(0, 3)] public int minNrOfOrnamnets;
    [Range(0, 3)] public int maxNrOfOrnamnets;
    [Range(0, 100)] public int chanceOfTreatment;

    [Header("Enemy waves")]
    public int nrOfSpawnsPerWave = 1;

    [Header("Unrest")]
    public float unrestModifier = 6;
}
