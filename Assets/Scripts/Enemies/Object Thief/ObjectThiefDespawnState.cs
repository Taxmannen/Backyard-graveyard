using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThiefDespawnState : ObjectThiefState
{

    public override void Enter(ObjectThief objectThief)
    {
        objectThief.DestroyStolenTarget();
        objectThief.DestroyObjectThief();
    }

    public override void Exit(ObjectThief objectThief)
    {

    }

    public override ObjectThiefState FixedUpdate(ObjectThief objectThief, float t)
    {
        return null;
    }

    public override ObjectThiefState Update(ObjectThief objectThief, float t)
    {
        return null;
    }

}
