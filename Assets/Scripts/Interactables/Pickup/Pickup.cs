using System.Collections;
using UnityEngine;

public enum CollisionTest { None, Drop, Collider }

public enum PickupType { Other, Weapon, Ornament, Head, Body }

/* Script Made By Daniel */
[RequireComponent(typeof(Rigidbody))]
public class Pickup : Interactable
{
    [Header("Pickup")]
    [SerializeField] private PickupType pickupType;
    [SerializeField] private bool snapOnPickup;
    //[SerializeField] protected bool snapWhenThrow;
    [SerializeField] protected bool shouldDespawnWhenOnGround;
    [SerializeField] protected float despawnTimeWhenOnGround;

    [Header("Debug")]
    [SerializeField] private CollisionTest collisionTest;
    [SerializeField] private float dropTime = 0.2f;

    private Coroutine coroutine;

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
        return this;
    }

    public virtual void Drop()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders) collider.enabled = true;
        ActiveHand = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && ActiveHand == null && shouldDespawnWhenOnGround)
        {
            if (coroutine == null) coroutine = StartCoroutine(DestoryMe());
        }
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Static"))
        {
            switch (collisionTest)
            {
                case CollisionTest.Collider:
                    if (ActiveHand)
                    {
                        Collider[] colliders = GetComponentsInChildren<Collider>();
                        foreach (Collider collider in colliders) collider.enabled = false;
                    }
                    break;
                case CollisionTest.Drop:
                    Invoke("Test", dropTime);
                    break;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Static"))
        {
            switch (collisionTest)
            {
                case CollisionTest.Drop:
                    CancelInvoke("Drop");
                    break;
            }
        }
    }

    private void OnDestroy()
    {
        if (ActiveHand != null) ActiveHand.Drop();
    }

    private IEnumerator DestoryMe()
    {
        yield return new WaitForSeconds(despawnTimeWhenOnGround);
        Destroy(gameObject);
    }

    public PickupType GetPickupType()
    {
        return pickupType;
    }

    private void Test()
    {
        ActiveHand?.Drop();
    }
}