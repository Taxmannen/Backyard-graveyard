using UnityEngine;
/* Script Made By Daniel */
public class ObjectSpawner : Interactable
{
    [Header("Spawner")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;

    public override Interactable Interact()
    {
        Pickup pickup = Instantiate(spawnPrefab, transform.position + position, Quaternion.Euler(rotation)).GetComponent<Pickup>();
        return pickup;
    }
}