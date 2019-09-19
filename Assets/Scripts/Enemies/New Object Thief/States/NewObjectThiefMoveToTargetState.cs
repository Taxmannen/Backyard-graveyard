using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThiefMoveToTargetState : NewObjectThiefState
{
    private float timeBeforeTryingNewTarget;

    //Change this to trigger-area later?
    private float distanceToTargetBeforeStateChange;

    public override void Enter(NewObjectThief objectThief)
    {
        timeBeforeTryingNewTarget = objectThief.GetTimeBeforeTryingNewTarget();
        distanceToTargetBeforeStateChange = objectThief.GetDistanceBeforeTargetIsReached();

        if (objectThief.debugStates)
        {
            Debug.Log("Entered Move to Target State");
        }
    }



    public override void Exit(NewObjectThief objectThief)
    {

    }



    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        //SetDirectionToTarget(objectThief);
        objectThief.MoveToTarget();
        return null;
    }

    //private void SetDirectionToTarget(NewObjectThief objectThief)
    //{
    //    marionetteStringPosition = objectThief.GetMarionetteStringPosition();
    //    directionToTarget = objectThief.GetDirectionToTarget(marionetteStringPosition);
    //}

    //private void MoveTowardsTarget(NewObjectThief objectThief)
    //{
    //    objectThief.MoveToTarget(directionToTarget);
    //    objectThief.enemyJump.TryJump();
    //}



    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        float distanceToTarget = objectThief.GetDistanceToTarget(objectThief.GetMarionetteStringPosition());

        timeBeforeTryingNewTarget -= t;

        if (timeBeforeTryingNewTarget < 0)
        {
            return new NewObjectThiefSearchState();
        }

        if (distanceToTarget < distanceToTargetBeforeStateChange)
        {
            if (objectThief.currentTargetObject.tag == "Interactable")
            {
                return new NewObjectThiefPickupState();
            }

            if (objectThief.currentTargetObject.tag == "RandomTargetObject")
            {
                return new NewObjectThiefSearchState();
            }
        }

        return null;
    }

    

}
