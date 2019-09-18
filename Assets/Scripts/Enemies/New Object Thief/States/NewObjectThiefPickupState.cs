using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThiefPickupState : NewObjectThiefState
{
    private float timeBeforeTryingNewTarget;


    //Should be changed
    private float distanceToObjectBeforeStateChange;

    private Vector3 armPosition;
    private Vector3 directionToTarget;


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
        armPosition = objectThief.GetArmPosition();

        directionToTarget = objectThief.GetDirectionToTarget(armPosition);
        objectThief.MoveArm(directionToTarget);

        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        timeBeforeTryingNewTarget -= t;
        if (timeBeforeTryingNewTarget < 0)
        {
            return new NewObjectThiefSearchState();
        }


        float distanceToTarget = objectThief.GetDistanceToTarget(armPosition);

        if (distanceToTarget < distanceToObjectBeforeStateChange)
        {
            //Sätt på hand och sätt objektet som hålls av zombien
            objectThief.pickupHand.PickupObject(objectThief.currentTargetObject.GetComponent<Rigidbody>());
            objectThief.objectInHand = objectThief.currentTargetObject;

            //Fulfix där fienden rör sig mot ett objekt med tagen "Out of Bounds". Används för att despawna fienden.
            objectThief.FindNewCurrentGameObjectWithTag("OutOfBounds");

            return new NewObjectThiefMoveToTargetState();
        }

        return null;
    }


}
