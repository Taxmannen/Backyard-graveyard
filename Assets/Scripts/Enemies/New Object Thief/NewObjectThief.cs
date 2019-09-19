using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThief : MonoBehaviour
{
    [Header("Testing stuff")]
    [SerializeField] public bool stateMachineOn = true;
    public bool debugStates = false;

    //Objects to target & Hold
    [Header("Object Check (DO NOT TOUCH!)")]
    public GameObject currentTargetObject;
    public GameObject objectInHand;
    public GameObject randomTargetObject;

    //States & Commands
    NewObjectThiefState currentState;
    NewObjectThiefState returnedState;

    [Header("Components (DO NOT TOUCH)")]
    public ObjectThiefJump enemyJump;
    public ObjectThiefPickupHand pickupHand;
    public ObjectThiefObjectSearcher objectSearcher;
    public NewObjectThiefRandomTargetArea randomTargetArea;

    [Header("Rigidbodies to Control (DO NOT TOUCH)")]
    [SerializeField] private Rigidbody rigidBodyMarionette;
    [SerializeField] private Rigidbody rigidBodyArm;

    [Header("Speed Settings")]
    [SerializeField] private float movementSpeedRun = 50;
    [SerializeField] private float movementSpeedSearch = 25;
    [SerializeField] private float armSpeed = 50;

    [Header("Distance Settings")]
    [SerializeField] private float distanceBeforeTargetIsReached = 3f;
    [SerializeField] private float distanceBeforeTargetIsPickedUp = 0.5f;
    [SerializeField] private float distanceBeforeEnemyDespawn = 20f;

    [Header("Timers")]
    [SerializeField] private float timeBeforeTryingNewPickupTarget = 2f;
    [SerializeField] private float timeBeforeTryingNewTarget = 10f;

    //Används för fulfixen för om objekt försvinner
    private bool isDead = false;



    private void Start()
    {
        randomTargetArea.SetTargetPositionToPlayArea();
        currentTargetObject = randomTargetObject;

        if (stateMachineOn)
        {
            currentState = new NewObjectThiefMoveToTargetState();
        }
        
        else{
            currentState = new NewObjectThiefEmptyStateForTesting();
        }

        currentState.Enter(this);
    }

    private void Update()
    {
        
        //Fulfixar
        if(!isDead)
        {
            //Fullfix för om target-objekt försvinner.
            if (currentTargetObject == null)
            {
                returnedState = new NewObjectThiefSearchState();
                StateSwap();
            }

            //Fulfix för om hand-objektet försvinner
            else if (currentTargetObject.tag == "OutOfBounds" && objectInHand == null)
            {
                returnedState = new NewObjectThiefSearchState();
                StateSwap();
            }
        }

        returnedState = currentState.Update(this, Time.deltaTime);
        if (returnedState != null)
        {
            StateSwap();
        }
    }

    private void FixedUpdate()
    {
        returnedState = currentState.FixedUpdate(this, Time.deltaTime);
    }

    private void StateSwap()
    {
        currentState.Exit(this);
        currentState = returnedState;
        currentState.Enter(this);
    }


    /*  MOVE BODYPART FUNCTIONS  */
    public void MoveToTarget()
    {
        Vector3 directionToTarget = GetDirectionToTarget(rigidBodyMarionette.transform.position);
        enemyJump.TryJump();
        MovePart(rigidBodyMarionette, directionToTarget, movementSpeedRun);
    }

    public void MoveAwayFromTarget()
    {
        Vector3 directionAwayFromTarget = -GetDirectionToTarget(rigidBodyMarionette.transform.position);
        enemyJump.TryJump();
        MovePart(rigidBodyMarionette, directionAwayFromTarget, movementSpeedRun);
    }

    public void MoveToTargetDuringSearch()
    {
        Vector3 directionToTarget = GetDirectionToTarget(rigidBodyMarionette.transform.position);
        enemyJump.TryJump();
        MovePart(rigidBodyMarionette, directionToTarget, movementSpeedSearch);
    }

    public void MoveArm()
    {
        Vector3 directionToTarget = GetDirectionToTarget(rigidBodyArm.transform.position);
        MovePart(rigidBodyArm, directionToTarget, armSpeed);
    }

    private void MovePart(Rigidbody rigidBodyToMove, Vector3 moveAgainst, float forceToMoveWith)
    {
        moveAgainst = moveAgainst.normalized;
        Vector3 movement = new Vector3(moveAgainst.x, 0, moveAgainst.z);
        rigidBodyToMove.AddForce(movement * forceToMoveWith);
    }



    /*  DIRECTIONS & POSITIONS  */
    public Vector3 GetDirectionToTarget(Vector3 bodyPartToMovePosition)
    {
        if(currentTargetObject != null)
        {
            Vector3 directionToTarget = currentTargetObject.transform.position - bodyPartToMovePosition;
            return directionToTarget;
        }
        else
        {
            Debug.Log("no current target. Couldn't get direction");
            return new Vector3(0, 0, 0);
        }
        
    }

    public float GetDistanceToTarget(Vector3 fromPosition)
    {
        return Vector3.Distance(fromPosition, currentTargetObject.transform.position);
    }

    public Vector3 GetTargetPosition()
    {
        return currentTargetObject.transform.position;
    }

    public Vector3 GetMarionetteStringPosition()
    {
        return rigidBodyMarionette.transform.position;
    }

    public Vector3 GetArmPosition()
    {
        return rigidBodyArm.transform.position;
    }

    public float GetDistanceBeforeTargetIsReached()
    {
        return distanceBeforeTargetIsReached;
    }

    public float GetDistanceBeforeEnemyDespawn()
    {
        return distanceBeforeEnemyDespawn;
    }

    public float GetDistanceBeforeTargetIsPickedUp()
    {
        return distanceBeforeTargetIsPickedUp;
    }




    /*  FIND OBJECT  */
    public void FindNewCurrentTargetObjectWithTag(string tag)
    {
        currentTargetObject = GameObject.FindGameObjectWithTag(tag);
    }

    public void DespawnDuringFlee()
    {
        if(objectInHand != null)
        {
            Destroy(objectInHand);
        }
        
        Destroy(gameObject);
    }

    public void DropItem()
    {
        pickupHand.DestroyJoint();
        objectInHand = null;
    }

    public void GoToDeathState()
    {
        isDead = true;
        returnedState = new NewObjectThiefDeathState();
        StateSwap();
    }


    /* TIMERS */

    public float GetTimeBeforeTryingNewPickupTarget()
    {
        return timeBeforeTryingNewPickupTarget;
    }

    public float GetTimeBeforeTryingNewTarget()
    {
        return timeBeforeTryingNewTarget;
    }

}
