using UnityEngine;

/* Script made by Petter*/
public class Incinerator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            Destroy(other.gameObject);
        }
    }
}
