﻿/* Script Made By Daniel */
using UnityEngine;

public class PlaceablePickup : Pickup
{
    [Header("Placeable Pickup")]
    [SerializeField] private Vector3 snappedPosition;
    [SerializeField] private Vector3 snappedRotation;

    public Placement Placement { get; set; } = null;

    public bool ThiefIsHolding { get; set; } = false;

    public override Interactable Interact()
    {
        PickupObjectFromPlacement();
        return this;
    }

    public void PickupObjectFromPlacement()
    {
        if (Placement)
        {
            Placement.RemovePlacedObject();
            Placement = null;
        }
    }

    public Vector3 GetPosition() { return snappedPosition; }
    public Vector3 GetRotation() { return snappedRotation; }
}