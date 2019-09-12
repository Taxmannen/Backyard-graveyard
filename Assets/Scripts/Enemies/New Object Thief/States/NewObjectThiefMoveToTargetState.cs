using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThiefMoveToTargetState : NewObjectThiefState
{
    private float timeBeforeTryingNewTarget;

    //Change this to trigger-area later?
    private float distanceToTargetBeforeStateChange;

    private Vector3 marionetteStringPosition;
    private Vector3 directionToTarget;

    public override void Enter(NewObjectThief objectThief)
    {
        timeBeforeTryingNewTarget = objectThief.GetTimeBeforeTryingNewTarget();

        if (objectThief.debugStates)
        {
            Debug.Log("Entered Move to Target State");
        }

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

        timeBeforeTryingNewTarget -= t;
        
        if (timeBeforeTryingNewTarget < 0)
        {
            Debug.Log(objectThief.currentTargetObject.tag);
            if (objectThief.currentTargetObject.tag == "RandomTargetObject" && objectThief.objectInHand == null)
            {
                return new NewObjectThiefSearchState();
            }

            if (objectThief.currentTargetObject.tag == "Interactable")
            {
                //objectThief.currentTargetObject = null;
                return new NewObjectThiefSearchState();
            }

            if (objectThief.currentTargetObject.tag == "OutOfBounds" && objectThief.objectInHand != null)
            {
                Debug.Log("Jumped");
                objectThief.Jump(2000);
                return new NewObjectThiefMoveToTargetState();
            }
        }

        if (distanceToTarget < distanceToTargetBeforeStateChange)
        {
            //byt denna till själva player-areat
            if(objectThief.currentTargetObject.tag == "RandomTargetObject")
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
