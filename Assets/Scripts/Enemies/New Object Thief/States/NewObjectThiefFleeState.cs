using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThiefFleeState : NewObjectThiefState
{

    private float timeBeforeJump;

    //Change this to trigger-area later?
    private float distanceBeforeEnemyDespawn;

    public override void Enter(NewObjectThief objectThief)
    {
        //Fulfix där fienden rör sig mot ett objekt med tagen "Out of Bounds". Används för att despawna fienden.
        //objectThief.FindNewCurrentTargetObjectWithTag("Player");
        objectThief.currentTargetObject = objectThief.randomTargetObject;

        timeBeforeJump = objectThief.GetTimeBeforeTryingNewTarget();
        distanceBeforeEnemyDespawn = objectThief.GetDistanceBeforeEnemyDespawn();

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
        //SetDirectionToTarget(objectThief);
        //MoveAwayFromTarget(objectThief);
        objectThief.MoveAwayFromTarget();
        return null;
    }

    private void SetDirectionToTarget(NewObjectThief objectThief)
    {
        //marionetteStringPosition = objectThief.GetMarionetteStringPosition();
        //directionAwayFromTarget = -objectThief.GetDirectionToTarget(marionetteStringPosition);
    }

    private void MoveAwayFromTarget(NewObjectThief objectThief)
    {
        //objectThief.MoveToTarget(directionAwayFromTarget);
        //objectThief.enemyJump.TryJump();
    }


    private Rigidbody objectInHandRigidBody;
    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {

        float distanceToTarget = objectThief.GetDistanceToTarget(objectThief.GetMarionetteStringPosition());

        timeBeforeJump -= t;
        if (timeBeforeJump <= 0)
        {
            //Reference to the object in the hand
            if(objectInHandRigidBody == null)
            {
                objectInHandRigidBody = objectThief.objectInHand.GetComponent<Rigidbody>();
            }

            //Jumps, and adds a force to the object in the hand as well.
            objectThief.enemyJump.FleeJump(objectInHandRigidBody);
            timeBeforeJump = objectThief.GetTimeBeforeTryingNewTarget();
        }

        if (distanceToTarget > distanceBeforeEnemyDespawn)
        {
            objectThief.DespawnDuringFlee();
        }

        return null;
    }

}
