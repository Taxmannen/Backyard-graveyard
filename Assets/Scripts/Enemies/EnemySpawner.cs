using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public class EnemySpawner : MonoBehaviour
{

    private float timeBetweenSpawn;

    [SerializeField] private float startTimeBetweenEachSpawn = 20f;

    [SerializeField] private float timeDecreaseBetweenEachSpawn = 0.5f;

    [SerializeField] private float endTimeBetweenEachSpawn = 5f;

    [SerializeField] private ObjectPool objectPool;

    //New stuff
    private Queue<EnemyWaves> enemyWaves = new Queue<EnemyWaves>();
    float timeBetweenSpawns = 0.5f;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    timeBetweenSpawn = startTimeBetweenEachSpawn;
    //    StartCoroutine(spawn());
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}


    //private IEnumerator spawn()
    //{
    //    while(true)
    //    {
    //        Instantiate(enemyToSpawnPrefab, transform.position, transform.rotation);
    //        yield return new WaitForSeconds(timeBetweenSpawn);

    //        if(timeBetweenSpawn >= endTimeBetweenEachSpawn)
    //        {
    //            timeBetweenSpawn -= timeDecreaseBetweenEachSpawn;
    //        }
    //    }
    //}

    public void SetWaves(Queue<EnemyWaves> enemyWaves, float timeBetweenSpawns) {
        this.enemyWaves = enemyWaves;
        this.timeBetweenSpawns = timeBetweenSpawns;
        StartSpawning();
    }

    private void StartSpawning() {
        if (enemyWaves != null && enemyWaves.Count > 0) {
            EnemyWaves enemyWave = enemyWaves.Dequeue();
            float delay = enemyWave.TimeUntilSpawn;
            int amount = enemyWave.NrOfEnemies;

            Debug.Log("Spawning " + amount + " things from " + name + " in " + delay + " seconds");

            StartCoroutine(SpawnOnDelay(amount, delay));
        }
    }

    IEnumerator SpawnOnDelay(int nrOfEnemies, float delay) {
        yield return new WaitForSecondsRealtime(delay);
        StartCoroutine(Spawn(nrOfEnemies));
    }

    private IEnumerator Spawn(int nrOfEnemies) {
        for(int i = 0; i < nrOfEnemies; i++) {
            //Instantiate(enemyToSpawnPrefab, transform.position, transform.rotation);
            objectPool.Get(transform.position, transform.rotation);
            yield return new WaitForSecondsRealtime(timeBetweenSpawns);
        }

        if (enemyWaves != null && enemyWaves.Count > 0) {
            EnemyWaves enemyWave = enemyWaves.Dequeue();
            float delay = enemyWave.TimeUntilSpawn;
            int amount = enemyWave.NrOfEnemies;

            Debug.Log("Spawning " + amount + " things from " + name + " in " + delay + " seconds");

            StartCoroutine(SpawnOnDelay(amount, delay));
        }
    }
}
