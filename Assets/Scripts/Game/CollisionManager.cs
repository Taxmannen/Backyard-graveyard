using UnityEngine;

/* Script Made By Daniel & Petter */
public class CollisionManager : Singleton<CollisionManager>
{
    [SerializeField] private bool collisionTest = false;
    [SerializeField] private Collider[] staticColliders;

    public void SetColliderState(Collider[] colliders, bool state)
    {
        foreach (Collider collider in staticColliders)
        {
            foreach (Collider pickupCollider in colliders) Physics.IgnoreCollision(collider, pickupCollider, state);
        }
    }

    public bool GetCollisionTest() { return collisionTest; }
}