using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThiefPickupState : NewObjectThiefState
{
    private float timeBeforeTryingNewTarget;


    //Should be changed
    private float distanceToObjectBeforeStateChange;


    public override void Enter(NewObjectThief objectThief)
    {
        timeBeforeTryingNewTarget = objectThief.GetTimeBeforeTryingNewPickupTarget();

        if (objectThief.debugStates)
        {
            Debug.Log("Entered Pickup State");
        }

        distanceToObjectBeforeStateChange = objectThief.GetDistanceBeforeTargetIsPickedUp();
    }

    public override void Exit(NewObjectThief objectThief)
    {

    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        //SetDirectionToTarget(objectThief);
        objectThief.MoveArm();
        return null;
    }

    //private void SetDirectionToTarget(NewObjectThief objectThief)
    //{
    //    armPosition = objectThief.GetArmPosition();
    //    directionToTarget = objectThief.GetDirectionToTarget(armPosition);
    //}

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        timeBeforeTryingNewTarget -= t;
        if (timeBeforeTryingNewTarget < 0)
        {
            return new NewObjectThiefSearchState();
        }

        float distanceToTarget = objectThief.GetDistanceToTarget(objectThief.GetArmPosition());
        if (distanceToTarget < distanceToObjectBeforeStateChange)
        {
            PickupObject(objectThief);
            return GoToCorrectFleeState(objectThief);
        }

        return null;
    }

    private void PickupObject(NewObjectThief objectThief)
    {
        //Pickup object
        objectThief.pickupHand.PickupObject(objectThief.currentTargetObject.GetComponent<Rigidbody>());

        //flytta denna till scriptet PickupObject() i pickupHand? (Sätter objektet i handen till objektet som togs upp)
        objectThief.objectInHand = objectThief.currentTargetObject;
    }

    private NewObjectThiefState GoToCorrectFleeState(NewObjectThief objectThief)
    {
        if (objectThief.objectSearcher.GetTargetType() == PickupType.Body)
        {
            return new NewObjectThiefFleeWithBodyState();
        }
        else if(objectThief.objectSearcher.GetTargetType() == PickupType.Ornament)
        {
            return new NewObjectThiefFleeWithOrnamentState();
        }
        else
        {
            return new NewObjectThiefFleeState();
        }
    }


}
