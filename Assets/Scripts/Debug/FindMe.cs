using System;
using UnityEngine;

/* Script Made By Daniel */
public class FindMe : MonoBehaviour
{
    [SerializeField] private string scriptName = "Interactable";

    private void Awake()
    {
        Debug.Log("FindMe: " + gameObject.name);
        if (scriptName.Length > 0)
        {
            Type type = Type.GetType(scriptName);
            UnityEngine.Object[] objects = FindObjectsOfType(type);
            for (int i = 0; i < objects.Length; i++) Debug.Log(objects[i].name);
        }
    }
}
