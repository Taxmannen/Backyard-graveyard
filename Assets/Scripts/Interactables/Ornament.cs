﻿using UnityEngine;

/* Script Made By Daniel */
public class Ornament : Pickup
{
    [Header("Ornament")]
    [SerializeField] private Vector3 snappedPosition;
    [SerializeField] private Vector3 snappedRotation;

    private OrnamentContainer container;
    private Rigidbody rb;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        base.Start();
    }

    public void PlaceOrnament(OrnamentContainer container, Vector3 position)
    {
        this.container = container;

        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.freezeRotation = true;

        transform.position = position + snappedPosition;
        transform.rotation = Quaternion.Euler(snappedRotation);
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
            rb.freezeRotation = false;
            container.RemoveOrnament();
            container = null;
        }
    }

    public Vector3 GetPosition() { return snappedPosition; }

    public Vector3 GetRotation() { return snappedRotation; }
}