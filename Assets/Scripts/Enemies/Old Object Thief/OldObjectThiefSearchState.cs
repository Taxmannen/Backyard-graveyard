﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public class OldObjectThiefSearchState : OldObjectThiefState
{

    private float timer;

    //Tempstuff just to get the zombie moving towards the player even without a target.
    private GameObject distanceCheckForDespawnObject;


    public override void Enter(OldObjectThief objectThief)
    {
        timer = objectThief.GetTimeBetweenEachSearch();
        FindClosestObject(objectThief);

        //Tempstuff just to get the zombie moving towards the player even without a target.
        distanceCheckForDespawnObject = GameObject.FindGameObjectWithTag("DistanceCheckForObjectThief");
    }

    public override void Exit(OldObjectThief objectThief)
    {

    }

    public override OldObjectThiefState FixedUpdate(OldObjectThief objectThief, float t)
    {
        Debug.Log(distanceCheckForDespawnObject.transform.position);
        //Tempstuff just to get the zombie moving towards the player even without a target.
        Vector2 directionToTarget = new Vector2((distanceCheckForDespawnObject.transform.position.x - objectThief.transform.position.x), (distanceCheckForDespawnObject.transform.position.z - objectThief.transform.position.z));
        directionToTarget = directionToTarget.normalized;
        objectThief.Move(directionToTarget);

        return null;
    }

    public override OldObjectThiefState Update(OldObjectThief objectThief, float t)
    {


        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = objectThief.GetTimeBetweenEachSearch();
            FindClosestObject(objectThief);
        }

        if(objectThief.GetCurrentTargetToSteal() != null)
        {
            return new OldObjectThiefHuntState();
        }

        return null;
    }

    private void FindClosestObject(OldObjectThief objectThief)
    {
        OldEnemyStealTarget[] stealTargetList;
        stealTargetList = objectThief.FindAllEnemyStealTargets();

        if (stealTargetList.Length != 0)
        {
            float distance = Mathf.Infinity;
            Vector3 position = objectThief.transform.position;
            foreach (OldEnemyStealTarget stealTarget in stealTargetList)
            {
                //Lägg till att kolla så den inte plockats upp av spelaren också!
                if (objectThief.GetNameOfObjectToSteal() == stealTarget.getObjectName() && stealTarget.pickedUpByEnemy == false)
                {
                    Vector3 diff = stealTarget.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        if(!stealTarget.pickedUpByEnemy)
                        {
                            if(stealTarget.GetComponent<Interactable>().ActiveHand == null)
                            {
                                objectThief.SetNewTargetToSteal(stealTarget);
                            }
                        }
                        distance = curDistance;
                    }
                }

            }
        }
    }

    public float GetDistanceToDespawnCheckObject(OldObjectThief objectThief)
    {
        return Vector3.Distance(objectThief.transform.position, distanceCheckForDespawnObject.transform.position);
    }
}