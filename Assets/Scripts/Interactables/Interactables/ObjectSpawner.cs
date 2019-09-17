using UnityEngine;

/* Script Made By Daniel */
public class ObjectSpawner : Interactable
{
    [Header("Spawner")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private bool despawnWhenPutBack;
    [SerializeField, Tooltip("Destroys the last spawned object when you spawn a new")] private bool onlyOneActive;

    private GameObject lastSpawned = null;

    public override Interactable Interact()
    {
        if (objectPool) return PickupWithObjectPool();
        else            return PickupWithoutObjectPool();
    }

    private Pickup PickupWithoutObjectPool()
    {
        if (onlyOneActive) Destroy(lastSpawned);
        Pickup pickup = Instantiate(spawnPrefab, transform.position + position, Quaternion.Euler(transform.eulerAngles + rotation)).GetComponent<Pickup>();
        lastSpawned = pickup.gameObject;
        return pickup;
    }

    private Pickup PickupWithObjectPool()
    {
        return objectPool.Get(transform.position + position, Quaternion.Euler(transform.eulerAngles + rotation), null).GetComponent<Pickup>();
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
                if (pickup.ActiveHand == null)
                {
                    if (objectPool) PoolManager.ReturnPickup(pickup);
                    else            Destroy(pickup.gameObject);
                }
            }
        }
    }
}