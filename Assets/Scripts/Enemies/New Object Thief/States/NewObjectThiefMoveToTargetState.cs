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

    private Body body;

    public override void Enter(NewObjectThief objectThief)
    {
        timeBeforeTryingNewTarget = objectThief.GetTimeBeforeTryingNewTarget();

        if (objectThief.debugStates)
        {
            Debug.Log("Entered Move to Target State");
        }

        //if(objectThief.currentTargetObject == null)
        //{
        //    objectThief.currentTargetObject = objectThief.randomTargetObject;
        //}

        if (objectThief.currentTargetObject.tag == "OutOfBounds" && objectThief.objectSearcher.GetTargetType() == PickupType.Body)
        {
            body = objectThief.objectInHand.GetComponent<Body>();
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
        
        //if(objectThief.currentTargetObject == null)
        //{
        //    return new NewObjectThiefSearchState();
        //}

        if (timeBeforeTryingNewTarget < 0)
        {
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


        //Check för om kroppen är i graven (Bör tas ut)
        if(objectThief.currentTargetObject.tag == "OutOfBounds" && objectThief.objectSearcher.GetTargetType() == PickupType.Body)
        { 
            if(objectThief.objectInHand != null)
            {
                if(objectThief.objectInHand.tag == "Interactable")
                {
                    if (body.IsInGrave)
                    {
                        objectThief.Jump(2000);
                        objectThief.pickupHand.DestroyJoint();
                        return new NewObjectThiefSearchState();
                    }
                }
            }
            
        }

        return null;
    }

    

}
