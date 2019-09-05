using UnityEngine;

public class Ornament : Pickup
{
    [Header("Ornament")]
    [SerializeField] private Vector3 snappedPosition;
    [SerializeField] private Quaternion snappedRotation;

    public void SetPositionAndRotation(Vector3 position)
    {
        transform.position = position + snappedPosition;
        transform.rotation = snappedRotation;
    }
}