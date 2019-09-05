using UnityEngine;

public class Ornament : Pickup
{
    [Header("Ornament")]
    [SerializeField] private Vector3 snappedPosition;
    [SerializeField] private Vector3 snappedRotation;

    private Rigidbody rb;
    private Coroutine coroutine;
    //private OrnamentPlacement placement;

    //For Debug
    [SerializeField] private bool canPlace = true;

    protected override void Start()
    {
        rb = GetComponent<Rigidbody>();
        base.Start();
    }

    public void PlaceOrnament(OrnamentPlacement placement, Vector3 position)
    {
        //this.placement = placement;

        Drop();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.freezeRotation = true;

        transform.position = position + snappedPosition;
        transform.rotation = Quaternion.Euler(snappedRotation);
    }

    public override Interactable Interact()
    {
        //PickupOrnament();
        return this;
    }


    /*
    public void PickupOrnament()
    {
        if (coroutine == null) coroutine = StartCoroutine(Kaj());
        rb.constraints = RigidbodyConstraints.None;
        rb.freezeRotation = false;
        if (placement)
        {
            placement.RemoveOrnament();
            placement = null;
        }
    }

    private IEnumerator Kaj()
    {
        canPlace = false;
        yield return new WaitForSeconds(1);
        canPlace = true;
        coroutine = null;
    }

    public bool CanPlace()
    {
        return canPlace;
    }
    */
}