using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

//Kopierar just nu sjukt mycket från MoveToTargetState. Kan säkert bryta ut en del grejer
public class NewObjectThiefSearchState : NewObjectThiefState
{

    private GameObject lastTargetObject;

    private float timeBeforeNewSearch;
    private float distanceToTargetBeforeNewSearch;

    public override void Enter(NewObjectThief objectThief)
    {
        if(objectThief.debugStates)
        {
            Debug.Log("Entered Search State");
        }

        //Sets the last target before changing the current target
        if(objectThief.currentTargetObject.tag != "RandomTargetObject")
        {
            lastTargetObject = objectThief.currentTargetObject;
        }
        else if(objectThief.objectInHand != null)
        {
            lastTargetObject = objectThief.objectInHand;
            objectThief.objectInHand = null;
            //objectThief.currentTargetObject = null;
        }
        else
        {
            Debug.Log("Didn't add any last target");
        }
        

        objectThief.randomTargetArea.SetTargetPositionToPlayArea();
        objectThief.currentTargetObject = objectThief.randomTargetObject;

        distanceToTargetBeforeNewSearch = objectThief.GetDistanceBeforeTargetIsReached();
        timeBeforeNewSearch = objectThief.GetTimeBeforeTryingNewTarget();
    }



    public override void Exit(NewObjectThief objectThief)
    {

    }



    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        //SetDirectionToTarget(objectThief);
        objectThief.MoveToTargetDuringSearch();

        return null;
    }

    //private void SetDirectionToTarget(NewObjectThief objectThief)
    //{
    //    marionetteStringPosition = objectThief.GetMarionetteStringPosition();
    //    directionToTarget = objectThief.GetDirectionToTarget(marionetteStringPosition);
    //}

    //private void MoveTowardsTarget(NewObjectThief objectThief)
    //{
    //    objectThief.MoveToTargetDuringSearch(directionToTarget);
    //    objectThief.enemyJump.TryJump();
    //}



    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        //Realtarget kollar om objectSearcher har hittat ett korrekt target
        //ReferenceEquals() Används för att kolla så att förra targetet som försökte plockas upp inte är detsamma som det nya som har hittats.
        if(objectThief.objectSearcher.realTarget != null 
            && !ReferenceEquals(objectThief.objectSearcher.realTarget.gameObject, lastTargetObject))
        {
            return SetTargetToObjectFound(objectThief);
        }

        else
        {
            //When the enemy gets close enough to a random point on the map, a new random point will be set.
            float distanceToTarget = objectThief.GetDistanceToTarget(objectThief.GetMarionetteStringPosition());
            if (distanceToTarget < distanceToTargetBeforeNewSearch)
            {
                SetTargetToRandomPositionAndResetSearchTimer(objectThief);
            }

            //Skippa denna nu när det gäller spöken? Kommer ändå bara gå igenom allt
            timeBeforeNewSearch -= t;
            if (timeBeforeNewSearch <= 0)
            {
                SetTargetToRandomPositionAndResetSearchTimer(objectThief);
            }
        }

        return null;
    }

    private NewObjectThiefState SetTargetToObjectFound(NewObjectThief objectThief)
    {
        objectThief.currentTargetObject = objectThief.objectSearcher.realTarget.gameObject;
        return new NewObjectThiefMoveToTargetState();
        
    }

    private void SetTargetToRandomPositionAndResetSearchTimer(NewObjectThief objectThief)
    {
        objectThief.randomTargetArea.SetTargetPositionToPlayArea();
        timeBeforeNewSearch = objectThief.GetTimeBeforeTryingNewTarget();
    }

}
