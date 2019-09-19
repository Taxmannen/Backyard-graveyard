using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThiefFleeWithBodyState : NewObjectThiefFleeState
{
    private Body body;

    public override void Enter(NewObjectThief objectThief)
    {
        base.Enter(objectThief);

        if (objectThief.debugStates)
        {
            Debug.Log(".. With Body");
        }

        body = objectThief.objectInHand.GetComponent<Body>();
    }

    public override void Exit(NewObjectThief objectThief)
    {

    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        base.FixedUpdate(objectThief, t);
        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        base.Update(objectThief, t);

        return DropBodyWhenPlacedInGrave(objectThief);
    }

    private NewObjectThiefState DropBodyWhenPlacedInGrave(NewObjectThief objectThief)
    {
        //Check för om kroppen placeras i graven när zombien har tagit tag i den (Bryt ut och gör så detta enbart görs när den stoppas in istället för att kolla hela tiden.)
        if (objectThief.objectInHand != null)
        {
            if (body.IsInGrave)
            {
                objectThief.enemyJump.FleeJump();
                objectThief.pickupHand.DestroyJoint();
                return new NewObjectThiefSearchState();
            }
        }
        return null;
    }
}
