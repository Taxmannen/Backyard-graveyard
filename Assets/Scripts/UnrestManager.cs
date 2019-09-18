using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnrestManager : Singleton<UnrestManager>
{
    //public delegate void UnrestChanged();
    //public static event UnrestChanged OnUnrestChange; //Level Manager should subscribe to this event!

    public static event Action<int> OnUnrestChange;

    [SerializeField] private int startingUnrest;
    private int minUnrest = 1;
    private int maxUnrest = 20;
    public int CurrentUnrest { get; private set; }

    private void Awake()
    {
        SetInstance(this);
        CurrentUnrest = startingUnrest;
    }

    //For other classes to call when unrest is updated
    public void UpdateUnrest(int plusOrMinusValue)
    {
        CurrentUnrest += plusOrMinusValue;
        CurrentUnrest = Mathf.Clamp(CurrentUnrest, minUnrest, maxUnrest);
        OnUnrestChange?.Invoke(CurrentUnrest);
        Debug.Log("Current Unrest(Function): " + CurrentUnrest);
    }
}
