using UnityEngine;

/* Script Made By Daniel */
public class Placement : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField, ReadOnly] protected GameObject placedObject;
    [SerializeField, ReadOnly] protected GameObject ghost;

    public void PlaceObject(GameObject objectToPlace, Vector3 position, Vector3 rotation)
    {
        if (!placedObject)
        {
            placedObject = objectToPlace;
            Rigidbody rb = placedObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            placedObject.transform.position = position;
            placedObject.transform.rotation = Quaternion.Euler(rotation);
        }
    }

    public void RemovePlacedObject()
    {
        if (placedObject)
        {
            Rigidbody rb = placedObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
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