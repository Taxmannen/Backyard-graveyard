using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThiefSearchState : NewObjectThiefState
{
    public override void Enter(NewObjectThief objectThief)
    {
        
    }

    public override void Exit(NewObjectThief objectThief)
    {

    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        //Få in funktionalitet för att röra sig omkring inom ett satt område
        //Står just nu bara stilla
        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        if(objectThief.objectSearcher.realTarget != null)
        {
            objectThief.currentTargetObject = objectThief.objectSearcher.realTarget.gameObject;
            return new NewObjectThiefMoveToTargetState();
        }

        return null;
    }

}
