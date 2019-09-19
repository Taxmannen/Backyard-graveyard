using UnityEngine;

/* Script made by Petter */
public class Incinerator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Pickup pickup = other.GetComponent<Pickup>();
            if (!PoolManager.ReturnPickup(pickup))
            {
                Destroy(other.gameObject);
            }
        }
    }
}