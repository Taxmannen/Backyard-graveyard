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
        Ornament ornament = pickupObject.GetComponent<Ornament>();
        if (ornament.ActiveHand) ornament.ActiveHand.Drop();
        ornament.ReturnToPool();
    }

    protected override void ReplayGame()
    {
        foreach(GameObject ornament in spawnedObjects)
        {
            if (ornament != null) ornament.GetComponent<Ornament>().ReturnToPool(true);
        }
        spawnedObjects.Clear();
    }
}