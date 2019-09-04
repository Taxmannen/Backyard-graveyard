using UnityEngine;

/* Script Made By Daniel */
public class Pickup : Interactable
{
    [Header("Pickup")]
    [SerializeField] private bool snapOnPickup;
    //[SerializeField] private bool snapWhenThrow; //Skall implementeras
    [SerializeField] protected bool shouldDespawnWhenOnGround;
    [SerializeField] protected float despawnTimeWhenOnGround;

    public bool SnapOnPickup
    {
        get { return snapOnPickup; }
    }

    public virtual void Drop()
    {
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
