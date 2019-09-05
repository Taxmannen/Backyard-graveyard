using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public class ObjectThiefSearchState : ObjectThiefState
{

    private float timer;


    public override void Enter(ObjectThief objectThief)
    {
        timer = objectThief.GetTimeBetweenEachSearch();
        FindClosestObject(objectThief);
    }

    public override void Exit(ObjectThief objectThief)
    {

    }

    public override ObjectThiefState FixedUpdate(ObjectThief objectThief, float t)
    {
        
        return null;
    }

    public override ObjectThiefState Update(ObjectThief objectThief, float t)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = objectThief.GetTimeBetweenEachSearch();
            FindClosestObject(objectThief);
        }

        if(objectThief.GetCurrentTargetToSteal() != null)
        {
            return new ObjectThiefHuntState();
        }

        return null;
    }

    private void FindClosestObject(ObjectThief objectThief)
    {
        EnemyStealTarget[] stealTargetList;
        stealTargetList = objectThief.FindAllEnemyStealTargets();

        if (stealTargetList.Length != 0)
        {
            float distance = Mathf.Infinity;
            Vector3 position = objectThief.transform.position;
            foreach (EnemyStealTarget stealTarget in stealTargetList)
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
}
