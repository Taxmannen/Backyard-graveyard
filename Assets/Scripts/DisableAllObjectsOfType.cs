using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>Simon</author>
/// </summary>
public struct SavedObject
{
    public string tag { get; private set; }
    public MonoBehaviour _object { get; private set; }

    public SavedObject(string tag, MonoBehaviour _object) 
    {
        this.tag = tag;
        this._object = _object;
    }
}

public static class DisableAllObjectsOfType
{
    private static Dictionary<System.Type, List<SavedObject>> typeToObjectList = new Dictionary<System.Type, List<SavedObject>>();

    public static void DisableAllObjects<T>() where T : MonoBehaviour 
    {
        if (typeToObjectList.ContainsKey(typeof(T))) return;

        typeToObjectList[typeof(T)] = new List<SavedObject>();

        T[] objects = GameObject.FindObjectsOfType<T>();
        for (int i = 0; i < objects.Length; i++) 
        {
            T _object = objects[i];
            typeToObjectList[typeof(T)].Add(new SavedObject(_object.tag, _object));
            _object.enabled = false;
            _object.tag = "Untagged";
        }
    }

    public static void EnableAllDisabledObjects<T>() where T : MonoBehaviour 
    {
        Debug.Log("Attempting to enable objects of type " + typeof(T).ToString());

        if (!typeToObjectList.ContainsKey(typeof(T))) throw new System.Exception("Object of type" + typeof(T).ToString() + "not found");

        foreach (SavedObject savedObject in typeToObjectList[typeof(T)]) 
        {
            savedObject._object.enabled = true;
            savedObject._object.tag = savedObject.tag;
        }

        typeToObjectList.Remove(typeof(T));
    }
}
