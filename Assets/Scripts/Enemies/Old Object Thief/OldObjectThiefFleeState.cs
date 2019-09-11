using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public class OldObjectThiefFleeState : OldObjectThiefState
{
    private Vector2 directionToRun;
    private GameObject distanceCheckForDespawnObject;

    public override void Enter(OldObjectThief objectThief)
    {
        directionToRun = Random.insideUnitCircle.normalized;
        distanceCheckForDespawnObject = GameObject.FindGameObjectWithTag("DistanceCheckForObjectThief");
    }

    public override void Exit(OldObjectThief objectThief)
    {

    }

    public override OldObjectThiefState FixedUpdate(OldObjectThief objectThief, float t)
    {
        objectThief.Move(directionToRun);

        return null;
    }

    public override OldObjectThiefState Update(OldObjectThief objectThief, float t)
    {
        //Debug.Log(objectThief.GetDistanceToDespawnCheckObject());
        if (GetDistanceToDespawnCheckObject(objectThief) >= objectThief.GetDistanceToDespawn())
        {
            return new OldObjectThiefDespawnState();
        }

        return null;
    }

    private void SetNewRandomDirection()
    {
        directionToRun = Random.insideUnitCircle.normalized;
    }

    public float GetDistanceToDespawnCheckObject(OldObjectThief objectThief)
    {
        return Vector3.Distance(objectThief.transform.position, distanceCheckForDespawnObject.transform.position);
    }
}
