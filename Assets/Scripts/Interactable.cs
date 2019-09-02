using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool snapOnPickup;
    [SerializeField] private Transform handle;

    public Hand ActiveHand { get; set; } = null;

    public bool SnapOnPickup
    {
        get { return snapOnPickup; }
    }

    public Vector3 GetPickupPosition()
    {
        if (handle != null) return handle.position;
        else                return transform.position;
    }
}