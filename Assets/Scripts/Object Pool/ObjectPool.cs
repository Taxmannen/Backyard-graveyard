using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public abstract class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int amount;
    [SerializeField] private bool scalable;
    [SerializeField] private bool reusable;

    [Header("Debug")]
    [SerializeField] private int amountInPool;

    private Queue<GameObject> objects = new Queue<GameObject>();
    private Queue<GameObject> usedObjects = new Queue<GameObject>();

    protected void Setup()
    {
        AddObjects(amount);
    }

    public GameObject Get()
    {
        if (amountInPool == 0)
        {
            if (scalable) AddObjects(1);
            else if (reusable) UseOldest();
        }

        if (amountInPool == 0) return null;
        else
        {
            amountInPool--;
            GameObject currentObject = objects.Dequeue();
            if (reusable) usedObjects.Enqueue(currentObject);
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
            amountInPool++;
        }
    }

    private void UseOldest()
    {
        GameObject oldObject = usedObjects.Dequeue();
        objects.Enqueue(oldObject);
        oldObject.SetActive(false);
        amountInPool++;
    }
}