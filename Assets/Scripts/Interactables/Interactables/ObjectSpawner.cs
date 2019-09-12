using UnityEngine;

/* Script Made By Daniel */
public class ObjectSpawner : Interactable
{
    [Header("Spawner")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private bool despawnWhenPutBack;

    public override Interactable Interact()
    {
        Pickup pickup = Instantiate(spawnPrefab, transform.position + position, Quaternion.Euler(transform.eulerAngles  + rotation)).GetComponent<Pickup>();
        return pickup;
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
                if (pickup.ActiveHand == null) Destroy(other.gameObject);
            }
        }
    }
}