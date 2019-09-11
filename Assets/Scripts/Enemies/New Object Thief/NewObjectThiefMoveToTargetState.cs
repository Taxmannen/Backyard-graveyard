using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThiefMoveToTargetState : NewObjectThiefState
{
    //Change this to trigger-area later?
    private float distanceToTargetBeforeStateChange;

    private Vector3 marionetteStringPosition;
    private Vector3 directionToTarget;

    public override void Enter(NewObjectThief objectThief)
    {
        distanceToTargetBeforeStateChange = objectThief.GetDistanceBeforeTargetIsReached();
    }

    public override void Exit(NewObjectThief objectThief)
    {

    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        marionetteStringPosition = objectThief.GetMarionetteStringPosition();

        //Gör om getdirectionToTarget till ett script för varje del så man inte behöver hoppa fram/tillbaks såhär
        directionToTarget = objectThief.GetDirectionToTarget(marionetteStringPosition);
        objectThief.Move(directionToTarget);
        objectThief.enemyJump.TryJump();
        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        float distanceToTarget = objectThief.GetDistanceToTarget(marionetteStringPosition);

        if (distanceToTarget < distanceToTargetBeforeStateChange)
        {
            //byt denna till själva player-areat
            if(objectThief.currentTargetObject.tag == "DistanceCheckForObjectThief")
            {
                return new NewObjectThiefSearchState();
            }

            if (objectThief.currentTargetObject.tag == "Interactable")
            {
                return new NewObjectThiefPickupState();
            }

            //Bör ändras
            if(objectThief.currentTargetObject.tag == "OutOfBounds")
            {
                objectThief.Despawn();
            }
        }

        return null;
    }

    

}
