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

    public void CheckGraveCompletion()
    {
        grave.CheckTaskCompletion();
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