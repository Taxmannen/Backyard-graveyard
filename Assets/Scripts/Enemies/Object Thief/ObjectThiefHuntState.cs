using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script by Christopher Tåqvist */

public class ObjectThiefHuntState : ObjectThiefState
{
    private EnemyStealTarget currentStealTarget;
    private Interactable interactable;
    private float speed;
    private float pickupDistance;
    private bool pickedUpByMe = false;

    public override void Enter(ObjectThief objectThief)
    {
        currentStealTarget = objectThief.GetCurrentTargetToSteal();
        interactable = currentStealTarget.GetComponent<Interactable>();
        speed = objectThief.GetSpeed();
        pickupDistance = objectThief.GetPickupDistance();
    }

    public override void Exit(ObjectThief objectThief)
    {
        
    }

    public override ObjectThiefState FixedUpdate(ObjectThief objectThief, float t)
    {

        if(currentStealTarget != null)
        {
            Vector2 directionToTarget = new Vector2((currentStealTarget.transform.position.x - objectThief.transform.position.x), (currentStealTarget.transform.position.z - objectThief.transform.position.z));
            directionToTarget = directionToTarget.normalized;
            objectThief.Move(directionToTarget);

            if (Vector3.Distance(objectThief.transform.position, currentStealTarget.transform.position) <= pickupDistance)
            {
                //Pick up the target    
                currentStealTarget.pickedUpByEnemy = true;
                pickedUpByMe = true;
                currentStealTarget.AddSpringJoint(objectThief.AccessRigidBody());
                currentStealTarget = null;
            }
        }

        return null;
    }

    public override ObjectThiefState Update(ObjectThief objectThief, float t)
    {
        if(currentStealTarget == null)
        {
            return new ObjectThiefFleeState();
        }
        if(currentStealTarget != null)
        {
            if(currentStealTarget.pickedUpByEnemy && !pickedUpByMe)
            {
                return new ObjectThiefSearchState();
            }
        }
        if(interactable.ActiveHand != null)
        {
            return new ObjectThiefSearchState();
        }

        return null;
    }
}
