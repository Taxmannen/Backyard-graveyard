using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <author>Simon</author>
/// </summary>
public class RandomManager : Singleton<RandomManager>
{
    private void Awake() {
        SetInstance(this);
    }

    public static int GetRandomNumber(int min, int max) {
        int number = Random.Range(min, max);
        return number;
    }

    public static float GetRandomNumber(float min, float max) {
        float number = Random.Range(min, max);
        Debug.Log("Finding random number between " + min + " and " + max + " resulting in " + number);
        return number;
    }
}
