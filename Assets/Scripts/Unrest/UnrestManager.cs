﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnrestManager : Singleton<UnrestManager>
{
    //public delegate void UnrestChanged();
    //public static event UnrestChanged OnUnrestChange; //Level Manager should subscribe to this event!

    public static event Action<int> OnUnrestChange;

    [SerializeField] private int startingUnrest = 10;
    [SerializeField] private int minUnrest = 0;
    [SerializeField] private int maxUnrest = 20;

    [Header("Debug")]
    [SerializeField] private bool debugMode;

    public int CurrentUnrest { get; private set; }
    public int MaxUnrest { get => maxUnrest; private set => maxUnrest = value; }

    private void Awake()
    {
        SetInstance(this);
        CurrentUnrest = startingUnrest;
    }

    //For other classes to call when unrest is updated
    public void UpdateUnrest(int plusOrMinusValue)
    {
        if (PlayButton.isPlaying)
        {
            CurrentUnrest += plusOrMinusValue;
            CurrentUnrest = Mathf.Clamp(CurrentUnrest, minUnrest, MaxUnrest);
            OnUnrestChange?.Invoke(CurrentUnrest);
            if (debugMode) Debug.Log(CurrentUnrest);
        }
    }
}
