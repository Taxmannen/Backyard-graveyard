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

    public Placement Placement { get; set; } = null;

    public bool ThiefIsHolding { get; set; } = false;
    #endregion

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
            Placement.RemovePlacedObject();
            Placement = null;
        }
    }

    public Vector3 GetPosition() { return snappedPosition; }
    public Vector3 GetRotation() { return snappedRotation; }
    public OrnamentType GetOrnamentType() { return ornamentType; }
}