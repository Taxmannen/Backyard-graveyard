using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField, Tooltip("The amount in pool")] private int amount;
    [SerializeField, Tooltip("Add more to the pool when empty")] private bool scalable;
    [SerializeField, Tooltip("Reuses first spawned object when empty")] private bool reusable;

    private Queue<GameObject> objects = new Queue<GameObject>();
    private Queue<GameObject> usedObjects = new Queue<GameObject>();

    protected void Setup()
    {
        AddObjects(amount);
    }

    public GameObject Get(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (objects.Count == 0)
        {
            if (scalable) AddObjects(1);
            else if (reusable) UseOldest();
        }

        if (objects.Count == 0) return null;
        else
        {
            GameObject currentObject = objects.Dequeue();
            if (reusable) usedObjects.Enqueue(currentObject);

            currentObject.transform.position = position;
            currentObject.transform.rotation = rotation;
            currentObject.transform.SetParent(parent);
            currentObject.SetActive(true);
            return currentObject;
        }
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newObject = Instantiate(prefab);
            newObject.transform.SetParent(transform);
            newObject.gameObject.SetActive(false);
            objects.Enqueue(newObject);
        }
    }

    private void UseOldest()
    {
        GameObject oldObject = usedObjects.Dequeue();
        objects.Enqueue(oldObject);
        oldObject.SetActive(false);
    }
}