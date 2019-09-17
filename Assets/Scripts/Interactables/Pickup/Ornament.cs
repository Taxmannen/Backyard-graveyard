using UnityEngine;

public enum OrnamentType { Candle, Flower, Statue, Heart, [System.ObsoleteAttribute] NumberOfTypes, [System.ObsoleteAttribute] None };

/* Script Made By Daniel */
public class Ornament : Pickup
{
    #region Variables
    [Header("Ornament")]
    [SerializeField] private OrnamentType ornamentType;
    [SerializeField] private Vector3 snappedPosition;
    [SerializeField] private Vector3 snappedRotation;

    private Rigidbody rb;

    public OrnamentPlacement Placement { get; private set; }

    public bool ThiefIsHolding { get; set; } = false;

    public bool Snappable { get; set; } = true;
    #endregion

    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        base.Start();
    }

    public void PlaceOrnament(OrnamentPlacement placement, Vector3 position)
    {
        Placement = placement;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.position = position + snappedPosition;
        transform.rotation = Quaternion.Euler(placement.transform.eulerAngles + snappedRotation);
    }

    public override Interactable Interact()
    {
        ThiefIsHolding = false;
        PickupOrnamentFromPlacement();
        return this;
    }
    
    public void PickupOrnamentFromPlacement()
    {
        if (Placement)
        {
            rb.constraints = RigidbodyConstraints.None;
            Placement.RemoveOrnament();
            Placement = null;
        }
    }
    public void ThiefPickup()
    {
        if (Placement)
        {
            ThiefIsHolding = true;
            PickupOrnamentFromPlacement();
        }
    }

    public Vector3 GetPosition() { return snappedPosition; }

    public Vector3 GetRotation() { return snappedRotation; }

    public OrnamentType GetOrnamentType() { return ornamentType; }
}