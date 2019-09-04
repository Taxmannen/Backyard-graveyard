using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Interactable
{

    [SerializeField] private GameObject[] bodyTypes;
    [SerializeField] private GameObject[] headTypes;
    [SerializeField] private Transform bodySpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnRandomFullBody();
    }

    private void SpawnRandomFullBody()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(bodyTypes[Random.Range(0, bodyTypes.Length)], bodySpawnPoint.position, Quaternion.identity, bodySpawnPoint);
        }
    }
}
