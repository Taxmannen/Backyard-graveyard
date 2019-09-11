using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldObjectThiefDespawnState : OldObjectThiefState
{

    public override void Enter(OldObjectThief objectThief)
    {
        objectThief.DestroyStolenTarget();
        objectThief.DestroyObjectThief();
    }

    public override void Exit(OldObjectThief objectThief)
    {

    }

    public override OldObjectThiefState FixedUpdate(OldObjectThief objectThief, float t)
    {
        return null;
    }

    public override OldObjectThiefState Update(OldObjectThief objectThief, float t)
    {
        return null;
    }

}
