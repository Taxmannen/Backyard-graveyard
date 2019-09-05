using UnityEngine;

public class OrnamentPlacement : MonoBehaviour
{
    private Ornament placedOrnament;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if (placedOrnament == null && other.gameObject.GetComponent<Ornament>() != null)
            {
                Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeAll;
                rb.freezeRotation = true;

                placedOrnament = other.gameObject.GetComponent<Ornament>();
                placedOrnament.SetPositionAndRotation(transform.position);
                placedOrnament.Drop();
            }
        }
    }
}