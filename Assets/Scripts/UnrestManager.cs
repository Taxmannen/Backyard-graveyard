using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnrestManager : Singleton<UnrestManager>
{
    //public delegate void UnrestChanged();
    //public static event UnrestChanged OnUnrestChange; //Level Manager should subscribe to this event!

    public static event Action<int> OnUnrestChange;

    private int minUnrest = 1;
    private int maxUnrest = 20;
    public int CurrentUnrest { get; private set; }

    private void Awake()
    {
        SetInstance(this);
        Mathf.Clamp(CurrentUnrest, minUnrest, maxUnrest);
    }

    //For other classes to call when unrest is updated
    public void UpdateUnrest(int plusOrMinusValue)
    {
        CurrentUnrest += plusOrMinusValue;
        OnUnrestChange?.Invoke(CurrentUnrest);
        Debug.Log(CurrentUnrest);
    }
}
