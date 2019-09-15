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

    private OrnamentPlacement placement;
    private Rigidbody rb;
    #endregion

    public bool Snappable { get; set; } = true;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        base.Start();
    }

    public void PlaceOrnament(OrnamentPlacement placement, Vector3 position)
    {
        this.placement = placement;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.position = position + snappedPosition;
        transform.rotation = Quaternion.Euler(placement.transform.eulerAngles + snappedRotation);
    }

    public override Interactable Interact()
    {
        PickupOrnamentFromPlacement();
        return this;
    }
    
    public void PickupOrnamentFromPlacement()
    {
        if (placement)
        {
            rb.constraints = RigidbodyConstraints.None;
            placement.RemoveOrnament();
            placement = null;
        }
    }

    public Vector3 GetPosition() { return snappedPosition; }

    public Vector3 GetRotation() { return snappedRotation; }

    public OrnamentType GetOrnamentType() { return ornamentType; }
}