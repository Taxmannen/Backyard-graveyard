using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel & Petter */
public class CollisionManager : Singleton<CollisionManager>
{
    [SerializeField] private bool collisionTest = false;
    [SerializeField] private Collider[] staticColliders;

    private List<Collider> allColliders = new List<Collider>();

    private void Awake()
    {
        SetInstance(this);
    }

    private void Start()
    {
        allColliders.AddRange(staticColliders);
        allColliders.AddRange(GetColliders("Static"));
        allColliders.AddRange(GetColliders("Ground"));
        allColliders.AddRange(GetColliders("Button Trigger"));
        allColliders.AddRange(GetColliders("Button Limiter"));
    }

    public void SetColliderState(Collider[] colliders, bool state)
    {
        foreach (Collider collider in allColliders)
        {
            foreach (Collider pickupCollider in colliders)
            {
                if (pickupCollider != null && collider != null) Physics.IgnoreCollision(collider, pickupCollider, state);
            }
        }
    }

    private Collider[] GetColliders(string tag)
    {
        GameObject[] staticObjects = GameObject.FindGameObjectsWithTag(tag);
        Collider[] colliders = new Collider[staticObjects.Length];
        for (int i = 0; i < staticObjects.Length; i++) colliders[i] = staticObjects[i].GetComponent<Collider>();
        return colliders;
    }

    public void AddToColliderList(Collider col) { allColliders.Add(col); }

    public void RemoveToColliderList(Collider col) { allColliders.Remove(col); }

    public bool GetCollisionTest() { return collisionTest; }
}