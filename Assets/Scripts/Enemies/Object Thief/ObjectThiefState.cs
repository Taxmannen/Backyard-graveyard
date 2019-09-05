using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public abstract class ObjectThiefState
{
    public abstract void Enter(ObjectThief objectThief);
    public abstract void Exit(ObjectThief objectThief);

    public abstract ObjectThiefState Update(ObjectThief objectThief, float t);
    public abstract ObjectThiefState FixedUpdate(ObjectThief objectThief, float t);

}
