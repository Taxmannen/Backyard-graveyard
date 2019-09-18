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
    [SerializeField] private float distanceBeforeTargetIsReached = 3;
    [SerializeField] private float distanceBeforeTargetIsPickedUp = 0.5f;

    [Header("Timers")]
    [SerializeField] private float timeBeforeTryingNewPickupTarget = 2f;
    [SerializeField] private float timeBeforeTryingNewTarget = 10f;



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
        //Fullfix för om target-objekt försvinner.
        if(currentTargetObject == null)
        {
            returnedState = new NewObjectThiefSearchState();
            StateSwap();
        }

        //Fulfix för om hand-objektet försvinner
        else if(currentTargetObject.tag == "OutOfBounds" && objectInHand == null)
        {
            returnedState = new NewObjectThiefSearchState();
            StateSwap();
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
    public void Move(Vector3 moveAgainst)
    {
        MovePart(rigidBodyMarionette, moveAgainst, movementSpeedRun);
    }

    public void MoveDuringSearch(Vector3 moveAgainst)
    {
        MovePart(rigidBodyMarionette, moveAgainst, movementSpeedSearch);
    }

    public void MoveArm(Vector3 moveAgainst)
    {
        MovePart(rigidBodyArm, moveAgainst, armSpeed);
    }

    public void Jump(float jumpForce)
    {
        rigidBodyMarionette.AddForce(new Vector3(0, jumpForce, 0));
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
        Vector3 directionToTarget = currentTargetObject.transform.position - bodyPartToMovePosition;
        return directionToTarget;
    }

    public float GetDistanceToTarget(Vector3 bodyPartToMovePosition)
    {
        return Vector3.Distance(bodyPartToMovePosition, currentTargetObject.transform.position);
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

    public float GetDistanceBeforeTargetIsPickedUp()
    {
        return distanceBeforeTargetIsPickedUp;
    }




    /*  FIND OBJECT  */
    public void FindNewCurrentTargetObjectWithTag(string tag)
    {
        currentTargetObject = GameObject.FindGameObjectWithTag(tag);
    }

    public void Despawn()
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
