using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

//Kopierar just nu sjukt mycket från MoveToTargetState. Kan säkert bryta ut en del grejer
public class NewObjectThiefSearchState : NewObjectThiefState
{
    private Vector3 marionetteStringPosition;
    private Vector3 directionToTarget;

    private GameObject lastObject;

    private float timeBeforeNewSearch;
    private float distanceToTargetBeforeNewSearch;

    public override void Enter(NewObjectThief objectThief)
    {
        if(objectThief.debugStates)
        {
            Debug.Log("Entered Search State");
        }

        lastObject = objectThief.currentTargetObject;

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
        marionetteStringPosition = objectThief.GetMarionetteStringPosition();

        directionToTarget = objectThief.GetDirectionToTarget(marionetteStringPosition);
        objectThief.MoveSearch(directionToTarget);
        objectThief.enemyJump.TryJump();

        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {

        //ReferenceEquals() Används för att kolla så att förra targetet som försökte plockas upp inte är detsamma som det nya som har hittats.
        if(objectThief.objectSearcher.realTarget != null && !ReferenceEquals(objectThief.objectSearcher.realTarget.gameObject, lastObject))
        {
            //Sets the current target to the object that's been found by the object-searcher
            objectThief.currentTargetObject = objectThief.objectSearcher.realTarget.gameObject;
            return new NewObjectThiefMoveToTargetState();
        }

        else
        {
            //When the enemy gets close enough to a random point on the map, a new random point will be set.
            float distanceToTarget = objectThief.GetDistanceToTarget(marionetteStringPosition);
            if (distanceToTarget < distanceToTargetBeforeNewSearch)
            {
                objectThief.randomTargetArea.SetTargetPositionToPlayArea();
            }

            timeBeforeNewSearch -= t;
            if (timeBeforeNewSearch <= 0)
            {
                objectThief.randomTargetArea.SetTargetPositionToPlayArea();
                timeBeforeNewSearch = objectThief.GetTimeBeforeTryingNewTarget();
            }
        }

        return null;
    }

}
