using System.Collections.Generic;
using UnityEngine;

/* Script Made By Daniel */
public abstract class ObjectPool : Singleton<ObjectPool>
{
    #region Variables
    [SerializeField] private GameObject prefab;
    [SerializeField, Tooltip("The amount in pool")] private int amount;
    [SerializeField, Tooltip("Add more to the pool when empty")] private bool scalable;
    [SerializeField, Tooltip("Reuses first spawned object when empty")] private bool reusable;

    [Header("Debug")]
    [SerializeField, ReadOnly] private List<GameObject> usedObjects = new List<GameObject>();

    private Queue<GameObject> objects = new Queue<GameObject>();
    private Vector3 scale;
    #endregion

    private void Awake()
    {
        scale = prefab.transform.localScale;
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
            if (reusable) usedObjects.Add(currentObject);
            currentObject.transform.position = position;
            currentObject.transform.rotation = rotation;
            SetObjectParent(currentObject.transform, parent);
            currentObject.SetActive(true);
            return currentObject;
        }
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        if (reusable) usedObjects.Remove(objectToReturn);
        objectToReturn.SetActive(false);
        objectToReturn.transform.SetParent(transform);
        objects.Enqueue(objectToReturn);
    }

    private void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.transform.SetParent(transform);
            newObject.SetActive(false);
            objects.Enqueue(newObject);
        }
    }

    private void UseOldest()
    {
        GameObject oldObject = usedObjects[0];
        usedObjects.RemoveAt(0);
        objects.Enqueue(oldObject);
        oldObject.SetActive(false);
    }

    private void SetObjectParent(Transform objectTransform, Transform parent)
    {
        objectTransform.SetParent(null);
        objectTransform.localScale = scale;
        objectTransform.SetParent(parent);
    }
}