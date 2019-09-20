using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectThiefDeathState : NewObjectThiefState
{
    private float despawnTime;

    public override void Enter(NewObjectThief objectThief)
    {
        UnrestManager.GetInstance().UpdateUnrest(+1);
        objectThief.DropItem();
        objectThief.ToggleMarionetteStrings(false);

        despawnTime = objectThief.GetDespawnAfterDeathTime();
    }

    public override void Exit(NewObjectThief objectThief)
    {

    }

    public override NewObjectThiefState FixedUpdate(NewObjectThief objectThief, float t)
    {
        return null;
    }

    public override NewObjectThiefState Update(NewObjectThief objectThief, float t)
    {
        despawnTime -= t;

        if(despawnTime <= 0)
        {
            //objectThief.gameObject.SetActive(false);
            //if (objectThief.objectSearcher.GetTargetType() == PickupType.Ornament)
            //{
            //    PoolManager.ReturnEnemy(objectThief.gameObject, EnemyType.OrnamentTheif);
            //}
            //else if (objectThief.objectSearcher.GetTargetType() == PickupType.Body)
            //{
            //    PoolManager.ReturnEnemy(objectThief.gameObject, EnemyType.Zombie);
            //}
            //else
            //{
            //    Debug.Log("Couldn't return enemy to pool!");
            //}
            objectThief.RestartAllPositions();
            objectThief.ReturnToObjectPool();

        }
        return null;
    }
}
