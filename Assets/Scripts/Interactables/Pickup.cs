using UnityEngine;

/* Script Made By Daniel */
[RequireComponent(typeof(Rigidbody))]
public class Pickup : Interactable
{
    [Header("Pickup")]
    [SerializeField] private bool snapOnPickup;
    //[SerializeField] protected bool snapWhenThrow;
    [SerializeField] protected bool shouldDespawnWhenOnGround;
    [SerializeField] protected float despawnTimeWhenOnGround;

    public bool SnapOnPickup
    {
        get { return snapOnPickup; }
        set { snapOnPickup = value; }
    }

    public virtual void Drop()
    {
        //SetToOutlineMaterial(MaterialType.Standard);
        ActiveHand = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && ActiveHand == null && shouldDespawnWhenOnGround)
        {
            Destroy(gameObject, despawnTimeWhenOnGround);
        }
    }

    private void OnDestroy()
    {
        if (ActiveHand != null) ActiveHand.Drop();
    }
}