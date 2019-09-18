using UnityEngine;

/* Script Made By Daniel */
public class OrnamentPlacement : Placement
{
    [Header("Ornament Placement")]
    [SerializeField] private Grave grave;

    private void Start()
    {
        Destroy(GetComponentInChildren<SpriteRenderer>().gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (placedObject == null && other.gameObject.CompareTag("Interactable"))
        {
            Ornament currentOrnament = other.gameObject.GetComponent<Ornament>();
            if (currentOrnament != null && !currentOrnament.ThiefIsHolding)
            {
                if (ghost == null) ghost = currentOrnament.CreateGhostObject(transform.position + currentOrnament.GetPosition(), transform.eulerAngles + currentOrnament.GetRotation());
                if (currentOrnament.ActiveHand == null)
                {
                    currentOrnament.Placement = this;
                    PlaceObject(other.gameObject, transform.position + currentOrnament.GetPosition(), transform.eulerAngles + currentOrnament.GetRotation());
                    DestroyGhost();
                    if (grave != null) grave.CheckTaskCompletion();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            if (ghost != null && other.gameObject.GetComponent<Ornament>() != null) DestroyGhost();
        }
    }

    public void ReturnOrnament()
    {
        if (placedObject)
        {
            PoolManager.ReturnPickup(placedObject.GetComponent<Pickup>());
            RemovePlacedObject();
        }
    }

    public Ornament GetPlacedOrnament()
    {
        if (placedObject != null) return placedObject.GetComponent<Ornament>();
        else return null;
    }
}