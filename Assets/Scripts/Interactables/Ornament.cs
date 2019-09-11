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

    private OrnamentContainer container;
    private Rigidbody rb;
    #endregion

    public bool Snappable { get; set; } = true;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        base.Start();
    }

    public void PlaceOrnament(OrnamentContainer container, Vector3 position)
    {
        this.container = container;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.position = position + snappedPosition;
        transform.rotation = Quaternion.Euler(container.transform.eulerAngles + snappedRotation);
    }

    public override Interactable Interact()
    {
        PickupOrnamentFromPlacement();
        return this;
    }
    
    public void PickupOrnamentFromPlacement()
    {
        if (container)
        {
            rb.constraints = RigidbodyConstraints.None;
            container.RemoveOrnament();
            container = null;
        }
    }

    public Vector3 GetPosition() { return snappedPosition; }

    public Vector3 GetRotation() { return snappedRotation; }

    public OrnamentType GetOrnamentType() { return ornamentType; }
}