using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Don't forget to set the instance in start!
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance;

    public static T GetInstance() {
        if (instance == null) {
            Debug.LogError("An instance of " + typeof(T) +
                   " is needed but instance is null." +
                   " Did you forget to set the instance in the Start() function?" +
                   " Attempting to find one in the scene anyway...");

            instance = FindObjectOfType<T>();
            if (instance == null) {
                Debug.LogError("An instance of " + typeof(T) +
               " is needed in the scene, but there is none.");

                throw new System.NullReferenceException();
            }
        }

        return instance;
    }

    /// <summary>
    /// Returns true if successful.
    /// If it returns false there is probably a duplicate in the scene.
    /// </summary>
    /// <param name="_object"></param>
    /// <param name="destroyDuplicates"></param>
    /// <returns></returns>
    public static bool SetInstance(T _object, bool destroyDuplicates = true) {
        if (instance == null) {
            instance = _object;
            return true;
        }
        else {
            if (destroyDuplicates) {
                Debug.LogWarning("Trying to assign " + _object.name + " as an instance of " + typeof(T) +
                                 " but one already exists in the scene, destroying instance on " + _object.name + ".");
                Destroy(_object);
            }
            else {
                Debug.LogWarning("Trying to assign " + _object.name + " as an instance of " + typeof(T) +
                 " but one already exists in the scene, returning...");
            }

            return false;
        }
    }
}
