using System.Collections;
using UnityEngine;

public enum CollisionTest { None, Drop, Collider }

public enum PickupType { Other, Weapon, Ornament, Head, Body, TaskCard, None }

/* Script Made By Daniel */
[RequireComponent(typeof(Rigidbody))]
public class Pickup : Interactable
{
    #region Variables
    [Header("Pickup")]
    [SerializeField] private PickupType pickupType;
    [SerializeField] private bool snapOnPickup;
    [SerializeField] protected bool shouldDespawnWhenOnGround;
    [SerializeField] protected float despawnTimeWhenOnGround;

    protected CollisionManager collisionManager;
    protected Collider[] colliders;
    private Coroutine coroutine;
    #endregion

    protected virtual void Awake()
    {
        collisionManager = CollisionManager.GetInstance();
        colliders = GetComponentsInChildren<Collider>();
    }

    public bool SnapOnPickup
    {
        get { return snapOnPickup; }
        set { snapOnPickup = value; }
    }

    public override Interactable Interact()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        if (collisionManager && collisionManager.GetCollisionTest()) collisionManager.SetColliderState(colliders, true);
        return this;
    }

    public virtual void Drop()
    {
        if (collisionManager && collisionManager.GetCollisionTest()) collisionManager.SetColliderState(colliders, false);
        ActiveHand = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && ActiveHand == null && shouldDespawnWhenOnGround)
        {
            if (coroutine == null) coroutine = StartCoroutine(DestoryMe());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (ActiveHand == null)
        {
            if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Static"))
            {
                Rigidbody rb = this.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.isKinematic = false;
                /*if (rb.velocity.sqrMagnitude >= Vector3.zero.sqrMagnitude)
                {
                    rb.AddForce(-rb.velocity);
                    rb.velocity = Vector3.zero;
                    Debug.Log(rb.velocity);
                }*/
            }
        }
    }

    private void OnDestroy()
    {
        IsBeingDestroyed = true;
        if (ActiveHand != null) ActiveHand.Drop();
    }

    private IEnumerator DestoryMe()
    {
        yield return new WaitForSeconds(despawnTimeWhenOnGround);
        Destroy(gameObject);
    }

    public PickupType GetPickupType() { return pickupType; }
}