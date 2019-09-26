using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public class ObjectSpawner : Interactable
{
    #region Variables
    [Header("Spawner")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] protected Vector3 position;
    [SerializeField] protected Vector3 rotation;
    [SerializeField] private bool despawnWhenPutBack;
    [SerializeField, Tooltip("Destroys the last spawned object when you spawn a new")] private bool onlyOneActive;
    [SerializeField] private bool shouldPlaceInHand;

    [Header("Debug")]
    [SerializeField, ReadOnly] protected List<GameObject> spawnedObjects = new List<GameObject>();
    private GameObject lastSpawned = null;
    #endregion

    protected virtual void Awake()
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
                if (pickup && pickup.ActiveHand == null) ReturnObject(pickup.gameObject);
            }
        }
    }

    public override Interactable Interact()
    {
        PlayInteractSound();
        Pickup pickup = SpawnObject();
        if (shouldPlaceInHand) return null;
        else return pickup.Interact();
    }

    protected virtual Pickup SpawnObject()
    {
        if (onlyOneActive) ReturnObject(lastSpawned);
        Pickup pickup = Instantiate(spawnPrefab, transform.position + position, Quaternion.Euler(transform.eulerAngles + rotation)).GetComponent<Pickup>();
        lastSpawned = pickup.gameObject;
        spawnedObjects.Add(pickup.gameObject);
        return pickup;
    }

    protected virtual void ReturnObject(GameObject pickup)
    {
        spawnedObjects.Remove(pickup);
        Destroy(pickup);
    }

    protected virtual void ReplayGame()
    {
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            if (spawnedObjects[i] != null) Destroy(spawnedObjects[i]);
        }
        spawnedObjects = new List<GameObject>();
    }

    public void RemoveFromSpawnedObjects(GameObject spawnedObject)
    {
        spawnedObjects.Remove(spawnedObject);
    }
}