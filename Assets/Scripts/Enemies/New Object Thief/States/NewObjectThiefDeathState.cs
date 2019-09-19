using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectThiefDeathState : NewObjectThiefState
{
    public override void Enter(NewObjectThief objectThief)
    {
        objectThief.DropItem();
    }

    public override void Exit(NewObjectThief objectThief)
    {

    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        return null;
    }
}
