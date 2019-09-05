﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
    public static int GetRandomNumber(int min, int max) {
        int number = Random.Range(min, max);
        Debug.Log("Finding random number between " + min + " and " + max + " resulting in "+ number);
        return number;
    }
}
