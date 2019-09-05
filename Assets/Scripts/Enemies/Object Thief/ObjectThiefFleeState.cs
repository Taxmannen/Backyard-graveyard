using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public class ObjectThiefFleeState : ObjectThiefState
{
    private Vector2 directionToRun;
    private GameObject distanceCheckForDespawnObject;

    public override void Enter(ObjectThief objectThief)
    {
        directionToRun = Random.insideUnitCircle.normalized;
        distanceCheckForDespawnObject = GameObject.FindGameObjectWithTag("DistanceCheckForObjectThief");
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
        if (GetDistanceToDespawnCheckObject(objectThief) >= objectThief.GetDistanceToDespawn())
        {
            return new ObjectThiefDespawnState();
        }

        return null;
    }

    private void SetNewRandomDirection()
    {
        directionToRun = Random.insideUnitCircle.normalized;
    }

    public float GetDistanceToDespawnCheckObject(ObjectThief objectThief)
    {
        return Vector3.Distance(objectThief.transform.position, distanceCheckForDespawnObject.transform.position);
    }
}
