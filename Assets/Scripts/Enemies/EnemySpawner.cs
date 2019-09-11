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

    [SerializeField] private Transform enemyToSpawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawn = startTimeBetweenEachSpawn;
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator spawn()
    {
        while(true)
        {
            Instantiate(enemyToSpawnPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(timeBetweenSpawn);

            if(timeBetweenSpawn >= endTimeBetweenEachSpawn)
            {
                timeBetweenSpawn -= timeDecreaseBetweenEachSpawn;
            }
        }
    }
}
