using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectThiefPickupState : NewObjectThiefState
{
    private Vector3 forceToMoveAgainst;

    //Should be changed
    private float distanceBeforeStateChange = 0.5f;

    public override void Enter(NewObjectThief objectThief)
    {

    }

    public override void Exit(NewObjectThief objectThief)
    {
        
    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        forceToMoveAgainst = objectThief.GetDirectionToTarget(objectThief.GetTargetPosition(), objectThief.GetArmPosition());
        objectThief.MoveArm(forceToMoveAgainst);

        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        float distanceToTarget = Vector3.Distance(objectThief.GetArmPosition(), objectThief.GetTargetPosition());

        if (distanceToTarget < distanceBeforeStateChange)
        {
            //Sätt på hand och sätt objektet som hålls på zombien
            objectThief.pickupHand.AddSpringJoint(objectThief.currentTargetObject.GetComponent<Rigidbody>());
            objectThief.objectInHand = objectThief.currentTargetObject;

            objectThief.FindNewCurrentGameObjectWithTag("OutOfBounds");


            return new NewObjectThiefMoveToTargetState();
        }

        //if(object is dropped)
        //set new target


        //När spelaren kommer in i play-area.
        return null;
    }


}
