﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Code by Christopher Tåqvist */

public class NewObjectThief : MonoBehaviour
{
    //Objects to target & Hold
    [Header("Object Check (DO NOT TOUCH!)")]
    public GameObject currentTargetObject;
    public GameObject objectInHand;

    //States & Commands
    NewObjectThiefState currentState;
    NewObjectThiefState returnedState;

    [Header("Components (DO NOT TOUCH)")]
    public ObjectThiefJump enemyJump;
    public ObjectThiefPickupHand pickupHand;
    public ObjectThiefObjectSearcher objectSearcher;

    [Header("Rigidbodies to Control (DO NOT TOUCH)")]
    [SerializeField] private Rigidbody rigidBodyMarionette;
    [SerializeField] private Rigidbody rigidBodyArm;

    [Header("Speed Settings")]
    [SerializeField] private float movementSpeed = 50;
    [SerializeField] private float armSpeed = 50;

    [Header("Distance Settings")]
    [SerializeField] private float distanceBeforeTargetIsReached = 3;
    [SerializeField] private float distanceBeforeTargetIsPickedUp = 0.5f;



    private void Start()
    {
        //Change this? Should be the play-area in the beginning
        currentTargetObject = GameObject.FindGameObjectWithTag("DistanceCheckForObjectThief");

        currentState = new NewObjectThiefMoveToTargetState();
        currentState.Enter(this);

        
    }

    private void Update()
    {
        //Fullfix för om target-objekt försvinner
        if(currentTargetObject == null)
        {
            returnedState = new NewObjectThiefMoveToTargetState();
            StateSwap();
            currentTargetObject = GameObject.FindGameObjectWithTag("DistanceCheckForObjectThief");
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


    /*  MOVE BODYPART FUBNCTIONS  */
    public void Move(Vector3 moveAgainst)
    {
        MovePart(rigidBodyMarionette, moveAgainst, movementSpeed);
    }

    public void MoveArm(Vector3 moveAgainst)
    {
        MovePart(rigidBodyArm, moveAgainst, armSpeed);
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
    public void FindNewCurrentGameObjectWithTag(string tag)
    {
        currentTargetObject = GameObject.FindGameObjectWithTag(tag);
    }

    public void Despawn()
    {
        Debug.Log("DEspawned");
        if(objectInHand != null)
        {
            Destroy(objectInHand);
        }
        
        Destroy(gameObject);
    }
    
}
