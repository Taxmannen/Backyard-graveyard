using UnityEngine;

/* Script Made By Daniel */
public class OrnamentContainer : MonoBehaviour
{
    private Ornament placedOrnament;
    private GameObject ghost;

    private void Start()
    {
        Destroy(GetComponentInChildren<SpriteRenderer>().gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (placedOrnament == null && other.gameObject.CompareTag("Interactable"))
        {
            if (other.gameObject.GetComponent<Ornament>() != null)
            {
                Ornament currentOrnamnet = other.gameObject.GetComponent<Ornament>();
                if (ghost == null) ghost = currentOrnamnet.CreateGhostObject(transform.position + currentOrnamnet.GetPosition(), transform.eulerAngles + currentOrnamnet.GetRotation());
                if (currentOrnamnet.ActiveHand == null)
                {
                    placedOrnament = currentOrnamnet;
                    placedOrnament.PlaceOrnament(this, transform.position);
                    Destroy(ghost);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if (ghost != null && other.gameObject.GetComponent<Ornament>() != null) Destroy(ghost);
        }
    }

    public void RemoveOrnament()
    {
        placedOrnament = null;
    }
}