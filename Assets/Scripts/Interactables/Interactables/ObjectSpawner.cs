using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class ObjectSpawner : Interactable
{
    #region Variables
    [Header("Spawner")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private bool despawnWhenPutBack;
    [SerializeField, Tooltip("Destroys the last spawned object when you spawn a new")] private bool onlyOneActive;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private GameObject lastSpawned = null;
    #endregion

    private void Awake()
    {
        PlayButton.PlayEvent += ReplayGame;
    }

    // Fixa ett bättre sätt
    private void OnTriggerStay(Collider other)
    {
        if (!despawnWhenPutBack) return;
        if (other.CompareTag("Interactable"))
        {
            if (other.name == string.Format("{0}(Clone)", spawnPrefab.name))
            {
                Pickup pickup = other.GetComponent<Pickup>();
                if (pickup && pickup.ActiveHand == null) ReturnObject(pickup);
            }
        }
    }

    public override Interactable Interact()
    {
        if (objectPool) return PickupWithObjectPool();
        else return PickupWithoutObjectPool();
    }

    private Pickup PickupWithoutObjectPool()
    {
        if (onlyOneActive) ReturnObject(lastSpawned.GetComponent<Pickup>());
        Pickup pickup = Instantiate(spawnPrefab, transform.position + position, Quaternion.Euler(transform.eulerAngles + rotation)).GetComponent<Pickup>();
        lastSpawned = pickup.gameObject;
        spawnedObjects.Add(pickup.gameObject);
        return pickup;
    }

    private Pickup PickupWithObjectPool()
    {
        GameObject pickup = objectPool.Get(transform.position + position, Quaternion.Euler(transform.eulerAngles + rotation), null);
        spawnedObjects.Add(pickup);
        return pickup.GetComponent<Pickup>();
    }

    private void ReturnObject(Pickup pickup)
    {
        spawnedObjects.Remove(pickup.gameObject);
        if (objectPool) PoolManager.ReturnPickup(pickup);
        else Destroy(pickup.gameObject);
    }

    private void ReplayGame()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            if (spawnedObjects[i] != null)
            {
                if (objectPool) objectPool.ReturnToPool(spawnedObjects[i]);
                else Destroy(spawnedObjects[i]);
            }
        }
        spawnedObjects = new List<GameObject>();
    }
}