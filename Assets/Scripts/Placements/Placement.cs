﻿using UnityEngine;

/* Script Made By Daniel */
public class Placement : MonoBehaviour
{
    [SerializeField] protected PickupType placementType;

    [Header("Debug")]
    [SerializeField, ReadOnly] protected GameObject placedObject = null;
    [SerializeField, ReadOnly] protected GameObject ghost = null;
    
    private void OnTriggerStay(Collider other)
    {
        if (placedObject == null && other.gameObject.CompareTag("Interactable"))
        {
            PlaceablePickup placeablePickup = other.gameObject.GetComponent<PlaceablePickup>();
            if (placeablePickup != null && placeablePickup.GetPickupType() == placementType && !placeablePickup.ThiefIsHolding)
            {
                if (!ghost)
                {
                    ghost = placeablePickup.CreateGhostObject(transform.position + placeablePickup.GetPosition(), transform.eulerAngles + placeablePickup.GetRotation());
                }
                if (!placeablePickup.ActiveHand && !placeablePickup.Placement)
                {
                    placeablePickup.Placement = this;
                    PlaceObject(placeablePickup, transform.position + placeablePickup.GetPosition(), transform.eulerAngles + placeablePickup.GetRotation());
                    DestroyGhost();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ghost != null && other.gameObject.CompareTag("Interactable"))
        {
            if (other.gameObject.GetComponent<Pickup>().GetPickupType() == placementType) DestroyGhost();
        }
    }

    public void PlaceObject(PlaceablePickup objectToPlace, Vector3 position, Vector3 rotation)
    {
        if (!placedObject)
        {
            placedObject = objectToPlace.gameObject;
            Rigidbody rb = placedObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            placedObject.transform.position = position;
            placedObject.transform.rotation = Quaternion.Euler(rotation);
            if (objectToPlace is Ornament)
            {
                (this as OrnamentPlacement).CheckGraveCompletion();
                (this as OrnamentPlacement).SetUnrestIEnumerator(true);
            }
            else
            {
                TaskCardPickup taskCard = placedObject.GetComponent<TaskCardPickup>();
                if (taskCard) taskCard.ScaleTaskCard(true);
            }
        }
    }

    public void RemovePlacedObject()
    {
        if (placedObject)
        {
            Rigidbody rb = placedObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;

            Ornament ornament = placedObject.GetComponent<Ornament>();
            if (ornament)
            {
                (this as OrnamentPlacement).SetUnrestIEnumerator(false); //Made By Petter
                ornament.Placement = null;
            }
            else
            {
                TaskCardPickup taskCard = placedObject.GetComponent<TaskCardPickup>();
                if (taskCard) taskCard.ScaleTaskCard(false);
            }
            placedObject = null;
        }
    }

    protected void DestroyGhost()
    {
        if (ghost)
        {
            Destroy(ghost);
            ghost = null;
        }
    }
}