using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectThiefMoveToTargetState : NewObjectThiefState
{
    private Vector3 forceToMoveAgainst;

    //Change this to trigger-area later?
    private float distanceBeforeStateChange = 3;

    public override void Enter(NewObjectThief objectThief)
    {
        
    }

    public override void Exit(NewObjectThief objectThief)
    {

    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        forceToMoveAgainst = objectThief.GetDirectionToTarget(objectThief.GetTargetPosition(), objectThief.GetStringPosition());

        objectThief.Move(forceToMoveAgainst);

        objectThief.enemyJump.TryJump();

        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        
        float distanceToTarget = Vector3.Distance(objectThief.GetStringPosition(), objectThief.GetTargetPosition());

        if (distanceToTarget < distanceBeforeStateChange)
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
                //return new NewObjectThiefDespawnState();
            }

        }

        //if(object is dropped)
        //set new target


        //När spelaren kommer in i play-area.
        return null;
    }

    

}
