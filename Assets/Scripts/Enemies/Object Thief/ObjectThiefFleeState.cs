using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public class ObjectThiefFleeState : ObjectThiefState
{
    private Vector2 directionToRun;

    public override void Enter(ObjectThief objectThief)
    {
        directionToRun = Random.insideUnitCircle.normalized;
    }

    public override void Exit(ObjectThief objectThief)
    {

    }

    public override ObjectThiefState FixedUpdate(ObjectThief objectThief, float t)
    {
        objectThief.Move(directionToRun);

        return null;
    }

    public override ObjectThiefState Update(ObjectThief objectThief, float t)
    {
        //Debug.Log(objectThief.GetDistanceToDespawnCheckObject());
        if (objectThief.GetDistanceToDespawnCheckObject() >= objectThief.GetDistanceToDespawn())
        {
            return new ObjectThiefDespawnState();
        }

        return null;
    }

    private void SetNewRandomDirection()
    {
        directionToRun = Random.insideUnitCircle.normalized;
    }
}
