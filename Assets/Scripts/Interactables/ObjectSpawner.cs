using UnityEngine;

public class ObjectSpawner : Interactable
{
    [Header("Spawner")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private bool snapOnPickup; //TODO

    public override Interactable Interact()
    {
        Pickup pickup = Instantiate(spawnPrefab).GetComponent<Pickup>();
        return pickup;
    }
}