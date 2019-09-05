using UnityEngine;

public class ObjectSpawner : Interactable
{
    [Header("Spawner")]
    [SerializeField] private GameObject spawnPrefab;

    public override Interactable Interact()
    {
        return Instantiate(spawnPrefab).GetComponent<Interactable>();
    }
}