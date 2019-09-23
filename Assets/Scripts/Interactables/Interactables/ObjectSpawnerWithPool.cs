using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class ObjectSpawnerWithPool : ObjectSpawner
{
    /*[Header("Object Pool Spawner")]
    [SerializeField] */private ObjectPool objectPool;

    protected override void Awake()
    {
        base.Awake();
        objectPool = GetComponent<ObjectPool>();
    }

    protected override Pickup SpawnObject()
    {
        GameObject pickup = objectPool.Get(transform.position + position, Quaternion.Euler(transform.eulerAngles + rotation), null);
        spawnedObjects.Add(pickup);
        return pickup.GetComponent<Pickup>();
    }

    protected override void ReturnObject(GameObject pickupObject)
    {
        Pickup pickup = pickupObject.GetComponent<Pickup>();
        if (pickup.ActiveHand) pickup.ActiveHand.Drop();
        PoolManager.ReturnPickup(pickup);
    }

    protected override void ReplayGame()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            if (spawnedObjects[i] != null)
            {
                Ornament ornament = spawnedObjects[i].GetComponent<Ornament>();
                if (ornament)
                {
                    if (ornament.ActiveHand) ornament.ActiveHand.Drop();
                    if (ornament.Placement) ornament.Placement.RemovePlacedObject();
                }
                objectPool.ReturnToPool(spawnedObjects[i], true);
            }
        }
        spawnedObjects = new List<GameObject>();
    }
}