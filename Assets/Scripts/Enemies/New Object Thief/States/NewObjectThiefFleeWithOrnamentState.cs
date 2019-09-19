using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectThiefFleeWithOrnamentState : NewObjectThiefFleeState
{

    public override void Enter(NewObjectThief objectThief)
    {
        base.Enter(objectThief);

        if (objectThief.debugStates)
        {
            Debug.Log(".. With Ornament");
        }
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
        //TakeOrnamentWhenPlacedOnPlacement(objectThief);
        return null;
    }

    //private void TakeOrnamentWhenPlacedOnPlacement(NewObjectThief objectThief)
    //{
    //    //Check för om kroppen placeras i graven när zombien har tagit tag i den (Bryt ut och gör så detta enbart görs när den stoppas in istället för att kolla hela tiden.)
    //    objectThief.pickupHand.DetachOrnamentFromPlacement();
    //}
}
