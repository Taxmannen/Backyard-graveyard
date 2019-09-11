using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public abstract class OldObjectThiefState
{
    public abstract void Enter(OldObjectThief objectThief);
    public abstract void Exit(OldObjectThief objectThief);

    public abstract OldObjectThiefState Update(OldObjectThief objectThief, float t);
    public abstract OldObjectThiefState FixedUpdate(OldObjectThief objectThief, float t);

}
