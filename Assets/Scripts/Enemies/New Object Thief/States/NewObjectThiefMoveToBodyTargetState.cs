using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectThiefMoveToBodyTargetState : NewObjectThiefState
{
    private float timeBeforeTryingNewTarget;

    //Change this to trigger-area later?
    private float distanceToTargetBeforeStateChange;

    private Vector3 marionetteStringPosition;
    private Vector3 directionToTarget;

    private Body body;

    public override void Enter(NewObjectThief objectThief)
    {
        
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
