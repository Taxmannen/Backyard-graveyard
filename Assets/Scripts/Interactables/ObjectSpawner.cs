using UnityEngine;

public class ObjectSpawner : Interactable
{
    [Header("Spawner")]
    [SerializeField] private GameObject spanwPrefab;

    public override Interactable Interact()
    {
        return Instantiate(spanwPrefab).GetComponent<Interactable>();
    }
}
