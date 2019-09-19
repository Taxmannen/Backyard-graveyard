using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist
 * Edits by Simon
 */

[System.Serializable]
public class EnemySpawnLocation{
    public ObjectPool objectPool;
    public Transform location;
}
public class EnemySpawner : Singleton<EnemySpawner>
{
    private float timeBetweenSpawn;
    private float unrestModifier;
    private int nrOfSpawnsPerWave;
    [Header("References")]
    [SerializeField] private EnemySpawnLocation[] enemySpawnLocations;

    public void SetWavesProperties(float timeBetweenSpawns, float unrestModifier, int nrOfSpawnsPerWave) {
        this.timeBetweenSpawn = timeBetweenSpawns;
        this.unrestModifier = unrestModifier;
        this.nrOfSpawnsPerWave = nrOfSpawnsPerWave;
        StartCoroutine(Spawn(1));
    }

    private IEnumerator Spawn(int nrOfEnemies) {
        EnemySpawnLocation enemySpawnLocation = enemySpawnLocations[RandomManager.GetRandomNumber(0, enemySpawnLocations.Length)];

        for(int i = 0; i < nrOfSpawnsPerWave; i++)
            enemySpawnLocation.objectPool.Get(enemySpawnLocation.objectPool.transform.position, enemySpawnLocation.objectPool.transform.rotation);

        yield return new WaitForSecondsRealtime(timeBetweenSpawn + (UnrestManager.GetInstance().CurrentUnrest * unrestModifier));

        StartCoroutine(Spawn(nrOfEnemies));
    }
}
