using UnityEngine;

/* Script Made By Daniel */
public class OrnamentContainer : MonoBehaviour
{
    [SerializeField] private Grave grave;

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
            if (other.gameObject.GetComponent<Ornament>() != null && other.gameObject.GetComponent<Ornament>().Snappable)
            {
                Ornament currentOrnamnet = other.gameObject.GetComponent<Ornament>();
                if (ghost == null) ghost = currentOrnamnet.CreateGhostObject(transform.position + currentOrnamnet.GetPosition(), transform.eulerAngles + currentOrnamnet.GetRotation());
                if (currentOrnamnet.ActiveHand == null)
                {
                    placedOrnament = currentOrnamnet;
                    placedOrnament.PlaceOrnament(this, transform.position);
                    Destroy(ghost);
                    if (grave != null) grave.CheckTaskCompletion();
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

    public void DestroyOrnament()
    {
        if (placedOrnament != null)
        {
            Destroy(placedOrnament);
            RemoveOrnament();
        }
    }

    public Ornament GetPlacedOrnament()
    {
        return placedOrnament;
    }
}