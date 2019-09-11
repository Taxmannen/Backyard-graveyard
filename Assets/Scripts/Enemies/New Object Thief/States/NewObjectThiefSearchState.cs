using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

//Kopierar just nu sjukt mycket från MoveToTargetState. Kan säkert bryta ut en del grejer
public class NewObjectThiefSearchState : NewObjectThiefState
{
    private float distanceToTargetBeforeNewSearch;

    private Vector3 marionetteStringPosition;
    private Vector3 directionToTarget;

    public override void Enter(NewObjectThief objectThief)
    {
        objectThief.randomTargetArea.SetTargetPositionToPlayArea();
        objectThief.currentTargetObject = objectThief.randomTargetObject;

        distanceToTargetBeforeNewSearch = objectThief.GetDistanceBeforeTargetIsReached();
    }

    public override void Exit(NewObjectThief objectThief)
    {

    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        marionetteStringPosition = objectThief.GetMarionetteStringPosition();

        directionToTarget = objectThief.GetDirectionToTarget(marionetteStringPosition);
        objectThief.Move(directionToTarget);
        objectThief.enemyJump.TryJump();

        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {

        if(objectThief.objectSearcher.realTarget != null)
        {
            objectThief.currentTargetObject = objectThief.objectSearcher.realTarget.gameObject;
            return new NewObjectThiefMoveToTargetState();
        }
        else
        {
            float distanceToTarget = objectThief.GetDistanceToTarget(marionetteStringPosition);
            if (distanceToTarget < distanceToTargetBeforeNewSearch)
            {
                objectThief.randomTargetArea.SetTargetPositionToPlayArea();
            }
        }

        return null;
    }

}
