using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThiefFleeState : NewObjectThiefState
{

    private float timeBeforeJump;

    //Change this to trigger-area later?
    private float distanceToTargetBeforeStateChange;

    private Vector3 marionetteStringPosition;
    private Vector3 directionToTarget;

    public override void Enter(NewObjectThief objectThief)
    {
        timeBeforeJump = objectThief.GetTimeBeforeTryingNewTarget();
        distanceToTargetBeforeStateChange = objectThief.GetDistanceBeforeTargetIsReached();

        if (objectThief.debugStates)
        {
            Debug.Log("Entered Flee-State");
        }
    }

    public override void Exit(NewObjectThief objectThief)
    {

    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        SetDirectionToTarget(objectThief);
        MoveTowardsTarget(objectThief);
        return null;
    }

    private void SetDirectionToTarget(NewObjectThief objectThief)
    {
        marionetteStringPosition = objectThief.GetMarionetteStringPosition();
        directionToTarget = objectThief.GetDirectionToTarget(marionetteStringPosition);
    }

    private void MoveTowardsTarget(NewObjectThief objectThief)
    {
        objectThief.Move(directionToTarget);
        objectThief.enemyJump.TryJump();
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        float distanceToTarget = objectThief.GetDistanceToTarget(marionetteStringPosition);

        timeBeforeJump -= t;
        if (timeBeforeJump <= 0)
        {
            objectThief.Jump(2000);
            timeBeforeJump = objectThief.GetTimeBeforeTryingNewTarget();
        }

        if (distanceToTarget < distanceToTargetBeforeStateChange)
        {
            objectThief.Despawn();
        }

        return null;
    }

}
