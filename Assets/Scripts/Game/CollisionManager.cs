using UnityEngine;

/* Script Made By Daniel & Petter */
public class CollisionManager : Singleton<CollisionManager>
{
    [SerializeField] private bool collisionTest = false;
    [SerializeField] private Collider[] staticColliders;

    private void Start()
    {
        GameObject[] staticObjects = GameObject.FindGameObjectsWithTag("Static");
        staticColliders = new Collider[staticObjects.Length];
        for (int i = 0; i < staticObjects.Length; i++)
        {
            staticColliders[i] = staticObjects[i].GetComponent<Collider>();
        }
    }

    public void SetColliderState(Collider[] colliders, bool state)
    {
        foreach (Collider collider in staticColliders)
        {
            foreach (Collider pickupCollider in colliders) Physics.IgnoreCollision(collider, pickupCollider, state);
        }
    }

    public bool GetCollisionTest() { return collisionTest; }
}