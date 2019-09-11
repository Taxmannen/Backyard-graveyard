using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public abstract class NewObjectThiefState
{
    public abstract void Enter(NewObjectThief objectThief);
    public abstract void Exit(NewObjectThief objectThief);

    public abstract NewObjectThiefState Update(NewObjectThief objectThief, float t);
    public abstract NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t);
}
